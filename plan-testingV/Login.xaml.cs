using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Security.Cryptography.X509Certificates;

namespace plan_testingV
{
    /// <summary>
    /// Interakční logika pro Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        public void LoginUser(object sender, RoutedEventArgs e)
        {
            string Username = txtUsername.Text;
            string Password = pswdPassword.Password;
            if (string.IsNullOrEmpty(Username) && string.IsNullOrEmpty(Password))
            {
                Warning_Label.Content = "Please provide a username and a password";
            }
            else if (string.IsNullOrEmpty(Username))
            {
                Warning_Label.Content = "Please provide a username";
            }
            else if (string.IsNullOrEmpty(Password))
            {
                Warning_Label.Content = "Please provide a password";
            }
            else if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                Warning_Label.Content = "";
                string DBcon_string = "Data Source=DESKTOP-LKC2C9H\\TEW_SQLEXPRESS;Initial Catalog=Reminder;Integrated Security=True";
                SqlConnection SQL_con = new SqlConnection(DBcon_string);
                try
                {
                    if (SQL_con.State == ConnectionState.Closed)
                    {
                        SQL_con.Open();
                    }
                    string query = "SELECT COUNT(1) FROM tbl_Users WHERE Username=@Username AND Password=@Password";
                    SqlCommand SQL_command = new SqlCommand(query, SQL_con);
                    SQL_command.CommandType = CommandType.Text;
                    SQL_command.Parameters.AddWithValue("@Username", Username);
                    SQL_command.Parameters.AddWithValue("@Password", Password);
                    int count = Convert.ToInt32(SQL_command.ExecuteScalar());
                    if (count == 1)
                    {
                        Home home = new Home();
                        home.menuUser.Header = Username;
                        NewPlan newPlan = new NewPlan();
                        home.Show();
                        this.Close();
                    }
                    else
                    {
                        Warning_Label.Content = "User was not found";
                    }
                }
                catch (Exception exception)
                {

                    MessageBox.Show(exception.Message);
                }
                finally
                {
                    SQL_con.Close();
                }
            }

        }
    }
}
