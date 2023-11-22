using System;
using System.Collections.Generic;
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
using System.IO;
using System.Security.Cryptography;
using System.Data.SqlClient;

namespace plan_testingV
{
    /// <summary>
    /// Interakční logika pro Signin.xaml
    /// </summary>
    public partial class Signin : Window
    {
        public Signin()
        {
            InitializeComponent();
        }


        public void RegisterUser(object sender, RoutedEventArgs e)
        {
            string Username = username.Text;
            string Confirm_Username = confirm_name.Text;
            string Password = pswd.Password;
            string Confirm_Password = confirm_pswd.Password;
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Confirm_Username) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Confirm_Password))
            {
                Warning_Label.Content = "Please fill all fields";
            }
            else if (Username != Confirm_Username)
            {
                Warning_Label.Content = "The usernames don't match";
            }
            else if (Password != Confirm_Password)
            {
                Warning_Label.Content = "The passwords don't match";
            }
            else if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Confirm_Username) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Confirm_Password) && Username == Confirm_Username && Password == Confirm_Password)
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
                    string query = "INSERT INTO tbl_Users VALUES(@Username, @Password)";
                    SqlCommand SQL_command = new SqlCommand(query, SQL_con);
                    SQL_command.Parameters.AddWithValue("@Username", Username);
                    SQL_command.Parameters.AddWithValue("@Password", Password);
                    SQL_command.ExecuteNonQuery();
                    
                    Home home = new Home();
                    home.Show();
                    this.Close();
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

        private void GoBack(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
