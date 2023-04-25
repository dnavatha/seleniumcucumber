using BoDi;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using NetDriverManager = WebDriverManager.DriverManager;

using System.IO;

using System.Reflection;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;

namespace FTADOTAutomation.Hooks.BrowserDriver
{
    [Binding]
   public class DriverInstance : IDisposable
    {
        private string osName;
        private string browser;
        private string driversPath;
        private string headlessMode;
        private string remoteServer;
        private Lazy<IWebDriver> _webDriver;

        public DriverInstance(IObjectContainer oc)
        {

            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build(); 
            

            OperatingSystem os = Environment.OSVersion;
            osName = os.Platform.ToString().ToLower();
            
            browser = MyConfig.GetValue<string>("BROWSER");
            driversPath = MyConfig.GetValue<string>("DRIVERSPATH");  
            headlessMode = MyConfig.GetValue<string>("HEADLESS");  
            remoteServer = MyConfig.GetValue<string>("REMOTEHUBSERVER");  
            _webDriver = new Lazy<IWebDriver>(CreateWebDriver);
        }

        private IWebDriver CreateWebDriver()
        {
            var options = new ChromeOptions();
            Uri RemoteHubServer = new Uri(remoteServer);
            IWebDriver driver;
            switch (browser?.ToLower())
            {
                case "chrome":
                case "Chrome":
                    if (osName == "win32nt")
                    {
                        options.AddUserProfilePreference("profile.cookie_controls_mode", 0);
                        //options.AddArgument("--incognito");
                        options.AddArgument("--start-maximized");
                        options.AddArgument("--no-sandbox");

                        driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options);
                    }
                    else
                    {
                        _ = new NetDriverManager().SetUpDriver(new ChromeConfig());
                        options.AddArgument("--no-sandbox");
                        if (headlessMode == "true")
                        {
                            options.AddArgument("--headless");
                        }
                        options.AddArgument("--incognito");
                        options.AddArgument("--disable-dev-shm-usage");
                        options.AddArgument("--start-maximized");
                        options.AddArgument("--window-size=1920x1080");
                        driver = new ChromeDriver(driversPath, options);
                    }
                    break;

                case "firefox":
                    _ = new NetDriverManager().SetUpDriver(new FirefoxConfig());
                    driver = new FirefoxDriver();
                    break;

                case "edge":
                    _ = new NetDriverManager().SetUpDriver(new EdgeConfig());
                    EdgeOptions edgeOptions = new EdgeOptions();
                    driver = new EdgeDriver(edgeOptions);
                    break;

                case "remote":
                    _ = new NetDriverManager().SetUpDriver(new ChromeConfig());
                    if (headlessMode == "true")
                    {
                        options.AddArgument("--headless");
                    }
                    options.AddArgument("--incognito");
                    options.AddArgument("--no-sandbox");
                    options.AddArgument("--disable-dev-shm-usage");
                    options.AddArgument("--start-maximized");
                    options.AddArgument("--window-size=1920x1080");
                    driver = new RemoteWebDriver(RemoteHubServer, options);
                    break;

                default:
                    throw new NotImplementedException($"Environment variable BROWSER value={browser} is not supported");
            }
            driver.Manage().Window.Maximize();
            return driver;
        }

        public IWebDriver GetWebDriver()
        {
            return _webDriver.Value;
        }

        public void Dispose()
        {
            if (_webDriver.IsValueCreated)
            {
                _webDriver.Value.Dispose();
            }
        }

    }

   
}
