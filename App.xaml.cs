using System.Configuration;
using System.Data;
using System.Windows;

namespace WS318_Project_Bowen
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        DatabaseDbContext dbContext = new DatabaseDbContext();
    }

}
