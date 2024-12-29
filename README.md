# ASP.NET Web API Project - Complete Authentication System

This project is a Web API built using **.NET 8**. It incorporates modern best practices and features, including:

- **JWT Authentication**: Secure access to API endpoints.
- **Email Confirmation**: Ensures verified user accounts before allowing access.
- **Role-Based Authentication**: Supports `Admin`, `Teacher`, and `Student` roles.
- **MS SQL Server**: A robust database backend for application data.
- **N-Tier Architecture**: A structured design for better scalability and maintainability.
- **Custom Authorization Policies**: Enables fine-grained access control beyond roles.

---

## Features

### 1. JWT Authentication
- Ensures secure API access with JSON Web Tokens.
- Tokens are issued after successful login and are required for accessing secured endpoints.

### 2. Email Confirmation
- New users must verify their email address before signing in.
- After registration, an email is sent to the user containing a confirmation link.
- The confirmation link includes a unique token that, when clicked, activates the user account.
- Unverified users cannot sign in until their email address is confirmed.
- Email confirmation logic is handled through a dedicated service.

### 3. Role-Based Authentication
- Supports the following roles:
  - `Admin`: Has full access to manage the application.
  - `Teacher`: Can access and manage resources specific to teachers.
  - `Student`: Has access to student-specific resources.
- Role-based access control is implemented using `[Authorize(Roles = "RoleName")]`.

### 4. MS SQL Server
- The project uses Microsoft SQL Server for data storage and management.
- Database connection is configured in `appsettings.json`.

### 5. N-Tier Architecture
- **Presentation Layer**: API controllers to handle HTTP requests.
- **Business Logic Layer (BLL)**: Contains application-specific logic.
- **Data Access Layer (DAL)**: Manages database operations.
- Provides clear separation of concerns for easier maintenance and scalability.

### 6. Custom Authorization Policies
- Adds fine-grained access control based on custom business logic or user attributes.
- Policies are defined in the `Policies` directory and applied to endpoints using `[Authorize(Policy = "PolicyName")]`.

---

## Email Confirmation Flow

1. **User Registration**:
   - Upon registration, the user’s account is created but remains inactive until the email is confirmed.
   - A unique email confirmation token is generated and stored in the database.

2. **Email Delivery**:
   - An email containing a confirmation link is sent to the user's registered email address.
   - The link format: `https://your-api-domain.com/api/auth/confirm-email?email={email}&token={token}`.

3. **Email Confirmation**:
   - When the user clicks the link, a request is made to the `ConfirmEmail` endpoint.
   - The system verifies the email and token, activates the account, and allows the user to sign in.

4. **Prevention of Sign-In for Unverified Users**:
   - API endpoints reject login attempts for accounts where email confirmation is pending.

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
   ```

2. Update the database connection string in `appsettings.json`.

3. Apply migrations to the database:
   ```bash
   dotnet ef database update
   ```

4. Configure email delivery settings for the email confirmation service in `appsettings.json`.

5. Run the application:
   ```bash
   dotnet run
   ```

---

With the email confirmation feature, your Web API now provides an additional layer of security and ensures only verified users can access the application.
