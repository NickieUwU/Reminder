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
            string plan_date = txtPlanDate.Text;
            

            if(string.IsNullOrEmpty(plan_name))
            {
                warning_label.Content = "Please enter the plan's name";
            }
            if(plan_date == null)
            {
                 warning_label.Content = "Please enter the plan's date";
            }
            if (!string.IsNullOrEmpty(plan_name) || !string.IsNullOrEmpty(plan_desc) || plan_date != null)
            {
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
    }
}
