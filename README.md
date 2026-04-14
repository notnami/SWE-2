# MyFitnessBud
- Use parameterized queries to prevent SQL injection
- Use clear table names:
	- Users
	- Favorites
	- Meals
- Maintain foreign key constraints for relational integrity
---
### Deployment Environment
__Development Deployment__
- Localhost development server
- SQLite local database file
- Backend hosted locally via .NET runtime

__Production Deployment__
- Deployment is currently local for development purposes. Cloud deployment is planned for a future release.
---
### Version Management
__Version Control Strategy__
- Git-based version control
- Centralized repository hosted on GitHub
- All development currently maintained within the main branch

__Branch Strategy__

Our project depends on our main branch with smaller, temporary branches being split off to test and implement individual features. This allows us to work on our different sections without risk of interfering with each other's work or accidentally pushing buggy code to the main branch before it is debugged. Such branches we've had include our databse and general backend testing. As the project scales, we may continue to add more feature-branch workflows to improve parallel development and code review processes.

__Commit Standards__

To maintain clarity and traceability, commits follow these guidelines:
- Use clear, descriptive messages
- Write in the present tense
- Focus on what changed and why

__Code Review & Collaboration__
- Team members test features locally before pushing changes
- Significant changes are discussed prior to implementation
- GitHub commit history is used to track feature additions and bug fixes

__Versioning Approach__
The project follows a simplified Semantic Versioning (SemVer) model:

`MAJOR.MINOR.PATCH`
- MAJOR: Breaking architectural or API changes
- MINOR: New features (e.g., Favorites page)
- PATCH: Bug fixes or small improvements
---
### Testing
__Test Plan__

The MyFitnessBud testing strategy is centered around automated end-to-end (E2E) validation using Playwright with xUnit. The goal is to ensure that all user-facing pages and workflows function correctly under realistic browser conditions.

The application consists of 7 pages, each with:
- A corresponding page class (encapsulating selectors and interactions)
- A test class (validating behavior)

This results in a Page Object Model (POM) architecture with 14 total files, promoting:
- Separation of concerns
- Reusability of UI interactions
- Maintainable and scalable test design

All tests inherit from a shared BaseTest class, which standardizes browser setup, teardown, and common interaction utilities.

__Test Types Performed__

A total of 28 tests are executed across all pages.

1. End-to-End (E2E) Tests

Validate complete user workflows from UI interaction to expected outcome
Run in a real Chromium browser environment (headless)

2. UI Interaction Tests

Clicking buttons, filling forms, navigating between pages
Verifying DOM updates and visible text

3. Functional Tests

Ensure core features behave correctly (e.g., form submissions, navigation, data display)

4. Dialog/Alert Handling Tests

Captured via centralized dialog handler in BaseTest
Validates expected alert messages using LastDialogMessage

5. Navigation & Routing Tests

Verify correct URL transitions and page loads

__Analysis Report__

Test Results Summary
- Total Tests: 28
- Passed: 28
- Failed: 0
- Skipped: 0
- Execution Time: ~37 seconds

Observations
- All tests passed successfully, indicating strong functional stability.
- Execution time is efficient for full E2E coverage.
- No flaky or intermittent failures observed.

Strengths
- Full page coverage via POM structure
- Reliable synchronization using explicit waits
- Centralized browser lifecycle management

Potential Improvements
- Add negative test cases (invalid inputs, error states)
- Introduce cross-browser testing (Firefox/WebKit)
- Add performance benchmarking tests
- Integrate code coverage reporting

__Test Automation__

Automation is implemented using:

- Playwright for .NET (browser automation)
- xUnit (test framework)
- Automation Features
- Fully automated browser interactions
- Headless execution for CI environments
- Independent test isolation via browser contexts
- Reusable helper methods from BaseTest

Tests are designed to run:
- Locally during development
- Automatically in CI/CD pipelines

Automated testing is configured via GitHub Actions (playwright.yml).

Workflow Overview

Trigger Conditions

Push to main or master
Pull requests targeting those branches

Execution Steps

1. Checkout repository
2. Set up .NET 10 SDK
3. Install Node.js (required for Playwright)
4. Restore NuGet dependencies
5. Install npm dependencies
6. Install Playwright browsers (Chromium)
7. Start the web application locally
8. Wait for server readiness
9. Execute test suite using dotnet test
10. Upload Playwright test artifacts

Key Configuration Details
- Environment: windows-latest
- Timeout: 60 minutes
- Base URL: http://localhost:5161/
- Browser: Chromium (headless)

Artifact Handling
- Test reports are uploaded as artifacts (playwright-report/)
- Retained for 30 days for debugging and traceability

---
### Change Management

The MyFitnessBud project follows a lightweight but structured change management process to ensure stability while allowing for iterative development. Changes are tracked on GitHub through commits, branches, and pull requests.

__Change Workflow__
1. Create a new feature or fix branch from main
2. Implement changes locally
3. Run tests locally to verify functionality
4. Push branch to GitHub
5. Automated tests run via GitHub Actions
6. Review changes (self-review or peer review if applicable)
7. Keep changes to main only if:
- All tests pass
- No breaking changes are introduced

__Continuous Integration (CI)__

The GitHub Actions workflow ensures:

- Every push and pull request is automatically tested
- The application builds successfully before merging
- Regressions are caught early

__Change Validation__

All changes are validated through:

- Existing automated test suite (28 tests)
- Page Object Model structure ensuring consistent UI validation
- Manual verification when introducing new UI features (if needed)

__Rollback Strategy__

If a change introduces issues:

- Revert the specific commit using Git
- Alternatively, roll back to a previous stable commit on main
- Re-run tests to confirm system stability
---
### Bug Tracking Process

The MyFitnessBud project uses a structured bug tracking workflow to identify, document, and resolve defects efficiently.

__Bug Identification__

Bugs are identified through:
- Automated test failures in CI
- Local development testing
- Manual UI interaction and exploratory testing
- Bug Reporting

Bugs are documented using GitHub Issues, including:
- Clear title and description
- Steps to reproduce
- Expected vs. actual behavior
- Screenshots or logs (if applicable)

__Bug Classification__

Each bug is categorized by:

- Severity

Critical – Application crash or major functionality failure

High – Core feature not working correctly

Medium – Partial functionality issue

Low – Minor UI or cosmetic issue

- Priority

Determines order of resolution based on impact and deadlines
Bug Resolution Workflow
Create a GitHub Issue
Assign severity and priority
Create a fix branch (e.g., fix/form-validation-error)
Implement the fix
Add or update tests (if applicable)
Run full test suite locally
Submit a Pull Request
CI pipeline runs all tests
Merge fix after successful validation
Regression Prevention

To prevent recurring issues:

- Bugs are often accompanied by new or updated tests
- The automated test suite ensures fixes remain stable over time
- CI enforces that all previous functionality still passes

__Tracking & Monitoring__
- Open and closed issues provide a history of defects
- CI results help monitor system health over time
- Test artifacts (Playwright reports) assist in debugging failures
---
### Architectural Design 
Architecture Style: Client-side Web Application (Frontend-based)

Components:
- UI Layer (HTML/CSS/Bootstrap)
- Logic Layer (JavaScript)
- Storage Layer (LocalStorage)\
\
`User -> Browser -> JS Logic -> LocalStorage`

__Architecture Overview__
- Client-server architecture
- Frontend communicates with backend via HTTP requests (REST API)
- Backend handles authentication, business logic, and database interaction
- SQLite stores user credentials, saved favorites, and other persistent data

__Major Components__

__UI Layer__
The presentation layer is made up of:
- HTML pages for Home, Logic, Snacks, Favorites, Calories, and Workouts
- CSS files for page styling
- Bootstrap for a consistent UI design
- JavaScript files to allow user-page interactions 
This layer’s job is to render the interface for the users, collect user input, and display the content as requested by the user.

__Logic Layer__
Currently the logic layer is split between frontend and backend:
- Frontend JavaScript that handles filtering, calorie logging, favorite management, and login behavior
- ASP.NET Core backend provides routing, startup configuration, and structure needed for business logic

__Data Layer__
The data layer consists of Entity Core models and database context classes:
- User
- Favorite
- SnackCache
These models will store the persistent data of the application going forward.

__Storage Layer__ 
