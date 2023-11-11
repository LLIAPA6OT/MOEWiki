using MOEWiki.DBMySQL.Gateways;
using MOEWiki.DBMySQL.Helpers;
using MOEWiki.DBMySQL.Models;
using MOEWiki.DBMySQL.Models.Interfaces;
using MOEWiki.Parser3.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MOEWiki.Parser3
{
    public partial class EditSubcategory : Form
    {
        public int currentId;
        public EditSubcategory()
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
                    GatewaysFacade.SubcategoryGateway.Delete(this.currentId);
                    bSearch_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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
                var cb = this.Controls.OfType<ComboBox>().FirstOrDefault(f => f.Name == $"cb{prop.Name}");
                if (cb != null)
                {
                    var selItem = cb.SelectedItem as ComboboxItem;
                    if (selItem != null)
                    {
                        entity.SetValueByPropertyName(prop.Name, selItem.Value);
                    }
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
                var entity = GatewaysFacade.SubcategoryGateway.Add(GetEntity(new Subcategory()));
                currentId = entity.Id;
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
                GatewaysFacade.SubcategoryGateway.Update(GetEntity(new Subcategory() { Id = currentId }));
                MessageBox.Show("Done");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Edit_Load(object sender, EventArgs e)
        {
            FillComboBox(cbCategoryId, GatewaysFacade.CategoryGateway.GetAll().ToList());
            dgw.Columns.Clear();
            dgw.Columns.Add("id", "id");
            dgw.Columns.Add("name", "name");
            dgw.Columns.Add("category", "category");
            dgw.Columns[0].Width = 0;
            bSearch_Click(sender, e);
        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            dgw.Rows.Clear();
            var listAll = GatewaysFacade.SubcategoryGateway.GetAllIncludeCategory().ToList();
            var searhString = tbSearch.Text;
            if (!string.IsNullOrWhiteSpace(searhString))
                listAll = listAll.Where(f => f.Name.Contains(searhString) || f.Synonyms.Contains(searhString)).ToList();
            foreach (var item in listAll)
            {
                dgw.Rows.Add(item.Id, item.Name, item.Category?.Name ?? item.CategoryId.ToString());
            }
        }

        private void dgw_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            currentId = dgw.Rows[e.RowIndex].Cells[0].Value.ToInt();
            var entity = GatewaysFacade.SubcategoryGateway.GetById(currentId);
            FillTextBoxes(entity);
            if (cbCategoryId.Items.Count == 0 || !ChangeComboBoxSelectedItem(cbCategoryId, entity.CategoryId))
            {
                FillComboBox(cbCategoryId, GatewaysFacade.CategoryGateway.GetAll().ToList(), entity.CategoryId);
            }
        }

        private void FillComboBox<TEntity>(ComboBox comboBox, List<TEntity> items, int selectedId = 0) where TEntity : class, IBase, IName
        {
            var counter = 0;
            comboBox.Items.Clear();
            foreach (var item in items)
            {
                comboBox.Items.Add(new ComboboxItem() { Text = item.Name, Value = item.Id });
                if (selectedId == item.Id)
                {
                    comboBox.SelectedIndex = counter;
                }
                counter++;
            }
            if (selectedId == 0 && comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }

        private bool ChangeComboBoxSelectedItem(ComboBox comboBox, int selectedId)
        {
            var result = false;
            foreach (ComboboxItem item in comboBox.Items)
            {
                if (item.Value == selectedId)
                {
                    comboBox.SelectedItem = item;
                    result = true;
                    break;
                }
            }
            return result;
        }

        private void dgw_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Close();
        }
    }
}
