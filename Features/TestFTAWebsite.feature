Feature: TestFTAWebsite

@TestAboutPage
Scenario: Launch FTA website and validate about page
	When User launches FTA website
	And Clicks on About link
	And Verify about page

@TestFundingPage
Scenario: Launch FTA website and validate funding page
	When User launches FTA website
	And Clicks on Funding link
	And Verify Funding page

@TestRegulationProgramsPage
Scenario: Launch FTA website and validate regulation and programs page
	When User launches FTA website
	And Clicks on regulation and programs link
	And Verify Regulation Programs page