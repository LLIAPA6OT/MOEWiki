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
using System.Net.Sockets;
using MOEWiki.DBMySQL.Enums;

namespace MOEWiki.Parser3
{
    public partial class EditProperty : Form
    {
        public EditProperty()
        {
            InitializeComponent();
        }

        private void bHardDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Sure", "Hard delete???", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    GatewaysFacade.PropertyGateway.HardDelete(this.currentId);
                    bSearch_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private int currentId;
        private void bDel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Sure", "Delete???", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    GatewaysFacade.PropertyGateway.Delete(this.currentId);
                    bSearch_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private Property GetEntity(Property entity)
        {
            entity.Name = tbName.Text;
            entity.SortId = (int)nudSortId.Value;
            PropertyTypeEnum v = PropertyTypeEnum.Text;
            Enum.TryParse<PropertyTypeEnum>(cbPropertyType.SelectedItem.ToString(), out v);
            entity.PropertyType = v;
            entity.IsDependsByQuality = chbIsDependsByQuality.Checked;
            return entity;
        }
        private void bAdd_Click(object sender, EventArgs e)
        {
            try
            {
                GatewaysFacade.PropertyGateway.Add(GetEntity(new Property()));
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
                GatewaysFacade.PropertyGateway.Update(GetEntity(new Property() { Id = currentId }));
                MessageBox.Show("Done");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Edit_Load(object sender, EventArgs e)
        {
            cbPropertyType.DataSource = Enum.GetValues(typeof(PropertyTypeEnum));
            dgw.Columns.Clear();
            dgw.Columns.Add("id", "id");
            dgw.Columns.Add("name", "name");
            dgw.Columns.Add("type", "type");
            dgw.Columns.Add("sortid", "sortid");
            dgw.Columns.Add("accesslvl", "accesslvl");
            dgw.Columns.Add("qual", "qual");
            dgw.Columns[0].Width = 0;
            bSearch_Click(sender, e);
        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            dgw.Rows.Clear();
            var listAll = GatewaysFacade.PropertyGateway.GetAll();
            var searhString = tbSearch.Text;
            if (!string.IsNullOrWhiteSpace(searhString))
                listAll = listAll.Where(f => f.Name.Contains(searhString));
            foreach (var item in listAll)
            {
                dgw.Rows.Add(item.Id, item.Name, item.PropertyType, item.SortId, item.AccessLvl, item.IsDependsByQuality);
            }
        }

        private void dgw_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            currentId = int.Parse(dgw.Rows[e.RowIndex].Cells[0].Value.ToString());
            var prop = GatewaysFacade.PropertyGateway.GetById(currentId);
            tbName.Text = prop.Name;
            nudSortId.Value = prop.SortId;
            this.cbPropertyType.SelectedItem = prop.PropertyType;
            this.chbIsDependsByQuality.Checked = prop.IsDependsByQuality;
        }
    }
}
