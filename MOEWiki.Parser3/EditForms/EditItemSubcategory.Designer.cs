namespace MOEWiki.Parser3.EditForms
{
    partial class EditItemSubcategory
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
            cbSubcategoryId = new ComboBox();
            bSave = new Button();
            SuspendLayout();
            // 
            // cbSubcategoryId
            // 
            cbSubcategoryId.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cbSubcategoryId.FormattingEnabled = true;
            cbSubcategoryId.Items.AddRange(new object[] { "Предмет", "Рецепт", "Предмет + рецепт", "Исследование", "Перк" });
            cbSubcategoryId.Location = new Point(12, 12);
            cbSubcategoryId.Name = "cbSubcategoryId";
            cbSubcategoryId.Size = new Size(358, 29);
            cbSubcategoryId.TabIndex = 34;
            // 
            // bSave
            // 
            bSave.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            bSave.Location = new Point(376, 12);
            bSave.Name = "bSave";
            bSave.Size = new Size(75, 29);
            bSave.TabIndex = 47;
            bSave.Text = "Save";
            bSave.UseVisualStyleBackColor = true;
            bSave.Click += bSave_Click;
            // 
            // EditItemSubcategory
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(463, 56);
            Controls.Add(bSave);
            Controls.Add(cbSubcategoryId);
            Name = "EditItemSubcategory";
            Text = "EditItemSubcategory";
            Load += EditItemSubcategory_Load;
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cbSubcategoryId;
        private Button bSave;
    }
}