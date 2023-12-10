using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace V2_Military_Tool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main_form());
        }
        public static bool Process_filepath(string filepath, out string unitspath, out string technologiespath, out string inventionspath)
        {
            unitspath = technologiespath = inventionspath = string.Empty;
            if (!Directory.Exists(filepath))
                return false;
            string unitslocation = @"\units\", technologieslocation = @"\technologies\", inventionslocation = @"\inventions\", modtext = @"\mod\";
            if (Directory.Exists(filepath + unitslocation))
                unitspath = filepath + unitslocation;
            else if (filepath.Contains(modtext) && Directory.Exists(filepath.Substring(0, filepath.IndexOf(modtext)) + unitslocation))
                unitspath = filepath.Substring(0, filepath.IndexOf(modtext)) + unitslocation;
            else return false;
            if (Directory.Exists(filepath + technologieslocation))
                technologiespath = filepath + technologieslocation;
            else if (filepath.Contains(modtext) && Directory.Exists(filepath.Substring(0, filepath.IndexOf(modtext)) + technologieslocation))
                technologiespath = filepath.Substring(0, filepath.IndexOf(modtext)) + technologieslocation;
            else return false;
            if (Directory.Exists(filepath + inventionslocation))
                inventionspath = filepath + inventionslocation;
            else if (filepath.Contains(modtext) && Directory.Exists(filepath.Substring(0, filepath.IndexOf(modtext)) + inventionslocation))
                inventionspath = filepath.Substring(0, filepath.IndexOf(modtext)) + inventionslocation;
            else return false;
            return true;
        }

        public static List<Technology> LoadTechnologies(string technologiespath)
        {
            string[] files = { "army_tech", "navy_tech", "commerce_tech", "culture_tech", "industry_tech" };
            List<Technology> technologies = new List<Technology>();
            for (int index = 0; index < files.Length; index++)
            {
                using (StreamReader reader = new StreamReader(technologiespath + files[index] + ".txt"))
                {
                    char[] ignore = { '=', '{', '}' };
                    string currentline, temp = string.Empty, name = string.Empty, target = string.Empty;
                    UInt16 year = 0, maneuver;
                    decimal organisation, attack, defence, support, experience, supply_consumption;
                    List<Effect> effects = new List<Effect>();
                    bool scopestart = false, yearscope = false, effectscope = false, effectscopestart = false;
                    bool organisationscope, attackscope, defencescope, supportscope, supply_consumptionscope, maneuverscope;
                    organisationscope = attackscope = defencescope = supportscope = supply_consumptionscope = maneuverscope = false;
                    organisation = attack = defence = support = supply_consumption = maneuver = 0;
                    int scope = 0;
                    // scopes
                    // 0 - technames
                    // 1 - details
                    // 2 - effect or ai_chance
                    // 3 - ai_chance modifiers

                    while (!reader.EndOfStream)
                    {
                        currentline = reader.ReadLine();
                        if (!yearscope && !effectscope) temp = string.Empty;
                        for (int i = 0; i < currentline.Length; i++)
                        {
                            switch (currentline[i])
                            {
                                case '#':
                                    i = currentline.Length;
                                    break;
                                case '\t':
                                    break;
                                case ' ':
                                    break;
                                case '=':
                                    if (scope < 2)
                                    {
                                        scopestart = true;
                                        if (scope == 0)
                                        {
                                            name = Trim(temp, ignore);
                                            effects = new List<Effect>();
                                            temp = string.Empty;
                                        }
                                        else if (scope == 1)
                                        {
                                            if (Trim(temp, ignore).ToLower() == "year")
                                            {
                                                yearscope = true;
                                                temp = string.Empty;
                                            }
                                            else if (Trim(temp, ignore).ToLower() != "ai_chance")
                                                effectscopestart = true;
                                        }
                                    }
                                    else if (effectscope)
                                    {
                                        if (scope != 2) throw new FileLoadException();
                                        else
                                        {
                                            scopestart = true;
                                            switch (Trim(temp, ignore).ToLower())
                                            {
                                                case "attack":
                                                    attackscope = true;
                                                    break;
                                                case "defence":
                                                    defencescope = true;
                                                    break;
                                                case "support":
                                                    supportscope = true;
                                                    break;
                                                case "maneuver":
                                                    maneuverscope = true;
                                                    break;
                                                case "supply_consumption":
                                                    supply_consumptionscope = true;
                                                    break;
                                                case "default_organisation":
                                                    organisationscope = true;
                                                    break;
                                                default:
                                                    break;
                                            }
                                            temp = string.Empty;
                                        }
                                    }
                                    break;
                                case '{':
                                    if (scope < 2)
                                    {
                                        if (scopestart)
                                        {
                                            scopestart = false;
                                            if (effectscopestart)
                                            {
                                                effectscopestart = false;
                                                effectscope = true;
                                                target = Trim(temp, ignore);
                                                temp = string.Empty;
                                            }
                                        }
                                        else throw new FileLoadException();
                                    }
                                    scope++;
                                    goto default;
                                case '}':
                                    if (scope == 1) technologies.Add(new Technology(name, year, effects));
                                    scope--;
                                    if (effectscope)
                                    {
                                        if (attackscope)
                                        {
                                            attackscope = false;
                                            attack = decimal.Parse(temp);
                                        }
                                        else if (defencescope)
                                        {
                                            defencescope = false;
                                            defence = decimal.Parse(temp);
                                        }
                                        else if (supportscope)
                                        {
                                            supportscope = false;
                                            support = decimal.Parse(temp);
                                        }
                                        else if (maneuverscope)
                                        {
                                            maneuverscope = false;
                                            maneuver = UInt16.Parse(temp);
                                        }
                                        else if (supply_consumptionscope)
                                        {
                                            supply_consumptionscope = false;
                                            supply_consumption = decimal.Parse(temp);
                                        }
                                        else if (organisationscope)
                                        {
                                            organisationscope = false;
                                            organisation = decimal.Parse(temp);
                                        }
                                        effectscope = false;
                                        effects.Add(new Effect(target, organisation, attack, defence, support, maneuver, supply_consumption));
                                        organisation = attack = defence = support = experience = supply_consumption = maneuver = 0;
                                    }
                                    temp = string.Empty;
                                    if (scope < 0) throw new FileLoadException();
                                    goto default;
                                default:
                                    if (scopestart) scopestart = false;
                                    if (effectscopestart)
                                    {
                                        effectscopestart = false;
                                        temp = string.Empty;
                                    }
                                    if (scope > 0 && currentline[i] != '-')
                                    {
                                        if (yearscope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    yearscope = false;
                                                    year = UInt16.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                        else if (attackscope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    attackscope = false;
                                                    attack = decimal.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                        else if (defencescope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    defencescope = false;
                                                    defence = decimal.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                        else if (supportscope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    supportscope = false;
                                                    support = decimal.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                        else if (maneuverscope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    maneuverscope = false;
                                                    maneuver = UInt16.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                        else if (supply_consumptionscope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    supply_consumptionscope = false;
                                                    supply_consumption = decimal.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                        else if (organisationscope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    organisationscope = false;
                                                    organisation = decimal.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                    }
                                    temp += currentline[i];
                                    break;
                            }
                        }
                    }
                }
            }
            return technologies;
        }

        public static List<Invention> LoadInventions(string inventionspath, List<Technology> techs)
        {
            string[] files = { "army_inventions", "navy_inventions", "commerce_inventions", "culture_inventions", "industry_inventions" };
            List<Invention> inventions = new List<Invention>();
            for (int index = 0; index < files.Length; index++)
            {
                using (StreamReader reader = new StreamReader(inventionspath + files[index] + ".txt"))
                {
                    char[] ignore = { '=', '{', '}' };
                    string currentline, temp = string.Empty, name = string.Empty, target = string.Empty;
                    UInt16 year = 0, maneuver;
                    decimal organisation, attack, defence, support, experience, supply_consumption;
                    List<Technology> requirements = new List<Technology>();
                    List<Effect> effects = new List<Effect>();
                    bool complexlimit = false, scopestart = false, limitscope = false, effectscope = false, targetscope = false, targetscopestart = false;
                    bool organisationscope, attackscope, defencescope, supportscope, supply_consumptionscope, maneuverscope;
                    organisationscope = attackscope = defencescope = supportscope = supply_consumptionscope = maneuverscope = false;
                    organisation = attack = defence = support = supply_consumption = maneuver = 0;
                    int scope = 0;
                    // scopes
                    // 0 - inventionnames
                    // 1 - details
                    // 2 - limit, chance or effect
                    // 3 - limit extra scopes, chance modifiers, effect target scope

                    while (!reader.EndOfStream)
                    {
                        currentline = reader.ReadLine();
                        if (!limitscope && !targetscope) temp = string.Empty;
                        for (int i = 0; i < currentline.Length; i++)
                        {
                            switch (currentline[i])
                            {
                                case '#':
                                    i = currentline.Length;
                                    break;
                                case '\t':
                                    break;
                                case ' ':
                                    break;
                                case '=':
                                    if (scope < 3)
                                    {
                                        scopestart = true;
                                        if (scope == 0)
                                        {
                                            name = Trim(temp, ignore);
                                            requirements = new List<Technology>();
                                            effects = new List<Effect>();
                                            temp = string.Empty;
                                        }
                                        else if (scope == 1)
                                        {
                                            switch (Trim(temp, ignore).ToLower())
                                            {
                                                case "limit":
                                                    limitscope = true;
                                                    temp = string.Empty;
                                                    break;
                                                case "effect":
                                                    effectscope = true;
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                        else if (scope == 2)
                                        {
                                            if (effectscope) targetscopestart = true;
                                            else if (limitscope && !complexlimit)
                                            {
                                                if (techs.Any(x => x.Name == (Trim(temp, ignore).ToLower()))) requirements.Add(techs.SingleOrDefault(x => x.Name == (Trim(temp, ignore).ToLower())));
                                                else
                                                {
                                                    YearEntryPopup popup = new YearEntryPopup(name);
                                                    popup.ShowDialog();
                                                    year = popup.year;
                                                    requirements.Clear();
                                                    complexlimit = true;
                                                }
                                            }
                                        }
                                    }
                                    else if (targetscope)
                                    {
                                        if (scope != 3) throw new FileLoadException();
                                        else
                                        {
                                            scopestart = true;
                                            switch (Trim(temp, ignore).ToLower())
                                            {
                                                case "attack":
                                                    attackscope = true;
                                                    break;
                                                case "defence":
                                                    defencescope = true;
                                                    break;
                                                case "support":
                                                    supportscope = true;
                                                    break;
                                                case "maneuver":
                                                    maneuverscope = true;
                                                    break;
                                                case "supply_consumption":
                                                    supply_consumptionscope = true;
                                                    break;
                                                case "default_organisation":
                                                    organisationscope = true;
                                                    break;
                                                default:
                                                    break;
                                            }
                                            temp = string.Empty;
                                        }
                                    }
                                    break;
                                case '{':
                                    if (scope < 3)
                                    {
                                        if (scopestart)
                                        {
                                            scopestart = false;
                                            if (targetscopestart)
                                            {
                                                targetscopestart = false;
                                                targetscope = true;
                                                target = Trim(temp, ignore);
                                                temp = string.Empty;
                                            }
                                        }
                                        else throw new FileLoadException();
                                    }
                                    scope++;
                                    goto default;
                                case '}':
                                    if (scope == 1)
                                    {
                                        if (requirements.Count > 0) inventions.Add(new Invention(name, effects, requirements: requirements));
                                        else
                                        {
                                            inventions.Add(new Invention(name, effects, year));
                                            requirements.Clear();
                                            complexlimit = false;
                                        }
                                    }
                                    scope--;
                                    if (targetscope)
                                    {
                                        if (attackscope)
                                        {
                                            attackscope = false;
                                            attack = decimal.Parse(temp);
                                        }
                                        else if (defencescope)
                                        {
                                            defencescope = false;
                                            defence = decimal.Parse(temp);
                                        }
                                        else if (supportscope)
                                        {
                                            supportscope = false;
                                            support = decimal.Parse(temp);
                                        }
                                        else if (maneuverscope)
                                        {
                                            maneuverscope = false;
                                            maneuver = UInt16.Parse(temp);
                                        }
                                        else if (supply_consumptionscope)
                                        {
                                            supply_consumptionscope = false;
                                            supply_consumption = decimal.Parse(temp);
                                        }
                                        else if (organisationscope)
                                        {
                                            organisationscope = false;
                                            organisation = decimal.Parse(temp);
                                        }
                                        targetscope = false;
                                        effects.Add(new Effect(target, organisation, attack, defence, support, maneuver, supply_consumption));
                                        organisation = attack = defence = support = experience = supply_consumption = maneuver = 0;
                                    }
                                    else if(effectscope) effectscope = false; 
                                    else if(limitscope) limitscope = false;
                                    temp = string.Empty;
                                    if (scope < 0) throw new FileLoadException();
                                    goto default;
                                default:
                                    if (scopestart) scopestart = false;
                                    if (targetscopestart)
                                    {
                                        targetscopestart = false;
                                        temp = string.Empty;
                                    }
                                    if (scope > 0 && currentline[i] != '-')
                                    {
                                        if (attackscope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    attackscope = false;
                                                    attack = decimal.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                        else if (defencescope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    defencescope = false;
                                                    defence = decimal.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                        else if (supportscope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    supportscope = false;
                                                    support = decimal.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                        else if (maneuverscope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    maneuverscope = false;
                                                    maneuver = UInt16.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                        else if (supply_consumptionscope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    supply_consumptionscope = false;
                                                    supply_consumption = decimal.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                        else if (organisationscope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    organisationscope = false;
                                                    organisation = decimal.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                    }
                                    temp += currentline[i];
                                    break;
                            }
                        }
                    }
                }
            }
            return inventions;
        }
        public static List<Unit> LoadUnits(string unitspath)
        {
            string[] files = Directory.GetFiles(unitspath);
            List<Unit> units = new List<Unit>();
            for (int index = 0; index < files.Length; index++)
            {
                using (StreamReader reader = new StreamReader(files[index]))
                {
                    char[] ignore = { '=', '{', '}' };
                    string currentline, temp = string.Empty, name = string.Empty, target = string.Empty;
                    UInt16 maneuver;
                    decimal organisation, attack, defence, discipline, support, supply_consumption;
                    bool scopestart = false, typescope = false;
                    bool organisationscope, attackscope, defencescope, disciplinescope, supportscope, supply_consumptionscope, maneuverscope;
                    organisationscope = attackscope = defencescope = disciplinescope = supportscope = supply_consumptionscope = maneuverscope = false;
                    organisation = attack = defence = discipline = support = supply_consumption = maneuver = 0;
                    int scope = 0;
                    // scopes
                    // 0 - unitname
                    // 1 - details
                    // 2 - supply_cost and build_cost

                    while (!reader.EndOfStream)
                    {
                        if (typescope)
                        {
                            if (Trim(temp, ignore) == "land") typescope = false;
                            else  break;
                        }
                        currentline = reader.ReadLine();
                        if (!organisationscope && !attackscope && !defencescope && !disciplinescope && !supportscope && !supply_consumptionscope && !maneuverscope) temp = string.Empty;
                        for (int i = 0; i < currentline.Length; i++)
                        {
                            switch (currentline[i])
                            {
                                case '#':
                                    i = currentline.Length;
                                    break;
                                case '\t':
                                    break;
                                case ' ':
                                    break;
                                case '=':
                                    if (scope == 0)
                                    {
                                        scopestart = true;
                                        name = Trim(temp, ignore);
                                    }
                                    else if (scope == 1)
                                    {
                                        scopestart = true;
                                        switch (Trim(temp, ignore).ToLower())
                                        {
                                            case "type":
                                                typescope = true;
                                                break;
                                            case "attack":
                                                attackscope = true;
                                                break;
                                            case "defence":
                                                defencescope = true;
                                                break;
                                            case "discipline":
                                                disciplinescope = true;
                                                break;
                                            case "support":
                                                supportscope = true;
                                                break;
                                            case "maneuver":
                                                maneuverscope = true;
                                                break;
                                            case "supply_consumption":
                                                supply_consumptionscope = true;
                                                break;
                                            case "default_organisation":
                                                organisationscope = true;
                                                break;
                                            default:
                                                break;
                                        }
                                        temp = string.Empty;
                                    }
                                    break;
                                case '{':
                                    if (scope < 2)
                                    {
                                        if (scopestart) scopestart = false;
                                        else throw new FileLoadException();
                                    }
                                    scope++;
                                    goto default;
                                case '}':
                                    if (scope == 1)
                                    {
                                        if (attackscope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    attackscope = false;
                                                    attack = decimal.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                        else if (defencescope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    defencescope = false;
                                                    defence = decimal.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                        else if (disciplinescope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    disciplinescope = false;
                                                    discipline = decimal.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                        else if (supportscope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    supportscope = false;
                                                    support = decimal.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                        else if (maneuverscope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    maneuverscope = false;
                                                    maneuver = UInt16.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                        else if (supply_consumptionscope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    supply_consumptionscope = false;
                                                    supply_consumption = decimal.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                        else if (organisationscope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    organisationscope = false;
                                                    organisation = decimal.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                        units.Add(new Unit(name, organisation, attack, defence, discipline, support, maneuver, supply_consumption));
                                    }
                                    scope--;
                                    temp = string.Empty;
                                    if (scope < 0) throw new FileLoadException();
                                    goto default;
                                default:
                                    if (scopestart) scopestart = false;
                                    if (scope > 0 && currentline[i] != '-')
                                    {
                                        if (attackscope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    attackscope = false;
                                                    attack = decimal.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                        else if (defencescope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    defencescope = false;
                                                    defence = decimal.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                        else if (disciplinescope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    disciplinescope = false;
                                                    discipline = decimal.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                        else if (supportscope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    supportscope = false;
                                                    support = decimal.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                        else if (maneuverscope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    maneuverscope = false;
                                                    maneuver = UInt16.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                        else if (supply_consumptionscope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    supply_consumptionscope = false;
                                                    supply_consumption = decimal.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                        else if (organisationscope)
                                        {
                                            if (!char.IsNumber(currentline[i]))
                                            {
                                                if (currentline[i] != '.' || temp.Contains('.'))
                                                {
                                                    organisationscope = false;
                                                    organisation = decimal.Parse(temp);
                                                    temp = string.Empty;
                                                }
                                            }
                                        }
                                    }
                                    temp += currentline[i];
                                    break;
                            }
                        }
                    }
                }
            }
            return units;
        }

        private static string Trim(string temp, char[] remove)
        {
            foreach (char item in remove)
            {
                temp = temp.Replace(item.ToString(), string.Empty);
            }
            return temp;
        }
    }
}
