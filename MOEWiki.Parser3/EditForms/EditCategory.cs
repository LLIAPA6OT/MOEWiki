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
    public partial class EditCategory : Form
    {
        private int currentId;
        public EditCategory()
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
            dgw.Columns[0].Width = 0;
            bSearch_Click(sender, e);
        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            dgw.Rows.Clear();
            var listAll = GatewaysFacade.CategoryGateway.GetAll().ToList();
            var searhString = tbSearch.Text;
            if (!string.IsNullOrWhiteSpace(searhString))
                listAll = listAll.Where(f => f.Name.Contains(searhString) || f.Synonyms.Contains(searhString)).ToList();
            foreach (var item in listAll)
            {
                dgw.Rows.Add(item.Id, item.Name);
            }
        }

        private void dgw_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            currentId = dgw.Rows[e.RowIndex].Cells[0].Value.ToInt();
            FillTextBoxes(GatewaysFacade.CategoryGateway.GetById(currentId));
        }
    }
}
