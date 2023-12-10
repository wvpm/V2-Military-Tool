using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace V2_Military_Tool
{
    public partial class YearEntryPopup : Form
    {
        public UInt16 year;
        public YearEntryPopup(string name)
        {
            InitializeComponent();
            Invention_Name_Box.Text = name;
        }

        private void Invention_Year_Box_TextChanged(object sender, EventArgs e)
        {
            if (UInt16.TryParse(Invention_Year_Box.Text, out year))
            {
                Invention_Year_Box.BackColor = default (Color);
            }
            else
            {
                Invention_Year_Box.BackColor = Color.Red;
            }
        }

        private void Confirm_Button_Click(object sender, EventArgs e)
        {
            if (UInt16.TryParse(Invention_Year_Box.Text, out year))
            this.Close();
        }
    }
}
