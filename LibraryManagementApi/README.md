# LibraryManagementApi

Click [here](https://localhost) for demo. 
Credentials:

| Username | Password | Role |
| ------------- |:-------------:| :-----:|
| admin | pass | admin / librarian |
| john.snow | throne | regular user |

# Specifications of this API (Technologies used)
 - .Net Core 6 (3.0+)
 - Docker
 - Swagger
 - PostgreSQL
 - EntityFramework Code-First approach
 - JWT authentication
 - Error handling/logging

# Project main ideas and rules
- one person can rent max. 5 books at once for a max. period of 30 days. _(o persoana poate imprumuta maxim 5 carti pentru o perioada de maxim 30 de zile)_
- one book can be rented only if it is available _(o carte poate fi imprumutata doar daca exista pe stoc.)_

# Feature requests
- CRUD operations for books, authors and lends
- Code-First approach
- Use these technologies to implement the solution: 
   - Swagger
   - PostgreSQL
   - EntityFramework
   - JWT authentication
   - error handling/logging
- Provide username and password for testing.

# Implementation
- I have added a background service that runs every 24 hours to update all users's BookRentalOverdue property.
- This property is also updated when a user returns the books.
- I didn't have the time to also implement the logging and error handling, but here are the next steps I would have done : 
   - use try catch blocks to catch the errors
   - depending on the error type and errorCode, I would have logged them as information warnings, errors, and critical errors.
   - provide the user with a meaningful error message (which would either be stored in const variables or in a text resource file)