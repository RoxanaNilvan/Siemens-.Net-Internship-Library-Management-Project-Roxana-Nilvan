Library Management System – Running Instructions

1. Prerequisites

Before running the application, please ensure the following are installed:
- .NET 7 SDK or higher: https://dotnet.microsoft.com/en-us/download
- PostgreSQL database server (tested with version 14+)

2. Configuration Steps

1. Clone or download the repository locally.

2. Create a PostgreSQL database (example name: librarydb) and update your connection string in the file:
   LibraryManagementSystem.Presentation/appsettings.json

   Example:

   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=librarydb;Username=postgres;Password=your_password"
     },
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
       }
     },
     "AllowedHosts": "*"
   }

3. Open the solution in Visual Studio (open the `.sln` file).

4. Open the Package Manager Console.
   - Set the default project to: LibraryManagementSystem.DataAccess
   - Run the command: Update-Database
   - This will generate all necessary tables: Books, Readers, Loans.

5. Set `LibraryManagementSystem.Presentation` as the startup project.
6. Run the application (F5 or Start).
7. The Swagger UI will open automatically in the browser (https://localhost:xxxx/swagger).

3. Example Usage in Swagger

- Add a new reader:
  POST /api/reader
  {
    "name": "Roxana"
  }

- Add a new book:
  POST /api/book
  {
    "title": "Clean Code",
    "author": "Robert C. Martin",
    "quantity": 3
  }

- Borrow a book:
  POST /api/loan
  {
    "readerId": 1,
    "bookId": 1
  }

- Return a book:
  POST /api/loan/return/1

- View all loans for a specific reader:
  GET /api/loan/reader/1

4. Functionality Added – Point 7 (Optional Feature)

To extend the basic project requirements, I implemented a complete loan tracking system with two additional entities:

- `Reader` – represents a user who borrows books
- `Loan` – tracks borrowing history, including book ID, reader ID, borrow date, and return date

Main features:
- Readers can borrow and return books
- A loan is created only if the book has available stock
- Returning a book increases the stock again
- Each reader has a complete borrowing history
- Prevents borrowing if the book is out of stock
- Prevents returning a loan that was already returned

All loan operations are available and testable through Swagger.

5. Notes

- The application follows a layered architecture (Domain, DataAccess, BusinessLogic, Presentation).
- DTOs are used to return only necessary data and avoid object reference cycles.
- Business logic is encapsulated in service classes with validation for stock and return conditions.
