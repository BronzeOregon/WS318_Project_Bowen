using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WS318_Project_Bowen
{
    /// <summary>
    /// Interaction logic for AddModelWindow.xaml
    /// </summary>

    public partial class AddModelWindow : Window
    {

        SpecialRule selection = new SpecialRule();

        RangedWeapon selectedRange = new RangedWeapon();

        MeleeWeapon selectedMelee = new MeleeWeapon();

        ObservableCollection<SpecialRule> AddedRules = new ObservableCollection<SpecialRule>();

        ObservableCollection<RangedWeapon> AddedRange = new ObservableCollection<RangedWeapon>();

        ObservableCollection<MeleeWeapon> AddedMelee = new ObservableCollection<MeleeWeapon>();

        public AddModelWindow()
        {
            InitializeComponent();
            setDataContext();
        }

        public void setDataContext()
        {
            using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
            {
                var allRules = context.SpecialRules.ToList();
                SpecialRuleGrid.ItemsSource = allRules;
            }
        }

        public void setWeaponContext()
        {
            using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
            {
                var meleeWeps = context.MeleeWeapons.ToList();
                MeleeWeaponGrid.ItemsSource = meleeWeps;
                var rangedWeps = context.RangedWeapons.ToList();
                RangedWeaponGrid.ItemsSource = rangedWeps;
            }
        }

        private void NonVehicleCheck_Checked(object sender, RoutedEventArgs e)
        {
            VehicleCheck.IsChecked = false;
            VehicleEntry.Visibility = Visibility.Collapsed;
            NonVehicleEntry.Visibility = Visibility.Visible;
            MeleeWeaponGrid.Visibility = Visibility.Visible;
            MeleeWeaponGrid.Width = this.Width/2 - 20;
            RangedWeaponGrid.Width = this.Width / 2 - 20;
            AddedMeleeWeaponsGrid.Visibility = Visibility.Visible;
            AddedMeleeWeaponsGrid.Width = this.Width / 2 - 20;
            AddedRangedWeaponsGrid.Width = this.Width / 2 - 20;
            setWeaponContext();
        }

        private void VehicleCheck_Checked(object sender, RoutedEventArgs e)
        {
            NonVehicleCheck.IsChecked = false;
            NonVehicleEntry.Visibility = Visibility.Collapsed;
            VehicleEntry.Visibility = Visibility.Visible;
            MeleeWeaponGrid.Visibility = Visibility.Collapsed;
            AddedMeleeWeaponsGrid.Visibility = Visibility.Collapsed;
            RangedWeaponGrid.Width = this.Width - 20;
            AddedRangedWeaponsGrid.Width = this.Width - 20;
            setWeaponContext() ;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBar.Text.Equals("") || SearchBar.Text.IsNullOrEmpty())
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

        private void AddSpecialRule_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddedRules.Add(selection);
                AddedRulesGrid.ItemsSource = AddedRules;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }

        private void DeleteRuleButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddedRulesGrid.SelectedItem != null)
            {
                try
                {
                    foreach (SpecialRule a in AddedRules)
                    {
                        if (a.Equals(selection.Id))
                        {
                            selection = a;
                        }
                    }

                    AddedRules.Remove(selection);
                }
                catch (Exception ex) { Debug.WriteLine(ex.Message); }
            }
        }

        private void SpecialRuleGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            { selection = (SpecialRule)SpecialRuleGrid.SelectedItem; }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }

        private void AddedRulesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selection = (SpecialRule)AddedRulesGrid.SelectedItem;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }

        private void WeaponSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NonVehicleCheck.IsChecked == true)
            {
                string search = WeaponSearchBar.Text;
                using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                {
                    var filteredEntities = context.RangedWeapons.Where(s => s.Name.StartsWith(search));
                    RangedWeaponGrid.ItemsSource = filteredEntities.ToList();

                    var mfilteredEntities = context.MeleeWeapons.Where(s => s.Name.StartsWith(search));
                    MeleeWeaponGrid.ItemsSource = mfilteredEntities.ToList();
                }
                if (WeaponSearchBar.Text.Equals("") || WeaponSearchBar.Text.IsNullOrEmpty())
                {
                    setWeaponContext();
                }
            }
            else if (VehicleCheck.IsChecked == true)
            {
                string search = WeaponSearchBar.Text;
                using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                {
                    var filteredEntities = context.RangedWeapons.Where(s => s.Name.StartsWith(search));
                    RangedWeaponGrid.ItemsSource = filteredEntities.ToList();
                }
                if (WeaponSearchBar.Text.Equals("") || WeaponSearchBar.Text.IsNullOrEmpty())
                {
                    setWeaponContext();
                }
            }
        }

        private void PickWeapon_Click(object sender, RoutedEventArgs e)
        {
            if (NonVehicleCheck.IsChecked == true)
            {
                using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                {
                    try
                    {
                        if (RangedWeaponGrid.SelectedItem != null)
                        {
                            selectedRange = (RangedWeapon)RangedWeaponGrid.SelectedItem;
                            AddedRange.Add(selectedRange);
                            AddedRangedWeaponsGrid.ItemsSource = AddedRange;
                        }

                        if (MeleeWeaponGrid.SelectedItem != null)
                        {
                            selectedMelee = (MeleeWeapon)MeleeWeaponGrid.SelectedItem;
                            AddedMelee.Add(selectedMelee);
                            AddedMeleeWeaponsGrid.ItemsSource = AddedMelee;

                        }
                    }
                    catch (Exception ex) { Debug.WriteLine(ex.Message); }
                }

            } else if (VehicleCheck.IsChecked == true)
            {

                using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                {
                    try
                    {
                        selectedRange = (RangedWeapon)RangedWeaponGrid.SelectedItem;
                        AddedRange.Add(selectedRange);
                        AddedRangedWeaponsGrid.ItemsSource = AddedRange;
                    }
                catch (Exception ex) { Debug.WriteLine(ex.Message); }
                }

            } else
            {
                ConfirmationLabel.Content = "Error adding weapons.";
            }
                 
        }

        private void DeleteWeaponButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddedRangedWeaponsGrid.SelectedItem != null)
            {
                try
                {
                    foreach (RangedWeapon a in AddedRange)
                    {
                        if (a.Equals(selectedRange.Id))
                        {
                            selectedRange = a;
                        }
                    }

                    AddedRange.Remove(selectedRange);
                }
                catch (Exception ex) { Debug.WriteLine(ex.Message); }
            } else if (AddedMeleeWeaponsGrid.SelectedItem != null)
            {
                try
                {
                    foreach (MeleeWeapon a in AddedMelee)
                    {
                        if (a.Equals(selectedMelee.Id))
                        {
                            selectedMelee = a;
                        }    

                        AddedMelee.Remove(selectedMelee);
                    }
                }catch (Exception ex) { Debug.WriteLine(ex.Message);}
            }
        }

        private void RangedWeaponGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MeleeWeaponGrid.SelectedItem = null;
        }

        private void MeleeWeaponGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RangedWeaponGrid.SelectedItem = null;
        }
                     
        private void AddedRangedWeaponsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddedMeleeWeaponsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SaveModelButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
            {
                if (NonVehicleCheck.IsChecked == true)
                {



                } else if (VehicleCheck.IsChecked == true)
                {



                } else
                {
                    ConfirmationLabel.Content = "Error. No unit designation selected. Please choose Vehicle or Non-Vehicle.";
                }
            }
        }
    }
}
