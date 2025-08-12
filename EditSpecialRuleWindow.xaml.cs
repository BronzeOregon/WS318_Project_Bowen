using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for EditSpecialRuleWindow.xaml
    /// </summary>
    public partial class EditSpecialRuleWindow : Window
    {

        GSchoolCWs318ProjectBowenDatabase1MdfContext dbContext = new GSchoolCWs318ProjectBowenDatabase1MdfContext();

        SpecialRule selection;

        public EditSpecialRuleWindow()
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

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBar.Text.Equals("")||SearchBar.Text.IsNullOrEmpty())
            {
                setDataContext();
            }
            else {string search = SearchBar.Text;
            using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
            {
                var filteredEntities = context.SpecialRules.Where(s => s.Name.StartsWith(search));
                SpecialRuleGrid.ItemsSource = filteredEntities.ToList();
            } }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
            {
                var selectedEntity = context.SpecialRules.Find(selection.Id);

                if(selectedEntity != null)
                {
                    selectedEntity.Name = RuleName.Text;
                    selectedEntity.Description = RuleDescription.Text;
                    selectedEntity.Detail = RuleDetail.Text;

                    context.SaveChanges();
                }
            }

        }

        private void RemoveRuleButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
            {
                var selectedEntity = context.SpecialRules.Find(selection.Id);

                if(selectedEntity != null)
                {
                    context.SpecialRules.Remove(selectedEntity);
                    context.SaveChanges();
                }

                
            }
        }

        private void SpecialRuleGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selection = (SpecialRule)SpecialRuleGrid.SelectedItem;
            if (selection != null)
            {
                RuleName.Text = selection.Name;
                RuleDescription.Text = selection.Description;
                RuleDetail.Text = selection.Detail;
            }
        }
    }
}
