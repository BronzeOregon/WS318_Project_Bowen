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

                    NonVehicleModel nvm = new NonVehicleModel();
                    try
                    {
                        int highestID = context.NonVehicleModels.Max(rwsr => rwsr.Id);

                        nvm.Id = highestID + 1;
                    }
                    catch (Exception ex) { Debug.WriteLine(ex); nvm.Id = context.NonVehicleModels.Count() + 1; }


                    nvm.Name = ModelName.Text;
                    nvm.M = int.Parse(MValue.Text);
                    nvm.Ws = int.Parse(WSValue.Text);
                    nvm.Bs = int.Parse(BSValue.Text);
                    nvm.S = int.Parse(SValue.Text);
                    nvm.T = int.Parse(TValue.Text);
                    nvm.W = int.Parse(WValue.Text);
                    nvm.I = int.Parse(IValue.Text);
                    nvm.A = int.Parse(AValue.Text);
                    nvm.Ld = int.Parse(LdValue.Text);
                    nvm.Cl = int.Parse(ClValue.Text);
                    nvm.Wp = int.Parse(WpValue.Text);
                    nvm.Int = int.Parse(IntValue.Text);
                    nvm.Sv = SvValue.Text;
                    nvm.Inv = InvValue.Text;
                    nvm.Type = TypeValue.Text;
                    nvm.Traits = TraitsValue.Text;

                    try
                    {
                        var nvmAdd = nvm;
                        context.NonVehicleModels.Add(nvmAdd);
                        context.SaveChanges();
                        ConfirmationLabel.Content = nvmAdd.Name + " added Successfully!";

                        foreach (RangedWeapon rw in AddedRange)
                        {
                            nvmAdd = context.NonVehicleModels.Find(nvm.Id);
                            if (nvmAdd != null)
                            {
                                var nvmRW = new NonVehicleModelRangedWeapon();
                                try
                                {
                                    int highestRWID = context.NonVehicleModelRangedWeapons.Max(r => r.Id);
                                    nvmRW.Id = highestRWID + 1;
                                }
                                catch (Exception ex)
                                {
                                    nvmRW.Id = context.NonVehicleModelRangedWeapons.Count() + 1;
                                }

                                nvmRW.RangedWeaponId = rw.Id;
                                nvmRW.NonVehicleModelId = nvm.Id;

                                context.NonVehicleModelRangedWeapons.Add(nvmRW);
                                context.SaveChanges();


                            }
                        }

                        foreach (MeleeWeapon rw in AddedMelee)
                        {
                            nvmAdd = context.NonVehicleModels.Find(nvm.Id);
                            if (nvmAdd != null)
                            {
                                var nvmRW = new NonVehicleModelMeleeWeapon();
                                try
                                {
                                    int highestRWID = context.NonVehicleModelMeleeWeapons.Max(r => r.Id);
                                    nvmRW.Id = highestRWID + 1;
                                }
                                catch (Exception ex)
                                {
                                    nvmRW.Id = context.NonVehicleModelMeleeWeapons.Count() + 1;
                                }

                                nvmRW.MeleeWeaponIdtoModel = rw.Id;
                                nvmRW.NonVehicleModelId = nvm.Id;

                                context.NonVehicleModelMeleeWeapons.Add(nvmRW);
                                context.SaveChanges();


                            }
                        }

                        foreach (SpecialRule s in AddedRules)
                        {
                            nvmAdd = context.NonVehicleModels.Find(nvm.Id);
                            if (nvmAdd != null)
                            {
                                var nvmSR = new NonVehicleModelSpecialRule();
                                try
                                {
                                    int highestSRID = context.NonVehicleModelSpecialRules.Max(rwsr => rwsr.Id);
                                    nvmSR.Id = highestSRID + 1;
                                }
                                catch (Exception ex)
                                {
                                    nvmSR.Id = context.NonVehicleModelSpecialRules.Count() + 1;
                                }

                                nvmSR.NonVehicleModelId = nvmAdd.Id;
                                nvmSR.SpecialRulesId = s.Id;
                                context.NonVehicleModelSpecialRules.Add(nvmSR);
                                context.SaveChanges();
                            }

                        }
                    }
                    catch (Exception ex) { Debug.WriteLine(ex.Message); }
                }
                else if (VehicleCheck.IsChecked == true) 
                {
                    VehicleModel vm = new VehicleModel();
                    try
                    {
                        int highestID = context.VehicleModels.Max(rwsr => rwsr.Id);

                        vm.Id = highestID + 1;
                    }
                    catch (Exception ex) { Debug.WriteLine(ex); vm.Id = context.VehicleModels.Count() + 1; }


                    vm.Name = ModelName.Text;
                    vm.M = int.Parse(VMValue.Text);
                    vm.Bs = int.Parse(VBSValue.Text);
                    vm.FrontAv = int.Parse(FrontAVValue.Text);
                    vm.SideAv = int.Parse(SideAVValue.Text);
                    vm.RearAv = int.Parse(RearAVValue.Text);
                    vm.Hp = int.Parse(HPValue.Text);
                    vm.Capacity = int.Parse(CapacityValue.Text);
                    vm.Type = VTypesValue.Text;
                    vm.Traits = VTraitsValue.Text;

                    try
                    {
                        var nvmAdd = vm;
                        context.VehicleModels.Add(nvmAdd);
                        context.SaveChanges();
                        ConfirmationLabel.Content = nvmAdd.Name + " added Successfully!";

                        foreach (RangedWeapon rw in AddedRange)
                        {
                            nvmAdd = context.VehicleModels.Find(vm.Id);
                            if (nvmAdd != null)
                            {
                                var nvmRW = new VehicleModelRangedWeapon();
                                try
                                {
                                    int highestRWID = context.VehicleModelRangedWeapons.Max(r => r.Id);
                                    nvmRW.Id = highestRWID + 1;
                                }
                                catch (Exception ex)
                                {
                                    nvmRW.Id = context.VehicleModelRangedWeapons.Count() + 1;
                                }

                                nvmRW.RangedWeaponId = rw.Id;
                                nvmRW.VehicleModelId = vm.Id;

                                context.VehicleModelRangedWeapons.Add(nvmRW);
                                context.SaveChanges();


                            }
                        }

                        foreach (SpecialRule s in AddedRules)
                        {
                            nvmAdd = context.VehicleModels.Find(vm.Id);
                            if (nvmAdd != null)
                            {
                                var nvmSR = new VehicleModelSpecialRule();
                                try
                                {
                                    int highestSRID = context.VehicleModelSpecialRules.Max(rwsr => rwsr.Id);
                                    nvmSR.Id = highestSRID + 1;
                                }
                                catch (Exception ex)
                                {
                                    nvmSR.Id = context.VehicleModelSpecialRules.Count() + 1;
                                }

                                nvmSR.VehicleModelId = nvmAdd.Id;
                                nvmSR.SpecialRulesId = s.Id;
                                context.VehicleModelSpecialRules.Add(nvmSR);
                                context.SaveChanges();
                            }

                        }
                    }
                    catch (Exception ex) { Debug.WriteLine(ex.Message); }
                }
            }
        }
    }
}
