Feature: DeleteBooking4

Delete booking

Scenario: Successful booking deletion
	Given The user is authenticated
	When The user creates a booking
	And The user performs a delete booking request
	Then The booking should be deleted4