namespace MOEWiki.Parser3
{
    partial class EditItem
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
            dgvP = new DataGridView();
            dgvR = new DataGridView();
            label2 = new Label();
            label1 = new Label();
            lItemName = new Label();
            lCategory = new Label();
            bAddP = new Button();
            bAddR = new Button();
            pictureBox1 = new PictureBox();
            bDel = new Button();
            ((System.ComponentModel.ISupportInitialize)dgw).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvP).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvR).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // dgw
            // 
            dgw.AllowUserToAddRows = false;
            dgw.AllowUserToDeleteRows = false;
            dgw.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgw.Location = new Point(12, 36);
            dgw.Name = "dgw";
            dgw.ReadOnly = true;
            dgw.RowTemplate.Height = 25;
            dgw.Size = new Size(432, 949);
            dgw.TabIndex = 47;
            dgw.RowEnter += dgw_RowEnter;
            // 
            // bSearch
            // 
            bSearch.Location = new Point(369, 6);
            bSearch.Name = "bSearch";
            bSearch.Size = new Size(75, 23);
            bSearch.TabIndex = 46;
            bSearch.Text = "Search";
            bSearch.UseVisualStyleBackColor = true;
            bSearch.Click += bSearch_Click;
            // 
            // tbSearch
            // 
            tbSearch.Location = new Point(12, 7);
            tbSearch.Name = "tbSearch";
            tbSearch.Size = new Size(351, 23);
            tbSearch.TabIndex = 45;
            // 
            // dgvP
            // 
            dgvP.AllowUserToAddRows = false;
            dgvP.AllowUserToDeleteRows = false;
            dgvP.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvP.Location = new Point(450, 67);
            dgvP.Name = "dgvP";
            dgvP.ReadOnly = true;
            dgvP.RowTemplate.Height = 25;
            dgvP.Size = new Size(720, 918);
            dgvP.TabIndex = 48;
            dgvP.CellDoubleClick += dgvP_CellDoubleClick;
            // 
            // dgvR
            // 
            dgvR.AllowUserToAddRows = false;
            dgvR.AllowUserToDeleteRows = false;
            dgvR.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvR.Location = new Point(1176, 67);
            dgvR.Name = "dgvR";
            dgvR.ReadOnly = true;
            dgvR.RowTemplate.Height = 25;
            dgvR.Size = new Size(741, 791);
            dgvR.TabIndex = 49;
            dgvR.CellDoubleClick += dgvR_CellDoubleClick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(450, 39);
            label2.Name = "label2";
            label2.Size = new Size(135, 25);
            label2.TabIndex = 50;
            label2.Text = "ItemProperties";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(1176, 39);
            label1.Name = "label1";
            label1.Size = new Size(112, 25);
            label1.TabIndex = 51;
            label1.Text = "ItemRecipes";
            // 
            // lItemName
            // 
            lItemName.AutoSize = true;
            lItemName.Font = new Font("Segoe UI Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lItemName.Location = new Point(760, 2);
            lItemName.Name = "lItemName";
            lItemName.Size = new Size(0, 25);
            lItemName.TabIndex = 52;
            // 
            // lCategory
            // 
            lCategory.AutoSize = true;
            lCategory.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            lCategory.Location = new Point(1511, 9);
            lCategory.Name = "lCategory";
            lCategory.Size = new Size(0, 25);
            lCategory.TabIndex = 53;
            lCategory.DoubleClick += lCategory_DoubleClick;
            // 
            // bAddP
            // 
            bAddP.Font = new Font("Segoe UI Black", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            bAddP.Location = new Point(1129, 36);
            bAddP.Name = "bAddP";
            bAddP.Size = new Size(41, 30);
            bAddP.TabIndex = 54;
            bAddP.Text = "+";
            bAddP.UseVisualStyleBackColor = true;
            bAddP.Click += bAddP_Click;
            // 
            // bAddR
            // 
            bAddR.Font = new Font("Segoe UI Black", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            bAddR.Location = new Point(1876, 34);
            bAddR.Name = "bAddR";
            bAddR.Size = new Size(41, 30);
            bAddR.TabIndex = 55;
            bAddR.Text = "+";
            bAddR.UseVisualStyleBackColor = true;
            bAddR.Click += bAddR_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.InitialImage = null;
            pictureBox1.Location = new Point(1796, 864);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(121, 121);
            pictureBox1.TabIndex = 56;
            pictureBox1.TabStop = false;
            // 
            // bDel
            // 
            bDel.Font = new Font("Segoe UI Black", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            bDel.Location = new Point(1619, 955);
            bDel.Name = "bDel";
            bDel.Size = new Size(82, 30);
            bDel.TabIndex = 57;
            bDel.Text = "Delete";
            bDel.UseVisualStyleBackColor = true;
            bDel.Click += bDel_Click_1;
            // 
            // EditItem
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1929, 997);
            Controls.Add(bDel);
            Controls.Add(pictureBox1);
            Controls.Add(bAddR);
            Controls.Add(bAddP);
            Controls.Add(lCategory);
            Controls.Add(lItemName);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(dgvR);
            Controls.Add(dgvP);
            Controls.Add(dgw);
            Controls.Add(bSearch);
            Controls.Add(tbSearch);
            Name = "EditItem";
            Text = "EditItem";
            Load += Edit_Load;
            ((System.ComponentModel.ISupportInitialize)dgw).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvP).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvR).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgw;
        private Button bSearch;
        private TextBox tbSearch;
        private DataGridView dgvP;
        private DataGridView dgvR;
        private Label label2;
        private Label label1;
        private Label lItemName;
        private Label lCategory;
        private Button bAddP;
        private Button bAddR;
        public PictureBox pictureBox1;
        private Button bDel;
    }
}