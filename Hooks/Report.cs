using BoDi;
using Microsoft.Extensions.Configuration;
using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using OpenQA.Selenium.Chrome;
using AventStack.ExtentReports.Reporter;
using System.IO;
using FTADOTAutomation.Hooks.BrowserDriver;

namespace FTADOTAutomation.Hooks
{
    [Binding]
    public class Report 
    {   
        private static string filepath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, @"TestResults","Report.html");
        private static ExtentReports Extent;
        private static ExtentTest Scenario;
        private static ExtentTest FeatureName;
        private DriverInstance DriverInstance;

        public MediaEntityModelProvider CaptureScreenShot(string name, ObjectContainer OC)
        {
            Screenshot ss = ((ITakesScreenshot)OC.Resolve<BrowserSupport>().GetWebDriver()).GetScreenshot();
            ss.SaveAsFile(filepath);
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(ss.ToString(), name).Build();
        }

       
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var htmlReporter = new ExtentHtmlReporter(filepath);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            Extent = new ExtentReports();
            Extent.AttachReporter(htmlReporter);
        }


        [BeforeFeature (Order =1)]
        public static void BeforeFeature(FeatureContext featurecontext)
        {
            FeatureName = Extent.CreateTest<Feature>(featurecontext.FeatureInfo.Title);
        }

        [BeforeScenario(Order = 1)]
        public static void BeforeScenario(ScenarioContext scenariocontext)
        {
            Scenario = FeatureName.CreateNode<Scenario>(scenariocontext.ScenarioInfo.Title);
        }


        [AfterStep]
        public void InsertReportingSteps(ScenarioContext scenariocontext, ObjectContainer oc)
        {
            var steptype = scenariocontext.StepContext.StepInfo.StepDefinitionType.ToString();
            string stepinfo = scenariocontext.StepContext.StepInfo.Text;


            string resultOfImplementation = scenariocontext.ScenarioExecutionStatus.ToString();
            if(scenariocontext.TestError == null && resultOfImplementation == "OK")
            {
                if (steptype == "Given")
                    Scenario.CreateNode<Given>(stepinfo);
                else if (steptype == "When")
                    Scenario.CreateNode<When>(stepinfo);
                else if (steptype == "Then")
                    Scenario.CreateNode<Then>(stepinfo);
                else if (steptype == "And")
                    Scenario.CreateNode<And>(stepinfo);
                else if (steptype == "But")
                    Scenario.CreateNode<And>(stepinfo);
             }
            else if(scenariocontext.TestError !=null)
            {
                string? testError = scenariocontext.TestError.Message;

                var mediaEntry = CaptureScreenShot(scenariocontext.ScenarioInfo.Title.Trim(),oc);

                if (steptype == "Given")
                    Scenario.CreateNode<Given>(stepinfo).Fail(testError, mediaEntry);
                else if (steptype == "When")
                    Scenario.CreateNode<When>(stepinfo).Fail(testError, mediaEntry);
                else if (steptype == "Then")
                    Scenario.CreateNode<Then>(stepinfo).Fail(testError, mediaEntry);
                else if (steptype == "And")
                    Scenario.CreateNode<And>(stepinfo).Fail(testError, mediaEntry);
                else if (steptype == "But")
                    Scenario.CreateNode<And>(stepinfo).Fail(testError, mediaEntry);


            }

            if(resultOfImplementation =="StepDefinationPending")
            {
                if (steptype == "Given")
                    Scenario.CreateNode<Given>(stepinfo).Skip("Step Defination Pending");
                else if (steptype== "When")
                    Scenario.CreateNode<When>(stepinfo).Skip("Step Defination Pending");
                else if (steptype == "Then")
                    Scenario.CreateNode<Then>(stepinfo).Skip("Step Defination Pending");
            }

        }


        [AfterTestRun]
        public static void AfterTestRun()
        {
            Extent.Flush();
        }

        


    }
    }
