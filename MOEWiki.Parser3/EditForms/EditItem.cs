using MOEWiki.DBMySQL.Gateways;
using MOEWiki.DBMySQL.Models.Interfaces;
using MOEWiki.DBMySQL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MOEWiki.Parser3.Helpers;
using MOEWiki.Parser3.EditForms;
using System.Threading.Channels;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Imaging;
using Image = System.Drawing.Image;
using MOEWiki.DBMySQL.Helpers;

namespace MOEWiki.Parser3
{
    public partial class EditItem : Form
    {
        private int currentId;
        public EditItem()
        {
            InitializeComponent();
        }

        private void bDel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Sure", "Delete???", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    GatewaysFacade.CategoryGateway.Delete(this.currentId);
                    bSearch_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }

        private TEntity GetEntity<TEntity>(TEntity entity) where TEntity : class, IBase
        {
            foreach (var prop in typeof(TEntity).GetProperties())
            {
                var tb = this.Controls.OfType<TextBox>().FirstOrDefault(f => f.Name == $"tb{prop.Name}");
                if (tb != null)
                {
                    entity.SetValueByPropertyName(prop.Name, tb.Text);
                }
            }
            return entity;
        }

        private void FillTextBoxes<TEntity>(TEntity entity) where TEntity : class, IBase
        {
            foreach (var prop in typeof(TEntity).GetProperties())
            {
                var tb = this.Controls.OfType<TextBox>().FirstOrDefault(f => f.Name == $"tb{prop.Name}");
                if (tb != null)
                {
                    tb.Text = prop.GetValue(entity, null)?.ToString() ?? "";
                }
            }
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            try
            {
                GatewaysFacade.CategoryGateway.Add(GetEntity(new Category()));
                MessageBox.Show("Done");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bEdit_Click(object sender, EventArgs e)
        {
            try
            {
                GatewaysFacade.CategoryGateway.Update(GetEntity(new Category() { Id = currentId }));
                MessageBox.Show("Done");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Edit_Load(object sender, EventArgs e)
        {
            dgw.Columns.Clear();
            dgw.Columns.Add("id", "id");
            dgw.Columns.Add("name", "name");
            dgw.Columns.Add("Subcat", "Subcat");
            dgw.Columns[0].Width = 0;
            dgw.Columns[1].Width = dgw.Columns[1].Width * 2;
            dgvP.Columns.Clear();
            dgvP.Columns.Add("id", "id");
            dgvP.Columns.Add("name", "name");
            dgvP.Columns.Add("value", "value");
            dgvP.Columns.Add("quality", "quality");
            dgvP.Columns[0].Width = 0;
            dgvP.Columns[1].Width = dgvP.Columns[1].Width * 2;
            dgvP.Columns[2].Width = dgvP.Columns[2].Width * 4;
            dgvR.Columns.Clear();
            dgvR.Columns.Add("id", "id");
            dgvR.Columns.Add("number", "number");
            dgvR.Columns.Add("name", "name");
            dgvR.Columns.Add("count", "count");
            dgvR.Columns.Add("stepby", "stepby");
            dgvR.Columns[0].Width = 0;
            dgvR.Columns[2].Width = dgvR.Columns[2].Width * 3;
            bSearch_Click(sender, e);
        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            bDel.Visible = false;
            dgw.Rows.Clear();
            var listAll = GatewaysFacade.ItemGateway.GetAllIncludeSubcat().ToList();
            var searhString = tbSearch.Text;
            if (!string.IsNullOrWhiteSpace(searhString))
                listAll = listAll.Where(f => f.Name.ToLower().Contains(searhString.ToLower())).ToList();
            foreach (var item in listAll.OrderBy(o => o.Name))
            {
                dgw.Rows.Add(item.Id, item.Name, item.Subcategory?.Name ?? item.SubcategoryId.ToString());
            }
            pictureBox1.Image = null;
        }

        private void dgw_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            currentId = dgw.Rows[e.RowIndex].Cells[0].Value.ToInt();
            lItemName.Text = dgw.Rows[e.RowIndex].Cells[1].Value.ToString();
            lCategory.Text = dgw.Rows[e.RowIndex].Cells[2].Value.ToString();
            LoadProperties(currentId);
            LoadRecipes(currentId);
            pictureBox1.Image = null;
            var item = GatewaysFacade.ItemGateway.GetById(currentId);
            if (item != null && !string.IsNullOrWhiteSpace(item.ImageName))
            {
                var path = $"E:\\MOE\\icon\\" + item.ImageName;
                if (File.Exists(path))
                {
                    pictureBox1.Image = Image.FromFile(path);
                }
            }
            bDel.Visible = true;
        }

        private void LoadProperties(int itemId)
        {
            dgvP.Rows.Clear();
            foreach (var item in GatewaysFacade.ItemPropertyGateway.GetAllByItemId(currentId))
            {
                dgvP.Rows.Add(item.Id, item.Name, item.Value, item.Quality);
            }
        }
        private void LoadRecipes(int itemId)
        {
            dgvR.Rows.Clear();
            foreach (var item in GatewaysFacade.ItemRecipeGateway.GetAllByItemId(currentId))
            {
                dgvR.Rows.Add(item.Id, item.Number, item.RecipeItemName, item.Count, item.IsStepByStep);
            }
        }

        private void lCategory_DoubleClick(object sender, EventArgs e)
        {
            var editForm = new EditItemSubcategory();
            editForm.itemId = currentId;
            editForm.ShowDialog();
            if (!string.IsNullOrWhiteSpace(editForm.name)) lCategory.Text = editForm.name;
        }

        private void bAddP_Click(object sender, EventArgs e)
        {
            var editForm = new EditItemProperty();
            editForm.itemId = currentId;
            editForm.ShowDialog();
            if (editForm.changed)
            {
                LoadProperties(currentId);
            }
        }

        private void bAddR_Click(object sender, EventArgs e)
        {
            var editForm = new EditItemRecipe();
            editForm.itemId = currentId;
            editForm.ShowDialog();
            if (editForm.changed)
            {
                LoadRecipes(currentId);
            }
        }

        private void dgvP_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var editForm = new EditItemProperty();
            editForm.id = dgvP.Rows[e.RowIndex].Cells[0].Value.ToInt();
            editForm.ShowDialog();
            if (editForm.changed)
            {
                LoadProperties(currentId);
            }
        }

        private void dgvR_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var editForm = new EditItemRecipe();
            editForm.id = dgvR.Rows[e.RowIndex].Cells[0].Value.ToInt();
            editForm.ShowDialog();
            if (editForm.changed)
            {
                LoadRecipes(currentId);
            }
        }

        private void bDel_Click_1(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Sure", "Delete???", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    GatewaysFacade.ItemGateway.Delete(currentId);
                    bSearch_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
