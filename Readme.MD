# Overview

CRUD project for future references. Project implies few technics that not so beneficial in small project like current implementation, but could bring benefits in bigger implementation.

# What used

Stack includes:
- .Net6 as backend.
- Entity Framework as DLA.
- SQLite as Database (can be easily changed).
- Angular 12 as a frontend.

Patterns:
- Repository.
- Unity of work.

Features:
- Database auto provisioning. If DB does not exist at the startup, then app will create an empty one.
- Automapper to map DB entities to DataTransferObjects and vice versa.
- Responses to control potential errors in services and have a better handling in controllers.
- Swagger API.

TODOs:
- Error controller
- Add front-end
- Unit tests