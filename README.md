# Expense Tracker (ASP.NET Core)

A personal expense tracking web application built using **C# and ASP.NET Core MVC**.
This project allows users to record daily expenses, categorize spending, and visualize financial data.

## Features

* Create, edit, and delete expenses
* Organize expenses by category
* View all expenses in a list
* Filter expenses by date and category
* Dashboard with spending charts
* SQLite database for lightweight local storage

## Tech Stack

* **Backend:** C#
* **Framework:** ASP.NET Core MVC
* **Database:** SQLite
* **ORM:** Entity Framework Core
* **Frontend:** Razor Views + Bootstrap
* **Version Control:** Git / GitHub

## Architecture

This project follows the **MVC (Model–View–Controller)** architecture:

```
Models → Data structures and database entities
Views → Razor UI templates
Controllers → Business logic and request handling
```

## Getting Started

1. Clone the repository

```
git clone https://github.com/shayansj7/expense-tracker-dotnet.git
```

2. Open the project in Visual Studio

3. Run the application

```
F5
```

The application will start on:

```
https://localhost:xxxx
```

## Database

The application uses **SQLite** with **Entity Framework Core migrations**.

To update the database:

```
Add-Migration InitialCreate
Update-Database
```

## Future Improvements

* User authentication
* Monthly spending analytics
* Export expenses to CSV
* API support for mobile apps

## Author

Shayan Shimura
