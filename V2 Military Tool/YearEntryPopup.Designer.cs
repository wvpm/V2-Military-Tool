namespace V2_Military_Tool
{
    partial class YearEntryPopup
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
            this.Label_Invention = new System.Windows.Forms.Label();
            this.Label_Year = new System.Windows.Forms.Label();
            this.Popup_Text = new System.Windows.Forms.TextBox();
            this.Invention_Name_Box = new System.Windows.Forms.TextBox();
            this.Invention_Year_Box = new System.Windows.Forms.TextBox();
            this.Confirm_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Label_Invention
            // 
            this.Label_Invention.AutoSize = true;
            this.Label_Invention.Location = new System.Drawing.Point(5, 48);
            this.Label_Invention.Name = "Label_Invention";
            this.Label_Invention.Size = new System.Drawing.Size(69, 17);
            this.Label_Invention.TabIndex = 2;
            this.Label_Invention.Text = "Invention:";
            // 
            // Label_Year
            // 
            this.Label_Year.AutoSize = true;
            this.Label_Year.Location = new System.Drawing.Point(4, 70);
            this.Label_Year.Name = "Label_Year";
            this.Label_Year.Size = new System.Drawing.Size(42, 17);
            this.Label_Year.TabIndex = 5;
            this.Label_Year.Text = "Year:";
            // 
            // Popup_Text
            // 
            this.Popup_Text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Popup_Text.Location = new System.Drawing.Point(7, 7);
            this.Popup_Text.Multiline = true;
            this.Popup_Text.Name = "Popup_Text";
            this.Popup_Text.ReadOnly = true;
            this.Popup_Text.Size = new System.Drawing.Size(282, 38);
            this.Popup_Text.TabIndex = 4;
            this.Popup_Text.TabStop = false;
            this.Popup_Text.Text = "Invention limit is complex, please manually enter the year at which it can be res" +
    "earched.";
            // 
            // Invention_Name_Box
            // 
            this.Invention_Name_Box.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Invention_Name_Box.Location = new System.Drawing.Point(80, 48);
            this.Invention_Name_Box.Name = "Invention_Name_Box";
            this.Invention_Name_Box.ReadOnly = true;
            this.Invention_Name_Box.Size = new System.Drawing.Size(190, 15);
            this.Invention_Name_Box.TabIndex = 3;
            this.Invention_Name_Box.TabStop = false;
            // 
            // Invention_Year_Box
            // 
            this.Invention_Year_Box.Location = new System.Drawing.Point(80, 70);
            this.Invention_Year_Box.Name = "Invention_Year_Box";
            this.Invention_Year_Box.Size = new System.Drawing.Size(109, 22);
            this.Invention_Year_Box.TabIndex = 0;
            this.Invention_Year_Box.TextChanged += new System.EventHandler(this.Invention_Year_Box_TextChanged);
            // 
            // Confirm_Button
            // 
            this.Confirm_Button.Location = new System.Drawing.Point(195, 69);
            this.Confirm_Button.Name = "Confirm_Button";
            this.Confirm_Button.Size = new System.Drawing.Size(75, 23);
            this.Confirm_Button.TabIndex = 1;
            this.Confirm_Button.Text = "Confirm";
            this.Confirm_Button.UseVisualStyleBackColor = true;
            this.Confirm_Button.Click += new System.EventHandler(this.Confirm_Button_Click);
            // 
            // YearEntryPopup
            // 
            this.AcceptButton = this.Confirm_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 99);
            this.Controls.Add(this.Confirm_Button);
            this.Controls.Add(this.Invention_Year_Box);
            this.Controls.Add(this.Invention_Name_Box);
            this.Controls.Add(this.Popup_Text);
            this.Controls.Add(this.Label_Year);
            this.Controls.Add(this.Label_Invention);
            this.Name = "YearEntryPopup";
            this.Text = "YearEntryPopup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_Invention;
        private System.Windows.Forms.Label Label_Year;
        private System.Windows.Forms.TextBox Popup_Text;
        private System.Windows.Forms.TextBox Invention_Name_Box;
        private System.Windows.Forms.TextBox Invention_Year_Box;
        private System.Windows.Forms.Button Confirm_Button;
    }
}