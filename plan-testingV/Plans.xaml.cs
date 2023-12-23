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
            DisplayData();
        }

        int count = 0;
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
                int User_ID = UserTempData.UserID;
                List<string> Plan_nameList = new List<string>();
                string query = "SELECT Plan_name FROM tblPlans WHERE User_ID=@User_ID";
                SqlCommand SQL_cmd = new SqlCommand(query, SQL_con);
                SQL_cmd.Parameters.AddWithValue("@User_ID", User_ID);
                using (SqlDataReader sqlDataReader = SQL_cmd.ExecuteReader())
                {
                    while(sqlDataReader.Read())
                    {
                        Plan_nameList.Add(sqlDataReader.GetString(0));
                    }
                }
                string[] Plan_nameArray = Plan_nameList.ToArray();
                return Plan_nameArray[count];
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
                int User_ID = UserTempData.UserID;
                List<string> Plan_descList = new List<string>();
                string query = "SELECT Plan_desc FROM tblPlans WHERE User_ID=@User_ID";
                SqlCommand SQL_cmd = new SqlCommand(query, SQL_con);
                SQL_cmd.Parameters.AddWithValue("@User_ID", User_ID);
                using (SqlDataReader sqlDataReader = SQL_cmd.ExecuteReader())
                {
                    while(sqlDataReader.Read())
                    {
                        Plan_descList.Add(sqlDataReader.GetString(0));
                    }
                    string[] Plan_descArray = Plan_descList.ToArray();
                    return Plan_descArray[count];
                }
                
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
                int User_ID = UserTempData.UserID;
                List<string> Plan_remindmeList = new List<string>();
                string query = "SELECT Plan_remindme FROM tblPlans WHERE User_ID=@User_ID";
                SqlCommand SQL_cmd = new SqlCommand(query, SQL_con);
                SQL_cmd.Parameters.AddWithValue("@User_ID", User_ID);
                using (SqlDataReader sqlDataReader = SQL_cmd.ExecuteReader())
                {
                    while(sqlDataReader.Read())
                    {
                        Plan_remindmeList.Add(sqlDataReader.GetString(0));
                    }
                }
                string[] Plan_remindmeArray = Plan_remindmeList.ToArray();
                return Plan_remindmeArray[count];
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

        
        private void GoAhead(object sender, RoutedEventArgs e)
        {
            count++;
            DisplayData();
        }

        private void GoBackPlan(object sender, RoutedEventArgs e)
        {
            count--;
            if (count < 0)
            {
                count = 0;
            }
            DisplayData();
        }

        private void DeletePlan(object sender, RoutedEventArgs e)
        {
            string DBcon_string = "Data Source=DESKTOP-LKC2C9H\\TEW_SQLEXPRESS;Initial Catalog=Reminder;Integrated Security=True";
            SqlConnection SQL_con = new SqlConnection(DBcon_string);
            try
            {
                if(SQL_con.State == System.Data.ConnectionState.Closed)
                {
                    SQL_con.Open();
                }
                string query = "DELETE FROM tblPlans WHERE User_ID=@User_ID AND Plan_name=@Plan_name AND Plan_desc=@Plan_desc AND Plan_remindme=@Plan_remindme";
                SqlCommand SQL_command = new SqlCommand(query, SQL_con);
                SQL_command.Parameters.AddWithValue("@User_ID", UserTempData.UserID);
                SQL_command.Parameters.AddWithValue("@Plan_name", GetPlansName());
                SQL_command.Parameters.AddWithValue("@Plan_desc", GetPlansDesc());
                SQL_command.Parameters.AddWithValue("@Plan_remindme", GetPlansDate());
                SQL_command.ExecuteScalar();
                count--;
                DisplayData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                SQL_con.Close();
            }
        }
    }
}
