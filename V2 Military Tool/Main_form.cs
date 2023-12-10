using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace V2_Military_Tool
{
    public partial class Main_form : Form
    {
        private string unitspath, technologiespath, inventionspath;
        private List<Technology> technologies;
        private List<Invention> inventions;
        private List<Unit> units;

        public Main_form()
        {
            InitializeComponent();
            YearsList.Columns.Add("Year");
            YearsList.Columns.Add("Attack");
            YearsList.Columns.Add("Defence");
            YearsList.Columns.Add("Maneuver");
            YearsList.Columns.Add("Support");
            YearsList.Columns.Add("Organisation", 100);
            YearsList.Columns.Add("Supply Consumption", 150);
            UnitsList.Columns.Add("Name");
            ChangesList.Columns.Add("Year", 40);
            ChangesList.Columns.Add("Name", 140);
        }

        private void load_Button_Click(object sender, EventArgs e)
        {
            if (Program.Process_filepath(filepath_Box.Text, out unitspath, out technologiespath, out inventionspath))
            {
                units = Program.LoadUnits(unitspath);
                UnitsList.Items.Clear();
                filepath_Box.BackColor = Color.Empty;
                technologies = Program.LoadTechnologies(technologiespath);
                inventions = Program.LoadInventions(inventionspath, technologies);
                ListViewItem temp;
                foreach (Unit item in units)
                {
                    temp = new ListViewItem(item.Name);
                    UnitsList.Items.Add(temp);
                }
            }
            else
                filepath_Box.BackColor = Color.Red;
        }

        private void UnitsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem temp;
            YearsList.Items.Clear();
            Unit unit = units.SingleOrDefault(x => x.Name == UnitsList.FocusedItem.Text);
            Dictionary<int, List<Effect>> changes = new Dictionary<int, List<Effect>>();
            foreach (Technology item in technologies.Where(x => x.Effects.Any(y => y.Target == unit.Name || y.Target == "army_base")))
            {
                if (!changes.ContainsKey(item.Year)) changes.Add(item.Year, new List<Effect>());
                foreach (Effect effect in item.Effects.Where(x => x.Target == unit.Name || x.Target == "army_base"))
                {
                    changes[item.Year].Add(effect);
                }
            }
            foreach (Invention item in inventions.Where(x => x.Effects.Any(y => y.Target == unit.Name || y.Target == "army_base")))
            {
                if (!changes.ContainsKey(item.Year)) changes.Add(item.Year, new List<Effect>());
                foreach (Effect effect in item.Effects.Where(x => x.Target == unit.Name || x.Target == "army_base"))
                {
                    changes[item.Year].Add(effect);
                }
            }

            List<int> years = changes.Keys.ToList();
            years.Sort();

            decimal organisation, attack, defence, support, supply_Consumption;
            UInt16 maneuver;

            organisation = unit.Organisation;
            attack = unit.Attack;
            defence = unit.Defence;
            support = unit.Support;
            supply_Consumption = unit.Supply_Consumption;
            maneuver = unit.Maneuver;

            temp = new ListViewItem("Base");
            temp.SubItems.Add(attack.ToString());
            temp.SubItems.Add(defence.ToString());
            temp.SubItems.Add(maneuver.ToString());
            temp.SubItems.Add(support.ToString());
            temp.SubItems.Add(organisation.ToString());
            temp.SubItems.Add(supply_Consumption.ToString());
            YearsList.Items.Add(temp);
            foreach (int year in years)
            {
                temp = new ListViewItem(year.ToString());
                foreach (Effect effect in changes[year])
                {
                    organisation += effect.Organisation;
                    attack += effect.Attack;
                    defence += effect.Defence;
                    support += effect.Support;
                    supply_Consumption += effect.Supply_Consumption;
                }
                temp.SubItems.Add(attack.ToString());
                temp.SubItems.Add(defence.ToString());
                temp.SubItems.Add(maneuver.ToString());
                temp.SubItems.Add(support.ToString());
                temp.SubItems.Add(organisation.ToString());
                temp.SubItems.Add(supply_Consumption.ToString());
                YearsList.Items.Add(temp);
            }
        }

        private void Details_Button_Click(object sender, EventArgs e)
        {
            Details_form form = new Details_form(technologies, inventions);
            form.Show();
        }

        private void YearsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangesList.Items.Clear();
            if (YearsList.FocusedItem.Text != "Base")
            {
                ListViewItem temp;
                foreach (Technology item in technologies.Where(x => x.Year.ToString() == YearsList.FocusedItem.Text && x.Effects.Any(y => y.Target == UnitsList.FocusedItem.Text || y.Target == "army_base")))
                {
                    temp = new ListViewItem(item.Year.ToString());
                    temp.SubItems.Add(item.Name);
                    ChangesList.Items.Add(temp);
                }
                foreach (Invention item in inventions.Where(x => x.Year.ToString() == YearsList.FocusedItem.Text && x.Effects.Any(y => y.Target == UnitsList.FocusedItem.Text || y.Target == "army_base")))
                {
                    temp = new ListViewItem(item.Year.ToString());
                    temp.SubItems.Add(item.Name);
                    ChangesList.Items.Add(temp);
                }
            }
        }
    }
}
