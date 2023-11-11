namespace MOEWiki.Parser3
{
    partial class HandlySearch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgw = new DataGridView();
            bSearch = new Button();
            tbSearch = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgw).BeginInit();
            SuspendLayout();
            // 
            // dgw
            // 
            dgw.AllowUserToAddRows = false;
            dgw.AllowUserToDeleteRows = false;
            dgw.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgw.Location = new Point(12, 41);
            dgw.Name = "dgw";
            dgw.ReadOnly = true;
            dgw.RowTemplate.Height = 25;
            dgw.Size = new Size(292, 729);
            dgw.TabIndex = 13;
            dgw.CellDoubleClick += dgw_CellDoubleClick;
            dgw.RowEnter += dgw_RowEnter;
            // 
            // bSearch
            // 
            bSearch.Location = new Point(246, 12);
            bSearch.Name = "bSearch";
            bSearch.Size = new Size(58, 23);
            bSearch.TabIndex = 12;
            bSearch.Text = "Search";
            bSearch.UseVisualStyleBackColor = true;
            bSearch.Click += bSearch_Click;
            // 
            // tbSearch
            // 
            tbSearch.Location = new Point(12, 12);
            tbSearch.Name = "tbSearch";
            tbSearch.Size = new Size(228, 23);
            tbSearch.TabIndex = 11;
            // 
            // HandlySearch
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(801, 782);
            Controls.Add(dgw);
            Controls.Add(bSearch);
            Controls.Add(tbSearch);
            Name = "HandlySearch";
            Text = "HandlySearch";
            Load += HandlySearch_Load;
            ((System.ComponentModel.ISupportInitialize)dgw).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgw;
        private Button bSearch;
        private TextBox tbSearch;
    }
}