using MOEWiki.DB.Gateways;
using MOEWiki.DB.Models;
using MOEWiki.DB.Models.Interfaces;
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

namespace MOEWiki.Parser2
{
    public partial class Edit : Form
    {
        private int currentId;
        public Edit()
        {
            InitializeComponent();
        }

        private void bDel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Sure", "Delete???", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                GatewaysFacade.CategoryGateway.Delete(this.currentId);
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
            GatewaysFacade.CategoryGateway.Add(GetEntity(new Category()));
        }

        private void bEdit_Click(object sender, EventArgs e)
        {
            GatewaysFacade.CategoryGateway.Add(GetEntity(new Category() { Id = currentId }));
        }

        private void lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentId = lb.SelectedIndex;
            FillTextBoxes(GatewaysFacade.CategoryGateway.GetById(currentId));
        }

        private void Edit_Load(object sender, EventArgs e)
        {
            bSearch_Click(sender, e);
        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            lb.Items.Clear();
            var listAll = GatewaysFacade.CategoryGateway.GetAll();
            var searhString = tbSearch.Text;
            if (!string.IsNullOrWhiteSpace(searhString))
                listAll = listAll.Where(f => f.Name.Contains(searhString) || f.Synonyms.Contains(searhString));
            foreach (var item in listAll)
            {
                lb.Items.Add(item);
            }
        }
    }
}
