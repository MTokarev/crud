# Overview 🔍

Yet another CRUD (Create Read Update Delete) project.
It can be used as a base to develop an app on top of it. The project implies a few techniques that make the current implementation more extensible than other available CRUD projects I found on the internet.

# Demo 📺
By default, we have an employee entity that has representation in the database.
Backend returns paginated data to the FrontEnd where the response renders as an editable table.
![demo](./crud.gif)

# What used 🛠️

Stack includes:
- .Net6 as backend.
- Entity Framework as DLA.
- SQLite as Database (can be easily changed).
- Angular 13 as a frontend.

Patterns:
- Repository.
- Unity of work.

Features:
- Database auto-provisioning. If DB does not exist at the startup, the app will create an empty one.
- Automapper to map DB entities to DataTransferObjects and vice versa.
- Responses to control potential errors in services and have better handling in controllers.
- Swagger API.
- Angular routing.
- Angular employee services.
- Toast notifications.
- Modal window on employee creation.
- Two ways component communication.
- Paginated response

## How to run 🏃‍♀️
To run the backend navigate to the project folder and run `dotnet run`.
To start the front end run `npm install` and then `ng serve` from `\client` folder.

**Note:** Please make sure that `baseUrl` in `\client\src\assets\config\config.dev.json` is equal backend URL.

TODOs:
- Error controller
- Unit tests
- Done ~~Switch form from template-driven to reactive~~
- Add search
- Add sorting
