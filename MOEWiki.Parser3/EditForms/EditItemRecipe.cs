using MOEWiki.DBMySQL.Enums;
using MOEWiki.DBMySQL.Gateways;
using MOEWiki.DBMySQL.Models;
using MOEWiki.Parser3.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MOEWiki.Parser3.EditForms
{
    public partial class EditItemRecipe : Form
    {
        public bool changed = false;
        public int id = 0;
        public int itemId = 0;
        private ItemRecipe? itemR;
        public EditItemRecipe()
        {
            InitializeComponent();
        }


        private void EditItemRecipe_Load(object sender, EventArgs e)
        {
            cbRecipeItem.Items.Clear();
            if (id > 0)
            {
                itemR = GatewaysFacade.ItemRecipeGateway.GetById(id);
                var counter = 0;
                foreach (var item in GatewaysFacade.ItemGateway.GetAllMaterials().ToList())
                {
                    cbRecipeItem.Items.Add(new ComboboxItem() { Text = $"{item.Name}", Value = item.Id });
                    if (itemR.RecipeItemId == item.Id)
                    {
                        cbRecipeItem.SelectedIndex = counter;
                    }
                    counter++;
                }
                nudCount.Value = itemR.Count;
                nudNumber.Value = itemR.Number;
                chbIsStepByStep.Checked = itemR.IsStepByStep;
                bSave.Visible = true;
                bDel.Visible = true;
                bHardDelete.Visible = true;
            }
            if (itemId > 0)
            {
                foreach (var item in GatewaysFacade.ItemGateway.GetAllMaterials().ToList())
                {
                    cbRecipeItem.Items.Add(new ComboboxItem() { Text = $"{item.Name}", Value = item.Id });
                }
                cbRecipeItem.SelectedIndex = 0;
                bAdd.Visible = true;
            }
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRecipe = cbRecipeItem.SelectedItem as ComboboxItem;
                if (selectedRecipe != null && (itemR.RecipeItemId != selectedRecipe.Value || itemR.Number != nudNumber.Value || itemR.Count != nudCount.Value || itemR.IsStepByStep != chbIsStepByStep.Checked))
                {
                    if (itemR.RecipeItemId != selectedRecipe.Value)
                    {
                        itemR.RecipeItemId = selectedRecipe.Value;
                        itemR.RecipeItemName = selectedRecipe.Text;
                    }
                    itemR.Number = (int)nudNumber.Value;
                    itemR.Count = (int)nudCount.Value;
                    itemR.IsStepByStep = chbIsStepByStep.Checked;
                    GatewaysFacade.ItemRecipeGateway.Update(itemR);
                    changed = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect values or nothing changed!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bDel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Sure", "Delete???", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    GatewaysFacade.ItemRecipeGateway.Delete(id);
                    changed = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void bHardDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Sure", "Hard Delete???", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    GatewaysFacade.ItemRecipeGateway.HardDelete(id);
                    changed = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRecipe = cbRecipeItem.SelectedItem as ComboboxItem;
                if (selectedRecipe != null)
                {
                    itemR = new ItemRecipe();
                    itemR.ItemId = itemId;
                    itemR.RecipeItemId = selectedRecipe.Value;
                    itemR.RecipeItemName = selectedRecipe.Text;
                    itemR.Number = (int)nudNumber.Value;
                    itemR.Count = (int)nudCount.Value;
                    itemR.IsStepByStep = chbIsStepByStep.Checked;
                    GatewaysFacade.ItemRecipeGateway.Add(itemR);
                    changed = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect values!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
