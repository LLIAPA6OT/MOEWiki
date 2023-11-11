namespace MOEWiki.AutoParse
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
            lb1 = new ListBox();
            bOne = new Button();
            bCircle = new Button();
            tmr1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // lb1
            // 
            lb1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lb1.FormattingEnabled = true;
            lb1.ItemHeight = 15;
            lb1.Location = new Point(3, 4);
            lb1.Name = "lb1";
            lb1.Size = new Size(595, 454);
            lb1.TabIndex = 0;
            // 
            // bOne
            // 
            bOne.Location = new Point(713, 4);
            bOne.Name = "bOne";
            bOne.Size = new Size(75, 23);
            bOne.TabIndex = 1;
            bOne.Text = "One";
            bOne.UseVisualStyleBackColor = true;
            bOne.Click += bStart_Click;
            // 
            // bCircle
            // 
            bCircle.Location = new Point(713, 33);
            bCircle.Name = "bCircle";
            bCircle.Size = new Size(75, 23);
            bCircle.TabIndex = 2;
            bCircle.Text = "Circle";
            bCircle.UseVisualStyleBackColor = true;
            bCircle.Click += bStop_Click;
            // 
            // tmr1
            // 
            tmr1.Interval = 60000;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 462);
            Controls.Add(bCircle);
            Controls.Add(bOne);
            Controls.Add(lb1);
            Name = "MainForm";
            Text = "AutoParse (stoped)";
            ResumeLayout(false);
        }

        #endregion

        private ListBox lb1;
        private Button bOne;
        private Button bCircle;
        private System.Windows.Forms.Timer tmr1;
    }
}
