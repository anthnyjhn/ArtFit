namespace Roll_Calculator
{
    partial class Form1
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
            label_materialTitle = new Label();
            materialLength = new TextBox();
            label_materialLength = new Label();
            label_materialWidth = new Label();
            materialWidth = new TextBox();
            artListBox = new ListBox();
            label_artList = new Label();
            addMaterial = new Button();
            deleteMaterial = new Button();
            label_artName = new Label();
            artName = new TextBox();
            label_artworkTitle = new Label();
            label_artWidth = new Label();
            artWidth = new TextBox();
            label_artLength = new Label();
            artLength = new TextBox();
            label_artQuantity = new Label();
            artQuantity = new TextBox();
            generateData = new Button();
            loadData = new Button();
            clearData = new Button();
            SuspendLayout();
            // 
            // label_materialTitle
            // 
            label_materialTitle.AutoSize = true;
            label_materialTitle.Font = new Font("Poppins SemiBold", 15F);
            label_materialTitle.Location = new Point(416, 218);
            label_materialTitle.Name = "label_materialTitle";
            label_materialTitle.Size = new Size(177, 36);
            label_materialTitle.TabIndex = 1;
            label_materialTitle.Text = "Material Details";
            // 
            // materialLength
            // 
            materialLength.BorderStyle = BorderStyle.FixedSingle;
            materialLength.Font = new Font("Poppins", 9.75F);
            materialLength.Location = new Point(535, 268);
            materialLength.Name = "materialLength";
            materialLength.Size = new Size(147, 27);
            materialLength.TabIndex = 2;
            // 
            // label_materialLength
            // 
            label_materialLength.AutoSize = true;
            label_materialLength.Font = new Font("Poppins", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_materialLength.Location = new Point(422, 271);
            label_materialLength.Name = "label_materialLength";
            label_materialLength.Size = new Size(113, 26);
            label_materialLength.TabIndex = 3;
            label_materialLength.Text = "Length (mm):";
            // 
            // label_materialWidth
            // 
            label_materialWidth.AutoSize = true;
            label_materialWidth.Font = new Font("Poppins", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_materialWidth.Location = new Point(422, 310);
            label_materialWidth.Name = "label_materialWidth";
            label_materialWidth.Size = new Size(107, 26);
            label_materialWidth.TabIndex = 5;
            label_materialWidth.Text = "Width (mm):";
            // 
            // materialWidth
            // 
            materialWidth.BorderStyle = BorderStyle.FixedSingle;
            materialWidth.Font = new Font("Poppins", 9.75F);
            materialWidth.Location = new Point(535, 309);
            materialWidth.Name = "materialWidth";
            materialWidth.Size = new Size(147, 27);
            materialWidth.TabIndex = 4;
            // 
            // artListBox
            // 
            artListBox.Font = new Font("Poppins", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            artListBox.FormattingEnabled = true;
            artListBox.HorizontalScrollbar = true;
            artListBox.ItemHeight = 22;
            artListBox.Location = new Point(12, 48);
            artListBox.Name = "artListBox";
            artListBox.ScrollAlwaysVisible = true;
            artListBox.Size = new Size(236, 378);
            artListBox.TabIndex = 6;
            // 
            // label_artList
            // 
            label_artList.AutoSize = true;
            label_artList.Font = new Font("Poppins SemiBold", 15F);
            label_artList.Location = new Point(23, 9);
            label_artList.Name = "label_artList";
            label_artList.Size = new Size(134, 36);
            label_artList.TabIndex = 7;
            label_artList.Text = "Artwork List";
            // 
            // addMaterial
            // 
            addMaterial.BackColor = Color.DodgerBlue;
            addMaterial.FlatStyle = FlatStyle.Popup;
            addMaterial.Font = new Font("Poppins", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            addMaterial.ForeColor = Color.White;
            addMaterial.Location = new Point(254, 48);
            addMaterial.Name = "addMaterial";
            addMaterial.Size = new Size(118, 42);
            addMaterial.TabIndex = 8;
            addMaterial.Text = "Add";
            addMaterial.UseVisualStyleBackColor = false;
            addMaterial.Click += addMaterial_Click;
            // 
            // deleteMaterial
            // 
            deleteMaterial.BackColor = Color.OrangeRed;
            deleteMaterial.FlatStyle = FlatStyle.Popup;
            deleteMaterial.Font = new Font("Poppins", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            deleteMaterial.ForeColor = Color.White;
            deleteMaterial.Location = new Point(254, 96);
            deleteMaterial.Name = "deleteMaterial";
            deleteMaterial.Size = new Size(118, 42);
            deleteMaterial.TabIndex = 9;
            deleteMaterial.Text = "Delete";
            deleteMaterial.UseVisualStyleBackColor = false;
            deleteMaterial.Click += deleteMaterial_Click;
            // 
            // label_artName
            // 
            label_artName.AutoSize = true;
            label_artName.Font = new Font("Poppins", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_artName.Location = new Point(416, 52);
            label_artName.Name = "label_artName";
            label_artName.Size = new Size(60, 26);
            label_artName.TabIndex = 12;
            label_artName.Text = "Name:";
            // 
            // artName
            // 
            artName.BorderStyle = BorderStyle.FixedSingle;
            artName.Font = new Font("Poppins", 9.75F);
            artName.Location = new Point(535, 52);
            artName.Name = "artName";
            artName.Size = new Size(147, 27);
            artName.TabIndex = 11;
            // 
            // label_artworkTitle
            // 
            label_artworkTitle.AutoSize = true;
            label_artworkTitle.Font = new Font("Poppins SemiBold", 15F);
            label_artworkTitle.Location = new Point(416, 9);
            label_artworkTitle.Name = "label_artworkTitle";
            label_artworkTitle.Size = new Size(171, 36);
            label_artworkTitle.TabIndex = 13;
            label_artworkTitle.Text = "Artwork Details";
            // 
            // label_artWidth
            // 
            label_artWidth.AutoSize = true;
            label_artWidth.Font = new Font("Poppins", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_artWidth.Location = new Point(416, 120);
            label_artWidth.Name = "label_artWidth";
            label_artWidth.Size = new Size(107, 26);
            label_artWidth.TabIndex = 17;
            label_artWidth.Text = "Width (mm):";
            // 
            // artWidth
            // 
            artWidth.BorderStyle = BorderStyle.FixedSingle;
            artWidth.Font = new Font("Poppins", 9.75F);
            artWidth.Location = new Point(535, 120);
            artWidth.Name = "artWidth";
            artWidth.Size = new Size(147, 27);
            artWidth.TabIndex = 16;
            // 
            // label_artLength
            // 
            label_artLength.AutoSize = true;
            label_artLength.Font = new Font("Poppins", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_artLength.Location = new Point(416, 87);
            label_artLength.Name = "label_artLength";
            label_artLength.Size = new Size(113, 26);
            label_artLength.TabIndex = 15;
            label_artLength.Text = "Length (mm):";
            // 
            // artLength
            // 
            artLength.BorderStyle = BorderStyle.FixedSingle;
            artLength.Font = new Font("Poppins", 9.75F);
            artLength.Location = new Point(535, 86);
            artLength.Name = "artLength";
            artLength.Size = new Size(147, 27);
            artLength.TabIndex = 14;
            // 
            // label_artQuantity
            // 
            label_artQuantity.AutoSize = true;
            label_artQuantity.Font = new Font("Poppins", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_artQuantity.Location = new Point(416, 152);
            label_artQuantity.Name = "label_artQuantity";
            label_artQuantity.Size = new Size(79, 26);
            label_artQuantity.TabIndex = 19;
            label_artQuantity.Text = "Quantity:";
            // 
            // artQuantity
            // 
            artQuantity.BorderStyle = BorderStyle.FixedSingle;
            artQuantity.Font = new Font("Poppins", 9.75F);
            artQuantity.Location = new Point(535, 153);
            artQuantity.Name = "artQuantity";
            artQuantity.Size = new Size(147, 27);
            artQuantity.TabIndex = 18;
            // 
            // generateData
            // 
            generateData.BackColor = Color.MediumSeaGreen;
            generateData.FlatStyle = FlatStyle.Popup;
            generateData.Font = new Font("Poppins", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            generateData.ForeColor = Color.White;
            generateData.Location = new Point(535, 377);
            generateData.Name = "generateData";
            generateData.Size = new Size(147, 47);
            generateData.TabIndex = 20;
            generateData.Text = "Generate";
            generateData.UseVisualStyleBackColor = false;
            generateData.Click += generateData_Click;
            // 
            // loadData
            // 
            loadData.BackColor = Color.DarkGray;
            loadData.FlatStyle = FlatStyle.Popup;
            loadData.Font = new Font("Poppins", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            loadData.ForeColor = Color.White;
            loadData.Location = new Point(254, 326);
            loadData.Name = "loadData";
            loadData.Size = new Size(118, 42);
            loadData.TabIndex = 21;
            loadData.Text = "Load";
            loadData.UseVisualStyleBackColor = false;
            // 
            // clearData
            // 
            clearData.BackColor = Color.Black;
            clearData.FlatStyle = FlatStyle.Popup;
            clearData.Font = new Font("Poppins", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            clearData.ForeColor = Color.White;
            clearData.Location = new Point(254, 379);
            clearData.Name = "clearData";
            clearData.Size = new Size(118, 42);
            clearData.TabIndex = 22;
            clearData.Text = "Clear";
            clearData.UseVisualStyleBackColor = false;
            clearData.Click += clearData_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(721, 450);
            Controls.Add(clearData);
            Controls.Add(loadData);
            Controls.Add(generateData);
            Controls.Add(label_artQuantity);
            Controls.Add(artQuantity);
            Controls.Add(label_artWidth);
            Controls.Add(artWidth);
            Controls.Add(label_artLength);
            Controls.Add(artLength);
            Controls.Add(label_artworkTitle);
            Controls.Add(label_artName);
            Controls.Add(artName);
            Controls.Add(deleteMaterial);
            Controls.Add(addMaterial);
            Controls.Add(label_artList);
            Controls.Add(artListBox);
            Controls.Add(label_materialWidth);
            Controls.Add(materialWidth);
            Controls.Add(label_materialLength);
            Controls.Add(materialLength);
            Controls.Add(label_materialTitle);
            Name = "Form1";
            Text = " ";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label_materialTitle;
        private TextBox materialLength;
        private Label label_materialLength;
        private Label label_materialWidth;
        private TextBox materialWidth;
        private ListBox artListBox;
        private Label label_artList;
        private Button addMaterial;
        private Button deleteMaterial;
        private Label label_artName;
        private TextBox artName;
        private Label label_artworkTitle;
        private Label label_artWidth;
        private TextBox artWidth;
        private Label label_artLength;
        private TextBox artLength;
        private Label label_artQuantity;
        private TextBox artQuantity;
        private Button generateData;
        private Button loadData;
        private Button clearData;
    }
}
