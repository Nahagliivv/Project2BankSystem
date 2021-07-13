using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BankRebootTimeService
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            Model.ConfigClass.getInstance();
            App.Current.MainWindow = new MainWindow();
            App.Current.MainWindow.Show();
        }
    }
}
