using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MOEWiki.DBMySQL.Enums;
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
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MOEWiki.Parser3.EditForms
{
    public partial class EditMapMarker : Form
    {
        public int currentId;
        public EditMapMarker()
        {
            InitializeComponent();
        }

        private void EditMapMarker_Load(object sender, EventArgs e)
        {
            cbeCoalition.DataSource = Enum.GetValues(typeof(CoalitionEnum));
            cbeMarkerType.DataSource = Enum.GetValues(typeof(MarkerTypeEnum));
            cbImageName.Items.Clear();
            foreach (var file in new DirectoryInfo("C:\\Users\\L\\Downloads\\Myth of Empires Map_files").GetFiles())
            {
                cbImageName.Items.Add(file.Name);
            }
            dgv.Columns.Clear();
            dgv.Columns.Add("id", "id");
            dgv.Columns.Add("name", "name");
            dgv.Columns.Add("note", "note");
            dgv.Columns[0].Width = 0;
            bSearch_Click(sender, e);
        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
            var listAll = GatewaysFacade.MapMarkerGateway.GetAll().ToList();
            var searhString = tbSearch.Text;
            if (nId.Value != 0)
                listAll = listAll.Where(f => f.Id == nId.Value).ToList();
            else
            if (!string.IsNullOrWhiteSpace(searhString))
                listAll = listAll.Where(f => f.Name.Contains(searhString)).ToList();
            foreach (var item in listAll)
            {
                dgv.Rows.Add(item.Id, item.Name, item.Note);
            }
        }

        private void FillNote()
        {
            if (string.IsNullOrWhiteSpace(tbNote.Text))
            {
                var w = 2560;
                var h = 2562;
                var minX = -4000;
                var maxX = 3997;
                var minY = -3998;
                var maxY = 4000;
                tbNote.Text = $"[{Math.Round((maxX - minX) / w * nudX.Value + minX, 0)},{Math.Round((maxY - minY) / h * nudY.Value + minY, 0)}]";
            }
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            FillNote();
            try
            {
                var entity = GatewaysFacade.MapMarkerGateway.Add(GetFullEntity(new MapMarker()));
                currentId = entity.Id;
                MessageBox.Show("Done");
                tbNote.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bEdit_Click(object sender, EventArgs e)
        {
            FillNote();
            try
            {
                GatewaysFacade.MapMarkerGateway.Update(GetFullEntity(new MapMarker() { Id = currentId }));
                MessageBox.Show("Done");
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
                    GatewaysFacade.MapMarkerGateway.Delete(this.currentId);
                    bSearch_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            currentId = dgv.Rows[e.RowIndex].Cells[0].Value.ToInt();
            var entity = GatewaysFacade.MapMarkerGateway.GetById(currentId);
            this.tbNote.Text = "";
            FillTextBoxes(entity);
            this.cbeCoalition.SelectedItem = entity.Coalition;
            this.cbeMarkerType.SelectedItem = entity.MarkerType;
            this.cbImageName.Text = entity.ImageName;
            this.cbName.Text = entity.Name;
            this.nudX.Value = entity.X;
            this.nudY.Value = entity.Y;
            this.nudMapId.Value = entity.MapId;
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

        private MapMarker GetFullEntity(MapMarker entity)
        {
            entity = GetEntity(entity);
            var coalition = entity.Coalition;
            Enum.TryParse<CoalitionEnum>(cbeCoalition.SelectedValue.ToString(), out coalition);
            entity.Coalition = coalition;
            var markerType = entity.MarkerType;
            Enum.TryParse<MarkerTypeEnum>(cbeMarkerType.SelectedValue.ToString(), out markerType);
            entity.MarkerType = markerType;
            return entity;
        }

        private void cbImageName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbName.Text = cbImageName.Text.Replace(".png", "").Replace("_", " ");
        }

        private void bClearNote_Click(object sender, EventArgs e)
        {
            tbNote.Text = "";
        }

        private TEntity GetEntity<TEntity>(TEntity entity) where TEntity : class, IBase
        {
            foreach (var prop in typeof(TEntity).GetProperties())
            {
                var tb = this.Controls.OfType<TextBox>().FirstOrDefault(f => f.Name == $"tb{prop.Name}");
                if (tb != null)
                {
                    entity.SetValueByPropertyName(prop.Name, tb.Text);
                    continue;
                }
                var cb = this.Controls.OfType<ComboBox>().FirstOrDefault(f => f.Name == $"cb{prop.Name}");
                if (cb != null)
                {
                    entity.SetValueByPropertyName(prop.Name, cb.Text);
                    continue;
                }
                var nud = this.Controls.OfType<NumericUpDown>().FirstOrDefault(f => f.Name == $"nud{prop.Name}");
                if (nud != null)
                {
                    entity.SetValueByPropertyName(prop.Name, nud.Value);
                }
            }
            return entity;
        }
    }
}
