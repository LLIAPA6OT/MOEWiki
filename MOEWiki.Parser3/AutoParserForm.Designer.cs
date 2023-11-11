namespace MOEWiki.Parser3
{
    partial class AutoParserForm
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
            bSearch = new Button();
            dgv = new DataGridView();
            cbResult = new ComboBox();
            tbResult = new TextBox();
            pb = new PictureBox();
            bCommitGreen = new Button();
            bDelFinished = new Button();
            bDelImages = new Button();
            bSaveAndReturn = new Button();
            bParseSelected = new Button();
            bParseAll = new Button();
            cbMode = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pb).BeginInit();
            SuspendLayout();
            // 
            // bSearch
            // 
            bSearch.Location = new Point(633, 6);
            bSearch.Name = "bSearch";
            bSearch.Size = new Size(75, 23);
            bSearch.TabIndex = 0;
            bSearch.Text = "Search";
            bSearch.UseVisualStyleBackColor = true;
            bSearch.Click += bSearch_Click;
            // 
            // dgv
            // 
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Location = new Point(7, 35);
            dgv.Name = "dgv";
            dgv.ReadOnly = true;
            dgv.RowTemplate.Height = 25;
            dgv.Size = new Size(701, 743);
            dgv.TabIndex = 48;
            dgv.RowEnter += dgv_RowEnter;
            // 
            // cbResult
            // 
            cbResult.FormattingEnabled = true;
            cbResult.Location = new Point(276, 6);
            cbResult.Name = "cbResult";
            cbResult.Size = new Size(351, 23);
            cbResult.TabIndex = 49;
            // 
            // tbResult
            // 
            tbResult.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            tbResult.Location = new Point(714, 35);
            tbResult.Multiline = true;
            tbResult.Name = "tbResult";
            tbResult.ScrollBars = ScrollBars.Both;
            tbResult.Size = new Size(460, 743);
            tbResult.TabIndex = 50;
            // 
            // pb
            // 
            pb.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            pb.Location = new Point(1192, 35);
            pb.Name = "pb";
            pb.Size = new Size(887, 743);
            pb.TabIndex = 51;
            pb.TabStop = false;
            // 
            // bCommitGreen
            // 
            bCommitGreen.Location = new Point(714, 5);
            bCommitGreen.Name = "bCommitGreen";
            bCommitGreen.Size = new Size(107, 23);
            bCommitGreen.TabIndex = 52;
            bCommitGreen.Text = "Commit green";
            bCommitGreen.UseVisualStyleBackColor = true;
            bCommitGreen.Click += bCommitGreen_Click;
            // 
            // bDelFinished
            // 
            bDelFinished.Location = new Point(851, 5);
            bDelFinished.Name = "bDelFinished";
            bDelFinished.Size = new Size(83, 23);
            bDelFinished.TabIndex = 53;
            bDelFinished.Text = "Del finished";
            bDelFinished.UseVisualStyleBackColor = true;
            bDelFinished.Click += bDelFinished_Click;
            // 
            // bDelImages
            // 
            bDelImages.Enabled = false;
            bDelImages.Location = new Point(955, 5);
            bDelImages.Name = "bDelImages";
            bDelImages.Size = new Size(81, 23);
            bDelImages.TabIndex = 54;
            bDelImages.Text = "Del images";
            bDelImages.UseVisualStyleBackColor = true;
            bDelImages.Click += bDelImages_Click;
            // 
            // bSaveAndReturn
            // 
            bSaveAndReturn.Location = new Point(1121, 6);
            bSaveAndReturn.Name = "bSaveAndReturn";
            bSaveAndReturn.Size = new Size(105, 23);
            bSaveAndReturn.TabIndex = 55;
            bSaveAndReturn.Text = "Save and Return";
            bSaveAndReturn.UseVisualStyleBackColor = true;
            bSaveAndReturn.Click += bSaveAndReturn_Click;
            // 
            // bParseSelected
            // 
            bParseSelected.Location = new Point(1501, 5);
            bParseSelected.Name = "bParseSelected";
            bParseSelected.Size = new Size(105, 23);
            bParseSelected.TabIndex = 56;
            bParseSelected.Text = "ParseSelected";
            bParseSelected.UseVisualStyleBackColor = true;
            bParseSelected.Click += bParseSelected_Click;
            // 
            // bParseAll
            // 
            bParseAll.Location = new Point(1643, 5);
            bParseAll.Name = "bParseAll";
            bParseAll.Size = new Size(105, 23);
            bParseAll.TabIndex = 57;
            bParseAll.Text = "ParseAll";
            bParseAll.UseVisualStyleBackColor = true;
            bParseAll.Click += bParseAll_Click;
            // 
            // cbMode
            // 
            cbMode.FormattingEnabled = true;
            cbMode.Location = new Point(7, 5);
            cbMode.Name = "cbMode";
            cbMode.Size = new Size(246, 23);
            cbMode.TabIndex = 58;
            // 
            // AutoParserForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2091, 790);
            Controls.Add(cbMode);
            Controls.Add(bParseAll);
            Controls.Add(bParseSelected);
            Controls.Add(bSaveAndReturn);
            Controls.Add(bDelImages);
            Controls.Add(bDelFinished);
            Controls.Add(bCommitGreen);
            Controls.Add(pb);
            Controls.Add(tbResult);
            Controls.Add(cbResult);
            Controls.Add(dgv);
            Controls.Add(bSearch);
            Name = "AutoParserForm";
            Text = "AutoParserForm";
            Load += AutoParserForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            ((System.ComponentModel.ISupportInitialize)pb).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button bSearch;
        private DataGridView dgv;
        private ComboBox cbResult;
        private TextBox tbResult;
        private PictureBox pb;
        private Button bCommitGreen;
        private Button bDelFinished;
        private Button bDelImages;
        private Button bSaveAndReturn;
        private Button bParseSelected;
        private Button bParseAll;
        private ComboBox cbMode;
    }
}