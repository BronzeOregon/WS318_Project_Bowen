using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.IdentityModel.Tokens;

namespace WS318_Project_Bowen
{
    /// <summary>
    /// Interaction logic for AddWeaponWindow.xaml
    /// </summary>
    public partial class AddWeaponWindow : Window
    {
        GSchoolCWs318ProjectBowenDatabase1MdfContext dbContext = new GSchoolCWs318ProjectBowenDatabase1MdfContext();

        SpecialRule selection = new SpecialRule();


        List<SpecialRule> AddedRules = new List<SpecialRule> { };

        public AddWeaponWindow()
        {
            InitializeComponent();
            setDataContext();
        }

        private void RangedWeaponCheck_Checked(object sender, RoutedEventArgs e)
        {
            MeleeWeaponCheck.IsChecked = false;
            MeleeEntry.Visibility = Visibility.Collapsed;
            RangedEntry.Visibility = Visibility.Visible;
        }

        private void MeleeWeaponCheck_Checked(object sender, RoutedEventArgs e)
        {
            RangedWeaponCheck.IsChecked = false;
            RangedEntry.Visibility = Visibility.Collapsed;
            MeleeEntry.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
            {

            }

            if (RangedWeaponCheck.IsChecked == true)
            {
                //gather properties from input in ranged weapon boxes.
                RangedWeapon RW = new RangedWeapon();
                RW.Name = WeaponName.Text;
                RW.Range = int.Parse(RangeValue.Text);
                RW.Fp = int.Parse(FPValue.Text);
                RW.Rs = int.Parse(RSValue.Text);
                RW.Ap = int.Parse(RangedAPValue.Text);
                RW.D = int.Parse(RangedDValue.Text);
                RW.Traits = RangedTraitsValue.Text;
                

                try
                {
                    using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                    {
                        //increment ID to place in table by 1, try and add weapon to table, cycle through special rules and create m-m resolution entries for each that will enable them to be pulled later.
                        RW.Id = context.RangedWeapons.Count() + 1;
                        var RangedWeapon = RW;
                        context.RangedWeapons.Add(RangedWeapon);
                        context.SaveChanges();
                        ConfirmationLabel.Content = RangedWeapon.Name + " added Successfully!";

                        foreach (SpecialRule s in AddedRules)
                        {
                            RangedWeapon = context.RangedWeapons.Find(RangedWeapon.Id);
                            if (RangedWeapon != null)
                            {
                                var RWSR = new RangedWeaponSpecialRule();
                                RWSR.Id = context.RangedWeaponSpecialRules.Count()+1;
                                RWSR.RangedWeaponId = RangedWeapon.Id;
                                RWSR.SpecialRuleId = s.Id;
                                context.RangedWeaponSpecialRules.Add(RWSR);
                                context.SaveChanges();
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            else if (MeleeWeaponCheck.IsChecked == true)
            {
                //gather properties from input in melee weapon boxes
                MeleeWeapon MW = new MeleeWeapon();
                MW.Name = WeaponName.Text;
                MW.Im = (IMValue.Text);
                MW.Am = (AMValue.Text);
                MW.As = ASValue.Text;
                MW.Ap = int.Parse(MeleeAPValue.Text);
                MW.D = int.Parse(MeleeDValue.Text);
                MW.Traits = MeleeTraitsValue.Text;


                try
                {
                    using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                    {
                        //increment ID to place in table by 1, try and add weapon to table, cycle through special rules and create m-m resolution entries for each that will enable them to be pulled later.
                        MW.Id = context.MeleeWeapons.Count() + 1;
                        var MeleeWeapon = MW;
                        context.MeleeWeapons.Add(MeleeWeapon);
                        context.SaveChanges();
                        ConfirmationLabel.Content = MeleeWeapon.Name + " added Successfully!";

                        foreach (SpecialRule s in AddedRules)
                        {
                            MeleeWeapon = context.MeleeWeapons.Find(MeleeWeapon.Id);
                            if (MeleeWeapon != null)
                            {
                                var RWSR = new MeleeWeaponSpecialRule();
                                RWSR.Id = context.MeleeWeaponSpecialRules.Count() + 1;
                                RWSR.MeleeWeaponId = MeleeWeapon.Id;
                                RWSR.SpecialRuleId = s.Id;
                                context.MeleeWeaponSpecialRules.Add(RWSR);
                                context.SaveChanges();
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

            } else { ConfirmationLabel.Content = "Error, please select a weapon type."; } //tell user to select radio button options to proceed.
        }

        private void SpecialRuleGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selection = (SpecialRule)SpecialRuleGrid.SelectedItem;
            
        }

        private void AddSpecialRule_Click(object sender, RoutedEventArgs e)
        {
            AddedRules.Add(selection);
            AddedRulesGrid.ItemsSource = AddedRules;
        }

        public void setDataContext()
        {
            using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
            {
                var allRules = context.SpecialRules.ToList();
                SpecialRuleGrid.ItemsSource = allRules;
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBar.Text.Equals("") || SearchBar.Text.Equals(null))
            {
                setDataContext();
            }
            else
            {
                string search = SearchBar.Text;
                using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                {
                    var filteredEntities = context.SpecialRules.Where(s => s.Name.StartsWith(search));
                    //if (SpecialRuleGrid.IsLoaded){ SpecialRuleGrid.ItemsSource = filteredEntities.ToList(); }
                    SpecialRuleGrid.ItemsSource = filteredEntities.ToList();
                }
            }
        }
    }
}
