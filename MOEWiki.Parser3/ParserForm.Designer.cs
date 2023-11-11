namespace MOEWiki.Parser3
{
    partial class ParserForm
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
            components = new System.ComponentModel.Container();
            txtResult2 = new TextBox();
            txtResult1 = new TextBox();
            lSubcat = new Label();
            bParse = new Button();
            bCompare = new Button();
            bAddNew = new Button();
            bCommitAll = new Button();
            bHandlySearch = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            lItemName = new Label();
            bUpdateSubcat = new Button();
            bAddAll = new Button();
            tmrMain = new System.Windows.Forms.Timer(components);
            bImg = new Button();
            pb = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pb).BeginInit();
            SuspendLayout();
            // 
            // txtResult2
            // 
            txtResult2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            txtResult2.Location = new Point(15, 518);
            txtResult2.Margin = new Padding(4);
            txtResult2.Multiline = true;
            txtResult2.Name = "txtResult2";
            txtResult2.ScrollBars = ScrollBars.Vertical;
            txtResult2.Size = new Size(414, 438);
            txtResult2.TabIndex = 31;
            // 
            // txtResult1
            // 
            txtResult1.Location = new Point(15, 60);
            txtResult1.Margin = new Padding(4);
            txtResult1.Multiline = true;
            txtResult1.Name = "txtResult1";
            txtResult1.ScrollBars = ScrollBars.Vertical;
            txtResult1.Size = new Size(414, 442);
            txtResult1.TabIndex = 30;
            // 
            // lSubcat
            // 
            lSubcat.AutoSize = true;
            lSubcat.Location = new Point(447, 18);
            lSubcat.Margin = new Padding(4, 0, 4, 0);
            lSubcat.Name = "lSubcat";
            lSubcat.Size = new Size(0, 21);
            lSubcat.TabIndex = 32;
            lSubcat.DoubleClick += EditSubcat;
            // 
            // bParse
            // 
            bParse.Anchor = AnchorStyles.Right;
            bParse.Location = new Point(354, 18);
            bParse.Name = "bParse";
            bParse.Size = new Size(75, 30);
            bParse.TabIndex = 34;
            bParse.Text = ">>";
            bParse.UseVisualStyleBackColor = true;
            bParse.Click += bParse_Click;
            // 
            // bCompare
            // 
            bCompare.Anchor = AnchorStyles.Right;
            bCompare.Location = new Point(1056, 18);
            bCompare.Name = "bCompare";
            bCompare.Size = new Size(75, 30);
            bCompare.TabIndex = 35;
            bCompare.Text = ">>";
            bCompare.UseVisualStyleBackColor = true;
            bCompare.Visible = false;
            bCompare.Click += bCompare_Click;
            // 
            // bAddNew
            // 
            bAddNew.Anchor = AnchorStyles.Right;
            bAddNew.Location = new Point(1872, 18);
            bAddNew.Name = "bAddNew";
            bAddNew.Size = new Size(111, 30);
            bAddNew.TabIndex = 36;
            bAddNew.Text = "Add as new";
            bAddNew.UseVisualStyleBackColor = true;
            bAddNew.Visible = false;
            bAddNew.Click += bAddNew_Click;
            // 
            // bCommitAll
            // 
            bCommitAll.Anchor = AnchorStyles.Right;
            bCommitAll.Location = new Point(1720, 18);
            bCommitAll.Name = "bCommitAll";
            bCommitAll.Size = new Size(126, 30);
            bCommitAll.TabIndex = 37;
            bCommitAll.Text = "Commit all";
            bCommitAll.UseVisualStyleBackColor = true;
            bCommitAll.Visible = false;
            bCommitAll.Click += bCommitAll_Click;
            // 
            // bHandlySearch
            // 
            bHandlySearch.Anchor = AnchorStyles.Right;
            bHandlySearch.Location = new Point(1575, 18);
            bHandlySearch.Name = "bHandlySearch";
            bHandlySearch.Size = new Size(117, 30);
            bHandlySearch.TabIndex = 38;
            bHandlySearch.Text = "Handly search";
            bHandlySearch.UseVisualStyleBackColor = true;
            bHandlySearch.Click += bHandlySearch_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel1.AutoScroll = true;
            panel1.Location = new Point(447, 60);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(684, 895);
            panel1.TabIndex = 33;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.AutoScroll = true;
            panel2.Location = new Point(1144, 60);
            panel2.Margin = new Padding(4);
            panel2.Name = "panel2";
            panel2.Size = new Size(839, 895);
            panel2.TabIndex = 34;
            // 
            // lItemName
            // 
            lItemName.AutoSize = true;
            lItemName.Location = new Point(1144, 23);
            lItemName.Margin = new Padding(4, 0, 4, 0);
            lItemName.Name = "lItemName";
            lItemName.Size = new Size(0, 21);
            lItemName.TabIndex = 39;
            // 
            // bUpdateSubcat
            // 
            bUpdateSubcat.Anchor = AnchorStyles.Right;
            bUpdateSubcat.Location = new Point(889, 18);
            bUpdateSubcat.Name = "bUpdateSubcat";
            bUpdateSubcat.Size = new Size(127, 30);
            bUpdateSubcat.TabIndex = 40;
            bUpdateSubcat.Text = "UpdateSubcat";
            bUpdateSubcat.UseVisualStyleBackColor = true;
            bUpdateSubcat.Visible = false;
            bUpdateSubcat.Click += bUpdateSubcat_Click;
            // 
            // bAddAll
            // 
            bAddAll.Anchor = AnchorStyles.Right;
            bAddAll.Location = new Point(810, 18);
            bAddAll.Name = "bAddAll";
            bAddAll.Size = new Size(73, 30);
            bAddAll.TabIndex = 41;
            bAddAll.Text = "AddAll";
            bAddAll.UseVisualStyleBackColor = true;
            bAddAll.Visible = false;
            bAddAll.Click += bAddAll_Click;
            // 
            // tmrMain
            // 
            tmrMain.Tick += tmrMain_Tick;
            // 
            // bImg
            // 
            bImg.Anchor = AnchorStyles.Right;
            bImg.Location = new Point(755, 18);
            bImg.Name = "bImg";
            bImg.Size = new Size(49, 30);
            bImg.TabIndex = 42;
            bImg.Text = "img";
            bImg.UseVisualStyleBackColor = true;
            bImg.Visible = false;
            bImg.Click += bImg_Click;
            // 
            // pb
            // 
            pb.Location = new Point(434, 763);
            pb.Name = "pb";
            pb.Size = new Size(100, 50);
            pb.TabIndex = 43;
            pb.TabStop = false;
            pb.Visible = false;
            // 
            // ParserForm
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1995, 975);
            Controls.Add(pb);
            Controls.Add(bImg);
            Controls.Add(bAddAll);
            Controls.Add(bUpdateSubcat);
            Controls.Add(lItemName);
            Controls.Add(panel2);
            Controls.Add(bHandlySearch);
            Controls.Add(bCommitAll);
            Controls.Add(bAddNew);
            Controls.Add(bCompare);
            Controls.Add(bParse);
            Controls.Add(panel1);
            Controls.Add(lSubcat);
            Controls.Add(txtResult2);
            Controls.Add(txtResult1);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(4);
            Name = "ParserForm";
            Text = "ParserForm";
            Load += ParserForm_Load;
            KeyDown += ParserForm_KeyDown;
            ((System.ComponentModel.ISupportInitialize)pb).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtResult2;
        private TextBox txtResult1;
        private Label lSubcat;
        private Button bParse;
        private Button bCompare;
        private Button bAddNew;
        private Button bCommitAll;
        private Button bHandlySearch;
        private Panel panel1;
        private Panel panel2;
        private Label lItemName;
        private Button bUpdateSubcat;
        private Button bAddAll;
        private System.Windows.Forms.Timer tmrMain;
        private Button bImg;
        private PictureBox pb;
    }
}