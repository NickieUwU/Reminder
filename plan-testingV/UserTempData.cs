using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan_testingV
{
    public class UserTempData
    {
        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }


        public UserTempData(string Username)
        {
            this.Username = Username;
        }
    }
}
