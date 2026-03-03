# MyFitnessBud
__Your fitness and nutrition companion.__

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
