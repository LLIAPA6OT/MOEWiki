namespace MOEWiki.Parser3
{
    partial class UpdateImage
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
            pbOld = new PictureBox();
            bNo = new Button();
            bYes = new Button();
            pbNew = new PictureBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pbOld).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbNew).BeginInit();
            SuspendLayout();
            // 
            // pbOld
            // 
            pbOld.InitialImage = null;
            pbOld.Location = new Point(32, 12);
            pbOld.Name = "pbOld";
            pbOld.Size = new Size(121, 121);
            pbOld.TabIndex = 2;
            pbOld.TabStop = false;
            // 
            // bNo
            // 
            bNo.BackColor = SystemColors.Control;
            bNo.DialogResult = DialogResult.No;
            bNo.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            bNo.ForeColor = Color.Red;
            bNo.Location = new Point(32, 160);
            bNo.Name = "bNo";
            bNo.Size = new Size(75, 23);
            bNo.TabIndex = 33;
            bNo.Text = "No";
            bNo.UseVisualStyleBackColor = false;
            // 
            // bYes
            // 
            bYes.BackColor = SystemColors.Control;
            bYes.DialogResult = DialogResult.Yes;
            bYes.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            bYes.ForeColor = Color.Green;
            bYes.Location = new Point(318, 160);
            bYes.Name = "bYes";
            bYes.Size = new Size(75, 23);
            bYes.TabIndex = 4;
            bYes.Text = "Yes";
            bYes.UseVisualStyleBackColor = false;
            // 
            // pbNew
            // 
            pbNew.InitialImage = null;
            pbNew.Location = new Point(272, 12);
            pbNew.Name = "pbNew";
            pbNew.Size = new Size(121, 121);
            pbNew.TabIndex = 34;
            pbNew.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(195, 69);
            label1.Name = "label1";
            label1.Size = new Size(25, 15);
            label1.TabIndex = 35;
            label1.Text = "<--";
            // 
            // UpdateImage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(429, 207);
            Controls.Add(label1);
            Controls.Add(pbNew);
            Controls.Add(bYes);
            Controls.Add(bNo);
            Controls.Add(pbOld);
            Name = "UpdateImage";
            Text = "UpdateImage";
            ((System.ComponentModel.ISupportInitialize)pbOld).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbNew).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button bNo;
        private Button bYes;
        public PictureBox pbOld;
        public PictureBox pbNew;
        private Label label1;
    }
}