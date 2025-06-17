# CRM System

This is a Customer Relationship Management (CRM) system built with Blazor Web App, .NET 7, Entity Framework Core, and SQL Server. It allows users to manage customer data and includes user authentication.

## Features

- User registration and login
- Create, Read, Update, and Delete (CRUD) operations for customer records
- Dashboard to display and manage customer information

## Setup

To run this application, you'll need to set up a SQL Server database and configure the connection string.

### Database Setup

1.  **Ensure SQL Server is running:** You need an accessible instance of SQL Server.
2.  **Configure Connection String:**
    Open `CRMSystem/appsettings.json`.
    Locate the `ConnectionStrings` section and update the `DefaultConnection` value with your SQL Server connection string. It should look something like this:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=your_server_name;Database=CrmSystemDB;User ID=your_user;Password=your_password;Trusted_Connection=False;Encrypt=True;"
    }
    ```
    Replace `your_server_name`, `CrmSystemDB`, `your_user`, and `your_password` with your actual SQL Server details.
3.  **Apply Migrations:**
    Open a terminal or command prompt, navigate to the `CRMSystem` directory, and run the following command to create or update the database schema:
    ```bash
    dotnet ef database update
    ```

### Running the Application

1.  Navigate to the `CRMSystem` directory in your terminal.
2.  Run the application using the following command:
    ```bash
    dotnet run
    ```
3.  Open your web browser and navigate to the application URL (usually `https://localhost:XXXX` or `http://localhost:YYYY`, which will be displayed in the terminal). You will be prompted to log in or sign up.

## Application Structure

Here's a brief overview of the key files and directories in the project:

-   `CRMSystem/`: The main project directory.
    -   `Program.cs`: The main entry point of the application. This file configures services (like `AppDbContext`, Identity, Blazor services), middleware, and the request processing pipeline.
    -   `Data/AppDbContext.cs`: Defines the Entity Framework `DbContext`. It inherits from `IdentityDbContext` to include ASP.NET Core Identity tables and includes a `DbSet<Customer>` for customer data.
    -   `Models/Customer.cs`: Defines the `Customer` entity with properties like `Id`, `Name`, `Email`, `Phone`, and `Address`.
    -   `Migrations/`: Contains Entity Framework Core database migration files, which track changes to the database schema.
    -   `Pages/`: Contains the routable Blazor components (pages).
        -   `Dashboard.razor`: The main page for displaying and managing customer records (CRUD operations). Requires authentication.
        -   `Login.razor`: Handles user login.
        -   `Signup.razor`: Handles user registration.
        -   `Index.razor`: The landing page, typically redirects to Login or Dashboard.
        -   `_Host.cshtml`: The main host page for the Blazor Server application.
    -   `Shared/`: Contains shared Blazor components like `MainLayout.razor` (defines the overall page structure) and `NavMenu.razor` (navigation menu).
    -   `appsettings.json`: Configuration file for the application, including the database connection string.
    -   `wwwroot/`: Contains static assets like CSS, JavaScript, and images (e.g., login background, logo).

## Code Documentation

This section provides more detail on some of the main components of the application.

### `Models/Customer.cs`

This class defines the structure of a customer record:

```csharp
namespace CRMSystem.Models
{
    public class Customer
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
```

### `Data/AppDbContext.cs`

This class is the Entity Framework Core database context. It's responsible for database interactions and integrates with ASP.NET Core Identity.

```csharp
using Microsoft.EntityFrameworkCore;
using CRMSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CRMSystem.Data
{
    public class AppDbContext : IdentityDbContext // Inherits from IdentityDbContext for user authentication
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; } // Represents the Customers table
    }
}
```
-   The constructor accepts `DbContextOptions<AppDbContext>` which allows configuration (like the connection string) to be passed in from `Program.cs`.
-   `Customers`: This `DbSet<Customer>` property represents the collection of customer records in the database. EF Core uses this to perform CRUD operations on the `Customers` table.
-   By inheriting from `IdentityDbContext`, it automatically includes `DbSet`s for Identity tables (Users, Roles, etc.).

### `Pages/Dashboard.razor`

This is the main Blazor component for interacting with customer data after a user is authenticated.

-   **Authentication Check:** It uses `AuthenticationStateProvider` to ensure the user is logged in before displaying customer data. If not authenticated, it redirects to the login page.
-   **Displaying Customers:** It fetches and displays a list of `Customer` objects in a grid or card layout.
-   **Adding Customers:** An inline form allows users to input details for a new customer (`newCustomer` object) and submit it.
-   **Editing Customers:** Users can switch a customer's display to an edit mode (`editCustomer` object), modify details, and save changes.
-   **Deleting Customers:** A "Delete" button is provided for each customer.
-   **Data Operations:** It injects `AppDbContext` (`_dbContext`) to directly interact with the database for creating, reading, updating, and deleting customer records. Methods like `LoadCustomers()`, `CreateCustomer()`, `EditCustomer()`, `UpdateCustomer()`, and `DeleteCustomer()` handle these operations.

### User Authentication

-   **ASP.NET Core Identity:** The application uses ASP.NET Core Identity for managing users, passwords, and roles. This is configured in `Program.cs`.
-   **Login/Signup Pages:**
    -   `Pages/Login.razor`: Allows existing users to sign in. It typically uses `SignInManager<IdentityUser>`.
    -   `Pages/Signup.razor`: Allows new users to register. It typically uses `UserManager<IdentityUser>`.
-   **Authentication State:** The `AuthenticationStateProvider` service is used throughout the application (especially in `.razor` components) to get the current user's authentication state and identity.
-   **Authorization:** Pages or features requiring authentication can be protected using the `[Authorize]` attribute or by checking `isAuthenticated` flags.

This CRM System provides a foundational platform for managing customer interactions and data.
