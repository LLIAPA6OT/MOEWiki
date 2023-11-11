using MOEWiki.DBMySQL.Gateways;
using MOEWiki.DBMySQL.Models;
using MOEWiki.DBMySQL.Models.Interfaces;
using MOEWiki.Parser3.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MOEWiki.Parser3.EditForms
{
    public partial class EditItemSubcategory : Form
    {
        public string name = "";
        public int itemId = 0;
        private Item? item;
        public EditItemSubcategory()
        {
            InitializeComponent();
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
        private void EditItemSubcategory_Load(object sender, EventArgs e)
        {
            item = GatewaysFacade.ItemGateway.GetById(itemId);
            FillComboBox(cbSubcategoryId, GatewaysFacade.SubcategoryGateway.GetAll().ToList(), item.SubcategoryId);
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            var selItem = cbSubcategoryId.SelectedItem as ComboboxItem;
            if (selItem != null)
            {
                if (item.SubcategoryId != selItem.Value)
                {
                    item.SubcategoryId = selItem.Value;
                    GatewaysFacade.ItemGateway.Update(item);
                    name = selItem.Text;
                    this.Close();
                }
            }
        }
    }
}
