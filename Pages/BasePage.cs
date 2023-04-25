using BoDi;
using FTADOTAutomation.Driver;
using OpenQA.Selenium;



namespace FTADOTAutomation.Pages
{
    public class BasePage
    {

        public readonly UserActions userActions;

        public BasePage(ObjectContainer objectContainer)
        {
            userActions = objectContainer.Resolve<UserActions>();
        }

        protected Element Element(string xpath)
        {
            return Element(By.XPath(xpath));
        }

        protected Element Element(By locator)
        {
            return new Element(locator, userActions);
        }


    }
}
