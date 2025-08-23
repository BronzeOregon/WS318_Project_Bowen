using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ArmyAddUnitWindow.xaml
    /// </summary>
    public partial class ArmyAddUnitWindow : Window
    {

        ObservableCollection<Unit> units = new ObservableCollection<Unit>();

        public Unit addedUnit;

        public ArmyAddUnitWindow()
        {
            InitializeComponent();
            setUnitContext();
        }

        public void setUnitContext()
        {
            units.Clear();

            using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
            {
                var unitQuery =
                    from unit in context.Units
                    select unit;

                foreach (var unit in unitQuery)
                {
                    units.Add(unit);
                }

                UnitGrid.ItemsSource = units;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            addedUnit = (Unit)UnitGrid.SelectedItem;
            this.DialogResult = true;
            this.Close();
        }

        private void UnitGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UnitSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
            {

                string search = UnitSearchBar.Text;              

                var filteredEntities = units.Where(s => s.Name.StartsWith(search));

                UnitGrid.ItemsSource = filteredEntities.ToList();

                
                if (UnitSearchBar.Text.Equals("") || UnitSearchBar.Text.IsNullOrEmpty())
                {
                    setUnitContext();
                }
            }
        }
    }
}
