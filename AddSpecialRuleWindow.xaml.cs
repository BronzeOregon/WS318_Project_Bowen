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

namespace WS318_Project_Bowen
{
    /// <summary>
    /// Interaction logic for AddSpecialRuleWindow.xaml
    /// </summary>
    public partial class AddSpecialRuleWindow : Window
    {

        GSchoolCWs318ProjectBowenDatabase1MdfContext dbContext = new GSchoolCWs318ProjectBowenDatabase1MdfContext();

        public AddSpecialRuleWindow()
        {
            
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!RuleName.Text.Equals("")&&!RuleName.Text.Equals("Rule Name"))
            {
                try
                {
                    SpecialRule sr = new SpecialRule();
                    sr.Id = (dbContext.SpecialRules.Count() + 1);
                    sr.Name = (RuleName.Text);
                    sr.Description = (RuleDescription.Text);
                    sr.Detail = (RuleDetail.Text);

                    dbContext.SpecialRules.Add(sr);
                    dbContext.SaveChanges();
                    ConfirmationLabel.Content = "Special Rule added Successfully!";
                }
                catch (Exception ex) { Debug.WriteLine(ex.Message);
                    ConfirmationLabel.Foreground = Brushes.Red;
                }
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
