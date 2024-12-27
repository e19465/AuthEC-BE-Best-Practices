# ASP.NET Web API Project

This project is a Web API built using **.NET 8**. It incorporates modern best practices and features, including:

- **JWT Authentication**: Secure access to API endpoints.
- **Role-Based Authentication**: Supports `Admin`, `Teacher`, and `Student` roles.
- **MS SQL Server**: A robust database backend for application data.
- **N-Tier Architecture**: A structured design for better scalability and maintainability.
- **Custom Authorization Policies**: Enables fine-grained access control beyond roles.

---

## Features

### 1. JWT Authentication
- Ensures secure API access with JSON Web Tokens.
- Tokens are issued after successful login and are required for accessing secured endpoints.

### 2. Role-Based Authentication
- Supports the following roles:
  - `Admin`: Has full access to manage the application.
  - `Teacher`: Can access and manage resources specific to teachers.
  - `Student`: Has access to student-specific resources.
- Role-based access control is implemented using `[Authorize(Roles = "RoleName")]`.

### 3. MS SQL Server
- The project uses Microsoft SQL Server for data storage and management.
- Database connection is configured in `appsettings.json`.

### 4. N-Tier Architecture
- **Presentation Layer**: API controllers to handle HTTP requests.
- **Business Logic Layer (BLL)**: Contains application-specific logic.
- **Data Access Layer (DAL)**: Manages database operations.
- Provides clear separation of concerns for easier maintenance and scalability.

### 5. Custom Authorization Policies
- Adds fine-grained access control based on custom business logic or user attributes.
- Policies are defined in the `Policies` directory and applied to endpoints using `[Authorize(Policy = "PolicyName")]`.

---

## Getting Started

### Prerequisites
- **.NET 8 SDK**
- **MS SQL Server**
- **Postman** or any API testing tool.

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/e19465/AuthEC-BE-Best-Practices.git
   cd AuthEC-BE-Best-Practices
