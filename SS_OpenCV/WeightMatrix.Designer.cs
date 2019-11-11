namespace SS_OpenCV
{
    partial class WeightMatrix
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
            this.Dimensions = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TopLeft = new System.Windows.Forms.TextBox();
            this.TopMiddle = new System.Windows.Forms.TextBox();
            this.TopRight = new System.Windows.Forms.TextBox();
            this.MiddleLeft = new System.Windows.Forms.TextBox();
            this.MiddleMiddle = new System.Windows.Forms.TextBox();
            this.MiddleRight = new System.Windows.Forms.TextBox();
            this.BottomLeft = new System.Windows.Forms.TextBox();
            this.BottomRight = new System.Windows.Forms.TextBox();
            this.BottomMiddle = new System.Windows.Forms.TextBox();
            this.Weight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Dimensions
            // 
            this.Dimensions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Dimensions.FormattingEnabled = true;
            this.Dimensions.Items.AddRange(new object[] {
            "3x3",
            "5x5"});
            this.Dimensions.Location = new System.Drawing.Point(12, 31);
            this.Dimensions.Name = "Dimensions";
            this.Dimensions.Size = new System.Drawing.Size(121, 21);
            this.Dimensions.TabIndex = 0;
            this.Dimensions.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filter";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // TopLeft
            // 
            this.TopLeft.Location = new System.Drawing.Point(15, 100);
            this.TopLeft.Name = "TopLeft";
            this.TopLeft.Size = new System.Drawing.Size(47, 20);
            this.TopLeft.TabIndex = 2;
            this.TopLeft.TextChanged += new System.EventHandler(this.TopLeft_TextChanged);
            // 
            // TopMiddle
            // 
            this.TopMiddle.Location = new System.Drawing.Point(68, 100);
            this.TopMiddle.Name = "TopMiddle";
            this.TopMiddle.Size = new System.Drawing.Size(47, 20);
            this.TopMiddle.TabIndex = 3;
            this.TopMiddle.TextChanged += new System.EventHandler(this.TopMiddle_TextChanged);
            // 
            // TopRight
            // 
            this.TopRight.Location = new System.Drawing.Point(121, 100);
            this.TopRight.Name = "TopRight";
            this.TopRight.Size = new System.Drawing.Size(47, 20);
            this.TopRight.TabIndex = 4;
            this.TopRight.TextChanged += new System.EventHandler(this.TopRight_TextChanged);
            // 
            // MiddleLeft
            // 
            this.MiddleLeft.Location = new System.Drawing.Point(15, 126);
            this.MiddleLeft.Name = "MiddleLeft";
            this.MiddleLeft.Size = new System.Drawing.Size(47, 20);
            this.MiddleLeft.TabIndex = 5;
            this.MiddleLeft.TextChanged += new System.EventHandler(this.MiddleLeft_TextChanged);
            // 
            // MiddleMiddle
            // 
            this.MiddleMiddle.Location = new System.Drawing.Point(68, 126);
            this.MiddleMiddle.Name = "MiddleMiddle";
            this.MiddleMiddle.Size = new System.Drawing.Size(47, 20);
            this.MiddleMiddle.TabIndex = 6;
            this.MiddleMiddle.TextChanged += new System.EventHandler(this.MiddleMiddle_TextChanged);
            // 
            // MiddleRight
            // 
            this.MiddleRight.Location = new System.Drawing.Point(121, 126);
            this.MiddleRight.Name = "MiddleRight";
            this.MiddleRight.Size = new System.Drawing.Size(47, 20);
            this.MiddleRight.TabIndex = 7;
            this.MiddleRight.TextChanged += new System.EventHandler(this.MiddleRight_TextChanged);
            // 
            // BottomLeft
            // 
            this.BottomLeft.Location = new System.Drawing.Point(15, 152);
            this.BottomLeft.Name = "BottomLeft";
            this.BottomLeft.Size = new System.Drawing.Size(47, 20);
            this.BottomLeft.TabIndex = 8;
            this.BottomLeft.TextChanged += new System.EventHandler(this.BottomLeft_TextChanged);
            // 
            // BottomRight
            // 
            this.BottomRight.Location = new System.Drawing.Point(121, 152);
            this.BottomRight.Name = "BottomRight";
            this.BottomRight.Size = new System.Drawing.Size(47, 20);
            this.BottomRight.TabIndex = 9;
            this.BottomRight.TextChanged += new System.EventHandler(this.BottomRight_TextChanged);
            // 
            // BottomMiddle
            // 
            this.BottomMiddle.Location = new System.Drawing.Point(68, 152);
            this.BottomMiddle.Name = "BottomMiddle";
            this.BottomMiddle.Size = new System.Drawing.Size(47, 20);
            this.BottomMiddle.TabIndex = 10;
            this.BottomMiddle.TextChanged += new System.EventHandler(this.BottomMiddle_TextChanged);
            // 
            // Weight
            // 
            this.Weight.Location = new System.Drawing.Point(59, 215);
            this.Weight.Name = "Weight";
            this.Weight.Size = new System.Drawing.Size(100, 20);
            this.Weight.TabIndex = 11;
            this.Weight.TextChanged += new System.EventHandler(this.Weight_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Weight";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 275);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 275);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "Ok";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // WeightMatrix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(174, 306);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Weight);
            this.Controls.Add(this.BottomMiddle);
            this.Controls.Add(this.BottomRight);
            this.Controls.Add(this.BottomLeft);
            this.Controls.Add(this.MiddleRight);
            this.Controls.Add(this.MiddleMiddle);
            this.Controls.Add(this.MiddleLeft);
            this.Controls.Add(this.TopRight);
            this.Controls.Add(this.TopMiddle);
            this.Controls.Add(this.TopLeft);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Dimensions);
            this.Name = "WeightMatrix";
            this.Text = "WeightMatrix";
            this.Load += new System.EventHandler(this.WeightMatrix_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.ComboBox Dimensions;
        public System.Windows.Forms.TextBox TopLeft;
        public System.Windows.Forms.TextBox TopMiddle;
        public System.Windows.Forms.TextBox TopRight;
        public System.Windows.Forms.TextBox MiddleLeft;
        public System.Windows.Forms.TextBox MiddleMiddle;
        public System.Windows.Forms.TextBox MiddleRight;
        public System.Windows.Forms.TextBox BottomLeft;
        public System.Windows.Forms.TextBox BottomRight;
        public System.Windows.Forms.TextBox BottomMiddle;
        public System.Windows.Forms.TextBox Weight;
    }
}