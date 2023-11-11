namespace MOEWiki.Parser3.EditForms
{
    partial class EditMapMarker
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
            label3 = new Label();
            cbeMarkerType = new ComboBox();
            dgv = new DataGridView();
            bDel = new Button();
            bEdit = new Button();
            bAdd = new Button();
            label2 = new Label();
            label1 = new Label();
            tbDescription = new TextBox();
            bSearch = new Button();
            tbSearch = new TextBox();
            llll = new Label();
            tbNote = new TextBox();
            label4 = new Label();
            nudX = new NumericUpDown();
            label5 = new Label();
            nudY = new NumericUpDown();
            label6 = new Label();
            cbImageName = new ComboBox();
            label8 = new Label();
            cbeCoalition = new ComboBox();
            label7 = new Label();
            tbEnemies = new TextBox();
            label9 = new Label();
            tbLink = new TextBox();
            label10 = new Label();
            tbLinkName = new TextBox();
            cbName = new ComboBox();
            bClearNote = new Button();
            nId = new NumericUpDown();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            tbTitle = new TextBox();
            label14 = new Label();
            nudMapId = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nId).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMapId).BeginInit();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(513, 212);
            label3.Name = "label3";
            label3.Size = new Size(92, 20);
            label3.TabIndex = 46;
            label3.Text = "MarkerType";
            // 
            // cbeMarkerType
            // 
            cbeMarkerType.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cbeMarkerType.FormattingEnabled = true;
            cbeMarkerType.Items.AddRange(new object[] { "Предмет", "Рецепт", "Предмет + рецепт", "Исследование", "Перк" });
            cbeMarkerType.Location = new Point(679, 209);
            cbeMarkerType.Name = "cbeMarkerType";
            cbeMarkerType.Size = new Size(345, 28);
            cbeMarkerType.TabIndex = 45;
            // 
            // dgv
            // 
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Location = new Point(12, 37);
            dgv.Name = "dgv";
            dgv.ReadOnly = true;
            dgv.RowTemplate.Height = 25;
            dgv.Size = new Size(483, 828);
            dgv.TabIndex = 44;
            dgv.RowEnter += dgv_RowEnter;
            // 
            // bDel
            // 
            bDel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            bDel.Location = new Point(1063, 86);
            bDel.Name = "bDel";
            bDel.Size = new Size(85, 33);
            bDel.TabIndex = 43;
            bDel.Text = "Delete";
            bDel.UseVisualStyleBackColor = true;
            bDel.Click += bDel_Click;
            // 
            // bEdit
            // 
            bEdit.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            bEdit.Location = new Point(1063, 47);
            bEdit.Name = "bEdit";
            bEdit.Size = new Size(85, 33);
            bEdit.TabIndex = 42;
            bEdit.Text = "Edit";
            bEdit.UseVisualStyleBackColor = true;
            bEdit.Click += bEdit_Click;
            // 
            // bAdd
            // 
            bAdd.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            bAdd.Location = new Point(1063, 8);
            bAdd.Name = "bAdd";
            bAdd.Size = new Size(85, 33);
            bAdd.TabIndex = 41;
            bAdd.Text = "Add new";
            bAdd.UseVisualStyleBackColor = true;
            bAdd.Click += bAdd_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(513, 395);
            label2.Name = "label2";
            label2.Size = new Size(89, 20);
            label2.TabIndex = 40;
            label2.Text = "Description";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(513, 11);
            label1.Name = "label1";
            label1.Size = new Size(51, 20);
            label1.TabIndex = 39;
            label1.Text = "Name";
            // 
            // tbDescription
            // 
            tbDescription.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbDescription.Location = new Point(679, 392);
            tbDescription.Multiline = true;
            tbDescription.Name = "tbDescription";
            tbDescription.ScrollBars = ScrollBars.Vertical;
            tbDescription.Size = new Size(345, 171);
            tbDescription.TabIndex = 38;
            // 
            // bSearch
            // 
            bSearch.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            bSearch.Location = new Point(418, 5);
            bSearch.Name = "bSearch";
            bSearch.Size = new Size(75, 26);
            bSearch.TabIndex = 36;
            bSearch.Text = "Search";
            bSearch.UseVisualStyleBackColor = true;
            bSearch.Click += bSearch_Click;
            // 
            // tbSearch
            // 
            tbSearch.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbSearch.Location = new Point(184, 5);
            tbSearch.Name = "tbSearch";
            tbSearch.Size = new Size(228, 26);
            tbSearch.TabIndex = 35;
            // 
            // llll
            // 
            llll.AutoSize = true;
            llll.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            llll.Location = new Point(513, 572);
            llll.Name = "llll";
            llll.Size = new Size(43, 20);
            llll.TabIndex = 48;
            llll.Text = "Note";
            // 
            // tbNote
            // 
            tbNote.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbNote.Location = new Point(679, 569);
            tbNote.Multiline = true;
            tbNote.Name = "tbNote";
            tbNote.ScrollBars = ScrollBars.Vertical;
            tbNote.Size = new Size(345, 109);
            tbNote.TabIndex = 47;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(513, 104);
            label4.Name = "label4";
            label4.Size = new Size(20, 20);
            label4.TabIndex = 72;
            label4.Text = "X";
            // 
            // nudX
            // 
            nudX.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            nudX.Location = new Point(679, 104);
            nudX.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudX.Name = "nudX";
            nudX.Size = new Size(120, 26);
            nudX.TabIndex = 71;
            nudX.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(513, 143);
            label5.Name = "label5";
            label5.Size = new Size(20, 20);
            label5.TabIndex = 74;
            label5.Text = "Y";
            // 
            // nudY
            // 
            nudY.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            nudY.Location = new Point(679, 143);
            nudY.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudY.Name = "nudY";
            nudY.Size = new Size(120, 26);
            nudY.TabIndex = 73;
            nudY.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(513, 178);
            label6.Name = "label6";
            label6.Size = new Size(96, 20);
            label6.TabIndex = 76;
            label6.Text = "ImageName";
            // 
            // cbImageName
            // 
            cbImageName.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cbImageName.FormattingEnabled = true;
            cbImageName.Items.AddRange(new object[] { "Предмет", "Рецепт", "Предмет + рецепт", "Исследование", "Перк" });
            cbImageName.Location = new Point(679, 175);
            cbImageName.Name = "cbImageName";
            cbImageName.Size = new Size(345, 28);
            cbImageName.TabIndex = 75;
            cbImageName.SelectedIndexChanged += cbImageName_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(513, 246);
            label8.Name = "label8";
            label8.Size = new Size(70, 20);
            label8.TabIndex = 80;
            label8.Text = "Coalition";
            // 
            // cbeCoalition
            // 
            cbeCoalition.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cbeCoalition.FormattingEnabled = true;
            cbeCoalition.Items.AddRange(new object[] { "Предмет", "Рецепт", "Предмет + рецепт", "Исследование", "Перк" });
            cbeCoalition.Location = new Point(679, 243);
            cbeCoalition.Name = "cbeCoalition";
            cbeCoalition.Size = new Size(345, 28);
            cbeCoalition.TabIndex = 79;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(513, 280);
            label7.Name = "label7";
            label7.Size = new Size(71, 20);
            label7.TabIndex = 82;
            label7.Text = "Enemies";
            // 
            // tbEnemies
            // 
            tbEnemies.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbEnemies.Location = new Point(679, 277);
            tbEnemies.Multiline = true;
            tbEnemies.Name = "tbEnemies";
            tbEnemies.ScrollBars = ScrollBars.Vertical;
            tbEnemies.Size = new Size(345, 109);
            tbEnemies.TabIndex = 81;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(513, 696);
            label9.Name = "label9";
            label9.Size = new Size(38, 20);
            label9.TabIndex = 84;
            label9.Text = "Link";
            // 
            // tbLink
            // 
            tbLink.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbLink.Location = new Point(679, 693);
            tbLink.Multiline = true;
            tbLink.Name = "tbLink";
            tbLink.Size = new Size(345, 57);
            tbLink.TabIndex = 83;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label10.Location = new Point(513, 768);
            label10.Name = "label10";
            label10.Size = new Size(80, 20);
            label10.TabIndex = 86;
            label10.Text = "LinkName";
            // 
            // tbLinkName
            // 
            tbLinkName.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbLinkName.Location = new Point(679, 765);
            tbLinkName.Multiline = true;
            tbLinkName.Name = "tbLinkName";
            tbLinkName.Size = new Size(345, 57);
            tbLinkName.TabIndex = 85;
            // 
            // cbName
            // 
            cbName.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cbName.FormattingEnabled = true;
            cbName.Items.AddRange(new object[] { "Bandit_Camp", "Big Stronghold", "Black Iron", "Boar", "Boss 20", "Boss 30", "Boss 40", "Boss 50", "Boss 60", "Cave", "Coal", "Copper", "Crocodile", "Deer", "Elephant", "Field", "Fox", "Granary", "Hunter_Camp", "Iron", "Jade", "Lumber", "Meteoric", "Minefield", "Perk point", "Pirate_Camp", "Quarry", "Rabbit", "Rebel_Camp", "Rhinoceros", "Salt", "Saltpeter", "Stronghold", "Vagrant", "Vagrant_Camp", "Wolf", "Xun Fish", "Yellow_Turban_Camp" });
            cbName.Location = new Point(679, 8);
            cbName.Name = "cbName";
            cbName.Size = new Size(345, 28);
            cbName.TabIndex = 87;
            // 
            // bClearNote
            // 
            bClearNote.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            bClearNote.Location = new Point(1037, 568);
            bClearNote.Name = "bClearNote";
            bClearNote.Size = new Size(111, 33);
            bClearNote.TabIndex = 88;
            bClearNote.Text = "Clear Note";
            bClearNote.UseVisualStyleBackColor = true;
            bClearNote.Click += bClearNote_Click;
            // 
            // nId
            // 
            nId.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            nId.Location = new Point(32, 5);
            nId.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nId.Name = "nId";
            nId.Size = new Size(89, 26);
            nId.TabIndex = 89;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label11.Location = new Point(130, 9);
            label11.Name = "label11";
            label11.Size = new Size(51, 20);
            label11.TabIndex = 90;
            label11.Text = "Name";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label12.Location = new Point(6, 8);
            label12.Name = "label12";
            label12.Size = new Size(23, 20);
            label12.TabIndex = 91;
            label12.Text = "Id";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label13.Location = new Point(513, 45);
            label13.Name = "label13";
            label13.Size = new Size(38, 20);
            label13.TabIndex = 93;
            label13.Text = "Link";
            // 
            // tbTitle
            // 
            tbTitle.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbTitle.Location = new Point(679, 42);
            tbTitle.Multiline = true;
            tbTitle.Name = "tbTitle";
            tbTitle.Size = new Size(345, 56);
            tbTitle.TabIndex = 92;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label14.Location = new Point(513, 831);
            label14.Name = "label14";
            label14.Size = new Size(54, 20);
            label14.TabIndex = 94;
            label14.Text = "MapId";
            // 
            // nudMapId
            // 
            nudMapId.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            nudMapId.Location = new Point(679, 831);
            nudMapId.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudMapId.Name = "nudMapId";
            nudMapId.Size = new Size(120, 26);
            nudMapId.TabIndex = 95;
            nudMapId.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // EditMapMarker
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1170, 918);
            Controls.Add(nudMapId);
            Controls.Add(label14);
            Controls.Add(label13);
            Controls.Add(tbTitle);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(nId);
            Controls.Add(bClearNote);
            Controls.Add(cbName);
            Controls.Add(label10);
            Controls.Add(tbLinkName);
            Controls.Add(label9);
            Controls.Add(tbLink);
            Controls.Add(label7);
            Controls.Add(tbEnemies);
            Controls.Add(label8);
            Controls.Add(cbeCoalition);
            Controls.Add(label6);
            Controls.Add(cbImageName);
            Controls.Add(label5);
            Controls.Add(nudY);
            Controls.Add(label4);
            Controls.Add(nudX);
            Controls.Add(llll);
            Controls.Add(tbNote);
            Controls.Add(label3);
            Controls.Add(cbeMarkerType);
            Controls.Add(dgv);
            Controls.Add(bDel);
            Controls.Add(bEdit);
            Controls.Add(bAdd);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(tbDescription);
            Controls.Add(bSearch);
            Controls.Add(tbSearch);
            Name = "EditMapMarker";
            Text = "EditMapMarker";
            Load += EditMapMarker_Load;
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudX).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudY).EndInit();
            ((System.ComponentModel.ISupportInitialize)nId).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMapId).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private ComboBox cbeMarkerType;
        private DataGridView dgv;
        private Button bDel;
        private Button bEdit;
        private Button bAdd;
        private Label label2;
        private Label label1;
        private TextBox tbDescription;
        private Button bSearch;
        private TextBox tbSearch;
        private Label llll;
        private TextBox tbNote;
        private Label label4;
        private NumericUpDown nudX;
        private Label label5;
        private NumericUpDown nudY;
        private Label label6;
        private ComboBox cbImageName;
        private Label label8;
        private ComboBox cbeCoalition;
        private Label label7;
        private TextBox tbEnemies;
        private Label label9;
        private TextBox tbLink;
        private Label label10;
        private TextBox tbLinkName;
        private ComboBox cbName;
        private Button bClearNote;
        private NumericUpDown nId;
        private Label label11;
        private Label label12;
        private Label label13;
        private TextBox tbTitle;
        private Label label14;
        private NumericUpDown nudMapId;
    }
}