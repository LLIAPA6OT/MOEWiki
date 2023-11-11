namespace MOEWiki.Parser3
{
    partial class EditProperty
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
            lPropertyType = new Label();
            cbPropertyType = new ComboBox();
            dgw = new DataGridView();
            bDel = new Button();
            bEdit = new Button();
            bAdd = new Button();
            label1 = new Label();
            tbName = new TextBox();
            bSearch = new Button();
            tbSearch = new TextBox();
            label2 = new Label();
            bHardDelete = new Button();
            nudSortId = new NumericUpDown();
            chbIsDependsByQuality = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)dgw).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSortId).BeginInit();
            SuspendLayout();
            // 
            // lPropertyType
            // 
            lPropertyType.AutoSize = true;
            lPropertyType.Location = new Point(572, 61);
            lPropertyType.Name = "lPropertyType";
            lPropertyType.Size = new Size(76, 15);
            lPropertyType.TabIndex = 46;
            lPropertyType.Text = "PropertyType";
            // 
            // cbPropertyType
            // 
            cbPropertyType.FormattingEnabled = true;
            cbPropertyType.Items.AddRange(new object[] { "Предмет", "Рецепт", "Предмет + рецепт", "Исследование", "Перк" });
            cbPropertyType.Location = new Point(652, 58);
            cbPropertyType.Name = "cbPropertyType";
            cbPropertyType.Size = new Size(228, 23);
            cbPropertyType.TabIndex = 45;
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
            dgw.Size = new Size(532, 828);
            dgw.TabIndex = 44;
            dgw.RowEnter += dgw_RowEnter;
            // 
            // bDel
            // 
            bDel.Location = new Point(914, 86);
            bDel.Name = "bDel";
            bDel.Size = new Size(85, 23);
            bDel.TabIndex = 43;
            bDel.Text = "Delete";
            bDel.UseVisualStyleBackColor = true;
            bDel.Click += bDel_Click;
            // 
            // bEdit
            // 
            bEdit.Location = new Point(914, 57);
            bEdit.Name = "bEdit";
            bEdit.Size = new Size(85, 23);
            bEdit.TabIndex = 42;
            bEdit.Text = "Edit";
            bEdit.UseVisualStyleBackColor = true;
            bEdit.Click += bEdit_Click;
            // 
            // bAdd
            // 
            bAdd.Location = new Point(914, 25);
            bAdd.Name = "bAdd";
            bAdd.Size = new Size(85, 23);
            bAdd.TabIndex = 41;
            bAdd.Text = "Add new";
            bAdd.UseVisualStyleBackColor = true;
            bAdd.Click += bAdd_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(572, 28);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 39;
            label1.Text = "Name";
            // 
            // tbName
            // 
            tbName.Location = new Point(652, 25);
            tbName.Name = "tbName";
            tbName.Size = new Size(228, 23);
            tbName.TabIndex = 37;
            // 
            // bSearch
            // 
            bSearch.Location = new Point(246, 7);
            bSearch.Name = "bSearch";
            bSearch.Size = new Size(75, 23);
            bSearch.TabIndex = 36;
            bSearch.Text = "Search";
            bSearch.UseVisualStyleBackColor = true;
            bSearch.Click += bSearch_Click;
            // 
            // tbSearch
            // 
            tbSearch.Location = new Point(12, 7);
            tbSearch.Name = "tbSearch";
            tbSearch.Size = new Size(228, 23);
            tbSearch.TabIndex = 35;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(572, 90);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 47;
            label2.Text = "SortId";
            // 
            // bHardDelete
            // 
            bHardDelete.Location = new Point(914, 155);
            bHardDelete.Name = "bHardDelete";
            bHardDelete.Size = new Size(85, 23);
            bHardDelete.TabIndex = 48;
            bHardDelete.Text = "HardDelete";
            bHardDelete.UseVisualStyleBackColor = true;
            bHardDelete.Click += bHardDelete_Click;
            // 
            // nudSortId
            // 
            nudSortId.Location = new Point(652, 90);
            nudSortId.Name = "nudSortId";
            nudSortId.Size = new Size(120, 23);
            nudSortId.TabIndex = 49;
            // 
            // chbIsDependsByQuality
            // 
            chbIsDependsByQuality.AutoSize = true;
            chbIsDependsByQuality.CheckAlign = ContentAlignment.MiddleRight;
            chbIsDependsByQuality.Location = new Point(572, 129);
            chbIsDependsByQuality.Name = "chbIsDependsByQuality";
            chbIsDependsByQuality.Size = new Size(131, 19);
            chbIsDependsByQuality.TabIndex = 51;
            chbIsDependsByQuality.Text = "IsDependsByQuality";
            chbIsDependsByQuality.UseVisualStyleBackColor = true;
            // 
            // EditProperty
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1013, 880);
            Controls.Add(chbIsDependsByQuality);
            Controls.Add(nudSortId);
            Controls.Add(bHardDelete);
            Controls.Add(label2);
            Controls.Add(lPropertyType);
            Controls.Add(cbPropertyType);
            Controls.Add(dgw);
            Controls.Add(bDel);
            Controls.Add(bEdit);
            Controls.Add(bAdd);
            Controls.Add(label1);
            Controls.Add(tbName);
            Controls.Add(bSearch);
            Controls.Add(tbSearch);
            Name = "EditProperty";
            Text = "EditProperty";
            Load += Edit_Load;
            ((System.ComponentModel.ISupportInitialize)dgw).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSortId).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lPropertyType;
        private ComboBox cbPropertyType;
        private DataGridView dgw;
        private Button bDel;
        private Button bEdit;
        private Button bAdd;
        private Label label1;
        private TextBox tbName;
        private Button bSearch;
        private TextBox tbSearch;
        private Label label2;
        private Button bHardDelete;
        private NumericUpDown nudSortId;
        private CheckBox chbIsDependsByQuality;
    }
}