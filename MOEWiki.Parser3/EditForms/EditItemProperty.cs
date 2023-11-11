using MOEWiki.DBMySQL.Enums;
using MOEWiki.DBMySQL.Gateways;
using MOEWiki.DBMySQL.Models;
using MOEWiki.Parser3.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MOEWiki.Parser3.EditForms
{
    public partial class EditItemProperty : Form
    {
        public bool changed = false;
        public int id = 0;
        public int itemId = 0;
        private ItemProperty? itemP;

        public EditItemProperty()
        {
            InitializeComponent();
        }

        private void EditItemProperty_Load(object sender, EventArgs e)
        {
            cbQuality.DataSource = Enum.GetValues(typeof(QualityEnum));
            cbProperty.Items.Clear();
            if (id > 0)
            {
                itemP = GatewaysFacade.ItemPropertyGateway.GetById(id);
                var counter = 0;
                foreach (var item in GatewaysFacade.PropertyGateway.GetAll().OrderBy(o => o.Name))
                {
                    cbProperty.Items.Add(new ComboboxItem() { Text = $"{item.Name} [{item.PropertyType}] [{item.AccessLvl}] [{item.IsDependsByQuality}]", Value = item.Id });
                    if (itemP.PropertyId == item.Id)
                    {
                        cbProperty.SelectedIndex = counter;
                    }
                    counter++;
                }
                cbQuality.SelectedItem = itemP.Quality;
                tbValue.Text = itemP.Value;
                bSave.Visible = true;
                bDel.Visible = true;
                bHardDelete.Visible = true;
            }
            if (itemId > 0)
            {
                foreach (var item in GatewaysFacade.PropertyGateway.GetAll().ToList().OrderBy(o => o.Name))
                {
                    cbProperty.Items.Add(new ComboboxItem() { Text = $"{item.Name} [{item.PropertyType}] [{item.AccessLvl}] [{item.IsDependsByQuality}]", Value = item.Id });
                }
                cbProperty.SelectedIndex = 0;
                bAdd.Visible = true;
            }
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedQuality = itemP.Quality;
                Enum.TryParse<QualityEnum>(cbQuality.SelectedValue.ToString(), out selectedQuality);
                var selectedProperty = cbProperty.SelectedItem as ComboboxItem;
                if (selectedProperty != null && !string.IsNullOrWhiteSpace(tbValue.Text) && (itemP.Quality != selectedQuality || itemP.PropertyId != selectedProperty.Value || itemP.Value != tbValue.Text))
                {
                    if (itemP.PropertyId != selectedProperty.Value)
                    {
                        var property = GatewaysFacade.PropertyGateway.GetById(selectedProperty.Value);
                        itemP.PropertyId = property.Id;
                        itemP.Name = property.Name;
                        itemP.AccessLvl = property.AccessLvl;
                        itemP.SortId = property.SortId;
                        itemP.PropertyType = property.PropertyType;
                    }
                    itemP.Quality = selectedQuality;
                    itemP.Value = tbValue.Text;
                    GatewaysFacade.ItemPropertyGateway.Update(itemP);
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
                    GatewaysFacade.ItemPropertyGateway.Delete(id);
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
                    GatewaysFacade.ItemPropertyGateway.HardDelete(id);
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
                var selectedProperty = cbProperty.SelectedItem as ComboboxItem;
                if (selectedProperty != null && !string.IsNullOrWhiteSpace(tbValue.Text))
                {
                    var property = GatewaysFacade.PropertyGateway.GetById(selectedProperty.Value);
                    itemP = new ItemProperty(property);
                    var selectedQuality = QualityEnum.Low;
                    Enum.TryParse<QualityEnum>(cbQuality.SelectedValue.ToString(), out selectedQuality);
                    itemP.ItemId = itemId;
                    itemP.Quality = selectedQuality;
                    itemP.Value = tbValue.Text;
                    GatewaysFacade.ItemPropertyGateway.Add(itemP);
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
