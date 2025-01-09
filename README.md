# GameZone

GameZone is a simple web application built as a practical implementation of **MVC (Model-View-Controller)** architecture to demonstrate **CRUD (Create, Read, Update, Delete)** operations.

## Features

- **Add New Records:** Create new game-related records in the system.
- **View Records:** Display the list of games with detailed information.
- **Update Records:** Edit existing game data.
- **Delete Records:** Remove unwanted records from the database.

## Technologies Used

- **ASP.NET MVC**: For structuring the application using the Model-View-Controller design pattern.
- **Entity Framework**: For database interaction and ORM (Object-Relational Mapping).
- **SQL Server**: As the database management system.
- **Bootstrap**: For responsive and user-friendly design.
- **HTML, CSS, JavaScript**: For front-end development.

## Installation and Setup

1. Clone this repository:
   ```bash
   git clone https://github.com/mo-shosha/GameZone.git
2. Open the project in Visual Studio.

3. Configure the connection string in the appsettings.json file:
   ```bash
    {
      "ConnectionStrings": {
        "DefaultConnection": "Your SQL Server connection string here"
      }
    }

4. Apply migrations to set up the database:
   ```bash
    update-database
