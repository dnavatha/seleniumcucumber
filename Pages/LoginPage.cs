using BoDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTADOTAutomation.Driver;

namespace FTADOTAutomation.Pages
{
   public class LoginPage : BasePage
    {
        public LoginPage(ObjectContainer OC) : base(OC)
        {

        }
        public void Navigate()
        {
            UserActions.Navigate("/");
        }

    }
}
