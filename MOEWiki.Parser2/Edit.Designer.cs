namespace MOEWiki.Parser2
{
    partial class Edit
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
            tbSearch = new TextBox();
            bSearch = new Button();
            lb = new ListBox();
            tbName = new TextBox();
            tbSynonyms = new TextBox();
            label1 = new Label();
            label2 = new Label();
            bAdd = new Button();
            bEdit = new Button();
            bDel = new Button();
            SuspendLayout();
            // 
            // tbSearch
            // 
            tbSearch.Location = new Point(10, 8);
            tbSearch.Name = "tbSearch";
            tbSearch.Size = new Size(228, 23);
            tbSearch.TabIndex = 0;
            // 
            // bSearch
            // 
            bSearch.Location = new Point(244, 8);
            bSearch.Name = "bSearch";
            bSearch.Size = new Size(75, 23);
            bSearch.TabIndex = 1;
            bSearch.Text = "Search";
            bSearch.UseVisualStyleBackColor = true;
            bSearch.Click += bSearch_Click;
            // 
            // lb
            // 
            lb.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lb.FormattingEnabled = true;
            lb.ItemHeight = 15;
            lb.Location = new Point(10, 37);
            lb.Name = "lb";
            lb.Size = new Size(309, 829);
            lb.TabIndex = 2;
            lb.SelectedIndexChanged += lb_SelectedIndexChanged;
            // 
            // tbName
            // 
            tbName.Location = new Point(418, 9);
            tbName.Name = "tbName";
            tbName.Size = new Size(228, 23);
            tbName.TabIndex = 3;
            // 
            // tbSynonyms
            // 
            tbSynonyms.Location = new Point(418, 38);
            tbSynonyms.Multiline = true;
            tbSynonyms.Name = "tbSynonyms";
            tbSynonyms.Size = new Size(228, 109);
            tbSynonyms.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(338, 12);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 5;
            label1.Text = "Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(338, 41);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 6;
            label2.Text = "Synonyms";
            // 
            // bAdd
            // 
            bAdd.Location = new Point(680, 9);
            bAdd.Name = "bAdd";
            bAdd.Size = new Size(85, 23);
            bAdd.TabIndex = 7;
            bAdd.Text = "Add new";
            bAdd.UseVisualStyleBackColor = true;
            bAdd.Click += bAdd_Click;
            // 
            // bEdit
            // 
            bEdit.Location = new Point(680, 41);
            bEdit.Name = "bEdit";
            bEdit.Size = new Size(85, 23);
            bEdit.TabIndex = 8;
            bEdit.Text = "Edit";
            bEdit.UseVisualStyleBackColor = true;
            bEdit.Click += bEdit_Click;
            // 
            // bDel
            // 
            bDel.Location = new Point(680, 70);
            bDel.Name = "bDel";
            bDel.Size = new Size(85, 23);
            bDel.TabIndex = 9;
            bDel.Text = "Delete";
            bDel.UseVisualStyleBackColor = true;
            bDel.Click += bDel_Click;
            // 
            // Edit
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1211, 877);
            Controls.Add(bDel);
            Controls.Add(bEdit);
            Controls.Add(bAdd);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(tbSynonyms);
            Controls.Add(tbName);
            Controls.Add(lb);
            Controls.Add(bSearch);
            Controls.Add(tbSearch);
            Name = "Edit";
            Text = "Edit";
            Load += Edit_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbSearch;
        private Button bSearch;
        private ListBox lb;
        private TextBox tbName;
        private TextBox tbSynonyms;
        private Label label1;
        private Label label2;
        private Button bAdd;
        private Button bEdit;
        private Button bDel;
    }
}