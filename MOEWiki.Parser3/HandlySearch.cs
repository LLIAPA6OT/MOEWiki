using MOEWiki.DBMySQL.Gateways;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MOEWiki.Parser3
{
    public partial class HandlySearch : Form
    {
        public HandlySearch()
        {
            InitializeComponent();
        }

        public int selectedId = 0;

        private void bSearch_Click(object sender, EventArgs e)
        {
            dgw.Rows.Clear();
            var searhString = tbSearch.Text;
            if (!string.IsNullOrWhiteSpace(searhString))
            {
                foreach (var item in GatewaysFacade.ItemGateway.SearchByName(searhString))
                {
                    dgw.Rows.Add(item.Id, item.Name);
                }
            }
        }

        private void dgw_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            selectedId = int.Parse(dgw.Rows[e.RowIndex].Cells[0].Value.ToString());
            //preview
        }

        private void dgw_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (selectedId == 0)
            {
                selectedId = int.Parse(dgw.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
            this.Close();
        }

        private void HandlySearch_Load(object sender, EventArgs e)
        {
            dgw.Columns.Clear();
            dgw.Columns.Add("id", "id");
            dgw.Columns.Add("name", "name");
            dgw.Columns[0].Width = 0;
        }
    }
}
