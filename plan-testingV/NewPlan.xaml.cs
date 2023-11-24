using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
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
    /// Interakční logika pro NewPlan.xaml
    /// </summary>
    public partial class NewPlan : Window
    {
        public NewPlan()
        {
            InitializeComponent();
        }
        private void GoBack(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }

        private void create_NewPlan(object sender, RoutedEventArgs e)
        {
            string plan_name = txtPlanName.Text;
            string plan_desc = txtPlanDesc.Text;
            int year = (int)comboYear.SelectedIndex;
            int month = (int)comboMonth.SelectedIndex;
            int day = (int)comboDay.SelectedIndex;
            int hour = (int)comboHour.SelectedIndex;
            int minute = (int)comboMinute.SelectedIndex;
            


            if (string.IsNullOrEmpty(plan_name))
            {
                warning_label.Content = "Please enter the plan's name";
            }
            if(comboYear.SelectedIndex == 0 || comboMonth.SelectedIndex == 0 || comboDay.SelectedIndex == 0 || comboHour.SelectedIndex == 0 || comboMinute.SelectedIndex == 0)
            {
                warning_label.Content = "Please enter the plan's date";
            }
            if (!string.IsNullOrEmpty(plan_name) || !string.IsNullOrEmpty(plan_desc) || comboYear.SelectedIndex!=0||comboMonth.SelectedIndex!=0||comboDay.SelectedIndex!=0||comboHour.SelectedIndex!=0||comboMinute.SelectedIndex!=0)
            {
                DateTime plan_date = new DateTime(year, month, day, hour, minute, 0);
                warning_label.Content = "";
                string DBcon_string = "Data Source=DESKTOP-LKC2C9H\\TEW_SQLEXPRESS;Initial Catalog=Reminder;Integrated Security=True";
                SqlConnection SQL_con = new SqlConnection(DBcon_string);
                try
                {
                    if(SQL_con.State == System.Data.ConnectionState.Closed)
                    {
                        SQL_con.Open();
                    }
                    string query = "INSERT INTO tbl_Plans VALUES(@Name, @Description, @Remind)";
                    SqlCommand SQL_cmd = new SqlCommand(query, SQL_con);
                    SQL_cmd.Parameters.AddWithValue("@Name", plan_name);
                    SQL_cmd.Parameters.AddWithValue("@Description", plan_desc);
                    SQL_cmd.Parameters.AddWithValue("@Remind", plan_date);
                    SQL_cmd.ExecuteNonQuery();
                    Home home = new Home();
                    home.Show();
                    this.Close();
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

        private void add_years(object sender, RoutedEventArgs e)
        {
            int date = DateTime.Now.Year;
            for(int i=date; i < date+100; i++)
            {
                comboYear.Items.Add(i);
            }
        }

        private void add_months(object sender, RoutedEventArgs e)
        { 
            for(int i=0; i < 12; i++)
            {
                comboMonth.Items.Add(i+1);
            }
        }

        private void add_days(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 12; i++)
            {
                string ending = "";
                if(i== 0)
                {
                    ending = "st";
                }
                else if(i == 1)
                {
                    ending = "nd";
                }
                else if(i == 2)
                {
                    ending = "rd";
                }
                else
                {
                    ending = "th";
                }
                comboDay.Items.Add($"{i+1}{ending}");
            }
        }

        private void add_hours(object sender, RoutedEventArgs e)
        {
            for(int i=0; i < 24; i++)
            {
                comboHour.Items.Add(i);
            }
        }

        private void add_minutes(object sender, RoutedEventArgs e)
        {
            for(int i=0; i < 60; i++)
            {
                comboMinute.Items.Add(i+1);
            }
        }
    }
}
