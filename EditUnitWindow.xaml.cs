using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace WS318_Project_Bowen
{
    /// <summary>
    /// Interaction logic for EditUnitWindow.xaml
    /// </summary>
    public partial class EditUnitWindow : Window
    {
        Unit selectedUnit = new Unit();

        NonVehicleModel nvm = new NonVehicleModel();

        VehicleModel vm = new VehicleModel();

        ObservableCollection<Unit> nvunitList = new ObservableCollection<Unit>();

        ObservableCollection<Unit> vunitList = new ObservableCollection<Unit>();

        ObservableCollection<NonVehicleModel> nvmAdded = new ObservableCollection<NonVehicleModel>();

        ObservableCollection<VehicleModel> vmAdded = new ObservableCollection<VehicleModel>();

        public EditUnitWindow()
        {
            InitializeComponent();
        }

        public void setNVUContext()
        {
            using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
            {
                nvunitList.Clear();
                List<int> unitIds = new List<int>();
                foreach (var unvm in context.UnitNonVehicleModels)
                {
                    unitIds.Add(unvm.UnitId);
                }

                foreach (var unitID in unitIds)
                {
                    var match = 
                        from unit in context.Units
                        where unit.Id == unitID
                        select unit;

                    foreach (var unit in match)
                    {
                        try
                        {
                            if (!nvunitList.Contains(unit))
                                nvunitList.Add(unit);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    
                }

                if (nvunitList.Count > 0)
                {
                    UnitGrid.ItemsSource = nvunitList;
                } else
                {
                    UnitGrid.ItemsSource = null;
                }
               
            }
        }

        public void setVUContext()
        {
            using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
            {
                vunitList.Clear();
                List<int> unitIds = new List<int>();
                foreach (var uvm in context.UnitVehicleModels)
                {
                    unitIds.Add(uvm.UnitId);
                }

                foreach (var unitID in unitIds)
                {
                    var match =
                        from unit in context.Units
                        where unit.Id == unitID
                        select unit;

                    foreach (var unit in match)
                    {
                        try
                        {
                            if (!nvunitList.Contains(unit))
                                vunitList.Add(unit);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                }

                if (vunitList.Count > 0)
                {
                    UnitGrid.ItemsSource = vunitList;
                } else
                {
                    UnitGrid.ItemsSource = null;
                }

            }
        }

        public void setNVModelContext()
        {
            using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
            {
                var nvm = context.NonVehicleModels.ToList();
                ModelGrid.ItemsSource = nvm;
            }
        }

        public void setVModelContext()
        {
            using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
            {
                var vm = context.VehicleModels.ToList();
                ModelGrid.ItemsSource = vm;
            }
        }

        private void NonVehicleCheck_Checked(object sender, RoutedEventArgs e)
        {
            vmAdded.Clear();
            nvmAdded.Clear();
            setNVUContext();
            setNVModelContext();
        }

        private void VehicleCheck_Checked(object sender, RoutedEventArgs e)
        {
            nvmAdded.Clear();
            vmAdded.Clear();
            setVUContext();
            setVModelContext();
        }

        private void UnitSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NonVehicleCheck.IsChecked == true)
            {
                string search = UnitSearchBar.Text;
                using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                {

                    var filteredEntities = nvunitList.Where(s => s.Name.StartsWith(search));

                    UnitGrid.ItemsSource = filteredEntities.ToList();
                                        
                }
                if (UnitSearchBar.Text.Equals("") || UnitSearchBar.Text.IsNullOrEmpty())
                {
                    setNVUContext();
                }
            }
            else if (VehicleCheck.IsChecked == true)
            {
                string search = UnitSearchBar.Text;
                using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                {
                    var filteredEntities = vunitList.Where(s => s.Name.StartsWith(search));
                    UnitGrid.ItemsSource = filteredEntities.ToList();
                }
                if (UnitSearchBar.Text.Equals("") || UnitSearchBar.Text.IsNullOrEmpty())
                {
                    setVUContext();
                }
            }
        }

        private void PickUnit_Click(object sender, RoutedEventArgs e)
        {
            if (NonVehicleCheck.IsChecked == true)
            {
                selectedUnit = (Unit)UnitGrid.SelectedItem;

                if (selectedUnit != null)
                {
                    UnitName.Text = selectedUnit.Name;

                    using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                    {
                        var unvm =
                            from u in context.UnitNonVehicleModels
                            where u.UnitId == selectedUnit.Id
                            select u;

                        

                        foreach (var u in unvm)
                        {
                            var m = 
                                from i in context.NonVehicleModels
                                where i.Id == u.NonVehicleModelId
                                select i;

                            foreach (var v in m)
                            {
                                nvmAdded.Add(v);
                            }
                        }

                        AddedModelsGrid.ItemsSource = nvmAdded;
                    }
                }

            } else if (VehicleCheck.IsChecked == true)
            {
                selectedUnit = (Unit)UnitGrid.SelectedItem;

                if (selectedUnit != null)
                {
                    UnitName.Text = selectedUnit.Name;

                    using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                    {
                        var uvm =
                            from u in context.UnitVehicleModels
                            where u.UnitId == selectedUnit.Id
                            select u;


                        foreach (var u in uvm)
                        {
                            var m =
                                from i in context.VehicleModels
                                where i.Id == u.VehicleModelId
                                select i;

                            foreach (var v in m)
                            {
                                vmAdded.Add(v);
                            }
                        }

                        AddedModelsGrid.ItemsSource = vmAdded;
                    }
                }

            } else
            {
                ConfirmationLabel.Content = "Please ensure you have a unit type checked!";
            }
        }

        private void UnitGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NonVehicleCheck.IsChecked == true)
            {
                string search = SearchBar.Text;
                using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                {
                    var filteredEntities = context.NonVehicleModels.Where(s => s.Name.StartsWith(search));
                    ModelGrid.ItemsSource = filteredEntities.ToList();

                }
                if (SearchBar.Text.Equals("") || SearchBar.Text.IsNullOrEmpty())
                {
                    setNVModelContext();
                }
            }
            else if (VehicleCheck.IsChecked == true)
            {
                string search = SearchBar.Text;
                using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                {
                    var filteredEntities = context.VehicleModels.Where(s => s.Name.StartsWith(search));
                    ModelGrid.ItemsSource = filteredEntities.ToList();
                }
                if (SearchBar.Text.Equals("") || SearchBar.Text.IsNullOrEmpty())
                {
                    setVModelContext();
                }
            }
        }

        private void AddModel_Click(object sender, RoutedEventArgs e)
        {
            if (NonVehicleCheck.IsChecked == true)
            {
                try
                { 
                    nvm = (NonVehicleModel)ModelGrid.SelectedItem;
                    nvmAdded.Add(nvm);
                    ModelGrid.ItemsSource = nvmAdded;

                } catch (Exception ex) { Debug.WriteLine(ex); }

            } else if (VehicleCheck.IsChecked == true)
            {
                try
                {
                    vm = (VehicleModel)ModelGrid.SelectedItem;
                    vmAdded.Add(vm);
                    ModelGrid.ItemsSource = vmAdded;
                }
                catch (Exception ex) { Debug.WriteLine(ex); }

            } else
            {
                ConfirmationLabel.Content = "Please select a unit/model type.";
            }
        }

        private void RemoveModel_Click(object sender, RoutedEventArgs e)
        {
            if (NonVehicleCheck.IsChecked == true)
            {
                try
                { nvmAdded.Remove(nvm); }
                catch (Exception ex) { Debug.WriteLine(ex); }

            }
            else if (VehicleCheck.IsChecked == true)
            {
                try
                { vmAdded.Remove(vm); }
                catch (Exception ex) { Debug.WriteLine(ex); }

            }
            else
            {
                ConfirmationLabel.Content = "Please select a unit/model type.";
            }
        }
        

        private void ModelGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NonVehicleCheck.IsChecked == true)
            {
                try
                { nvm = (NonVehicleModel)ModelGrid.SelectedItem;}
                catch (Exception ex) { Debug.WriteLine(ex); }

            } else if (VehicleCheck.IsChecked == true)
            {
                try
                {
                    vm = (VehicleModel)ModelGrid.SelectedItem;
                } catch (Exception ex) { Debug.WriteLine(ex);  }

            } else
            {
                ConfirmationLabel.Content = "No Unit/Model type selected. Please select a type.";
            }
        }

        private void AddedModelsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NonVehicleCheck.IsChecked == true)
            {
                try
                { 
                    nvm = (NonVehicleModel)AddedModelsGrid.SelectedItem; 
                    
                }
                catch (Exception ex) { Debug.WriteLine(ex); }

            }
            else if (VehicleCheck.IsChecked == true)
            {
                try
                {
                    vm = (VehicleModel)AddedModelsGrid.SelectedItem;
                    
                }
                catch (Exception ex) { Debug.WriteLine(ex);  }

            }
            else
            {
                ConfirmationLabel.Content = "No Unit/Model type selected. Please select a type.";
            }
        }

        private void UpdateUnit_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (NonVehicleCheck.IsChecked == true)
                {
                    //gather properties to update Unit.
                    Unit u = new Unit();
                    u.Name = UnitName.Text;
                    u.ModelCount = nvmAdded.Count();

                    try
                    {
                        using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                        {


                            //Update selected Unit and associated Unit to NonVehicleModel entities, removing the obsolete entries.
                            u.Id = selectedUnit.Id;
                            var un = u;

                            context.Units.Update(u);
                            context.SaveChanges();
                            ConfirmationLabel.Content = u.Name + " updated Successfully!";

                            List<int> extantNVM = new List<int>();

                            foreach (NonVehicleModel s in nvmAdded)
                            {
                                un = context.Units.Find(u.Id);                                                              

                                if (u != null)
                                {
                                    IQueryable<UnitNonVehicleModel> unvmSearch =
                                        (from unvm in context.UnitNonVehicleModels
                                         where unvm.UnitId == un.Id
                                         select unvm);


                                    var unv = new UnitNonVehicleModel();
                                    try
                                    {
                                        int highestNVMID = context.UnitNonVehicleModels.Max(r => r.Id);
                                        unv.Id = highestNVMID + 1;
                                    }
                                    catch { unv.Id = context.UnitNonVehicleModels.Count() + 1; }

                                    unv.UnitId = un.Id;
                                    unv.NonVehicleModelId = s.Id;

                                    List<int> unvmIds = new List<int>();

                                    foreach (UnitNonVehicleModel a in unvmSearch)
                                    {
                                        Debug.WriteLine("Compare " + u.Id + " and " + unv.UnitId );
                                        Debug.WriteLine("Compare " + s.Id + " and " + unv.NonVehicleModelId);
                                        if (unv.UnitId == a.UnitId && unv.NonVehicleModelId == a.NonVehicleModelId)
                                        {
                                            extantNVM.Add(a.NonVehicleModelId);
                                            break;
                                        }
                                        else
                                        {
                                                unvmIds.Add(a.Id);
                                        }

                                    }

                                    if (!unvmIds.Contains(unv.Id) && !extantNVM.Contains(unv.NonVehicleModelId))
                                    {
                                        context.UnitNonVehicleModels.Add(unv);
                                        context.SaveChanges();
                                    }
                                }

                            }

                            List<int> AddedNVMIds = new List<int>();

                            foreach (NonVehicleModel a in nvmAdded)
                            {
                                AddedNVMIds.Add(a.Id);
                            }

                            IQueryable<UnitNonVehicleModel> obsoleteunvm =
                                                        (from unvm in context.UnitNonVehicleModels
                                                         where unvm.UnitId == u.Id && !AddedNVMIds.Contains(unvm.NonVehicleModelId)
                                                         select unvm);
                            if (obsoleteunvm != null)
                            {
                                foreach (UnitNonVehicleModel a in obsoleteunvm)
                                { context.UnitNonVehicleModels.Remove(a); }

                                context.SaveChanges();
                            }

                        }

                        setNVModelContext();
                        setNVUContext();
                        
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                }
                else if (VehicleCheck.IsChecked == true)
                {
                    //gather properties to update Unit.
                    Unit u = new Unit();
                    u.Name = UnitName.Text;
                    u.ModelCount = vmAdded.Count();

                    try
                    {
                        using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                        {


                            //Update selected Unit and associated Unit to NonVehicleModel entities, removing the obsolete entries.
                            u.Id = selectedUnit.Id;
                            var un = u;

                            context.Units.Update(u);
                            context.SaveChanges();
                            ConfirmationLabel.Content = u.Name + " updated Successfully!";

                            List<int> extantNVM = new List<int>();

                            foreach (VehicleModel s in vmAdded)
                            {
                                un = context.Units.Find(u.Id);

                                if (u != null)
                                {
                                    IQueryable<UnitVehicleModel> uvmSearch =
                                        (from unvm in context.UnitVehicleModels
                                         where unvm.UnitId == un.Id
                                         select unvm);


                                    var unv = new UnitVehicleModel();
                                    try
                                    {
                                        int highestVMID = context.UnitVehicleModels.Max(r => r.Id);
                                        unv.Id = highestVMID + 1;
                                    }
                                    catch { unv.Id = context.UnitVehicleModels.Count() + 1; }

                                    unv.UnitId = un.Id;
                                    unv.VehicleModelId = s.Id;

                                    List<int> uvmIds = new List<int>();

                                    foreach (UnitVehicleModel a in uvmSearch)
                                    {
                                        Debug.WriteLine("Compare " + u.Id + " and " + unv.UnitId);
                                        Debug.WriteLine("Compare " + s.Id + " and " + unv.VehicleModelId);
                                        if (unv.UnitId == a.UnitId && unv.VehicleModelId == a.VehicleModelId)
                                        {
                                            extantNVM.Add(a.VehicleModelId);
                                            break;
                                        }
                                        else
                                        {
                                            uvmIds.Add(a.Id);
                                        }

                                    }

                                    if (!uvmIds.Contains(unv.Id) && !extantNVM.Contains(unv.VehicleModelId))
                                    {
                                        context.UnitVehicleModels.Add(unv);
                                        context.SaveChanges();
                                    }
                                }

                            }

                            List<int> AddedNVMIds = new List<int>();

                            foreach (VehicleModel a in vmAdded)
                            {
                                AddedNVMIds.Add(a.Id);
                            }

                            IQueryable<UnitVehicleModel> obsoleteunvm =
                                                        (from unvm in context.UnitVehicleModels
                                                         where unvm.UnitId == u.Id && !AddedNVMIds.Contains(unvm.VehicleModelId)
                                                         select unvm);
                            if (obsoleteunvm != null)
                            {
                                foreach (UnitVehicleModel a in obsoleteunvm)
                                { context.UnitVehicleModels.Remove(a); }

                                context.SaveChanges();
                            }

                        }

                        setVModelContext();
                        setVUContext();

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }

                }
                else { ConfirmationLabel.Content = "Error, please select a Unit/Model type."; } //tell user to select radio button options to proceed.
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }

        private void DeleteUnit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                {
                    if (NonVehicleCheck.IsChecked == true)
                    {
                        try
                        {
                            IQueryable<UnitNonVehicleModel> rwsr =
                                from rwsrpick in context.UnitNonVehicleModels
                                where rwsrpick.UnitId == selectedUnit.Id
                                select rwsrpick;

                            foreach (UnitNonVehicleModel a in rwsr)
                            {
                                context.UnitNonVehicleModels.Remove(a);
                                context.SaveChanges();
                            }

                            context.Units.Remove(selectedUnit);
                            context.SaveChanges();

                            nvmAdded.Clear();
                            setNVModelContext();
                            setNVUContext();

                        }
                        catch (Exception ex) { Debug.WriteLine(ex.Message); }
                    }
                    else if (VehicleCheck.IsChecked == true)
                    {
                        try
                        {
                            IQueryable<UnitVehicleModel> rwsr =
                                from rwsrpick in context.UnitVehicleModels
                                where rwsrpick.UnitId == selectedUnit.Id
                                select rwsrpick;

                            context.Units.Remove(selectedUnit);
                            context.SaveChanges();

                            foreach (UnitVehicleModel a in rwsr)
                            {
                                context.UnitVehicleModels.Remove(a);
                                context.SaveChanges();
                            }

                            //context.Units.Remove(selectedUnit);
                            //context.SaveChanges();

                            vmAdded.Clear();
                            setVModelContext();
                            setVUContext();

                        }
                        catch (Exception ex) { Debug.WriteLine(ex.Message); }
                    }
                    else
                    {
                        ConfirmationLabel.Content = "Error with deletion.";
                    }


                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }
    }
}
