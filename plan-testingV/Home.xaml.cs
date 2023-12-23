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

namespace plan_testingV
{
    /// <summary>
    /// Interakční logika pro Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            menuUser.Header = UserTempData.Username;
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void CreateNewPlan(object sender, RoutedEventArgs e)
        {
            NewPlan newPlan = new NewPlan();
            newPlan.Show();
            this.Close();
        }

        private void ViewPlans(object sender, RoutedEventArgs e)
        {
            Plans viewPlans = new Plans();
            viewPlans.Show();
            this.Close();
        }
    }
}
