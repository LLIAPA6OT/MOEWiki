using EnumsNET;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MOEWiki.DBMySQL.Enums;
using MOEWiki.DBMySQL.Gateways;
using MOEWiki.DBMySQL.Helpers;
using MOEWiki.DBMySQL.Models;
using MOEWiki.Parser3.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using TextBox = System.Windows.Forms.TextBox;

namespace MOEWiki.Parser3
{
    public partial class ParserForm : Form
    {

        #region Common
        public bool fromAutoParse = false;
        public AutoParse? autoParse = null;
        public Font font = new Font("Courier New", 9, FontStyle.Regular, GraphicsUnit.Point);
        public int lWidth = 290;
        public int charInRow = 40;
        public int rowHeight = 15;
        public string result1 = "";
        public string result2 = "";
        public Bitmap? image;
        public ModeEnum mode;
        public Dictionary<int, ItemProperty> properties = new Dictionary<int, ItemProperty>();
        public Dictionary<int, ItemRecipe> recipes = new Dictionary<int, ItemRecipe>();
        public Subcategory? subcategory = null;
        public Subcategory? material = null;
        public Item? item = null;
        public QualityEnum quality = QualityEnum.Low;
        private Char separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator[0];
        private Perk? parsedPerk = null;
        public Perk? perk = null;
        private Research? parsedResearch = null;
        public Research? research = null;
        public ParserForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Other
        public string PrepareForDouble(string source)
        {
            return source.Replace(',', separator).Replace('.', separator);
        }
        private bool CheckIsWord(string word)
        {
            var regex = new Regex("^[a-zA-z]+[-]?[']?[a-zA-z]+$");
            return regex.IsMatch(word);
        }
        private string SaveOnlyLetters(string sourse)
        {
            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            alphabet += alphabet.ToLower() + ' ';
            return string.Join("",sourse.Where(w => alphabet.Contains(w)));
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int keys);
        private ItemProperty ParseWithColon(string text)
        {
            var result = new ItemProperty();
            result.Name = text[..text.IndexOf(':')].Trim();
            result.Value = RemoveSlash(text[(text.IndexOf(":") + 1)..].Trim());
            return result;
        }
        private string RemoveSlash(string s)
        {
            if (s.IndexOf("/") == -1)
                return s;
            else
                return s[(s.IndexOf("/") + 1)..];
        }
        private void ParserForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void CheckItem(string name)
        {
            bCompare.Visible = true;
            bAddNew.Visible = false;
            if (item == null || item.Name != name)
            {
                var p = GatewaysFacade.ItemGateway.GetByNameSmart(name);
                if (p != null)
                {
                    item = p;
                    subcategory = GatewaysFacade.SubcategoryGateway.GetById(p.SubcategoryId);
                    lSubcat.Text = $"Category: {subcategory.Name}";
                    lItemName.Text = item.Name;
                    lItemName.ForeColor = Color.Green;
                }
                else
                {
                    lItemName.Text = name;
                    lItemName.ForeColor = Color.Red;
                    bCompare.Visible = false;
                    bAddNew.Visible = true;
                    bCommitAll.Visible = false;
                }
            }
        }
        private int ResearchOrange(string propName, int val1, int val2, int lastY)
        {
            return ResearchOrange(propName, val1.ToString(), val2.ToString(), lastY);
        }
        private int ResearchOrange(string propName, string val1, string val2, int lastY)
        {
            var l = GenerateLabel($"{propName}: {val1}->{val2}", Color.Orange, lastY, 5);
            panel2.Controls.Add(l);
            var b = GenerateButton("UpdateResearch", 10 + lWidth, lastY);
            b.Tag = propName;
            b.Click += new EventHandler(UpdateResearch);
            panel2.Controls.Add(b);
            return lastY + l.Height + 10;
        }
        private int ResearchGreen(string propName, int val1, int lastY)
        {
            return ResearchGreen(propName, val1.ToString(), lastY);
        }
        private int ResearchGreen(string propName, string val1, int lastY)
        {
            var l = GenerateLabel($"{propName}: {val1}", Color.Green, lastY, 5);
            panel2.Controls.Add(l);
            return lastY + l.Height + 5;
        }
        private int PerkOrange(string propName, int val1, int val2, int lastY)
        {
            return PerkOrange(propName, val1.ToString(), val2.ToString(), lastY);
        }
        private int PerkOrange(string propName, string val1, string val2, int lastY)
        {
            var l = GenerateLabel($"{propName}: {val1}->{val2}", Color.Orange, lastY, 5);
            panel2.Controls.Add(l);
            var b = GenerateButton("UpdatePerk", 10 + lWidth, lastY);
            b.Tag = propName;
            b.Click += new EventHandler(UpdatePerk);
            panel2.Controls.Add(b);
            return lastY + l.Height + 10;
        }
        private int PerkGreen(string propName, int val1, int lastY)
        {
            return PerkGreen(propName, val1.ToString(), lastY);
        }
        private int PerkGreen(string propName, string val1, int lastY)
        {
            var l = GenerateLabel($"{propName}: {val1}", Color.Green, lastY, 5);
            panel2.Controls.Add(l);
            return lastY + l.Height + 5;
        }
        private Button GenerateButton(string text, int x, int y)
        {
            var result = new Button() { Text = text, Width = 100, Height = 28, Location = new Point(x, y) };
            result.Name += text;
            return result;
        }
        private Label GenerateLabel(string text, int y)
        {
            return GenerateLabel(text, Color.Black, y);
        }
        private Label GenerateLabel(string text, Color color, int y)
        {
            return GenerateLabel(text, color, y, 5);
        }
        private Label GenerateLabel(string text, Color color, int y, int x)
        {
            return new Label() { AutoSize = false, Font = font, Width = lWidth, Text = text, Location = new Point(x, y), Height = (text.Length / charInRow + 1) * rowHeight, ForeColor = color };
        }
        private Label GenerateLabel(string text, Color color, int y, int x, string name)
        {
            var result = GenerateLabel(text, color, y, x);
            result.Name = name;
            return result;
        }
        private void TryToUpdateImage(bool canClose = false)
        {
            if (image != null)
            {
                if (item != null && string.IsNullOrWhiteSpace(item.ImageName))
                {
                    var imagename = $"{Guid.NewGuid()}.jpg";
                    var path = $"E:\\MOE\\icon";
                    if (Directory.Exists(path))
                    {
                        image.Save(path + '\\' + imagename, ImageFormat.Jpeg);
                    }
                    item.ImageName = imagename;
                    GatewaysFacade.ItemGateway.Update(item);
                    if (canClose) { this.Close(); }
                }
                else
                {
                    var updateImageForm = new UpdateImage();
                    updateImageForm.pbNew.Image = image;
                    if (item != null && !string.IsNullOrWhiteSpace(item.ImageName))
                    {
                        var path = $"E:\\MOE\\icon\\{item.ImageName}";
                        if (File.Exists(path))
                        {
                            updateImageForm.pbOld.Image = Image.FromFile(path);
                        }
                    }
                    DialogResult dialogResult = updateImageForm.ShowDialog();
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            var imagename = $"{Guid.NewGuid()}.jpg";
                            var path = $"E:\\MOE\\icon";
                            if (Directory.Exists(path))
                            {
                                image.Save(path + '\\' + imagename, ImageFormat.Jpeg);
                            }
                            item.ImageName = imagename;
                            GatewaysFacade.ItemGateway.Update(item);
                            if (canClose) { this.Close(); }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }
        private void UpdateItemLatUpdateDate()
        {
            if (item == null) return;
            if (mode == ModeEnum.Item || mode == ModeEnum.ItemPlusRecipe)
            {
                item.PropertiesLastUpdateDate = DateTime.Now;
            }
            if (mode == ModeEnum.Recipe || mode == ModeEnum.ItemPlusRecipe)
            {
                item.RecipesLastUpdateDate = DateTime.Now;
            }
            try
            {
                GatewaysFacade.ItemGateway.Update(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private string GetLastAndAnothrWords(string[] sourse)
        {
            var last = sourse[sourse.Length - 1];
            return (string.Join(' ', sourse.Where(w => CheckIsWord(w) && w != last)) + ' ' + last).Trim();
        }
        #endregion

        #region Buttons
        private void bAddAll_Click(object sender, EventArgs e)
        {
            foreach (var b in panel1.Controls.OfType<Label>().Where(w => w.ForeColor == Color.Red))
            {
                if (b.Name.Contains("lIPKey"))
                    AddNewProperty(b, e);
                else if (b.Name.Contains("lIRKey"))
                    AddNewItem(b, e);
                else if (b.Name.Contains("lResKey"))
                    AddNewItemFromResearch(b, e);
            }
            bAddAll.Visible = false;
        }
        private void tmrMain_Tick(object sender, EventArgs e)
        {
            if (GetAsyncKeyState(99) != 0) //num 3
            {
                tmrMain.Enabled = false;
                this.Close();
            }
            else
            if (GetAsyncKeyState(39) != 0) //->
            {
                tmrMain.Enabled = false;
                if (bAddAll.Visible)
                {
                    bAddAll_Click(sender, e);
                }
                else if (bCommitAll.Visible)
                {
                    bCommitAll_Click(sender, e);
                }
                else if (bAddNew.Visible)
                {
                    bAddNew.Visible = false;
                    bAddNew_Click(sender, e);
                }
                else if (bCompare.Visible)
                {
                    bCompare_Click(sender, e);
                }
                else if (bParse.Visible)
                {
                    bParse_Click(sender, e);
                }
                tmrMain.Enabled = true;
            }
        }
        private void bImg_Click(object sender, EventArgs e)
        {
            if (pb.Visible)
            {
                pb.Visible = false;
            }
            else
            {
                if (File.Exists(autoParse.ImageName))
                {
                    pb.Visible = true;
                    pb.Image = Image.FromFile(autoParse.ImageName);
                    pb.Width = pb.Image.Width;
                    pb.Height = pb.Image.Height;
                    pb.Location = new Point(txtResult1.Width + 5, 30);
                }
            }
        }
        private void ParserForm_Load(object sender, EventArgs e)
        {
            tmrMain.Enabled = true;
            if (fromAutoParse)
            {
                result1 = autoParse.RecognitionResult;
                result2 = autoParse.AdditionalString1;
                bImg.Visible = true;
            }
            if (result1.StartsWith("2"))
            {
                result1 = result1.Substring(1);
            }
            if (result2.StartsWith("2"))
            {
                result2 = result2.Substring(1);
            }
            txtResult1.Text = result1.Replace("[Description", "Description").Replace("{Description", "Description").Replace("(Description", "Description");
            txtResult2.Text = result2;
            this.Text = mode.AsString(EnumFormat.Description);
            if (fromAutoParse)
            {
                bParse_Click(sender, e);
            }
        }
        private void bParse_Click(object sender, EventArgs e)
        {
            tmrMain.Enabled = false;
            result1 = txtResult1.Text;
            result2 = txtResult2.Text;
            panel2.Controls.Clear();
            bAddAll.Visible = false;
            switch (mode)
            {
                case ModeEnum.Item:
                    ParseItemProperties(result1);
                    break;
                case ModeEnum.ItemPlusRecipe:
                    ParseItemProperties(result1);
                    ParseItemRecipes(result2);
                    break;
                case ModeEnum.Recipe:
                    ParseItemRecipes(result1);
                    break;
                case ModeEnum.Perk:
                    ParsePerk(result1);
                    break;
                case ModeEnum.Research:
                    ParseResearch(result1);
                    break;
            }
            //if (!bAddAll.Visible)
            //{
            //    if (bAddNew.Visible)
            //    {
            //        bAddNew_Click(sender, e);
            //    }
            //    else if (bCompare.Visible)
            //    {
            //        bCompare_Click(sender, e);
            //    }
            //}
            tmrMain.Enabled = true;
        }
        private void bHandlySearch_Click(object sender, EventArgs e)
        {
            var form = new HandlySearch();
            form.ShowDialog();
            if (form.selectedId > 0)
            {
                item = GatewaysFacade.ItemGateway.GetById(form.selectedId);
                lItemName.Text = item.Name;
                lItemName.ForeColor = Color.Green;
                bCompare.Visible = true;
                bAddNew.Visible = false;
            }
            CheckBUpdateSubcatVisidility();
        }
        private void bCompare_Click(object sender, EventArgs e)
        {
            if (mode == ModeEnum.Item || mode == ModeEnum.ItemPlusRecipe || mode == ModeEnum.Recipe)
            {
                if (item == null && subcategory == null && mode != ModeEnum.Recipe)
                {
                    MessageBox.Show("Get subcategory first!");
                    return;
                }
                if (panel1.Controls.OfType<TextBox>().Any(a => a.ForeColor == Color.Red))
                {
                    MessageBox.Show("Add all properties and recipes first!");
                    return;
                }
            } else if (mode == ModeEnum.Research)
            {

                if (panel1.Controls.OfType<TextBox>().Any(a => a.ForeColor == Color.Red))
                {
                    MessageBox.Show("Add all recipe item first!");
                    return;
                }
            }

            bCommitAll.Visible = true;
            panel2.Controls.Clear();
            var lastY = 5;
            if (mode == ModeEnum.Item || mode == ModeEnum.ItemPlusRecipe)
            {
                var itemProperties = GatewaysFacade.ItemPropertyGateway.GetAllByItemId(item.Id).OrderBy(o => o.SortId);
                var parsedP = properties.Values;
                foreach (var property in parsedP.Where(w => w.Quality != QualityEnum.Low))
                {
                    var inBoth = itemProperties.Where(w => w.PropertyId == property.PropertyId);
                    foreach (var ib in inBoth)
                    {
                        if (ib.Quality != property.Quality || ib.Value == property.Value)
                        {
                            var l = GenerateLabel($"{ib.Name} {ib.Value} [{ib.Quality}]", Color.Green, lastY);
                            panel2.Controls.Add(l);
                            lastY += l.Height + 10;
                        }
                        else
                        {
                            var l = GenerateLabel($"{ib.Name} {ib.Value}->{property.Value} [{property.Quality}]", Color.Orange, lastY);
                            panel2.Controls.Add(l);
                            var b = GenerateButton("Update", 10 + lWidth, lastY);
                            ib.Value = property.Value;
                            b.Tag = ib;
                            b.Click += new EventHandler(UpdateItemProperty);
                            panel2.Controls.Add(b);
                            lastY += l.Height + 15;
                        }
                    }
                    if (!itemProperties.Any(w => w.PropertyId == property.PropertyId && w.Quality == property.Quality))
                    {
                        var l = GenerateLabel($"{property.Name} {property.Value} [{property.Quality}]", Color.Red, lastY);
                        panel2.Controls.Add(l);
                        var b = GenerateButton("Add", 10 + lWidth, lastY);
                        property.ItemId = item.Id;
                        b.Tag = property;
                        b.Click += new EventHandler(AddItemProperty);
                        panel2.Controls.Add(b);
                        lastY += l.Height + 15;
                    }
                }
                foreach (var property in parsedP.Where(w => w.Quality == QualityEnum.Low))
                {
                    var inBoth = itemProperties.Where(w => w.PropertyId == property.PropertyId);
                    foreach (var ib in inBoth)
                    {
                        if (ib.Value == property.Value)
                        {
                            var l = GenerateLabel($"{ib.Name} {ib.Value} [{ib.Quality}]", Color.Green, lastY);
                            panel2.Controls.Add(l);
                            lastY += l.Height + 10;
                        }
                        else
                        {
                            var l = GenerateLabel($"{ib.Name} {ib.Value}->{property.Value} [{property.Quality}]", Color.Orange, lastY);
                            panel2.Controls.Add(l);
                            var b = GenerateButton("Update", 10 + lWidth, lastY);
                            ib.Value = property.Value;
                            b.Tag = ib;
                            b.Click += new EventHandler(UpdateItemProperty);
                            panel2.Controls.Add(b);
                            lastY += l.Height + 15;
                        }
                    }
                    if (!inBoth.Any())
                    {
                        var l = GenerateLabel($"{property.Name} {property.Value} [{property.Quality}]", Color.Red, lastY);
                        panel2.Controls.Add(l);
                        var b = GenerateButton("Add", 10 + lWidth, lastY);
                        property.ItemId = item.Id;
                        b.Tag = property;
                        b.Click += new EventHandler(AddItemProperty);
                        panel2.Controls.Add(b);
                        lastY += l.Height + 15;
                    }
                }
                var ipids = parsedP.Select(s => s.PropertyId);
                foreach (var property in itemProperties.Where(w => !ipids.Contains(w.PropertyId)))
                {
                    var l = GenerateLabel($"{property.Name} {property.Value} [{property.Quality}]", Color.Black, lastY);
                    panel2.Controls.Add(l);
                    var b = GenerateButton("Delete", 10 + lWidth, lastY);
                    b.Tag = property;
                    b.Click += new EventHandler(DelItemProperty);
                    panel2.Controls.Add(b);
                    lastY += l.Height + 15;
                }
                TryToUpdateImage();
            }
            if (mode == ModeEnum.Recipe || mode == ModeEnum.ItemPlusRecipe)
            {
                panel2.Controls.Add(GenerateLabel("Recipe", lastY));
                lastY += rowHeight + 5;
                var itemRecipes = GatewaysFacade.ItemRecipeGateway.GetAllByItemId(item.Id);
                var parsedR = recipes.Values;
                foreach (var recipe in itemRecipes)
                {
                    var r = parsedR.FirstOrDefault(a => a.RecipeItemId == recipe.RecipeItemId);
                    if (r == null)
                    {
                        var l = GenerateLabel($"{recipe.RecipeItemName} {recipe.Count} {recipe.IsStepByStep}", Color.Black, lastY, 5);
                        panel2.Controls.Add(l);
                        var b = GenerateButton("DeleteR", 10 + lWidth, lastY);
                        b.Tag = recipe;
                        b.Click += new EventHandler(DelItemRecipe);
                        panel2.Controls.Add(b);
                        lastY += l.Height + 10;
                    }
                    else
                    {
                        if (r.Count == recipe.Count && r.IsStepByStep == recipe.IsStepByStep)
                        {
                            var l = GenerateLabel($"{recipe.RecipeItemName} {recipe.Count} {recipe.IsStepByStep}", Color.Green, lastY, 5);
                            panel2.Controls.Add(l);
                            lastY += l.Height + 10;
                        }
                        else
                        {
                            var l = GenerateLabel($"{recipe.RecipeItemName} {recipe.Count}->{r.Count} {recipe.IsStepByStep}->{r.IsStepByStep}", Color.Orange, lastY, 5);
                            panel2.Controls.Add(l);
                            var b = GenerateButton("UpdateR", 10 + lWidth, lastY);
                            recipe.Count = r.Count;
                            recipe.IsStepByStep = r.IsStepByStep;
                            b.Tag = recipe;
                            b.Click += new EventHandler(UpdateItemRecipe);
                            panel2.Controls.Add(b);
                            lastY += l.Height + 10;
                        }
                    }
                }
                foreach (var i in parsedR.Where(w => !itemRecipes.Any(a => a.RecipeItemId == w.RecipeItemId)))
                {
                    var l = GenerateLabel($"{i.RecipeItemName} {i.Count} {i.IsStepByStep}", Color.Red, lastY, 5);
                    panel2.Controls.Add(l);
                    var b = GenerateButton("AddR", 10 + lWidth, lastY);
                    i.ItemId = item.Id;
                    b.Tag = i;
                    b.Click += new EventHandler(AddItemRecipe);
                    panel2.Controls.Add(b);
                    lastY += l.Height + 10;
                }
            }
            if (mode == ModeEnum.Perk)
            {
                if (perk == null || parsedPerk == null) return;
                if (perk.Description != parsedPerk.Description)
                    lastY = PerkOrange("Description", perk.Description, parsedPerk.Description, lastY);
                else
                    lastY = PerkGreen("Description", perk.Description, lastY);
                if (perk.RequiredItemId != parsedPerk.RequiredItemId)
                    lastY = PerkOrange("RequiredItemId", perk.RequiredItemId, parsedPerk.RequiredItemId, lastY);
                else
                    lastY = PerkGreen("RequiredItemId", perk.RequiredItemId, lastY);
                if (perk.Level != parsedPerk.Level)
                    lastY = PerkOrange("Level", perk.Level, parsedPerk.Level, lastY);
                else
                    lastY = PerkGreen("Level", perk.Level, lastY);
                if (perk.SkillLevel != parsedPerk.SkillLevel)
                    lastY = PerkOrange("SkillLevel", perk.SkillLevel, parsedPerk.SkillLevel, lastY);
                else
                    lastY = PerkGreen("SkillLevel", perk.SkillLevel, lastY);
                if (perk.ComprehensionPoint != parsedPerk.ComprehensionPoint)
                    lastY = PerkOrange("ComprehensionPoint", perk.ComprehensionPoint, parsedPerk.ComprehensionPoint, lastY);
                else
                    lastY = PerkGreen("ComprehensionPoint", perk.ComprehensionPoint, lastY);
                if (perk.CopperCoins != parsedPerk.CopperCoins)
                    lastY = PerkOrange("CopperCoins", perk.CopperCoins, parsedPerk.CopperCoins, lastY);
                else
                    lastY = PerkGreen("CopperCoins", perk.CopperCoins, lastY);
                if (perk.PreviousName != parsedPerk.PreviousName)
                    lastY = PerkOrange("PreviousName", perk.PreviousName, parsedPerk.PreviousName, lastY);
                else
                    lastY = PerkGreen("PreviousName", perk.PreviousName, lastY);
            }
            if (mode == ModeEnum.Research)
            {
                if (research == null || parsedResearch == null) return;
                if (research.Description != parsedResearch.Description)
                    lastY = ResearchOrange("Description", research.Description, parsedResearch.Description, lastY);
                else
                    lastY = ResearchGreen("Description", research.Description, lastY);
                if (research.RequiredItemId != parsedResearch.RequiredItemId)
                    lastY = ResearchOrange("RequiredItemId", research.RequiredItemId ?? 0, parsedResearch.RequiredItemId ?? 0, lastY);
                else
                    lastY = ResearchGreen("RequiredItemId", research.RequiredItemId ?? 0, lastY);
                if (research.Level != parsedResearch.Level)
                    lastY = ResearchOrange("Level", research.Level, parsedResearch.Level, lastY);
                else
                    lastY = ResearchGreen("Level", research.Level, lastY);
                if (research.RecipePoint != parsedResearch.RecipePoint)
                    lastY = ResearchOrange("RecipePoint", research.RecipePoint, parsedResearch.RecipePoint, lastY);
                else
                    lastY = ResearchGreen("RecipePoint", research.RecipePoint, lastY);                
                if (research.PreviousName != parsedResearch.PreviousName)
                    lastY = ResearchOrange("PreviousName", research.PreviousName, parsedResearch.PreviousName, lastY);
                else
                    lastY = ResearchGreen("PreviousName", research.PreviousName, lastY);
                if (research.GuildLevel != parsedResearch.GuildLevel)
                    lastY = ResearchOrange("GuildLevel", research.GuildLevel, parsedResearch.GuildLevel, lastY);
                else
                    lastY = ResearchGreen("GuildLevel", research.GuildLevel, lastY);
                if (research.GuildActivity != parsedResearch.GuildActivity)
                    lastY = ResearchOrange("GuildActivity", research.GuildActivity, parsedResearch.GuildActivity, lastY);
                else
                    lastY = ResearchGreen("GuildActivity", research.GuildActivity, lastY);
                if (research.CopperCoins != parsedResearch.CopperCoins)
                    lastY = ResearchOrange("CopperCoins", research.CopperCoins, parsedResearch.CopperCoins, lastY);
                else
                    lastY = ResearchGreen("CopperCoins", research.CopperCoins, lastY);
                var ll = GenerateLabel("Item List:", Color.Black, lastY);
                lastY += ll.Height + 5;
                panel2.Controls.Add(ll);
                var items = GatewaysFacade.ItemGateway.GetAllByResearchId(research.Id);
                var ids = parsedResearch.ItemsList.Select(s => s.Id).Distinct();
                foreach ( var i in items )
                {
                    var r = parsedResearch.ItemsList.FirstOrDefault(a => a.Id == i.Id);
                    if (r == null)
                    {
                        var l = GenerateLabel(i.Name, Color.Black, lastY, 5);
                        panel2.Controls.Add(l);
                        var b = GenerateButton("DeleteFR", 10 + lWidth, lastY);
                        i.ResearchId = null;
                        b.Tag = i;
                        b.Click += new EventHandler(UpdateItemResearch);
                        panel2.Controls.Add(b);
                        lastY += l.Height + 10;
                    }
                    else
                    {
                        var l = GenerateLabel(i.Name, Color.Green, lastY, 5);
                        panel2.Controls.Add(l);
                        lastY += l.Height + 10;
                    }
                }
                foreach (var i in parsedResearch.ItemsList.Where(w => !items.Any(a => a.Id == w.Id)))
                {
                    var l = GenerateLabel(i.Name, Color.Red, lastY, 5);
                    panel2.Controls.Add(l);
                    var b = GenerateButton("AddFR", 10 + lWidth, lastY);
                    i.ResearchId = research.Id;
                    b.Tag = i;
                    b.Click += new EventHandler(UpdateItemResearch);
                    panel2.Controls.Add(b);
                    lastY += l.Height + 10;
                }
            }
        }
        private void bAddNew_Click(object sender, EventArgs e)
        {
            if (mode == ModeEnum.Item || mode == ModeEnum.Recipe || mode == ModeEnum.ItemPlusRecipe)
            {
                if (subcategory == null)
                {
                    MessageBox.Show("Get subcategory first!");
                    return;
                }
                if (panel1.Controls.OfType<TextBox>().Any(a => a.ForeColor == Color.Red && a.Name.StartsWith("tbProp")))
                {
                    MessageBox.Show("Add all properties first!");
                    return;
                }
                if (panel1.Controls.OfType<TextBox>().Any(a => a.ForeColor == Color.Red && a.Name.StartsWith("tbItemName")))
                {
                    MessageBox.Show("Add all recipe item first!");
                    return;
                }
                try
                {
                    var newItem = new Item() { Name = lItemName.Text, SubcategoryId = subcategory.Id };
                    item = GatewaysFacade.ItemGateway.Add(newItem);
                    if (newItem.Id > 0)
                    {
                        foreach (var prop in properties.Values)
                        {
                            prop.ItemId = item.Id;
                            GatewaysFacade.ItemPropertyGateway.Add(prop);
                        }
                        foreach (var recipe in recipes.Values)
                        {
                            recipe.ItemId = item.Id;
                            GatewaysFacade.ItemRecipeGateway.Add(recipe);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                TryToUpdateImage(true);
                //if (image != null)
                //{
                //    var imagename = $"{Guid.NewGuid()}.jpg";
                //    var path = $"E:\\MOE\\icon";
                //    if (Directory.Exists(path))
                //    {
                //        image.Save(path + '\\' + imagename, ImageFormat.Jpeg);
                //    }
                //    item.ImageName = imagename;
                //    GatewaysFacade.ItemGateway.Update(item);
                //}
            }
            if (mode == ModeEnum.Perk)
            {
                try
                {
                    GatewaysFacade.PerkGateway.Add(parsedPerk);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (mode == ModeEnum.Research)
            {
                if (panel1.Controls.OfType<TextBox>().Any(a => a.ForeColor == Color.Red))
                {
                    MessageBox.Show("Add all recipe item first!");
                    return;
                }
                try
                {
                    parsedResearch = GatewaysFacade.ResearchGateway.Add(parsedResearch);
                    foreach (var i in parsedResearch.ItemsList)
                    {
                        GatewaysFacade.ItemGateway.UpdateResearch(i.Id, parsedResearch.Id);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void bCommitAll_Click(object sender, EventArgs e)
        {
            foreach (var b in panel2.Controls.OfType<Button>())
            {
                switch (b.Text)
                {
                    case "AddFR":
                    case "DeleteFR":
                        UpdateItemResearch(b, e);
                        break;
                    case "UpdateResearch":
                        UpdateResearch(b, e);
                        break;
                    case "UpdatePerk":
                        UpdatePerk(b, e);
                        break;
                    case "Update":
                        UpdateItemProperty(b, e);
                        break;
                    case "Add":
                        AddItemProperty(b, e);
                        break;
                    case "UpdateR":
                        UpdateItemRecipe(b, e);
                        break;
                    case "AddR":
                        AddItemRecipe(b, e);
                        break;
                }
            }
            this.Close();
            //if (!panel2.Controls.OfType<Button>().Any())
            //{
            //    if (!fromAutoParse) UpdateItemLatUpdateDate();
            //    this.DialogResult = DialogResult.OK;
            //}
        }
        private void bUpdateSubcat_Click(object sender, EventArgs e)
        {
            try
            {
                item.SubcategoryId = subcategory.Id;
                GatewaysFacade.ItemGateway.Update(item);
                MessageBox.Show("Done");
                bUpdateSubcat.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Item
        private void EditSubcat(object sender, EventArgs e)
        {
            var editForm = new EditSubcategory();
            if (lSubcat.Tag != null)
            {
                var s = lSubcat.Tag.ToString();
                Clipboard.SetText(s);
                editForm.Text += " " + s;
            }
            editForm.ShowDialog();
            subcategory = GatewaysFacade.SubcategoryGateway.GetById(editForm.currentId);
            lSubcat.Text = $"Category: {subcategory.Name}";
            CheckBUpdateSubcatVisidility();
        }
        private void CheckBUpdateSubcatVisidility()
        {
            bUpdateSubcat.Visible = item != null && subcategory != null && item.SubcategoryId != subcategory.Id;
        }
        private void ParseItemProperties(string sourse)
        {
            var s = sourse.Split('\n').Select(s => s.Replace("\r", "").Replace('\t', ' ').Trim()).Where(w => !string.IsNullOrWhiteSpace(w)).ToList().ToArray();
            if (s.Length > 0)
            {
                lSubcat.Text = "Category not found";
                properties.Clear();
                quality = QualityEnum.Low;
                var allProperties = GatewaysFacade.PropertyGateway.GetAll().ToList();
                var descrNum = 0;
                var sbt = 0;
                var findSBT = false;
                CheckItem(s[0]);
                var counter = 0;
                for (int i = 1; i < s.Length; i++)
                {
                    if (s[i].StartsWith("Description"))
                    {
                        descrNum = i;
                        break;
                    }
                    else
                    if (s[i].StartsWith("Switchable building types"))
                    {
                        sbt = i;
                        findSBT = true;
                    }
                    else if (s[i - 1].StartsWith("Level:"))
                    {
                        if (item == null)
                        {
                            lSubcat.Tag = s[i].Trim();
                            subcategory = GatewaysFacade.SubcategoryGateway.GetByNameOrSynonym(s[i].Trim());
                            if (subcategory != null)
                            {
                                lSubcat.Text = $"Category: {subcategory.Name}";
                            }
                        }
                    }
                    else if (s[i].StartsWith("Quality"))
                    {
                        switch (s[i].Split(' ').Where(w => !string.IsNullOrWhiteSpace(w)).Last().ToLower().Replace(".", "").Replace(" ", ""))
                        {
                            case "lv10":
                                quality = QualityEnum.Lvl10;
                                break;
                            case "lv30":
                                quality = QualityEnum.Lvl30;
                                break;
                            case "lv50":
                                quality = QualityEnum.Lvl50;
                                break;
                            case "lv70":
                                quality = QualityEnum.Lvl70;
                                break;
                            case "lv100":
                                quality = QualityEnum.Lvl100;
                                break;
                            default:
                                quality = QualityEnum.Low;
                                break;
                        }
                        if (quality != QualityEnum.Low)
                        {
                            foreach (var p in properties.Values)
                            {
                                if (p.Name == "Durability")
                                {
                                    p.Quality = quality;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        var prop = allProperties.FirstOrDefault(f => s[i].StartsWith(f.Name));
                        if (prop != null)
                        {
                            var sameStart = allProperties.Where(w => w.Name.StartsWith(prop.Name));
                            if (sameStart.Count() > 1)
                            {
                                foreach (var p in sameStart.OrderByDescending(o => o.Name.Length))
                                {
                                    if (s[i].StartsWith(p.Name))
                                    {
                                        prop = p;
                                        break;
                                    }
                                }
                            }
                            var newIP = new ItemProperty(prop);
                            if (prop.IsDependsByQuality)
                            {
                                newIP.Quality = quality;
                            }
                            switch (prop.PropertyType)
                            {
                                case PropertyTypeEnum.Text:
                                    newIP.Value = RemoveSlash(GetLastAndAnothrWords(s[i].Replace(prop.Name, "").Replace("(", "").Replace(")", "").Replace(':', ' ').Split(' ').Where(w => !string.IsNullOrWhiteSpace(w)).ToList().ToArray()));
                                    break;
                                case PropertyTypeEnum.Percent:
                                case PropertyTypeEnum.Number:
                                    newIP.Value = RemoveSlash(s[i].Replace(prop.Name, "").Replace("(", "").Replace(")", "").Replace(':', ' ').Split(' ').Where(w => !string.IsNullOrWhiteSpace(w)).LastOrDefault() ?? "");
                                    break;
                            }
                            properties.Add(counter, newIP);
                            counter++;
                        }
                        else
                        {
                            if (findSBT) continue;
                            if (s[i].Contains(':'))
                            {
                                var p = ParseWithColon(s[i]);
                                if (!string.IsNullOrWhiteSpace(p.Value))
                                {
                                    properties.Add(counter, p);
                                    counter++;
                                }
                            }
                            else
                            {
                                var newIP = new ItemProperty();
                                var ss = s[i].Split(' ').Where(w => !string.IsNullOrWhiteSpace(w)).ToList().ToArray();
                                if (ss.Length == 1)
                                {
                                    newIP.Name = ss[0];
                                    newIP.Value = RemoveSlash(ss[0]);
                                }
                                else
                                {
                                    var last = ss.Last();
                                    newIP.Value = RemoveSlash(last);
                                    newIP.Name = string.Join(' ', ss.Where(w => CheckIsWord(w) && w != last));
                                }
                                properties.Add(counter, newIP);
                                counter++;
                            }
                        }
                    }
                }
                if (findSBT)
                {
                    var sbtProp = allProperties.FirstOrDefault(f => f.Name == "Switchable building types");
                    var value = s[sbt].Replace("Switchable building types", "").Trim();
                    for (int i = sbt + 1; i < s.Length; i++)
                    {
                        if (s[i] == s[0] || s[i].Contains(':')) break;
                        value += ' ' + s[i].Trim();
                    }
                    if (sbtProp == null)
                    {
                        properties.Add(counter, new ItemProperty() { Name = "Switchable building types", Value = value });
                    }
                    else
                    {
                        properties.Add(counter, new ItemProperty(sbtProp) { Value = value });
                    }
                    counter++;
                }
                if (descrNum != 0)
                {
                    var descrProp = allProperties.FirstOrDefault(f => f.Name == "Description");
                    var value = s[descrNum].Replace("Description:", "").Trim();
                    for (int i = descrNum + 1; i < s.Length; i++)
                    {
                        if (s[i] == s[0]) break;
                        value += ' ' + s[i].Trim();
                    }
                    value += '.';
                    value = value.Replace("..", ".").Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "");
                    if (descrProp == null)
                    {
                        properties.Add(counter, new ItemProperty() { Name = "Description", Value = value });
                    }
                    else
                    {
                        properties.Add(counter, new ItemProperty(descrProp) { Value = value });
                    }
                }
            }
            FillItemsInPanel();
        }
        private void FillItemsInPanel()
        {
            var lastY = 5;
            panel1.Controls.Clear();
            foreach (var ip in properties)
            {
                var l = GenerateLabel($"{ip.Value.Name}: {ip.Value.Value} [{ip.Value.Quality}]", ip.Value.PropertyId == 0 ? Color.Red : Color.Green, lastY, 5, $"lIPKey{ip.Key}");
                lastY += l.Height + 10;
                l.Tag = ip.Key;
                if (ip.Value.PropertyId == 0)
                {
                    bAddAll.Visible = true;
                    l.DoubleClick += new EventHandler(AddNewProperty);
                }
                panel1.Controls.Add(l);
            }
        }
        private void AddNewProperty(object sender, EventArgs e)
        {
            var c = sender as Label;
            if (c != null)
            {
                var key = c.Tag.ToInt();
                var ip = properties[key];
                DialogResult dialogResult = MessageBox.Show("Sure", $"Add new property \"{ip.Name}\"???", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        var valType = PropertyTypeEnum.Text;
                        Double d = 0;
                        if (ip.Value.EndsWith('%'))
                        {
                            valType = PropertyTypeEnum.Percent;
                        }
                        else if (Double.TryParse(PrepareForDouble(ip.Value), out d))
                        {
                            valType = PropertyTypeEnum.Number;
                        }
                        var entity = GatewaysFacade.PropertyGateway.Add(new Property() { Name = ip.Name, AccessLvl = 1, PropertyType = valType });
                        ip.PropertyId = entity.Id;
                        properties[key] = ip;
                        c.ForeColor = Color.Green;
                        c.DoubleClick -= new EventHandler(AddNewProperty);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void DelItemProperty(object sender, EventArgs e)
        {
            var b = sender as Button;
            var entity = b.Tag as ItemProperty;
            try
            {
                GatewaysFacade.ItemPropertyGateway.Delete(entity.Id);
                panel2.Controls.Remove(b);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateItemProperty(object sender, EventArgs e)
        {
            var b = sender as Button;
            var entity = b.Tag as ItemProperty;
            try
            {
                GatewaysFacade.ItemPropertyGateway.Update(entity);
                panel2.Controls.Remove(b);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddItemProperty(object sender, EventArgs e)
        {
            var b = sender as Button;
            var entity = b.Tag as ItemProperty;
            try
            {
                GatewaysFacade.ItemPropertyGateway.Add(entity);
                panel2.Controls.Remove(b);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Recipe
        private void ParseItemRecipes(string sourse)
        {
            var v = sourse.Split('\n').Select(s => s.Replace("\r", "").Replace('\t', ' ').Trim()).Where(w => !string.IsNullOrWhiteSpace(w)).ToList().ToArray();
            recipes.Clear();
            if (v.Length > 0)
            {
                var descrNum = 0;
                var costNum = 0;
                if (item == null || mode == ModeEnum.Recipe)
                {
                    lSubcat.Text = "Category not found";
                    lSubcat.Tag = "Category not found";
                    CheckItem(v[0]);
                }
                for (int i = 1; i < v.Length; i++)
                {
                    if (v[i].StartsWith("Cost"))
                    {
                        costNum = i;
                        break;
                    }
                }
                for (int i = costNum + 1; i < v.Length; i++)
                {
                    if (v[i].Contains("Description"))
                    {
                        descrNum = i;
                        break;
                    }
                }
                var isStep = false;
                var counter = 0;
                for (int i = costNum + 1; i <= descrNum - 1; i++)
                {
                    if (v[i].ToLower().StartsWith("step"))
                    {
                        isStep = true;
                        continue;
                    }
                    var newIR = new ItemRecipe() { IsStepByStep = isStep };
                    var s = v[i].Split(' ').Where(w => !string.IsNullOrWhiteSpace(w)).ToList().ToArray();
                    var last = s.Last();
                    newIR.RecipeItemName = string.Join(' ', s.Where(w => CheckIsWord(w) && w != last));
                    var count = 0;
                    int.TryParse(RemoveSlash(last), out count);
                    newIR.Count = count;
                    var p = GatewaysFacade.ItemGateway.GetByNameSmart(newIR.RecipeItemName);
                    if (p != null)
                    {
                        newIR.RecipeItemName = p.Name;
                        newIR.RecipeItemId = p.Id;
                    }
                    recipes.Add(counter, newIR);
                    counter++;
                }
                FillRecipesInPanel();
            }
        }
        private void FillRecipesInPanel()
        {
            var lastY = 5;
            if (mode == ModeEnum.Recipe)
            {
                panel1.Controls.Clear();
            }
            else
            {
                var lastL = panel1.Controls.OfType<Label>().LastOrDefault();
                if (lastL != null)
                {
                    lastY = lastL.Height + lastL.Location.Y + 10;
                }
                panel1.Controls.Add(GenerateLabel("Recipe", lastY));
                lastY += rowHeight + 5;

            }
            foreach (var ir in recipes)
            {
                var l = GenerateLabel($"{ir.Value.RecipeItemName} {ir.Value.Count} {ir.Value.IsStepByStep}", ir.Value.RecipeItemId == 0 ? Color.Red : Color.Green, lastY, 5, $"lIRKey{ir.Key}");
                lastY += l.Height + 10;
                l.Tag = ir.Key;
                if (ir.Value.RecipeItemId == 0)
                {
                    bAddAll.Visible = true;
                    l.DoubleClick += new EventHandler(AddNewItem);
                }
                panel1.Controls.Add(l);
            }
        }
        private void AddNewItem(object sender, EventArgs e)
        {
            var c = sender as Label;
            if (c != null)
            {
                var key = c.Tag.ToInt();
                var ir = recipes[key];
                if (material == null)
                    material = GatewaysFacade.SubcategoryGateway.GetByNameOrSynonym("Materials");
                DialogResult dialogResult = MessageBox.Show("Sure", $"Add new item \"{ir.RecipeItemName}\"???", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        var entity = GatewaysFacade.ItemGateway.Add(new Item() { Name = ir.RecipeItemName, SubcategoryId = material.Id });
                        ir.RecipeItemId = entity.Id;
                        recipes[key] = ir;
                        c.ForeColor = Color.Green;
                        c.DoubleClick -= new EventHandler(AddNewItem);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void DelItemRecipe(object sender, EventArgs e)
        {
            var b = sender as Button;
            var entity = b.Tag as ItemRecipe;
            try
            {
                GatewaysFacade.ItemRecipeGateway.Delete(entity.Id);
                panel2.Controls.Remove(b);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateItemRecipe(object sender, EventArgs e)
        {
            var b = sender as Button;
            var entity = b.Tag as ItemRecipe;
            try
            {
                GatewaysFacade.ItemRecipeGateway.Update(entity);
                panel2.Controls.Remove(b);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddItemRecipe(object sender, EventArgs e)
        {
            var b = sender as Button;
            var entity = b.Tag as ItemRecipe;
            try
            {
                GatewaysFacade.ItemRecipeGateway.Add(entity);
                panel2.Controls.Remove(b);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Perk

        private void ParsePerk(string sourse)
        {
            var v = sourse.Split('\n').Select(s => s.Replace("\r", "").Replace('\t', ' ').Trim()).Where(w => !string.IsNullOrWhiteSpace(w)).ToList().ToArray();
            parsedPerk = null;
            if (v.Length > 0)
            {
                parsedPerk = new Perk();
                if (!string.IsNullOrWhiteSpace(result2))
                {
                    parsedPerk.Skill = Enum.GetValues(typeof(SkillEnum)).Cast<SkillEnum>().FirstOrDefault(f => f.ToString() == result2);
                }
                parsedPerk.Name = v[0];
                CheckPerk(parsedPerk.Name);
                var act = 1;
                while (act < v.Length && v[act] != "Activation Condition:")
                {
                    parsedPerk.Description += v[act] + ' ';
                    act++;
                }
                if (act < v.Length)
                {
                    for (int i = act + 1; i < v.Length; i++)
                    {
                        foreach (var s in new List<string>() { "Level", "Requires Skill Lv.:", "Requires Talent:", "Comprehension Required:", "Copper Coins Required:" })
                        {
                            try
                            {
                                if (v[i].StartsWith(s))
                                {
                                    switch (s)
                                    {
                                        case "Level": parsedPerk.Level = int.Parse(v[i].Replace(s, "").Trim()); break;
                                        case "Requires Skill Lv.:": parsedPerk.SkillLevel = int.Parse(v[i].Replace(s, "").Trim()); break;
                                        case "Comprehension Required:": parsedPerk.ComprehensionPoint = int.Parse(v[i].Replace(s, "").Trim()); break;
                                        case "Copper Coins Required:": parsedPerk.CopperCoins = int.Parse(v[i].Replace(s, "").Trim()); break;
                                        case "Requires Talent:": parsedPerk.PreviousName = v[i].Replace(s, "").Trim(); break;
                                    }
                                    continue;
                                }
                            }
                            catch (Exception ex) { MessageBox.Show(ex.Message); }
                        }
                        if (v[i].StartsWith("Item Requires:") && i + 1 < v.Length)
                        {
                            var all = string.Join(' ', v[(i + 1)..]);
                            var talentBookName = all.Substring(all.IndexOf('(') + 1, all.IndexOf(':') - 2 - all.IndexOf('('));
                            var talentBook = GatewaysFacade.ItemGateway.GetByNameSmart(talentBookName);
                            if (talentBook == null)
                            {
                                talentBook = new Item() { Name = talentBookName, SubcategoryId = GatewaysFacade.SubcategoryGateway.GetByNameOrSynonym("Talant book").Id };
                                talentBook = GatewaysFacade.ItemGateway.Add(talentBook);
                            }
                            parsedPerk.RequiredItemId = talentBook.Id;
                        }
                    }
                }
            }
            FillPerkInPanel();
        }
        private void FillPerkInPanel()
        {
            panel1.Controls.Clear();
            if (parsedPerk == null) return;
            var lastY = 5;
            var l = GenerateLabel($"Name: {parsedPerk.Name}", lastY);
            lastY += l.Height + 10;
            panel1.Controls.Add(l);
            l = GenerateLabel($"Description: {parsedPerk.Description}", lastY);
            lastY += l.Height + 10;
            panel1.Controls.Add(l);
            if (parsedPerk.Level != 0)
            {
                l = GenerateLabel($"R Level: {parsedPerk.Level}", lastY);
                lastY += l.Height + 10;
                panel1.Controls.Add(l);
            }
            if (parsedPerk.SkillLevel != 0)
            {
                l = GenerateLabel($"R SkillLevel: {parsedPerk.SkillLevel}", lastY);
                lastY += l.Height + 10;
                panel1.Controls.Add(l);
            }
            if (parsedPerk.ComprehensionPoint != 0)
            {
                l = GenerateLabel($"R Comprehension: {parsedPerk.ComprehensionPoint}", lastY);
                lastY += l.Height + 10;
                panel1.Controls.Add(l);
            }
            if (parsedPerk.CopperCoins != 0)
            {
                l = GenerateLabel($"R Comprehension: {parsedPerk.ComprehensionPoint}", lastY);
                lastY += l.Height + 10;
                panel1.Controls.Add(l);
            }
            if (parsedPerk.RequiredItemId != 0)
            {
                l = GenerateLabel($"RequiredItemId: {parsedPerk.RequiredItemId}", lastY);
                lastY += l.Height + 10;
                panel1.Controls.Add(l);
            }
            if (!string.IsNullOrWhiteSpace(parsedPerk.PreviousName))
            {
                l = GenerateLabel($"PreviousName: {parsedPerk.PreviousName}", lastY);
                lastY += l.Height + 10;
                panel1.Controls.Add(l);
            }
        }
        private void CheckPerk(string name)
        {
            if (perk == null || perk.Name != name)
            {
                var p = GatewaysFacade.PerkGateway.GetByName(name);
                if (p != null)
                {
                    perk = p;
                    lItemName.Text = perk.Name;
                    lItemName.ForeColor = Color.Green;
                    bCompare.Visible = true;
                    bAddNew.Visible = false;
                }
                else
                {
                    lItemName.Text = name;
                    lItemName.ForeColor = Color.Red;
                    bCompare.Visible = false;
                    bAddNew.Visible = true;
                    bCommitAll.Visible = false;
                }
            }
        }
        private void UpdatePerk(object sender, EventArgs e)
        {
            var b = sender as Button;
            var propName = b.Tag as String;
            try
            {
                perk.SetValueByPropertyName(propName, parsedPerk.GetValueByPropertyName(propName));
                if (!fromAutoParse) perk.LastUpdateDate = DateTime.Now;
                this.DialogResult = DialogResult.OK;
                perk = GatewaysFacade.PerkGateway.Update(perk);
                panel2.Controls.Remove(b);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Research

        private void ParseResearch(string sourse)
        {
            try
            {
                var v = sourse.Split('\n').Select(s => s.Replace("\r", "").Replace('\t', ' ').Trim()).Where(w => !string.IsNullOrWhiteSpace(w)).ToList().ToArray();
                parsedResearch = null;
                if (v.Length > 0)
                {
                    parsedResearch = new Research();
                    if (!string.IsNullOrWhiteSpace(result2))
                    {
                        parsedResearch.ResearchBranch = Enum.GetValues(typeof(ResearchBranchEnum)).Cast<ResearchBranchEnum>().FirstOrDefault(f => f.ToString() == result2);
                    }
                    var ind = 0;
                    if (SaveOnlyLetters(v[0]).EndsWith("Locked"))
                    {
                        parsedResearch.Name = v[0].Substring(0, v[0].IndexOf("Locked") - 1).Trim();
                        parsedResearch.Level = int.Parse(v[1].Replace("Level", "").Trim());
                        ind = 2;
                        if (v[ind].StartsWith("Recipe Points Required"))
                        {
                            parsedResearch.RecipePoint = int.Parse(v[ind].Replace("Recipe Points Required:", "").Trim());
                            ind++;
                        }
                        if (v[ind].StartsWith("Guild Level"))
                        {
                            parsedResearch.GuildLevel = int.Parse(v[ind].Replace("Guild Level:", "").Trim());
                            ind++;
                        }
                        if (v[ind].StartsWith("Guild Activity Points"))
                        {
                            parsedResearch.GuildActivity = int.Parse(v[ind].Replace("Guild Activity Points:", "").Trim());
                            ind++;
                        }
                        if (v[ind].StartsWith("Guild Bank Copper Coins"))
                        {
                            parsedResearch.CopperCoins = int.Parse(v[ind].Replace("Guild Bank Copper Coins:", "").Trim());
                            ind++;
                        }

                        if (v[ind].StartsWith("Item Requires"))
                        {
                            ind++;
                            parsedResearch.RequiredItemName = v[ind][0..(v[ind].Trim().LastIndexOf(' '))];
                            parsedResearch.RequiredItemCount = int.Parse(v[ind][(v[ind].Trim().LastIndexOf('/') + 1)..]);
                            var item = TryToGetItem(parsedResearch.RequiredItemName);
                            if (item.Id != 0)
                            {
                                parsedResearch.RequiredItemName = item.Name;
                                parsedResearch.RequiredItemId = item.Id;
                            }
                            ind++;
                        }
                        else
                        if (v[ind].StartsWith("Need to unlock the"))
                        {
                            ind++;
                            parsedResearch.PreviousName = SaveOnlyLetters(v[ind]).Trim();
                            ind++;
                        }
                    }
                    else
                    {
                        parsedResearch.Name = v[0].Substring(0, v[0].Length - 7).Trim();
                        parsedResearch.Level = 1;
                        parsedResearch.RecipePoint = 0;
                        ind = 1;
                    }
                    CheckResearch(parsedResearch.Name);
                    var descr = 0;
                    for (int i = ind; i < v.Length; i++)
                    {
                        if (v[i].StartsWith('/'))
                        {
                            descr = i;
                            break;
                        }
                    }
                    if (descr == 0)
                    {
                        for (int i = ind; i < v.Length; i++)
                        {
                            if (EndWithUpperWord(v[i]))
                            {
                                if (i == v.Length - 1)
                                {
                                    descr = i;
                                    break;
                                }
                                parsedResearch.ItemsList.Add(TryToGetItem(v[i]));
                            }
                            else
                            {
                                descr = i;
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = ind; i < descr; i++)
                        {
                            parsedResearch.ItemsList.Add(TryToGetItem(v[i]));
                        }
                    }
                    if (descr != 0)
                    {
                        for (int i = descr; i < v.Length; i++)
                        {
                            parsedResearch.Description += v[i] + ' ';
                        }
                        parsedResearch.Description = parsedResearch.Description.Replace("/", "").Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Trim();
                    }
                    else
                    {
                        MessageBox.Show("Not find Descr!");
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
            FillResearchInPanel();
        }
        private Item TryToGetItem(string name)
        {
            var result = GatewaysFacade.ItemGateway.GetByNameSmart(name);
            if (result == null)
            {
                return new Item() { Name = name};
            }
            else
            {
                return result;
            }
        }
        private bool EndWithUpperWord(string sourse)
        {
            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var last = sourse.Split(' ').Where(w => !string.IsNullOrWhiteSpace(w)).Last();
            if (last == null) return true;
            return alphabet.Contains(last[0]);
        }
        private void FillResearchInPanel()
        {
            panel1.Controls.Clear();
            if (parsedResearch == null) return;
            var lastY = 5;
            var l = GenerateLabel($"Name: {parsedResearch.Name}", lastY);
            lastY += l.Height + 10;
            panel1.Controls.Add(l);
            l = GenerateLabel($"Description: {parsedResearch.Description}", lastY);
            lastY += l.Height + 10;
            panel1.Controls.Add(l);
            if (parsedResearch.Level != 0)
            {
                l = GenerateLabel($"R Level: {parsedResearch.Level}", lastY);
                lastY += l.Height + 10;
                panel1.Controls.Add(l);
            }
            if (parsedResearch.RecipePoint != 0)
            {
                l = GenerateLabel($"R RecipePoint: {parsedResearch.RecipePoint}", lastY);
                lastY += l.Height + 10;
                panel1.Controls.Add(l);
            }
            if (parsedResearch.GuildLevel != 0)
            {
                l = GenerateLabel($"R GuildLevel: {parsedResearch.GuildLevel}", lastY);
                lastY += l.Height + 10;
                panel1.Controls.Add(l);
            }
            if (parsedResearch.GuildActivity != 0)
            {
                l = GenerateLabel($"R GuildActivity: {parsedResearch.GuildActivity}", lastY);
                lastY += l.Height + 10;
                panel1.Controls.Add(l);
            }
            if (parsedResearch.CopperCoins != 0)
            {
                l = GenerateLabel($"R CopperCoins: {parsedResearch.CopperCoins}", lastY);
                lastY += l.Height + 10;
                panel1.Controls.Add(l);
            }
            if (!string.IsNullOrWhiteSpace(parsedResearch.PreviousName))
            {
                l = GenerateLabel($"PreviousName: {parsedResearch.PreviousName}", lastY);
                lastY += l.Height + 10;
                panel1.Controls.Add(l);
            }
            if (!string.IsNullOrWhiteSpace(parsedResearch.RequiredItemName))
            {
                if (parsedResearch.RequiredItemId != 0)
                {
                    l = GenerateLabel($"R ItemName: {parsedResearch.RequiredItemName}", lastY);
                }
                else
                {
                    l = GenerateLabel($"R ItemName: {parsedResearch.RequiredItemName}", Color.Red, lastY, 5, $"lResKey{parsedResearch.RequiredItemName}");
                    bAddAll.Visible = true;
                    l.Tag = parsedResearch.RequiredItemName;
                    l.DoubleClick += new EventHandler(AddNewItemFromResearch);
                }
                lastY += l.Height + 10;
                panel1.Controls.Add(l);
            }
            l = GenerateLabel("ItemList:", Color.Black, lastY);
            lastY += l.Height + 10;
            panel1.Controls.Add(l);
            foreach (var i in parsedResearch.ItemsList)
            {
                if (i.Id != 0)
                {
                    l = GenerateLabel(i.Name, lastY);
                }
                else
                {
                    l = GenerateLabel(i.Name, Color.Red, lastY, 5, $"lResKey{i.Name}");
                    bAddAll.Visible = true;
                    l.Tag = i.Name;
                    l.DoubleClick += new EventHandler(AddNewItemFromResearch);
                }
                lastY += l.Height + 10;
                panel1.Controls.Add(l);
            }
        }
        private void CheckResearch(string name)
        {
            if (research == null || research.Name != name)
            {
                var p = GatewaysFacade.ResearchGateway.GetByName(name);
                if (p != null)
                {
                    research = p;
                    lItemName.Text = research.Name;
                    lItemName.ForeColor = Color.Green;
                    bCompare.Visible = true;
                    bAddNew.Visible = false;
                }
                else
                {
                    lItemName.Text = name;
                    lItemName.ForeColor = Color.Red;
                    bCompare.Visible = false;
                    bAddNew.Visible = true;
                    bCommitAll.Visible = false;
                }
            }
        }
        private void UpdateResearch(object sender, EventArgs e)
        {
            var b = sender as Button;
            var propName = b.Tag as String;
            try
            {
                research.SetValueByPropertyName(propName, parsedResearch.GetValueByPropertyName(propName));
                if (!fromAutoParse) research.LastUpdateDate = DateTime.Now;
                this.DialogResult = DialogResult.OK;
                research = GatewaysFacade.ResearchGateway.Update(research);
                panel2.Controls.Remove(b);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddNewItemFromResearch(object sender, EventArgs e)
        {
            var c = sender as Label;
            if (c != null)
            {
                var key = c.Tag.ToString();
                if (material == null)
                    material = GatewaysFacade.SubcategoryGateway.GetByNameOrSynonym("Materials");
                DialogResult dialogResult = MessageBox.Show("Sure", $"Add new item \"{key}\"???", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        var entity = GatewaysFacade.ItemGateway.Add(new Item() { Name = key, SubcategoryId = material.Id });
                        if (parsedResearch.RequiredItemName == key)
                        {
                            parsedResearch.RequiredItemId = entity.Id;
                            parsedResearch.RequiredItemName = entity.Name;
                        }
                        else
                        {
                            var f = parsedResearch.ItemsList.FirstOrDefault(w => w.Name == key);
                            if (f != null)
                            {
                                f.Id = entity.Id;
                                f.Name = entity.Name;
                            }
                        }
                        c.ForeColor = Color.Green;
                        c.DoubleClick -= new EventHandler(AddNewItem);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void UpdateItemResearch(object sender, EventArgs e)
        {
            var b = sender as Button;
            var entity = b.Tag as Item;
            try
            {
                GatewaysFacade.ItemGateway.UpdateResearch(entity.Id, entity.ResearchId);
                panel2.Controls.Remove(b);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}
