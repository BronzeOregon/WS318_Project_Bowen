using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.Json;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;

namespace WS318_Project_Bowen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Page
    {
        public ObservableCollection<Unit> units = new ObservableCollection<Unit>();
        
        public MainWindow()
        {
            InitializeComponent();
            UnitPanel.ItemsSource = units;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewSpecialRule_Click(object sender, RoutedEventArgs e)
        {
            AddSpecialRuleWindow popup = new AddSpecialRuleWindow();
            popup.ShowDialog();
        }

        private void EditSpecialRule_Click(object sender, RoutedEventArgs e)
        {
            EditSpecialRuleWindow popup = new EditSpecialRuleWindow();
            popup.ShowDialog();
        }

        private void NewWeapon_Click(object sender, RoutedEventArgs e)
        {
            AddWeaponWindow popup = new AddWeaponWindow();
            popup.ShowDialog();
        }

        private void EditWeapon_Click(object sender, RoutedEventArgs e)
        {
            EditWeaponWindow popup = new EditWeaponWindow();
            popup.ShowDialog();
        }

        private void NewModel_Click(object sender, RoutedEventArgs e)
        {
            AddModelWindow popup = new AddModelWindow();
            popup.ShowDialog();
        }

        private void EditModel_Click(object sender, RoutedEventArgs e)
        {
            EditModelWindow popup = new EditModelWindow();
            popup.ShowDialog();
        }

        private void NewUnit_Click(object sender, RoutedEventArgs e)
        {
            AddUnitWindow popup = new AddUnitWindow();
            popup.ShowDialog();
        }

        private void EditUnit_Click(object sender, RoutedEventArgs e)
        {
            EditUnitWindow popup = new EditUnitWindow();
            popup.ShowDialog();
        }

        private void AddUnit_Click(object sender, RoutedEventArgs e)
        {
            ArmyAddUnitWindow popup = new ArmyAddUnitWindow();
            popup.ShowDialog();
            if (popup.DialogResult == true)
                units.Add(popup.addedUnit);

            UnitCount.Text = units.Count.ToString();

        }

        private void RemoveUnit_Click(object sender, RoutedEventArgs e)
        {
            units.Remove((Unit)UnitPanel.SelectedItem);
        }

        private void SaveArmy_Click(object sender, RoutedEventArgs e)
        {
            string ArmyJSON = JsonSerializer.Serialize(units, new JsonSerializerOptions { WriteIndented = true });
            Debug.WriteLine(ArmyJSON);

            SaveFileDialog ArmySaveDialog = new SaveFileDialog();
            ArmySaveDialog.Filter = "JSON Files (*.JSON)|*.JSON|All Files (*.*)|*.*";
            ArmySaveDialog.Title = "Save your Army";

            if (ArmySaveDialog.ShowDialog() == true)
            {
                string filepath =  ArmySaveDialog.FileName;

                File.WriteAllText(filepath, ArmyJSON);
            }
        }

        private void NewArmy_Click(object sender, RoutedEventArgs e)
        {
            units.Clear();
        }

        private void LoadArmy_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ArmyLoadDialog = new OpenFileDialog();
            ArmyLoadDialog.Filter = "JSON Files (*.JSON)|*.JSON|All Files (*.*)|*.*";

            if(ArmyLoadDialog.ShowDialog() == true)
            {
                string selectedfilepath = ArmyLoadDialog.FileName;

                try
                {
                    string ArmyContents = File.ReadAllText(selectedfilepath);

                    ObservableCollection<Unit> import = JsonSerializer.Deserialize<ObservableCollection<Unit>>(ArmyContents)!;

                    Debug.WriteLine(ArmyContents);

                    foreach (Unit unit in import)
                    {
                        units.Add(unit);
                    }
                                        

                } catch (Exception ex)
                {
                    Debug.WriteLine("Error reading JSON.");
                }

                
            }
        }
    }
}