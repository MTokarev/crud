# Overview

CRUD project for future references. Project implies few technics that not so beneficial in small project like current implementation, but could bring benefits in bigger implementation.

# Demo
![demo](./crud.gif)

# What used

Stack includes:
- .Net6 as backend.
- Entity Framework as DLA.
- SQLite as Database (can be easily changed).
- Angular 13 as a frontend.

Patterns:
- Repository.
- Unity of work.

Features:
- Database auto provisioning. If DB does not exist at the startup, then app will create an empty one.
- Automapper to map DB entities to DataTransferObjects and vice versa.
- Responses to control potential errors in services and have a better handling in controllers.
- Swagger API.
- Angular routing.
- Angular employee services.
- Toast notifications.
- Modal window on employee creation.
- Two ways component communication.
- Paginated response

## How to run
To run backend navigate to the project folder and run `dotnet run`.
To start front end run `npm install` and then `ng serve` from `\client` folder.

**Note:** Please make sure that `baseUrl` in `\client\src\assets\config\config.dev.json` is equal backend URL.

TODOs:
- Error controller
- Unit tests
- Switch form from template driven to reactive
- Add search
- Add sorting