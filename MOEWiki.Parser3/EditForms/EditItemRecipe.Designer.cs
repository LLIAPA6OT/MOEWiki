namespace MOEWiki.Parser3.EditForms
{
    partial class EditItemRecipe
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
            bHardDelete = new Button();
            bDel = new Button();
            label2 = new Label();
            bSave = new Button();
            cbRecipeItem = new ComboBox();
            label4 = new Label();
            nudNumber = new NumericUpDown();
            nudCount = new NumericUpDown();
            label1 = new Label();
            chbIsStepByStep = new CheckBox();
            bAdd = new Button();
            ((System.ComponentModel.ISupportInitialize)nudNumber).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCount).BeginInit();
            SuspendLayout();
            // 
            // bHardDelete
            // 
            bHardDelete.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            bHardDelete.Location = new Point(683, 177);
            bHardDelete.Name = "bHardDelete";
            bHardDelete.Size = new Size(105, 30);
            bHardDelete.TabIndex = 66;
            bHardDelete.Text = "Hard Delete";
            bHardDelete.UseVisualStyleBackColor = true;
            bHardDelete.Visible = false;
            bHardDelete.Click += bHardDelete_Click;
            // 
            // bDel
            // 
            bDel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            bDel.Location = new Point(713, 61);
            bDel.Name = "bDel";
            bDel.Size = new Size(75, 30);
            bDel.TabIndex = 62;
            bDel.Text = "Delete";
            bDel.UseVisualStyleBackColor = true;
            bDel.Visible = false;
            bDel.Click += bDel_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 10);
            label2.Name = "label2";
            label2.Size = new Size(87, 21);
            label2.TabIndex = 61;
            label2.Text = "RecipeItem";
            // 
            // bSave
            // 
            bSave.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            bSave.Location = new Point(713, 16);
            bSave.Name = "bSave";
            bSave.Size = new Size(75, 30);
            bSave.TabIndex = 59;
            bSave.Text = "Save";
            bSave.UseVisualStyleBackColor = true;
            bSave.Visible = false;
            bSave.Click += bSave_Click;
            // 
            // cbRecipeItem
            // 
            cbRecipeItem.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cbRecipeItem.FormattingEnabled = true;
            cbRecipeItem.Items.AddRange(new object[] { "Предмет", "Рецепт", "Предмет + рецепт", "Исследование", "Перк" });
            cbRecipeItem.Location = new Point(12, 34);
            cbRecipeItem.Name = "cbRecipeItem";
            cbRecipeItem.Size = new Size(601, 29);
            cbRecipeItem.TabIndex = 58;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(12, 66);
            label4.Name = "label4";
            label4.Size = new Size(68, 21);
            label4.TabIndex = 67;
            label4.Text = "Number";
            // 
            // nudNumber
            // 
            nudNumber.Location = new Point(12, 90);
            nudNumber.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudNumber.Name = "nudNumber";
            nudNumber.Size = new Size(120, 23);
            nudNumber.TabIndex = 68;
            nudNumber.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // nudCount
            // 
            nudCount.Location = new Point(12, 140);
            nudCount.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudCount.Name = "nudCount";
            nudCount.Size = new Size(120, 23);
            nudCount.TabIndex = 69;
            nudCount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 116);
            label1.Name = "label1";
            label1.Size = new Size(52, 21);
            label1.TabIndex = 70;
            label1.Text = "Count";
            // 
            // chbIsStepByStep
            // 
            chbIsStepByStep.AutoSize = true;
            chbIsStepByStep.CheckAlign = ContentAlignment.MiddleRight;
            chbIsStepByStep.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            chbIsStepByStep.Location = new Point(12, 182);
            chbIsStepByStep.Name = "chbIsStepByStep";
            chbIsStepByStep.Size = new Size(117, 25);
            chbIsStepByStep.TabIndex = 71;
            chbIsStepByStep.Text = "IsStepByStep";
            chbIsStepByStep.UseVisualStyleBackColor = true;
            // 
            // bAdd
            // 
            bAdd.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            bAdd.Location = new Point(713, 107);
            bAdd.Name = "bAdd";
            bAdd.Size = new Size(75, 30);
            bAdd.TabIndex = 72;
            bAdd.Text = "Add";
            bAdd.UseVisualStyleBackColor = true;
            bAdd.Visible = false;
            bAdd.Click += bAdd_Click;
            // 
            // EditItemRecipe
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 227);
            Controls.Add(bAdd);
            Controls.Add(chbIsStepByStep);
            Controls.Add(label1);
            Controls.Add(nudCount);
            Controls.Add(nudNumber);
            Controls.Add(label4);
            Controls.Add(bHardDelete);
            Controls.Add(bDel);
            Controls.Add(label2);
            Controls.Add(bSave);
            Controls.Add(cbRecipeItem);
            Name = "EditItemRecipe";
            Text = "EditItemRecipe";
            Load += EditItemRecipe_Load;
            ((System.ComponentModel.ISupportInitialize)nudNumber).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button bHardDelete;
        private Button bDel;
        private Label label2;
        private Button bSave;
        private ComboBox cbRecipeItem;
        private Label label4;
        private NumericUpDown nudNumber;
        private NumericUpDown nudCount;
        private Label label1;
        private CheckBox chbIsStepByStep;
        private Button bAdd;
    }
}