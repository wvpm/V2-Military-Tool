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
    public partial class Details_form : Form
    {
        private List<Technology> technologies;
        private List<Invention> inventions;

        public Details_form(List<Technology> techs, List<Invention> invents)
        {
            InitializeComponent();
            technologies = techs;
            inventions = invents;
            StatsList.Columns.Add("Target", 40);
            StatsList.Columns.Add("Attack", 40);
            StatsList.Columns.Add("Defence", 40);
            StatsList.Columns.Add("Maneuver", 40);
            StatsList.Columns.Add("Support", 40);
            StatsList.Columns.Add("Organisation", 40);
            StatsList.Columns.Add("Supply Consumption", 40);
            TechsList.Columns.Add("Year", 40);
            TechsList.Columns.Add("Name");
            InventionsList.Columns.Add("Year", 40);
            InventionsList.Columns.Add("Name");

            TechsList.Items.Clear();
            InventionsList.Items.Clear();
            ListViewItem temp;
            foreach (Technology item in techs)
            {
                temp = new ListViewItem(item.Year.ToString());
                temp.SubItems.Add(item.Name);
                TechsList.Items.Add(temp);
            }
            foreach (Invention item in inventions)
            {
                temp = new ListViewItem(item.Year.ToString());
                temp.SubItems.Add(item.Name);
                InventionsList.Items.Add(temp);
            }
        }

        private void TechsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            StatsList.Items.Clear();
            ListViewItem temp;
            Technology selected = technologies.SingleOrDefault(x => x.Name == TechsList.FocusedItem.SubItems[1].Text);
            foreach (Effect item in selected.Effects)
            {
                temp = new ListViewItem(item.Target);
                temp.SubItems.Add(item.Attack.ToString());
                temp.SubItems.Add(item.Defence.ToString());
                temp.SubItems.Add(item.Maneuver.ToString());
                temp.SubItems.Add(item.Support.ToString());
                temp.SubItems.Add(item.Organisation.ToString());
                temp.SubItems.Add(item.Supply_Consumption.ToString());
                StatsList.Items.Add(temp);
            }
        }

        private void InventionsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            StatsList.Items.Clear();
            ListViewItem temp;
            Invention selected = inventions.SingleOrDefault(x => x.Name == InventionsList.FocusedItem.SubItems[1].Text);
            foreach (Effect item in selected.Effects)
            {
                temp = new ListViewItem(item.Target);
                temp.SubItems.Add(item.Attack.ToString());
                temp.SubItems.Add(item.Defence.ToString());
                temp.SubItems.Add(item.Maneuver.ToString());
                temp.SubItems.Add(item.Support.ToString());
                temp.SubItems.Add(item.Organisation.ToString());
                temp.SubItems.Add(item.Supply_Consumption.ToString());
                StatsList.Items.Add(temp);
            }
        }
    }
}
