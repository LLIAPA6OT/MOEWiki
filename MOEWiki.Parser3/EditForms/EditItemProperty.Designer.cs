namespace MOEWiki.Parser3.EditForms
{
    partial class EditItemProperty
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
            bSave = new Button();
            cbProperty = new ComboBox();
            label2 = new Label();
            tbValue = new TextBox();
            bDel = new Button();
            label1 = new Label();
            label3 = new Label();
            cbQuality = new ComboBox();
            bHardDelete = new Button();
            bAdd = new Button();
            SuspendLayout();
            // 
            // bSave
            // 
            bSave.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            bSave.Location = new Point(713, 12);
            bSave.Name = "bSave";
            bSave.Size = new Size(75, 30);
            bSave.TabIndex = 49;
            bSave.Text = "Save";
            bSave.UseVisualStyleBackColor = true;
            bSave.Visible = false;
            bSave.Click += bSave_Click;
            // 
            // cbProperty
            // 
            cbProperty.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cbProperty.FormattingEnabled = true;
            cbProperty.Items.AddRange(new object[] { "Предмет", "Рецепт", "Предмет + рецепт", "Исследование", "Перк" });
            cbProperty.Location = new Point(12, 23);
            cbProperty.Name = "cbProperty";
            cbProperty.Size = new Size(601, 29);
            cbProperty.TabIndex = 48;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, -1);
            label2.Name = "label2";
            label2.Size = new Size(70, 21);
            label2.TabIndex = 52;
            label2.Text = "Property";
            // 
            // tbValue
            // 
            tbValue.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tbValue.Location = new Point(12, 90);
            tbValue.Multiline = true;
            tbValue.Name = "tbValue";
            tbValue.ScrollBars = ScrollBars.Both;
            tbValue.Size = new Size(601, 127);
            tbValue.TabIndex = 51;
            // 
            // bDel
            // 
            bDel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            bDel.Location = new Point(713, 57);
            bDel.Name = "bDel";
            bDel.Size = new Size(75, 30);
            bDel.TabIndex = 53;
            bDel.Text = "Delete";
            bDel.UseVisualStyleBackColor = true;
            bDel.Visible = false;
            bDel.Click += bDel_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 66);
            label1.Name = "label1";
            label1.Size = new Size(48, 21);
            label1.TabIndex = 54;
            label1.Text = "Value";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(12, 236);
            label3.Name = "label3";
            label3.Size = new Size(60, 21);
            label3.TabIndex = 55;
            label3.Text = "Quality";
            // 
            // cbQuality
            // 
            cbQuality.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cbQuality.FormattingEnabled = true;
            cbQuality.Items.AddRange(new object[] { "Предмет", "Рецепт", "Предмет + рецепт", "Исследование", "Перк" });
            cbQuality.Location = new Point(12, 260);
            cbQuality.Name = "cbQuality";
            cbQuality.Size = new Size(162, 29);
            cbQuality.TabIndex = 56;
            // 
            // bHardDelete
            // 
            bHardDelete.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            bHardDelete.Location = new Point(683, 263);
            bHardDelete.Name = "bHardDelete";
            bHardDelete.Size = new Size(105, 30);
            bHardDelete.TabIndex = 57;
            bHardDelete.Text = "Hard Delete";
            bHardDelete.UseVisualStyleBackColor = true;
            bHardDelete.Visible = false;
            bHardDelete.Click += bHardDelete_Click;
            // 
            // bAdd
            // 
            bAdd.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            bAdd.Location = new Point(713, 105);
            bAdd.Name = "bAdd";
            bAdd.Size = new Size(75, 30);
            bAdd.TabIndex = 58;
            bAdd.Text = "Add";
            bAdd.UseVisualStyleBackColor = true;
            bAdd.Visible = false;
            bAdd.Click += bAdd_Click;
            // 
            // EditItemProperty
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 305);
            Controls.Add(bAdd);
            Controls.Add(bHardDelete);
            Controls.Add(cbQuality);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(bDel);
            Controls.Add(label2);
            Controls.Add(tbValue);
            Controls.Add(bSave);
            Controls.Add(cbProperty);
            Name = "EditItemProperty";
            Text = "EditItemProperty";
            Load += EditItemProperty_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button bSave;
        private ComboBox cbProperty;
        private Label label2;
        private TextBox tbValue;
        private Button bDel;
        private Label label1;
        private Label label3;
        private ComboBox cbQuality;
        private Button bHardDelete;
        private Button bAdd;
    }
}