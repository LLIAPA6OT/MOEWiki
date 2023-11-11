using MOEWiki.DBMySQL.Enums;
using MOEWiki.DBMySQL.Gateways;
using MOEWiki.DBMySQL.Helpers;
using MOEWiki.DBMySQL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MOEWiki.Parser3
{
    public partial class AutoParserForm : Form
    {
        private AutoParse? currentAP = null;
        public AutoParserForm()
        {
            InitializeComponent();
        }

        private void AutoParserForm_Load(object sender, EventArgs e)
        {
            cbResult.DataSource = Enum.GetValues(typeof(ParseResultEnum));
            cbMode.DataSource = Enum.GetValues(typeof(ModeEnum));
            this.cbResult.SelectedItem = ParseResultEnum.ItemNotFound;
            cbMode.DataSource = Enum.GetValues(typeof(ModeEnum));
            this.cbMode.SelectedItem = ModeEnum.Item;
            dgv.Columns.Clear();
            dgv.Columns.Add("id", "id");
            dgv.Columns.Add("ParseResult", "ParseResult");
            dgv.Columns.Add("ParseState", "ParseState");
            dgv.Columns.Add("Mode", "Mode");
            dgv.Columns[0].Width = 0;
        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
            var listAll = GatewaysFacade.AutoParseGateway.GetAllParsed().ToList();
            //var cbsi = ParseResultEnum.NothingChanged;
            //Enum.TryParse<ParseResultEnum>(cbResult.SelectedValue.ToString(), out cbsi);
            var mode = ModeEnum.Item;
            Enum.TryParse<ModeEnum>(cbMode.SelectedValue.ToString(), out mode);
            //listAll = listAll.Where(f => (f.ParseResult.Contains($";{cbsi}:") || f.ParseResult.StartsWith($"{cbsi}:") && f.Mode == mode)).ToList();
            listAll = listAll.Where(f => f.Mode == mode).ToList();
            foreach (var item in listAll.OrderBy(o => o.ParseResult))
            {
                dgv.Rows.Add(item.Id, item.ParseResult, item.ParseState, item.Mode);
            }
        }

        private void bCommitGreen_Click(object sender, EventArgs e)
        {
            foreach (var ap in GatewaysFacade.AutoParseGateway.GetAllParsed().Where(w => w.ParseResult.StartsWith($"{ParseResultEnum.NothingChanged}:") && w!.ParseResult.Contains($";{ParseResultEnum.ImageChanged}:")).ToList())
            {
                try
                {
                    var item = GatewaysFacade.ItemGateway.GetById(ap.Id);
                    if (item != null)
                    {
                        switch (ap.Mode)
                        {
                            case ModeEnum.Item:
                                item.PropertiesLastUpdateDate = ap.CreationDate;
                                break;
                            case ModeEnum.Recipe:
                                item.RecipesLastUpdateDate = ap.CreationDate;
                                break;
                        }
                        GatewaysFacade.ItemGateway.Update(item);
                    }
                    ap.ParseState = ParseStateEnum.Confirmed;
                    GatewaysFacade.AutoParseGateway.Update(ap);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void bDelFinished_Click(object sender, EventArgs e)
        {
            foreach (var ap in GatewaysFacade.AutoParseGateway.GetAll().Where(w => w.ParseState == ParseStateEnum.Confirmed).ToList())
            {
                try
                {
                    GatewaysFacade.AutoParseGateway.Delete(ap.Id);
                    File.Delete(ap.ImageName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void bDelImages_Click(object sender, EventArgs e)
        {
            var counterDel = 0;
            var counterSkip = 0;
            var path = $"E:\\MOE\\auto";
            if (Directory.Exists(path))
            {
                foreach (var file in new DirectoryInfo(path).GetFiles())
                {
                    if (!GatewaysFacade.AutoParseGateway.CheckImageName(file.Name))
                    {
                        file.Delete();
                        counterDel++;
                    }
                    else
                    {
                        counterSkip++;
                    }
                }
                MessageBox.Show($"Deleted:{counterDel}. Skiped:{counterSkip}");
            }
        }

        private void bSaveAndReturn_Click(object sender, EventArgs e)
        {
            if (currentAP != null)
            {
                try
                {
                    currentAP.RecognitionResult = tbResult.Text;
                    currentAP.ParseState = ParseStateEnum.Returned;
                    GatewaysFacade.AutoParseGateway.Update(currentAP);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        private void OpenParseForm(AutoParse autoParse)
        {
            var f = new ParserForm();
            f.autoParse = autoParse;
            f.fromAutoParse = true;
            f.mode = autoParse.Mode;
            if (autoParse.ItemId != 0)
            {
                switch (autoParse.Mode) 
                {
                    case ModeEnum.Item:
                    case ModeEnum.Recipe:
                        f.item = GatewaysFacade.ItemGateway.GetById(autoParse.ItemId);
                        if (string.IsNullOrWhiteSpace(f.item.ImageName))
                        {
                            var b = new Bitmap(Image.FromFile(autoParse.ImageName));
                            f.image = b.Clone(new Rectangle(16, 16, 118, 118), b.PixelFormat);
                        }
                        break;
                    case ModeEnum.Perk:
                        f.perk = GatewaysFacade.PerkGateway.GetById(autoParse.ItemId);
                        break;
                }
            }
            if (autoParse.ParseResult.StartsWith($"{ParseResultEnum.ImageChanged}:") || autoParse.ParseResult.Contains($";{ParseResultEnum.ImageChanged}:") || autoParse.ItemId == 0)
            {
                var b = new Bitmap(Image.FromFile(autoParse.ImageName));
                f.image = b.Clone(new Rectangle(16, 16, 118, 118), b.PixelFormat);
            }
            DialogResult dialogResult = f.ShowDialog();
            if (dialogResult != DialogResult.No && dialogResult != DialogResult.Cancel)
            {
                try
                {
                    if (autoParse.ItemId != 0)
                    {
                        if (autoParse.Mode == ModeEnum.Item || autoParse.Mode == ModeEnum.Recipe)
                        {
                            var item = GatewaysFacade.ItemGateway.GetById(autoParse.ItemId);
                            if (item != null)
                            {
                                switch (autoParse.Mode)
                                {
                                    case ModeEnum.Item:
                                        item.PropertiesLastUpdateDate = autoParse.CreationDate;
                                        break;
                                    case ModeEnum.Recipe:
                                        item.RecipesLastUpdateDate = autoParse.CreationDate;
                                        break;
                                }
                                GatewaysFacade.ItemGateway.Update(item);
                            }
                        }
                        else
                        if (autoParse.Mode == ModeEnum.Perk)
                        {
                            var perk = GatewaysFacade.PerkGateway.GetById(autoParse.ItemId);
                            perk.LastUpdateDate = autoParse.CreationDate;
                            GatewaysFacade.PerkGateway.Update(perk);
                        }
                    }
                    autoParse.ParseState = ParseStateEnum.Confirmed;
                    GatewaysFacade.AutoParseGateway.Update(autoParse);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void bParseSelected_Click(object sender, EventArgs e)
        {
            if (currentAP == null) return;
            currentAP.RecognitionResult = tbResult.Text;
            OpenParseForm(currentAP);
        }

        private void bParseAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                var autoParse = GatewaysFacade.AutoParseGateway.GetById(dgv.Rows[i].Cells[0].Value.ToInt());
                if (autoParse != null)
                    OpenParseForm(autoParse);
            }
        }

        private void dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var id = dgv.Rows[e.RowIndex].Cells[0].Value.ToInt();
            tbResult.Text = "";
            pb.Image = null;
            currentAP = GatewaysFacade.AutoParseGateway.GetById(id);
            if (currentAP != null)
            {
                tbResult.Text = currentAP.RecognitionResult;
                if (!string.IsNullOrWhiteSpace(currentAP.ImageName))
                {
                    if (File.Exists(currentAP.ImageName))
                    {
                        pb.Image = Image.FromFile(currentAP.ImageName);
                    }
                }
            }
        }
    }
}
