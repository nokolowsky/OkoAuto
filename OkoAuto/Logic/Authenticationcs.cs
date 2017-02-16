using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OkoAuto.Database;


namespace OkoAuto.Logic
{ 
    public class Authentications
    {
        public Authentications()
        {
            CurrentUser = new User { UserName = "Name", Password = "Password" };
            
        }

        private bool _isAuthenticated;
        public bool IsAuthenticated
        {
            get
            {
                return _isAuthenticated;
            }
            set
            {
               _isAuthenticated = value;
                
            }
        }

        private User _CurrentUser;
        public User CurrentUser
        {
            get { return _CurrentUser; }
            set
            {
                _CurrentUser = value;

            }
        }

        public bool Authenticate(string UserName, string Password)
        {
            DataPortal dp = new DataPortal();
            if(dp.authenticateLogin(UserName,Password))
            {
                return true;
            }
            return false;
        }

        public void LogOff()
        {
            IsAuthenticated = false;
        }


    }
}
