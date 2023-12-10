namespace V2_Military_Tool
{
    partial class Details_form
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
            this.InventionsList = new System.Windows.Forms.ListView();
            this.TechsList = new System.Windows.Forms.ListView();
            this.StatsList = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // InventionsList
            // 
            this.InventionsList.FullRowSelect = true;
            this.InventionsList.Location = new System.Drawing.Point(771, 12);
            this.InventionsList.Name = "InventionsList";
            this.InventionsList.Size = new System.Drawing.Size(245, 672);
            this.InventionsList.TabIndex = 4;
            this.InventionsList.UseCompatibleStateImageBehavior = false;
            this.InventionsList.View = System.Windows.Forms.View.Details;
            this.InventionsList.SelectedIndexChanged += new System.EventHandler(this.InventionsList_SelectedIndexChanged);
            // 
            // TechsList
            // 
            this.TechsList.FullRowSelect = true;
            this.TechsList.Location = new System.Drawing.Point(12, 12);
            this.TechsList.Name = "TechsList";
            this.TechsList.Size = new System.Drawing.Size(245, 672);
            this.TechsList.TabIndex = 5;
            this.TechsList.UseCompatibleStateImageBehavior = false;
            this.TechsList.View = System.Windows.Forms.View.Details;
            this.TechsList.SelectedIndexChanged += new System.EventHandler(this.TechsList_SelectedIndexChanged);
            // 
            // StatsList
            // 
            this.StatsList.Location = new System.Drawing.Point(263, 12);
            this.StatsList.Name = "StatsList";
            this.StatsList.Size = new System.Drawing.Size(502, 672);
            this.StatsList.TabIndex = 6;
            this.StatsList.UseCompatibleStateImageBehavior = false;
            this.StatsList.View = System.Windows.Forms.View.Details;
            // 
            // Details_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 696);
            this.Controls.Add(this.StatsList);
            this.Controls.Add(this.TechsList);
            this.Controls.Add(this.InventionsList);
            this.Name = "Details_form";
            this.Text = "Technologies and Inventions";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView InventionsList;
        private System.Windows.Forms.ListView StatsList;
        private System.Windows.Forms.ListView TechsList;
    }
}