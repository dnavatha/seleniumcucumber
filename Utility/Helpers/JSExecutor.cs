using OpenQA.Selenium;
using System;
using System.Threading;

namespace FTADOTAutomation.Helpers
{
    public class JSExecutor
    {
        public static object execute(String script, params object[] args)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver.UserActions.WebDriver;
            return js.ExecuteScript(script,args );
        }

        // Highligth Element
        public static void highlight(IWebElement element, int borderwidth = 3)
        {
            if (borderwidth == 0)
            {
                Thread.Sleep(500);
            }
            execute(script: "arguments[0].style.border='" + borderwidth + "px solid red'", element);
        }
    }
}
