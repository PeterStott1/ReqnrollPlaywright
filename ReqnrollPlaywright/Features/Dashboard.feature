Feature: Dashboard
  As an authenticated user
  I want to access my dashboard
  So that I can view notifications and navigate the application

  @User
  Scenario: View cards on the dashboard
    Given I am logged in and navigate to the home page 
    When I have landed on the homepage 
    Then I should see at least one card
  
  @Odh 
  Scenario: View Odh cards on the dashboard
    Given I am logged in and navigate to the home page 
    When I have landed on the homepage 
    Then I should see at least one card