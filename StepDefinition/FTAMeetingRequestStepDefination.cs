using AventStack.ExtentReports.Gherkin.Model;
using OpenQA.Selenium;

using TechTalk.SpecFlow;
using System;

using FTADOTAutomation.Pages;
using AventStack.ExtentReports;

namespace FTADOTAutomation.Steps
{
    [Binding]
    public sealed class FTAMeetingRequestStepDefination
    {
       
        public FTAMeetingRequestPage FTAMeetingRequestPage;

        private readonly LoginPage loginpage;
        

        public FTAMeetingRequestStepDefination(FTAMeetingRequestPage ftameetingpage, LoginPage loginPage)
        {
            FTAMeetingRequestPage = ftameetingpage;
            loginpage = loginPage;
        }

       

        [Given(@"User launches FTA dot meeting request form")]
        public void GivenUserLaunchesFTADotMeetingRequestForm()
        {
            loginpage.Navigate();
            Console.WriteLine("Ccompleted step");
        }


        [Given(@"Verify Page")]
        public void GivenVerifyPage()
        {
            if (FTAMeetingRequestPage.MeetingRequestFormValidation().assertElementIsVisible(10, true))
            {
                Log.Info("Page Validated");
            }
            else
            {
                Assert.IsFalse(true);
            }
        }


        [Given(@"Verify first section of group organization")]
        public void GivenVerifyFirstSectionOfGroupOrganization()
        {
            if (FTAMeetingRequestPage.MeetingRequestFormValidation().assertElementIsVisible(10, true))
            {
                Log.Info("Page Validated");
            }
            else
            {
                Assert.IsFalse(true);
            }

            FTAMeetingRequestPage.OrganizationName().setText("Test Organization");
            Log.Info("Test Organization");
            FTAMeetingRequestPage.BusinessAddress().setText("Business Address");
            Log.Info("Business Address");
            FTAMeetingRequestPage.CityName().setText("Concord");
            Log.Info("Concord");
            FTAMeetingRequestPage.zipCode().setText("28025");
            Log.Info("28025");
            FTAMeetingRequestPage.BusinessPhone().setText("9806227545");
            Log.Info("9806227545");
        }


        [Given(@"Verify first section of general point of contact")]
        public void GivenVerifyFirstSectionOfGeneralPointOfContact()
        {
            if (FTAMeetingRequestPage.MeetingRequestFormValidation().assertElementIsVisible(10, true))
            {
                Log.Info("Page Validated");
            }
            else
            {
                Assert.IsFalse(true);
            }

            FTAMeetingRequestPage.PointOfContactFirstName().setText("First Name");
            Log.Info("Frist name entered in point of contact");

            FTAMeetingRequestPage.PointOfContactLastName().setText("Last Name");
            Log.Info("Last name entered in point of contact");

            FTAMeetingRequestPage.PrincipleOtherContactsBusinessEmail().setText("Business Email ");
            Log.Info("Business email entered in point of contact");
        }


        [Given(@"Verify first section of principal and other attendees")]
        public void GivenVerifyFirstSectionOfPrincipalAndOtherAttendees()
        {   

            FTAMeetingRequestPage.PrincipleOtherContactsFirstName().setText("First name in Principal");
            Log.Info("First name in Principal");

            FTAMeetingRequestPage.PrincipleOtherContactsLastName().setText("Last name in Principal");
            Log.Info("Last name in Principal");

            FTAMeetingRequestPage.PrincipleOtherContactsBusinessEmail().setText("Business email in Principal");
            Log.Info("Business email in Principal");

        }


        [Given(@"Verify first section of meeting event")]
        public void GivenVerifyFirstSectionOfMeetingEvent()
        {
            FTAMeetingRequestPage.MeetingSubject().setText("Meeting Subject");
            Log.Info("Meeting Subject");
        }

        [Given(@"Verify first Agenda field")]
        public void GivenVerifyFirstAgendaField()
        {
           FTAMeetingRequestPage. 
        }





    }
}
