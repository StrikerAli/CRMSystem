# Blazor CRUD Application

This is a simple To-Do list application built with Blazor Web App, .NET 8, Entity Framework Core, and SQL Server. It demonstrates basic CRUD (Create, Read, Update, Delete) operations for managing To-Do items.

## Setup

To run this application, you'll need to set up a SQL Server database and configure the connection string.

### Database Setup

1.  **Ensure SQL Server is running:** You need an accessible instance of SQL Server.
2.  **Configure Connection String:**
    Open `CrudApp/appsettings.json` and `CrudApp/appsettings.Development.json`.
    Locate the `ConnectionStrings` section and update the `DefaultConnection` value with your SQL Server connection string. It should look something like this:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=your_server_name;Database=ToDoDB;User ID=your_user;Password=your_password;Trusted_Connection=False;Encrypt=True;"
    }
    ```
    Replace `your_server_name`, `ToDoDB`, `your_user`, and `your_password` with your actual SQL Server details.
3.  **Apply Migrations:**
    Open a terminal or command prompt, navigate to the `CrudApp` directory, and run the following command to create the database schema:
    ```bash
    dotnet ef database update
    ```

### Running the Application

1.  Navigate to the `CrudApp` directory in your terminal.
2.  Run the application using the following command:
    ```bash
    dotnet run
    ```
3.  Open your web browser and navigate to the application URL (usually `https://localhost:XXXX` or `http://localhost:YYYY`, which will be displayed in the terminal).

## Application Structure

Here's a brief overview of the key files and directories in the project:

-   `CrudApp/`: The main project directory.
    -   `Program.cs`: The main entry point of the application. This file configures services, middleware, and the request processing pipeline.
    -   `Data/AppDbContext.cs`: Defines the Entity Framework `DbContext` for interacting with the database. It includes `DbSet` properties for the application's models.
    -   `Models/ToDoItem.cs`: Represents the schema for the `ToDoItems` table in the database.
    -   `Migrations/`: Contains Entity Framework Core database migration files, which track changes to the database schema.
    -   `Components/`: Contains the Blazor components.
        -   `Pages/`: Contains the routable Blazor components (pages).
            -   `TodoList.razor`: The main page for displaying and managing To-Do items.
        -   `Layout/`: Contains layout components like `MainLayout.razor` and `NavMenu.razor`.
    -   `ToDoService.cs`: A service class responsible for handling the business logic for CRUD operations on To-Do items. It interacts with `AppDbContext`.
    -   `appsettings.json` (and `appsettings.Development.json`): Configuration files for the application, including the database connection string.
    -   `wwwroot/`: Contains static assets like CSS, JavaScript, and images.

## Code Documentation

This section provides more detail on the main components of the application.

### `Models/ToDoItem.cs`

This class defines the structure of a To-Do item:

```csharp
namespace CrudApp.Models
{
    public class ToDoItem
    {
        public int Id { get; set; } // Primary Key
        public string Title { get; set; } // The content of the To-Do item
        public bool IsCompleted { get; set; } // Status of the To-Do item
    }
}
```

### `Data/AppDbContext.cs`

This class is the Entity Framework Core database context. It's responsible for database interactions.

```csharp
using CrudApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ToDoItem> ToDoItems { get; set; } // Represents the ToDoItems table
    }
}
```
-   The constructor accepts `DbContextOptions<AppDbContext>` which allows configuration (like the connection string) to be passed in from `Program.cs`.
-   `ToDoItems`: This `DbSet<ToDoItem>` property represents the collection of To-Do items in the database. EF Core uses this to perform CRUD operations on the `ToDoItems` table.

### `ToDoService.cs`

This service encapsulates the logic for managing To-Do items. It's injected into components that need to interact with To-Do data.

Key methods:
-   `GetToDoItemsAsync()`: Retrieves all To-Do items from the database.
-   `AddToDoItemAsync(ToDoItem item)`: Adds a new To-Do item to the database.
-   `UpdateToDoItemAsync(ToDoItem item)`: Updates an existing To-Do item in the database.
-   `DeleteToDoItemAsync(ToDoItem item)`: Deletes a To-Do item from the database.

This service uses the `AppDbContext` to perform these operations.

### `Components/Pages/TodoList.razor`

This is the main Blazor component for interacting with the To-Do list.

-   **Displaying Items:** It fetches and displays the list of `ToDoItem` objects. Each item shows its title, a checkbox for its completion status, and a delete button.
-   **Adding Items:** An input field and an "Add" button allow users to create new To-Do items.
    -   Input validation ensures the title is not empty.
    -   A JavaScript confirmation dialog (`JSRuntime.InvokeAsync<bool>("confirmDialog", ...)` ) is used before adding an item.
-   **Updating Items:** Users can toggle the completion status of an item using the checkbox. This triggers the `UpdateItem` method.
-   **Deleting Items:** A "Delete" button is provided for each item.
    -   A JavaScript confirmation dialog is used before deleting an item.
-   **User Feedback:** The component uses `IJSRuntime` to show JavaScript notifications (`JSRuntime.InvokeVoidAsync("showNotification", ...)` ) to the user after adding, updating, or deleting items, or if there's an input validation error.
-   **State Management:** The component maintains a list of `toDoItems` and a `newItem` object for the add form. It calls `RefreshToDoItems()` to update the list after any CRUD operation.
-   **Dependency Injection:** It injects `ToDoService` to interact with the backend, `NavigationManager` for potential navigation tasks (not heavily used in this specific page but good practice), and `IJSRuntime` to call JavaScript functions.
