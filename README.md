# Task Management System

A RESTful Task Management System built using **ASP.NET Core Web API**, following Clean Architecture principles and applying modern software design patterns to create a maintainable, scalable, and testable backend application.

The system provides APIs for managing users, projects, tasks, and collaboration features, with authentication, authorization, and structured request handling.

---

## 🚀 Features

### Authentication & Authorization

* User Registration and Login.
* Role-Based Access Control.
* Secure API endpoints.
* User management and role updates.

### Project Management

* Create and manage projects.
* Assign project ownership.
* Retrieve projects with pagination.
* Search and filter project data.

### Task Management

* Create, retrieve, update, and delete tasks.
* Associate tasks with projects.
* Retrieve tasks by project.
* Manage task-related information.

### Collaboration

* Task comments.
* Organized project and task workflows.

### API Capabilities

* RESTful API design.
* Pagination and searching.
* Request validation.
* Centralized request handling.
* Consistent API responses.
* Error handling.

---

## 🛠️ Tech Stack

| Technology            | Purpose                                 |
| --------------------- | --------------------------------------- |
| C#                    | Backend programming language            |
| ASP.NET Core Web API  | RESTful API development                 |
| Entity Framework Core | ORM and database access                 |
| SQL Server            | Relational database                     |
| MediatR               | Mediator Pattern and request handling   |
| CQRS                  | Separation of read and write operations |
| AutoMapper            | Object-to-object mapping                |
| FluentValidation      | Request validation                      |
| JWT                   | Authentication                          |
| Dependency Injection  | Loose coupling and testability          |

---

## 🏗️ Architecture

The project follows **Clean Architecture** principles, separating business logic from infrastructure and presentation concerns.

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
│   └── Implementations
│
└── WebApi
    ├── Controllers
    ├── Middleware
    ├── Authentication
    └── Dependency Injection
```

### Request Flow

```text
HTTP Request
     │
     ▼
API Controller
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

### 1. CQRS Pattern

Separates operations that modify data (Commands) from operations that retrieve data (Queries).

**Why?**

* Separation of concerns.
* Independent request handling.
* Easier maintenance and testing.
* Better organization of business logic.

### 2. Mediator Pattern

Implemented using MediatR to decouple API controllers from application logic.

**Why?**

* Reduces direct dependencies.
* Keeps controllers lightweight.
* Centralizes request handling.
* Supports pipeline behaviors such as validation and logging.

### 3. Dependency Injection

Uses constructor injection and the ASP.NET Core DI container to manage dependencies.

**Why?**

* Loose coupling.
* Improved testability.
* Flexible implementations.
* Better dependency management.

### 4. Data Access Abstraction

Uses `IApplicationDbContext` to abstract database access from application handlers.

**Why?**

* Separates application logic from infrastructure details.
* Improves testability.
* Reduces direct dependencies on database implementations.

### 5. DTO Pattern

Uses Data Transfer Objects to control data exchanged between application layers and API clients.

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
* Easier API integration.

### 7. Pagination

Retrieves large datasets in smaller pages instead of loading all records at once.

**Why?**

* Reduces response payload size.
* Improves API efficiency.
* Supports scalable data retrieval.

---

## ⚙️ Getting Started

### Prerequisites

* .NET SDK
* SQL Server
* Visual Studio 2022 or Visual Studio Code
* Git

### 1. Clone the Repository

```bash
git clone https://github.com/Ahmed03555/Task-Management-System.git

cd Task-Management-System
```

### 2. Configure the Database

Update the connection string in `appsettings.json` or User Secrets.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=TaskManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 3. Apply Database Migrations

```bash
dotnet ef database update
```

### 4. Run the API

```bash
dotnet restore
dotnet run
```

The API will be available at the URL configured in the project's launch settings.

---

## 🔌 API Endpoints

### Users

| Method | Endpoint               | Description              |
| ------ | ---------------------- | ------------------------ |
| POST   | `/api/Users/register`  | Register a new user      |
| POST   | `/api/Users/login`     | Authenticate a user      |
| GET    | `/api/Users`           | Retrieve paginated users |
| GET    | `/api/Users/{id}`      | Get user by ID           |
| DELETE | `/api/Users/{id}`      | Delete a user            |
| PUT    | `/api/Users/{id}/role` | Update user role         |

### Projects

| Method | Endpoint        | Description       |
| ------ | --------------- | ----------------- |
| GET    | `/api/Projects` | Retrieve projects |
| POST   | `/api/Projects` | Create a project  |

### Pagination Example

```http
GET /api/Users?pageNumber=1&pageSize=10&search=Ahmed
```

---

## 🎯 Key Learning Outcomes

* Applying Clean Architecture in ASP.NET Core.
* Implementing CQRS with MediatR.
* Building RESTful APIs.
* Applying SOLID principles and Design Patterns.
* Implementing authentication and authorization.
* Working with Entity Framework Core.
* Designing maintainable and testable backend systems.

---

## 👨‍💻 Author

**Ahmed Yasser**

Backend Developer | ASP.NET Core | C#

GitHub: [Ahmed03555](https://github.com/Ahmed03555)

---

⭐ If you find this project useful, feel free to star the repository!
