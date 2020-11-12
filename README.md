# Healthy-Me - Online food ordering website

This is a backend application for an online food ordering website called Healthy-Me.

## Installation

1. Download and install [.NET SDK](https://dotnet.microsoft.com/download) (If not already installed)
2. Clone this repository
3. Execute the command "dotnet run" inside the WebApp directory

## Admin user
The website has an admin user, who can edit, create and delete all sorts of items.<br>
The admin user account information can be changed in DAL.App.EF -> Helpers -> DataInitializers

**Default admin user credentials:**
```
Username: admin@taltech.ee"
Password = "Bad.Password.2020"
First name = "Admin"
Last name = "User"
Phone number = "54395039"
```

## API documentation with Swagger
Launch the server and go to:
```
https://localhost:5001/swagger/
```

## Technologies
* ASP.NET Core
* Entity Framework Core
* NuGet packages created by me
* Swagger
* JWT Authentication
* Razor pages
* Microsoft SQL server
* SQLite
* PostgreSQL

## Authors

* **Marko Gordejev** - [markogo](https://github.com/markogo)
