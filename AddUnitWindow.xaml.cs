using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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
    /// Interaction logic for AddUnitWindow.xaml
    /// </summary>
    public partial class AddUnitWindow : Window
    {

        NonVehicleModel nvmSelection = new NonVehicleModel();

        VehicleModel vmSelection = new VehicleModel();

        ObservableCollection<NonVehicleModel> nvmColl = new ObservableCollection<NonVehicleModel>();

        ObservableCollection<VehicleModel> vmColl = new ObservableCollection<VehicleModel>();

        public AddUnitWindow()
        {
            InitializeComponent();
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
            VehicleCheck.IsChecked = false;
            setNVModelContext();
        }

        private void VehicleCheck_Checked(object sender, RoutedEventArgs e)
        {
            NonVehicleCheck.IsChecked = false;
            setVModelContext();
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
            if (NonVehicleCheck.IsChecked == true && nvmSelection != null)
            {
                try
                {
                    nvmColl.Add(nvmSelection);
                    AddedModelsGrid.ItemsSource = nvmColl;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            else if (VehicleCheck.IsChecked == true && vmSelection != null) 
            {
                try
                {
                    vmColl.Add(vmSelection);
                    AddedModelsGrid.ItemsSource= vmColl;

                } catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            } else
            {
                ConfirmationLabel.Content = "Please select a model.";
            }
        }

        private void ModelGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NonVehicleCheck.IsChecked == true)
            {
                try
                {
                    nvmSelection = (NonVehicleModel)ModelGrid.SelectedItem;
                } catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            } else if (VehicleCheck.IsChecked == true)
            {
                try
                {
                    vmSelection = (VehicleModel)ModelGrid.SelectedItem;
                } catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        private void AddedModelsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CreateUnit_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
            {
                if (NonVehicleCheck.IsChecked == true)
                {
                    Unit nvu = new Unit();

                    nvu.Name = UnitName.Text;

                    //Check for highest extant unit ID and add one, if empty use exception to initiate Id at 1.
                    try
                    {
                        int highestID = context.Units.Max(rwsr => rwsr.Id);

                        nvu.Id = highestID + 1;
                    }
                    catch (Exception ex) { Debug.WriteLine(ex); nvu.Id = context.Units.Count() + 1; }

                    if (!nvmColl.IsNullOrEmpty())
                    {
                        nvu.ModelCount = nvmColl.Count();
                    } else
                    {
                        ConfirmationLabel.Content = "No models to make unit! Please add models.";
                        return;
                    }

                    context.Units.Add(nvu);

                    foreach(var m in nvmColl)
                    {
                        UnitNonVehicleModel unvm = new UnitNonVehicleModel();
                        try
                        {
                            int highestID = context.UnitNonVehicleModels.Max(unvm => unvm.Id);

                            unvm.Id = highestID + 1;
                        }
                        catch (Exception ex) { Debug.WriteLine(ex); unvm.Id = context.UnitNonVehicleModels.Count() + 1; }

                        unvm.UnitId = nvu.Id;
                        unvm.NonVehicleModelId = m.Id;

                        context.UnitNonVehicleModels.Add(unvm);
                    }

                    context.SaveChanges();

                    ConfirmationLabel.Content = "Unit created successfully!";

                }
                else if (VehicleCheck.IsChecked == true)
                {
                    Unit vu = new Unit();

                    vu.Name = UnitName.Text;

                    //Check for highest extant unit ID and add one, if empty use exception to initiate Id at 1.
                    try
                    {
                        int highestID = context.Units.Max(rwsr => rwsr.Id);

                        vu.Id = highestID + 1;
                    }
                    catch (Exception ex) { Debug.WriteLine(ex); vu.Id = context.Units.Count() + 1; }

                    if (!vmColl.IsNullOrEmpty())
                    {
                        vu.ModelCount = vmColl.Count();
                    }
                    else
                    {
                        ConfirmationLabel.Content = "No models to make unit! Please add models.";
                        return;
                    }

                    context.Units.Add(vu);

                    foreach (var m in vmColl)
                    {
                        UnitVehicleModel uvm = new UnitVehicleModel();
                        try
                        {
                            int highestID = context.UnitVehicleModels.Max(uvm => uvm.Id);

                            uvm.Id = highestID + 1;
                        }
                        catch (Exception ex) { Debug.WriteLine(ex); uvm.Id = context.UnitVehicleModels.Count() + 1; }

                        uvm.UnitId = vu.Id;
                        uvm.VehicleModelId = m.Id;

                        context.UnitVehicleModels.Add(uvm);
                    }

                    context.SaveChanges();

                    ConfirmationLabel.Content = "Unit created successfully!";

                }
                else
                {

                }
            }
        }
    }
}
