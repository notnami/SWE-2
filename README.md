# MyFitnessBud
__Your fitness and nutrition companion.__

### Product Vision
MyFitnessBud is a web-based snack discovery and food transparency platform designed to help users search for snacks, filter allergens, view ingredient details, and save favorites securely. The system aims to promote informed dietary decisions, especially for users with allergies or dietary restrictions.

The product focuses on:
- Snack search and filtering
- Ingredient transparency
- User personalization via saved favorites
- Secure account management
---
### Project Goals
- Implement snack search by keyword
- Implement allergen filtering
- Display full ingredient lists
- Allow users to save favorites
- Implement authentication system
---
### Release Plan
__Release 1__
- Static snack list
- Search functionality
- Basic UI

__Release 2__
- Allergen filtering
- Ingredient detail modal
- Workout page

__Release 3__
- Favorites feature
- Local storage persistence

__Release 4 (Final)__
- Authentication
- Session management
- UI improvements
- Bug fixes
---
### Definition of Ready
A story is ready when:
- It has clear description
- Acceptance criteria defined
- Dependencies identified
- Estimated effort assigned
---
### Definition of Done 
A story is done when:
- Code is written
- Tested manually
- No console errors
- Pushed to GitHub
- UI reviewed
---
### Architectural Design 
Architecture Style: Client-side Web Application (Frontend-based)

Components:
- UI Layer (HTML/CSS/Bootstrap)
- Logic Layer (JavaScript)
- Storage Layer (LocalStorage)\
\
`User -> Browser -> JS Logic -> LocalStorage`
---
### Development Environment (Tech Stack)
__Frontend__
- HTML5 - Application structure and semantic markup
- CSS3 - Styiling and responsive layout
- JavaScript - Client-side interactivity and DOM manipulation

__Backend__
- C# (.NET) - RESTful API and server-side logic
- SQLite - Lightweight relational database for user and nutrition data

__Development Tools__
- Visual Studio Code - Primary development environment
- Git - Distributed version control
- GitHub - Source control and collaboration platform

__Architecture Overview__
- Client-server architecture
- Frontend communicates with backend via HTTP requests (REST API)
- Backend handles authentication, business logic, and database interaction
- SQLite stores user credentials, saved favorites, and other persistent data
---
### Coding Standards
To maintain code quality and team consistency, the following standards are enforced:

__General Standards__
- Follow clean code principles
- Use meaningful, descriptive variable and function names
- Keep functions small and single-purpose
- Comment non-obvious logic
- Remove dead code before committing

__Frontend Standards__
- Use semantic HTML elements
- Follow consistent indentation (2 or 4 spaces — define what your team uses)
- Use camelCase for JavaScript variables and functions:
- Keep CSS modular and organized by component/feature
- Avoid inline styles and inline JavaScript
- Use event delegation where appropriate

__Backend Standards__
- Follow Microsoft C# naming conventions
- PascalCase for classes and methods
- camelCase for local variables
- Separate concerns:
	- Controllers → Handle HTTP requests
	- Services → Business logic
	- Data access layer → Database interaction
- Use async/await where appropriate
- Validate all user inputs before database operations
- Never store plaintext passwords (hash + salt)

__Database Standards__
- Use normalized schema design
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

At this stage of development, the project uses a single-branch workflow. Since the team is small and development is tightly coordinated, changes are committed directly to main after testing locally. As the project scales, a feature-branch workflow may be adopted to improve parallel development and code review processes.

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
The storage layer currently consists of:
- SQLite database for long-term storage
- localStorage for the current prototype features like favorites, and calorie tracking. 

__Data Flow__
In the current implementation, most user interactions occur directly in the browser. For example, when a user logs in, favorites a snack, or adds a calorie entry, the information is stored in localStorage and immediately reflected in the interface. The backend database structure exists to support future persistence, but the frontend has not yet been fully connected to it for all features.

A typical current data flow is:
`The user interacts with the page in the browser -> JavaScript processes the input -> Data is validated on the client side -> Information is stored in localStorage -> The page re-renders updated results.`

A future data flow would be:
`The user performs an action in the browser -> The frontend sends an HTTP request to the ASP.NET Core backend -> The backend validates the request -> The backend reads or writes to SQLite -> The backend returns the result to the frontend -> The UI updates using persistent server data`

---
### UI/UX Design

__Design Goals__

The UI/UX design for MyFitnessBud is designed to be simple, accessible, and easy to traverse. The app is made mostly for users who want to make tracking their workouts, snacks, and calorie intake more easily. This means making the application interface as easy and smooth to interact with as possible for the users is a high priority.

The main design goals for the UI/UX are as follows: 	
- Make commonly used tasks easy to locate
- Keep navigation consistent across the applications pages
- Provide a clean beginner-friendly interface
- Make interaction as non-confusing for the user as possible

__Navigation Structure__
MyFitnessBud uses a multi-page navigation structure. Beginning at the home page, users can move back and forth through the applications main features via the navigation bar.

Home
|-----Workouts
|-----Calories
|-----Snacks
|-----Favorites
|-----Login/Logout

__Page Descriptions__ 

__Home Page__
The home page is the first page the user will see. It displays the title of the app, a short and sweet description of the application, and quick access to sign up or log in capabilities if they’re not logged into an account. If they’re already logged into the app it will give the user a greeting instead of the application name.

__Login Page__
The login page provides the user with a way to log in to their already made account or create a new account if they don’t have one. Both of these are done on the same page just with separate buttons. 

__Snacks Page__
The snacks page will allow users to search through a list of snacks and even make their own snacks if desired. This page also allows users to mark down certain snacks as favorite snacks using a star icon next to the snack name.

__Favorites Page__
Allows users to access all their favorited snacks, also allows them to unfavorite snacks if they wish.

__Calories Page__
The calories page lets users enter a food name and calorie amount, it will then track this data and let the user know their total amount of calories consumed, as well as showing them all the snacks they’re entered in. 

__Workouts Page__
In the current iteration of the project the workouts page is a placeholder for the future workout functionality. 

__Page Wireframes__ 



__UX Design Decisions__
A list of decisions made to improve user experience:
- Keep navigation consistent across all pages so users know exactly where they are
- Keeping forms simple to remove unnecessary difficulty for users
- Making interfaces update immediately to give users quick feedback
- Separate pages by their uses in order to reduce clutter as well as confusion
---
### Detailed Design

__Backend Design__

__Program.cs:__
Program.cs is the main startup file of the application. It configures services, registers MVC support, sets up the database connection, and defines the middleware pipeline.
Its responsibilities include:
- adding controller and view services
- registering ApplicationDbContext
- configuring SQLite through the default connection string
- enabling HTTPS redirection
- configuring routing
- mapping controller routes

__HomeController.cs:__
The HomeController provides the default MVC routes for the application. It supports the home page, privacy page, and error handling page.
Its responsibilities include:
- returning the main view
- returning the privacy view
- returning error information when needed

__Frontend Module Design__ 
__index.js__
The index.js module controls home page behavior based on whether a user is logged in.
Purpose:
- check loggedInUser in localStorage
- personalize the welcome text when logged in
- hide sign-up and login buttons when a session exists
- convert the logout link into a login link when no user is active
- remove session state when the user logs out

__login.js__
The login.js module handles sign-up and login behavior on the login page.
Purpose:
- read username and password values
- validate that fields are not empty
- create a local user record using localStorage
- compare entered credentials to stored credentials
- save loggedInUser on successful login
- redirect users to the home page

__snacks.js__
The snacks.js module manages the snack discovery page.
Purpose:
- maintain an array of default snack items
- filter snacks based on search text
- render the snack list dynamically
- allow users to add new snacks'
- mark or unmark a snack as favorite
- persist favorites in localStorage
The module uses star icons to visually communicate favorite status.

__favorites.js__
The favorites.js module manages the favorites page.
Purpose:
- read favorite items from localStorage
- render the saved favorites list
- remove selected favorites
- update localStorage after changes

__calories.js__
The calories.js module manages calorie tracking.
Purpose:
- read calorie entries from localStorage 
- display previously stored entries
- validate new input
- add new food/calorie records
- update total calorie count dynamically
- persist entries in localStorage

__workouts.js__
The workouts.js file currently has no logic in the current prototype.

__Design Decisions__
- localStorage was chosen for the prototype as a temporary storage location for the application due to being fast and easy to implement
- Page-specific JavaScript files keep features separated and easier to maintain for future development
- SQLite was chosen as a lightweight database that is appropriate for development
- Entity Framework Core simplifies data modeling and future backend expansion
- Bootstrap supports consistent styling and responsive layout to make the user experience more smooth
