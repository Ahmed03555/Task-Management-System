# Task Management System

A full-stack Task Management System built with **ASP.NET Core Web API** and **React**, designed to help users organize projects, manage tasks, collaborate, and track progress through a clean and maintainable architecture.

The project focuses on applying software engineering principles, Design Patterns, and Clean Architecture to build a scalable and testable application.

---

## 🚀 Features

### Authentication & Authorization

* User Registration and Login.
* Role-based access control.
* Secure API endpoints.
* User management and role updates.

### Project Management

* Create, view, update, and delete projects.
* Assign project ownership.
* Retrieve projects with pagination.
* Search and filter project data.

### Task Management

* Create and manage tasks.
* Associate tasks with projects.
* Retrieve tasks by project.
* Track task information and status.

### Collaboration

* Comments associated with tasks.
* Organized project and task workflows.

### API Features

* RESTful API design.
* Pagination and searching.
* Centralized request handling.
* Consistent API responses.
* Input validation and error handling.

---

## 🛠️ Tech Stack

### Backend

| Technology            | Purpose                                 |
| --------------------- | --------------------------------------- |
| C#                    | Backend programming language            |
| ASP.NET Core Web API  | RESTful API development                 |
| Entity Framework Core | ORM and database access                 |
| SQL Server            | Relational database                     |
| MediatR               | Request handling and Mediator Pattern   |
| CQRS                  | Separation of read and write operations |
| AutoMapper            | Object-to-object mapping                |
| FluentValidation      | Request validation                      |
| JWT                   | Authentication                          |
| Dependency Injection  | Loose coupling and testability          |

### Frontend

| Technology   | Purpose               |
| ------------ | --------------------- |
| React        | User interface        |
| TypeScript   | Type-safe development |
| React Router | Client-side routing   |
| Axios        | HTTP requests         |
| Tailwind CSS | UI styling            |

---

## 🏗️ Architecture

The backend follows **Clean Architecture** principles to separate business logic from infrastructure and presentation concerns.

### Architecture Layers

```text
Task Management System
│
├── Domain
│   ├── Entities
│   ├── Enums
│   └── Business Rules
│
├── Application
│   ├── Commands
│   ├── Queries
│   ├── Handlers
│   ├── DTOs
│   ├── Validators
│   └── Interfaces
│
├── Infrastructure
│   ├── Database
│   ├── Entity Framework Core
│   └── External Implementations
│
└── WebApi
    ├── Controllers
    ├── Middleware
    ├── Authentication
    └── Dependency Injection
```

### Request Flow

```text
React Frontend
      │
      ▼
ASP.NET Core Controller
      │
      ▼
MediatR
      │
      ▼
Command / Query
      │
      ▼
Handler
      │
      ▼
Application Abstractions
      │
      ▼
Infrastructure
      │
      ▼
SQL Server
```

---

## 🧩 Design Patterns

The project applies several design patterns and architectural concepts to improve maintainability, scalability, and testability.

### 1. CQRS Pattern

Separates operations that modify data from operations that retrieve data.

* **Commands:** CreateProjectCommand, CreateTaskCommand, DeleteTaskCommand.
* **Queries:** GetAllProjectsQuery, GetTaskByIdQuery.

**Why?**

* Separation of concerns.
* Independent request handling.
* Easier maintenance and testing.
* Better organization of business logic.

### 2. Mediator Pattern

Implemented using MediatR to decouple controllers from application logic.

Controllers send requests to the mediator, which dispatches them to their corresponding handlers.

**Why?**

* Reduces direct dependencies.
* Keeps controllers lightweight.
* Centralizes request handling.
* Supports pipeline behaviors such as validation and logging.

### 3. Dependency Injection

Dependencies are injected through constructors and managed by the ASP.NET Core DI container.

**Why?**

* Loose coupling.
* Easier unit testing.
* Flexible implementations.
* Better dependency management.

### 4. Data Access Abstraction

Uses `IApplicationDbContext` to abstract database access from application handlers.

**Why?**

* Separates application logic from infrastructure details.
* Improves testability.
* Reduces direct dependencies on database implementations.

### 5. DTO Pattern

Uses Data Transfer Objects to control the data transferred between application layers and API clients.

**Why?**

* Prevents exposing database entities directly.
* Controls API response structure.
* Reduces unnecessary data transfer.
* Separates API contracts from database models.

### 6. Result Pattern

Provides a structured way to represent successful and failed operations.

**Why?**

* Consistent operation responses.
* Cleaner error handling.
* Easier frontend integration.

### 7. Pagination

Retrieves large datasets in smaller pages instead of loading all records at once.

**Why?**

* Reduces response payload size.
* Improves API efficiency.
* Supports scalable data retrieval.
* Improves frontend usability.

---

## 📂 Project Structure

```text
Task-Management-System/
│
├── Backend/
│   ├── TaskManagement.Domain/
│   ├── TaskManagement.Application/
│   ├── TaskManagement.Infrastructure/
│   └── TaskManagement.WebApi/
│
├── Frontend/
│   └── React Application
│
└── README.md
```

*The directory names above represent the logical project organization. Adjust them to match the actual repository structure.*

---

## ⚙️ Getting Started

### Prerequisites

Make sure you have installed:

* .NET SDK
* SQL Server
* Node.js and npm
* Git

### 1. Clone the Repository

```bash
git clone https://github.com/Ahmed03555/Task-Management-System.git

cd Task-Management-System
```

### 2. Backend Setup

Navigate to the backend Web API project:

```bash
cd Backend/TaskManagement.WebApi
```

Restore dependencies:

```bash
dotnet restore
```

Configure your database connection string in `appsettings.json` or User Secrets.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=TaskManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Apply database migrations:

```bash
dotnet ef database update
```

Run the API:

```bash
dotnet run
```

The API will be available at the URL configured in the launch settings.

### 3. Frontend Setup

Navigate to the React application:

```bash
cd Frontend
```

Install dependencies:

```bash
npm install
```

Configure the API base URL in your frontend environment configuration.

Start the development server:

```bash
npm run dev
```

Open the local URL displayed in your terminal.

> Note: Update the example directory paths and migration commands according to the actual repository structure.

---

## 🔌 API Endpoints

The following are representative endpoints based on the implemented API controllers.

### Users

| Method | Endpoint               | Description         |
| ------ | ---------------------- | ------------------- |
| POST   | `/api/Users/register`  | Register a new user |
| POST   | `/api/Users/login`     | Authenticate a user |
| GET    | `/api/Users`           | Get paginated users |
| GET    | `/api/Users/{id}`      | Get user by ID      |
| DELETE | `/api/Users/{id}`      | Delete a user       |
| PUT    | `/api/Users/{id}/role` | Update user role    |

### Projects

| Method | Endpoint        | Description       |
| ------ | --------------- | ----------------- |
| GET    | `/api/Projects` | Retrieve projects |
| POST   | `/api/Projects` | Create a project  |

Additional project and task operations depend on the corresponding controller actions.

### Pagination Example

```http
GET /api/Users?pageNumber=1&pageSize=10&search=Ahmed
```

---

## 🧪 Testing

The architecture supports unit testing by separating application logic from infrastructure dependencies.

Recommended testing areas:

* Command and Query Handlers.
* Request validators.
* Business rules.
* Authorization and access control.
* API endpoint behavior.

---

## 🎯 Learning Objectives

This project demonstrates practical experience with:

* ASP.NET Core Web API development.
* Clean Architecture.
* CQRS and MediatR.
* SOLID principles.
* Entity Framework Core.
* Dependency Injection.
* Authentication and Authorization.
* RESTful API design.
* React and TypeScript integration.
* Maintainable and testable software design.

---

## 👨‍💻 Author

**Ahmed Taha**

Backend Developer | ASP.NET Core | C#

GitHub: [Ahmed03555](https://github.com/Ahmed03555)

Project Repository: [Task Management System](https://github.com/Ahmed03555/Task-Management-System)

---

⭐ If you find this project useful, feel free to star the repository!
