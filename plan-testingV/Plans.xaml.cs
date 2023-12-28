using Notifications.Wpf;
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

        private void SendNotification()
        {
            List<Plan> plans = GetAllPlans();
            DateTime now = DateTime.Now;
            NotificationManager notificationManager = new NotificationManager();
            foreach(var plan in plans)
            {
                DateTime set_date = DateTime.Parse(plan.Plan_remindme);
                if((now >= set_date))
                {
                    notificationManager.Show(new NotificationContent
                    {
                        Title = plan.Plan_name,
                        Message = plan.Plan_desc,
                        Type = NotificationType.Information
                    });
                }
            }
        }

        private List<Plan> GetAllPlans()
        {
            const string connection_string = "Data Source=DESKTOP-LKC2C9H\\TEW_SQLEXPRESS;Initial Catalog=Reminder;Integrated Security=True";
            List<Plan> plans = new List<Plan>();
            SqlConnection SQL_con = new SqlConnection(connection_string);
            try
            {
                if(SQL_con.State == System.Data.ConnectionState.Closed)
                {
                    SQL_con.Open();
                }
                string query = "SELECT Plan_name, Plan_desc, Plan_remindme FROM tblPlans WHERE User_ID=@User_ID";
                SqlCommand SQL_command = new SqlCommand(query, SQL_con);
                SQL_command.Parameters.AddWithValue("@User_ID", UserTempData.UserID);
                using(SqlDataReader sqlDataReader = SQL_command.ExecuteReader())
                {
                    while(sqlDataReader.Read())
                    {
                        plans.Add(new Plan
                        {
                            Plan_name = sqlDataReader.GetString(0),
                            Plan_desc = sqlDataReader.GetString(1),
                            Plan_remindme = sqlDataReader.GetString(2),
                        });
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return plans;
        }
    }
    public class Plan
    {
        public string Plan_name { get; set; }
        public string Plan_desc { get; set; }
        public string Plan_remindme { get; set; }
    }
}
