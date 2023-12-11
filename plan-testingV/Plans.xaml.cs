using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interakční logika pro Plans.xaml
    /// </summary>
    public partial class Plans : Window
    {
        public Plans()
        {
            InitializeComponent();
        }

        private int count = 0;
        private void DisplayData()
        {
            lblPlanName.Content = GetPlansName();
            lblPlanDesc.Content = GetPlansDesc();
            lblPlanRemindDate.Content = GetPlansDate();
        }
        private void GoBack(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayData();
        }

        private int getUserID()
        {
            Login login = new Login();
            int User_ID = 0;
            string Username = GetText();
            string DBcon_string = "Data Source=DESKTOP-LKC2C9H\\TEW_SQLEXPRESS;Initial Catalog=Reminder;Integrated Security=True";
            SqlConnection SQL_con = new SqlConnection(DBcon_string);
            try
            {
                if (SQL_con.State == System.Data.ConnectionState.Closed)
                {
                    SQL_con.Open();
                }
                string query_getUserID = "SELECT USER_ID FROM tbl_Users WHERE Username=@Username";
                SqlCommand SQL_cmd_getUserID = new SqlCommand(query_getUserID, SQL_con);
                SQL_cmd_getUserID.Parameters.AddWithValue("@Username", Username);

                using (SqlDataReader sqlDataReader = SQL_cmd_getUserID.ExecuteReader())
                {
                    if (sqlDataReader.Read())
                    {
                        User_ID = sqlDataReader.GetInt32(0);
                    }
                }

                //MessageBox.Show(User_ID.ToString() + " <= user id succesfully obtained\n" + Username + " <= username succesfully obtained");
                return User_ID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                SQL_con.Close();
            }
        }
        
        private string GetPlansName()
        {
            string DBcon_string = "Data Source=DESKTOP-LKC2C9H\\TEW_SQLEXPRESS;Initial Catalog=Reminder;Integrated Security=True";
            SqlConnection SQL_con = new SqlConnection(DBcon_string);
            try
            {
                if (SQL_con.State == System.Data.ConnectionState.Closed)
                {
                    SQL_con.Open();
                }
                int User_ID = getUserID();
                string Plan_name = "";
                string query = "SELECT Plan_name FROM tblPlans WHERE User_ID=@User_ID";
                SqlCommand SQL_cmd = new SqlCommand(query, SQL_con);
                SQL_cmd.Parameters.AddWithValue("@User_ID", User_ID);
                using (SqlDataReader sqlDataReader = SQL_cmd.ExecuteReader())
                {
                    if (sqlDataReader.HasRows && sqlDataReader.Read())
                    {
                        Plan_name = sqlDataReader.GetString(count);
                    }
                }
                return Plan_name;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
            finally
            {
                SQL_con.Close();
            }

        }

        private string GetPlansDesc()
        {
            string DBcon_string = "Data Source=DESKTOP-LKC2C9H\\TEW_SQLEXPRESS;Initial Catalog=Reminder;Integrated Security=True";
            SqlConnection SQL_con = new SqlConnection(DBcon_string);
            try
            {
                if (SQL_con.State == System.Data.ConnectionState.Closed)
                {
                    SQL_con.Open();
                }
                int User_ID = getUserID();
                string Plan_desc = "";
                string query = "SELECT Plan_desc FROM tblPlans WHERE User_ID=@User_ID";
                SqlCommand SQL_cmd = new SqlCommand(query, SQL_con);
                SQL_cmd.Parameters.AddWithValue("@User_ID", User_ID);
                using (SqlDataReader sqlDataReader = SQL_cmd.ExecuteReader())
                {
                    if (sqlDataReader.HasRows && sqlDataReader.Read())
                    {
                        Plan_desc = sqlDataReader.GetString(count);
                    }
                }
                return Plan_desc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
            finally
            {
                SQL_con.Close();
            }

        }

        private string GetPlansDate()
        {
            string DBcon_string = "Data Source=DESKTOP-LKC2C9H\\TEW_SQLEXPRESS;Initial Catalog=Reminder;Integrated Security=True";
            SqlConnection SQL_con = new SqlConnection(DBcon_string);
            try
            {
                if (SQL_con.State == System.Data.ConnectionState.Closed)
                {
                    SQL_con.Open();
                }
                int User_ID = getUserID();
                string Plan_remindme = "";
                string query = "SELECT Plan_remindme FROM tblPlans WHERE User_ID=@User_ID";
                SqlCommand SQL_cmd = new SqlCommand(query, SQL_con);
                SQL_cmd.Parameters.AddWithValue("@User_ID", User_ID);
                using (SqlDataReader sqlDataReader = SQL_cmd.ExecuteReader())
                {
                    if (sqlDataReader.HasRows && sqlDataReader.Read())
                    {
                        Plan_remindme = sqlDataReader.GetString(count);
                    }
                }
                return Plan_remindme;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
            finally
            {
                SQL_con.Close();
            }

        }



        private string GetText()
        {
            Login login = new Login();
            //return login.txtUsername.Paste();
            return "as";
        }

        private void GoAhead(object sender, RoutedEventArgs e)
        {
            count++;
            DisplayData();
        }

        private void GoBackPlan(object sender, RoutedEventArgs e)
        {
            count--;
            if (count < 0) count = 0;
            DisplayData();
        }
    }
}
