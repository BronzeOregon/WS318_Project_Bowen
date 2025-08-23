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

namespace WS318_Project_Bowen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Page
    {
        public MainWindow()
        {
            InitializeComponent();
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

        }

        private void AddUnit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveUnit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}