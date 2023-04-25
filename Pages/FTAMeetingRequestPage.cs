using BoDi;
using FTADOTAutomation.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTADOTAutomation.Pages
{
    public class FTAMeetingRequestPage : BasePage
    {
        public FTAMeetingRequestPage(ObjectContainer OC) : base(OC)
        {

        }

        public Element MeetingRequestFormValidation()
        {
            return Element("//h1[contains(text(),'Meeting / Event Request Form')]");
        }
    
    
        public Element Last12MonthsYesorNo()
        {
            return Element("//input[@id='rdbPreviousMeetingYes']");
        }

        public Element OrganizationName()
        {
            return Element("//input[@id='txtOrganizationName']");
        }
        public Element BusinessAddress()
        {
            return Element("//input[@id='txtBusinessAddress1']");
        }

        public Element CityName()
        {
            return Element("//input[@id='txtcity']");
        }

        public Element zipCode()
        {
            return Element("//input[@id='txtzipCode']");
        }

        public Element BusinessPhone()
        {
            return Element("//input[@id='txtbusinessPhone']");
        }

        public Element PointOfContactFirstName()
        {
            return Element("//input[@id='txtpocFirstName']");
        }

        public Element PointOfContactLastName()
        {
            return Element("//input[@id='txtpocLastName']");
        }

        public Element PointOfContactBusinessEmail()
        {
            return Element("//input[@id='txtpocBusinessEmail']");
        }

        public Element PrincipleOtherContactsFirstName()
        {
            return Element("//input[@id='txtOtherFirstName']");
        }


        public Element PrincipleOtherContactsLastName()
        {
            return Element("//input[@id='txtOtherLastName']");
        }

        public Element PrincipleOtherContactsBusinessEmail()
        {
            return Element("//input[@id='txtOtherBusinessEmail']");
        }

        public Element MeetingInfo()
        {
            return Element("//input[@id='txtspecifyTime']");
        }

        public Element MeetingSubject()
        {
            return Element("//textarea[@id='txt Subject']");
            
        }

        public Element StateDropDown()
        {

            return Element("//Select[@id='ddlstate1']");
        }


    }

}
