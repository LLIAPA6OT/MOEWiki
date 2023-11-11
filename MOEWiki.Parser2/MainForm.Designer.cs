namespace MOEWiki.Parser2
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            bStart = new Button();
            pictureBox1 = new PictureBox();
            tmrMain = new System.Windows.Forms.Timer(components);
            txtResult1 = new TextBox();
            pictureBox2 = new PictureBox();
            textBox1 = new TextBox();
            txtResult2 = new TextBox();
            cbocrengine = new ComboBox();
            label1 = new Label();
            cbMode = new ComboBox();
            lWarn = new Label();
            nudColorVariation = new NumericUpDown();
            menuStrip1 = new MenuStrip();
            категорииToolStripMenuItem = new ToolStripMenuItem();
            categoryToolStripMenuItem = new ToolStripMenuItem();
            subcategoryToolStripMenuItem = new ToolStripMenuItem();
            itemToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudColorVariation).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // bStart
            // 
            bStart.BackColor = Color.Red;
            bStart.Location = new Point(1195, 4);
            bStart.Name = "bStart";
            bStart.Size = new Size(75, 23);
            bStart.TabIndex = 0;
            bStart.Text = "Start";
            bStart.UseVisualStyleBackColor = false;
            bStart.Click += button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.InitialImage = null;
            pictureBox1.Location = new Point(13, 38);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(533, 464);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // tmrMain
            // 
            tmrMain.Tick += tmrMain_Tick;
            // 
            // txtResult1
            // 
            txtResult1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtResult1.Location = new Point(1159, 38);
            txtResult1.Multiline = true;
            txtResult1.Name = "txtResult1";
            txtResult1.ScrollBars = ScrollBars.Vertical;
            txtResult1.Size = new Size(427, 411);
            txtResult1.TabIndex = 10;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(596, 38);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(533, 878);
            pictureBox2.TabIndex = 11;
            pictureBox2.TabStop = false;
            pictureBox2.Visible = false;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(1060, 5);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(77, 23);
            textBox1.TabIndex = 26;
            textBox1.Text = "код символа";
            textBox1.KeyDown += textBox1_KeyDown_1;
            // 
            // txtResult2
            // 
            txtResult2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtResult2.Location = new Point(1159, 455);
            txtResult2.Multiline = true;
            txtResult2.Name = "txtResult2";
            txtResult2.ScrollBars = ScrollBars.Vertical;
            txtResult2.Size = new Size(427, 465);
            txtResult2.TabIndex = 29;
            // 
            // cbocrengine
            // 
            cbocrengine.FormattingEnabled = true;
            cbocrengine.Items.AddRange(new object[] { "1", "2", "3", "5" });
            cbocrengine.Location = new Point(1553, 5);
            cbocrengine.Name = "cbocrengine";
            cbocrengine.Size = new Size(39, 23);
            cbocrengine.TabIndex = 30;
            cbocrengine.Text = "5";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1487, 10);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 31;
            label1.Text = "ocrengine";
            // 
            // cbMode
            // 
            cbMode.FormattingEnabled = true;
            cbMode.Items.AddRange(new object[] { "Предмет", "Рецепт", "Предмет + рецепт", "Исследование", "Перк" });
            cbMode.Location = new Point(1360, 5);
            cbMode.Name = "cbMode";
            cbMode.Size = new Size(121, 23);
            cbMode.TabIndex = 32;
            cbMode.SelectedIndexChanged += cbMode_SelectedIndexChanged;
            // 
            // lWarn
            // 
            lWarn.AutoSize = true;
            lWarn.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lWarn.ForeColor = Color.Red;
            lWarn.Location = new Point(147, 3);
            lWarn.Name = "lWarn";
            lWarn.Size = new Size(0, 30);
            lWarn.TabIndex = 33;
            // 
            // nudColorVariation
            // 
            nudColorVariation.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            nudColorVariation.Location = new Point(425, 9);
            nudColorVariation.Name = "nudColorVariation";
            nudColorVariation.Size = new Size(120, 23);
            nudColorVariation.TabIndex = 34;
            nudColorVariation.Value = new decimal(new int[] { 15, 0, 0, 0 });
            nudColorVariation.ValueChanged += nudColorVariation_ValueChanged;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { категорииToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1598, 24);
            menuStrip1.TabIndex = 35;
            menuStrip1.Text = "menuStrip1";
            // 
            // категорииToolStripMenuItem
            // 
            категорииToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { categoryToolStripMenuItem, subcategoryToolStripMenuItem, itemToolStripMenuItem });
            категорииToolStripMenuItem.Name = "категорииToolStripMenuItem";
            категорииToolStripMenuItem.Size = new Size(39, 20);
            категорииToolStripMenuItem.Text = "Edit";
            // 
            // categoryToolStripMenuItem
            // 
            categoryToolStripMenuItem.Name = "categoryToolStripMenuItem";
            categoryToolStripMenuItem.Size = new Size(180, 22);
            categoryToolStripMenuItem.Text = "Category";
            categoryToolStripMenuItem.Click += categoryToolStripMenuItem_Click;
            // 
            // subcategoryToolStripMenuItem
            // 
            subcategoryToolStripMenuItem.Name = "subcategoryToolStripMenuItem";
            subcategoryToolStripMenuItem.Size = new Size(180, 22);
            subcategoryToolStripMenuItem.Text = "Subcategory";
            // 
            // itemToolStripMenuItem
            // 
            itemToolStripMenuItem.Name = "itemToolStripMenuItem";
            itemToolStripMenuItem.Size = new Size(180, 22);
            itemToolStripMenuItem.Text = "Item";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1598, 932);
            Controls.Add(txtResult2);
            Controls.Add(txtResult1);
            Controls.Add(nudColorVariation);
            Controls.Add(lWarn);
            Controls.Add(cbMode);
            Controls.Add(label1);
            Controls.Add(cbocrengine);
            Controls.Add(textBox1);
            Controls.Add(pictureBox1);
            Controls.Add(bStart);
            Controls.Add(pictureBox2);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "0";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudColorVariation).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button bStart;
        private PictureBox pictureBox1;
        private System.Windows.Forms.Timer tmrMain;
        private TextBox txtResult1;
        private PictureBox pictureBox2;
        private TextBox textBox1;
        private TextBox txtResult2;
        private ComboBox cbocrengine;
        private Label label1;
        private ComboBox cbMode;
        private Label lWarn;
        private NumericUpDown nudColorVariation;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem категорииToolStripMenuItem;
        private ToolStripMenuItem categoryToolStripMenuItem;
        private ToolStripMenuItem subcategoryToolStripMenuItem;
        private ToolStripMenuItem itemToolStripMenuItem;
    }
}