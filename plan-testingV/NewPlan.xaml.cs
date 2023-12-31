﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
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
using System.ServiceProcess;
using System.Timers;
using Microsoft.Toolkit.Uwp.Notifications;
using Notifications.Wpf;

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
            int currentYear = DateTime.Now.Year;
            int lastYear = currentYear - 1;
            string plan_name = txtPlanName.Text;
            string plan_desc = txtPlanDesc.Text;
            int year = (int)comboYear.SelectedIndex + lastYear;
            int month = (int)comboMonth.SelectedIndex;
            int day = (int)comboDay.SelectedIndex;
            int hour = (int)comboHour.SelectedIndex - 1;
            int minute = (int)comboMinute.SelectedIndex;


            if (string.IsNullOrEmpty(plan_name))
            {
                warning_label.Content = "Please enter the plan's name";
            }
            if(year == 0 || month == 0 || day == 0 || hour == 0 || minute == 0)
            {
                warning_label.Content = "Please enter the plan's date";
            }
            if (!string.IsNullOrEmpty(plan_name) || year!=0||month!=0||day!=0||hour!=0||minute!=0)
            {
                DateTime _plan_date = new DateTime(year, month, day, hour, minute, 0);
                string plan_date = _plan_date.ToString();
                if(string.IsNullOrEmpty(plan_date))
                {
                    warning_label.Content = "please enter a valid date";
                }
                else
                {
                    warning_label.Content = "";
                    string DBcon_string = "Data Source=DESKTOP-LKC2C9H\\TEW_SQLEXPRESS;Initial Catalog=Reminder;Integrated Security=True";
                    SqlConnection SQL_con = new SqlConnection(DBcon_string);
                    try
                    {
                        if (SQL_con.State == System.Data.ConnectionState.Closed)
                        {
                            SQL_con.Open();
                        }
                        Login login = new Login();
                        int User_ID = UserTempData.UserID;
                        string query = "INSERT INTO tblPlans (User_ID, Plan_name, Plan_desc, Plan_remindme) VALUES (@User_ID, @Plan_name, @Plan_desc, @Plan_remindme)";
                        SqlCommand SQL_cmd = new SqlCommand(query, SQL_con);
                        SQL_cmd.CommandType = CommandType.Text;
                        SQL_cmd.Parameters.AddWithValue("@User_ID", User_ID);
                        SQL_cmd.Parameters.AddWithValue("@Plan_name", plan_name);
                        SQL_cmd.Parameters.AddWithValue("@Plan_desc", plan_desc);
                        SQL_cmd.Parameters.AddWithValue("@Plan_remindme", plan_date);
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
            for(int i=0; i < DateTime.Now.Month; i++)
            {
                comboMonth.Items.Add(i+1);
            }

        }

        private void add_days(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 31; i++)
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
