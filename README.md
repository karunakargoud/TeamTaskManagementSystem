# Team Task Management System

## Project Overview

Team Task Management System is a role-based task management application developed using .NET 8 Web API and React.js.

The application allows administrators, managers, and users to manage teams, assign tasks, update task status, add comments, and view task-related information.

## User Roles

### Admin

* Manage users
* Manage teams
* Assign tasks
* Manage team members

### Manager

* Create tasks
* Assign tasks to team members
* Manage team members
* Update task information

### User

* View assigned tasks
* Update task status
* Add comments
* View task details

## Technology Stack

### Backend

* C#
* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* JWT Authentication
* BCrypt Password Hashing
* Swagger / OpenAPI
* Clean Architecture
* Repository Pattern
* Dependency Injection

### Frontend

* React.js
* JavaScript
* HTML5
* CSS3
* Axios
* Vite

### Testing

* xUnit
* Moq

### Development Tools

* Visual Studio 2022
* Visual Studio Code
* SQL Server Management Studio
* Git
* GitHub

## Project Structure

```text
TeamTaskManagementSystem
│
├── Backend
│   ├── TeamTaskManagement.API
│   ├── TeamTaskManagement.Application
│   ├── TeamTaskManagement.Domain
│   ├── TeamTaskManagement.Infrastructure
│   └── TeamTaskManagement.Tests
│
├── Frontend
│   └── task-management-ui
│
├── README.md
└── .gitignore
```

## Backend Architecture

The backend follows Clean Architecture.

```text
API
 │
 ▼
Application
 │
 ▼
Domain

Infrastructure
 │
 └── Database / Repository Implementation
```

### API Layer

Contains:

* Controllers
* Program.cs
* JWT configuration
* Swagger configuration
* Middleware configuration

### Application Layer

Contains:

* DTOs
* Interfaces
* Services
* Business logic

### Domain Layer

Contains:

* Entities
* Enums
* Core business models

### Infrastructure Layer

Contains:

* Entity Framework Core
* DbContext
* Repository implementations
* Database access

## Database

Database:

```text
TeamTaskManagementDB
```

Database platform:

```text
Microsoft SQL Server
```

The application uses Entity Framework Core for database access.

## Backend Setup

### 1. Open the solution

Open:

```text
Backend/TeamTaskManagementSystem.sln
```

using Visual Studio 2022.

### 2. Configure the database

Update the connection string in:

```text
TeamTaskManagement.API/appsettings.json
```

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=TeamTaskManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Replace `YOUR_SERVER` with your SQL Server instance.

### 3. Create the database

Create the database:

```text
TeamTaskManagementDB
```

and create the required tables using the SQL scripts provided with the project.

### 4. Set startup project

In Visual Studio:

```text
Right click TeamTaskManagement.API
        ↓
Set as Startup Project
```

### 5. Run the backend

Press:

```text
F5
```

or:

```text
Ctrl + F5
```

## Swagger API Documentation

Swagger is included in the application.

After running the API, open:

```text
/swagger/index.html
```

Example:

```text
https://localhost:7198/swagger/index.html
```

The actual port may be different depending on the local configuration.

## API Endpoints

### Authentication

```text
POST /api/Auth/register
POST /api/Auth/login
```

### Users

```text
GET    /api/Users
GET    /api/Users/{id}
POST   /api/Users
PUT    /api/Users/{id}
DELETE /api/Users/{id}
```

### Teams

```text
GET    /api/Teams
GET    /api/Teams/{id}
POST   /api/Teams
PUT    /api/Teams/{id}
DELETE /api/Teams/{id}
```

### Work Items

```text
GET    /api/WorkItems
GET    /api/WorkItems/{id}
POST   /api/WorkItems
PUT    /api/WorkItems/{id}
DELETE /api/WorkItems/{id}
```

### Comments

GET  /api/Comments
POST /api/Comments

### Dashboard

GET /api/Dashboard

## JWT Authentication

The application uses JWT Bearer authentication.

After successful login, the API returns a JWT token.

The token can be used to access protected endpoints.

In Swagger, click:

Authorize

and enter:

Bearer YOUR_JWT_TOKEN

## Frontend Setup

Navigate to:
Frontend/task-management-ui

Install dependencies:

npm install

Start the React application:

npm run dev

The frontend will normally run on:

http://localhost:5173

## Sample Credentials

Use the following credentials if they have been created in the database:

### Admin

Email: admin@test.com
Password: Admin@123

### Manager

Email: manager@test.com
Password: Manager@123

### User

Email: user@test.com
Password: User@123

## Task Status

The application supports:
To Do
In Progress
Done

## Priority

Tasks can have different priorities such as:
Low
Medium
High

## Notifications

The application includes notification handling when tasks are assigned or task status is updated.

## Testing

The project contains unit tests using:

* xUnit
* Moq

Run tests from Visual Studio:

Test
 ↓
Test Explorer
 ↓
Run All Tests

## Running the Complete Application

### Backend

Visual Studio 2022
        ↓
TeamTaskManagement.API
        ↓

### Frontend

cd Frontend/task-management-ui
npm install
npm run dev

## Authentication Flow

User
  ↓
Login
  ↓
Auth API
  ↓
Validate Email & Password
  ↓
Generate JWT
  ↓
Return JWT Token
  ↓
Frontend stores token
  ↓
Token sent with API requests
  ↓
JWT Authentication
  ↓
Role-Based Authorization

## License

This project was developed as a technical assessment project.
