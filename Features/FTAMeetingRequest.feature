Feature: FTA Meeting Request

@meetingRequestVerifyorganization
Scenario: Verify meeting request form and group organization
	Given User launches FTA dot meeting request form
	And Verify Page
	And Verify first section of group organization

@meetingRequestgroupofcontact
Scenario: Verify meeting request form and general point of contact
	Given User launches FTA dot meeting request form
	And Verify first section of general point of contact

@meetingRequestPrincipalandotherattendences
Scenario: Verify meeting request form and principal and other attendees
	Given User launches FTA dot meeting request form
	And Verify first section of principal and other attendees

@meetingRequestmeetingevent
Scenario: Verify meeting request form and meeting event
	Given User launches FTA dot meeting request form
	And Verify first section of meeting event

@meetingRequestmeetingevent1
Scenario: Verify meeting request form and meeting event1
	Given User launches FTA dot meeting request form
	And Verify first Agenda field