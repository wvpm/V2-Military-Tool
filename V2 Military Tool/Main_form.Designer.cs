namespace V2_Military_Tool
{
    partial class Main_form
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
            this.filepath_label = new System.Windows.Forms.Label();
            this.filepath_Box = new System.Windows.Forms.TextBox();
            this.load_Button = new System.Windows.Forms.Button();
            this.UnitsList = new System.Windows.Forms.ListView();
            this.YearsList = new System.Windows.Forms.ListView();
            this.Details_Button = new System.Windows.Forms.Button();
            this.ChangesList = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // filepath_label
            // 
            this.filepath_label.AutoSize = true;
            this.filepath_label.Location = new System.Drawing.Point(13, 13);
            this.filepath_label.Name = "filepath_label";
            this.filepath_label.Size = new System.Drawing.Size(98, 17);
            this.filepath_label.TabIndex = 0;
            this.filepath_label.Text = "Mod directory:";
            // 
            // filepath_Box
            // 
            this.filepath_Box.Location = new System.Drawing.Point(118, 13);
            this.filepath_Box.Name = "filepath_Box";
            this.filepath_Box.Size = new System.Drawing.Size(492, 22);
            this.filepath_Box.TabIndex = 1;
            this.filepath_Box.Text = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Victoria 2\\mod\\WVPM";
            // 
            // load_Button
            // 
            this.load_Button.Location = new System.Drawing.Point(616, 13);
            this.load_Button.Name = "load_Button";
            this.load_Button.Size = new System.Drawing.Size(75, 23);
            this.load_Button.TabIndex = 2;
            this.load_Button.Text = "Load";
            this.load_Button.UseVisualStyleBackColor = true;
            this.load_Button.Click += new System.EventHandler(this.load_Button_Click);
            // 
            // UnitsList
            // 
            this.UnitsList.Location = new System.Drawing.Point(16, 80);
            this.UnitsList.Name = "UnitsList";
            this.UnitsList.Size = new System.Drawing.Size(245, 443);
            this.UnitsList.TabIndex = 3;
            this.UnitsList.UseCompatibleStateImageBehavior = false;
            this.UnitsList.View = System.Windows.Forms.View.Details;
            this.UnitsList.SelectedIndexChanged += new System.EventHandler(this.UnitsList_SelectedIndexChanged);
            // 
            // YearsList
            // 
            this.YearsList.Location = new System.Drawing.Point(267, 80);
            this.YearsList.Name = "YearsList";
            this.YearsList.Size = new System.Drawing.Size(700, 443);
            this.YearsList.TabIndex = 4;
            this.YearsList.UseCompatibleStateImageBehavior = false;
            this.YearsList.View = System.Windows.Forms.View.Details;
            this.YearsList.SelectedIndexChanged += new System.EventHandler(this.YearsList_SelectedIndexChanged);
            // 
            // Details_Button
            // 
            this.Details_Button.Location = new System.Drawing.Point(697, 13);
            this.Details_Button.Name = "Details_Button";
            this.Details_Button.Size = new System.Drawing.Size(198, 23);
            this.Details_Button.TabIndex = 5;
            this.Details_Button.Text = "Techs and Inventions";
            this.Details_Button.UseVisualStyleBackColor = true;
            this.Details_Button.Click += new System.EventHandler(this.Details_Button_Click);
            // 
            // ChangesList
            // 
            this.ChangesList.Location = new System.Drawing.Point(973, 80);
            this.ChangesList.Name = "ChangesList";
            this.ChangesList.Size = new System.Drawing.Size(245, 443);
            this.ChangesList.TabIndex = 6;
            this.ChangesList.UseCompatibleStateImageBehavior = false;
            this.ChangesList.View = System.Windows.Forms.View.Details;
            // 
            // Main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1235, 565);
            this.Controls.Add(this.ChangesList);
            this.Controls.Add(this.Details_Button);
            this.Controls.Add(this.YearsList);
            this.Controls.Add(this.UnitsList);
            this.Controls.Add(this.load_Button);
            this.Controls.Add(this.filepath_Box);
            this.Controls.Add(this.filepath_label);
            this.Name = "Main_form";
            this.Text = "V2 Military Tool by WVPM";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label filepath_label;
        private System.Windows.Forms.TextBox filepath_Box;
        private System.Windows.Forms.Button load_Button;
        private System.Windows.Forms.ListView UnitsList;
        private System.Windows.Forms.ListView YearsList;
        private System.Windows.Forms.Button Details_Button;
        private System.Windows.Forms.ListView ChangesList;
    }
}

