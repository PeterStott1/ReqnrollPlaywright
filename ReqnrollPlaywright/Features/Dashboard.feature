Feature: Dashboard
  As an authenticated user
  I want to access my dashboard
  So that I can view notifications and navigate the application

  @Smoke
  Scenario: View notifications on the dashboard
    Given I am on the dashboard
    When I open the notifications panel
    Then I should see at least one notification