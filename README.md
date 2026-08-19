# Blog API Platform

A RESTful blog API built with ASP.NET Core, Entity Framework Core, and SQLite.  
The project supports blog post and comment management through structured API endpoints and follows a layered architecture with separate API, Core, and Infrastructure projects.

## Overview

This project was built to practice backend API development using C# and ASP.NET Core. It demonstrates how to design REST endpoints, separate application layers, use repository interfaces, and persist data using Entity Framework Core with SQLite.

## Tech Stack

- **Language:** C#
- **Framework:** ASP.NET Core
- **ORM:** Entity Framework Core
- **Database:** SQLite
- **Architecture:** Layered architecture
- **Tools:** Git, GitHub, Postman, Visual Studio / VS Code

## Features

- Create, read, update, partially update, and delete blog posts
- Create, read, update, partially update, and delete comments
- Retrieve comments for a specific blog post
- Uses DTOs for partial update requests
- Uses repository interfaces to separate business logic from data access
- Uses SQLite for local database persistence

## Project Structure

```text
BlogAPI/
├── Blog.API/              # API layer with controllers and DTOs
├── Blog.Core/             # Core models and repository interfaces
├── Blog.Infrastructure/   # EF Core database context, migrations, and repositories
└── BlogAPI.sln            # Solution file
