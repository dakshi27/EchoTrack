# EchoTrack

EchoTrack is an application designed to model how real enterprise systems handle **authentication**, **authorization**, and **data access**.  
It represents a lightweight internal **ticketing / feedback system** used within organizations.

## What EchoTrack Does

EchoTrack allows users inside an organization to raise issues or feedback, and allows administrators to manage those issues securely.


### Typical Flow

- A user logs in  
- The user submits an issue or feedback  
- An admin reviews the issue  
- The admin updates or closes it  


## Architecture Overview

EchoTrack follows a **layered architecture**:
```
Client (Swagger / Angular)
↓
Controllers
↓
Repository Interfaces
↓
Repository Implementations
↓
Unit of Work
↓
Entity Framework Core
↓
SQL Server
```

This structure ensures:
- Clean separation of concerns
- Maintainable and testable code
- Scalable backend design

## Authentication & Authorization

### Authentication

- Implemented using **JWT (JSON Web Tokens)**
- Users authenticate once using **username and password**
- A signed JWT token is issued on successful login
- Passwords are stored as **hashed values**, never in plain text

### Authorization

- All APIs are protected using `[Authorize]`
- Role-based access is enforced using:
  - `[Authorize(Roles = "User")]`
  - `[Authorize(Roles = "Admin")]`
- No manual role checks inside controller logic
- The system is completely **stateless**

### User Endpoints

| Method | Endpoint           | Description                                       |
| ------ | ------------------ | ------------------------------------------------- |
| POST   | `/api/Feedback`    | Create a new feedback                             |
| GET    | `/api/Feedback/my` | Retrieve feedback submitted by the logged-in user |


### Admin Endpoints

| Method | Endpoint                   | Description                                |
| ------ | -------------------------- | ------------------------------------------ |
| GET    | `/api/Feedback`            | Retrieve all feedback                      |
| GET    | `/api/Feedback/{id}`       | Retrieve feedback by ID                    |
| PUT    | `/api/Feedback/{id}`       | Update feedback                            |
| DELETE | `/api/Feedback/{id}`       | Delete feedback                            |
| PUT    | `/api/Feedback/{id}/close` | Close feedback and update admin statistics |


## Unit of Work (Phase 2)

### Why Unit of Work was introduced

When a business operation affects multiple repositories, all changes must succeed or fail together.

Example:
- Admin closes a feedback
- Admin statistics are updated


### How Unit of Work Works

- Repositories track changes
- Unit of Work performs a single commit
- All changes are saved together


## Summary

EchoTrack is a **feedback system** built to demonstrate how real-world enterprise APIs are structured, secured, and maintained.  
The project prioritizes **architecture quality, security, and clarity**.

