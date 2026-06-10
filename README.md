# Tickets API

Tickets API is a lab project for building a support-ticket management API with .NET and C#.

The goal of this project is to practice API development while studying Domain-Driven Design
and Clean Architecture. The codebase is intentionally organized into separate projects so each
layer has a clear responsibility and dependency direction.

## Purpose

- Manage support tickets through a web API.
- Practice Domain-Driven Design concepts around entities, domain rules, and behavior.
- Practice Clean Architecture with separated Domain, Application, Infrastructure, and API layers.
- Keep the solution small enough to experiment with architectural decisions.

## Project Structure

```text
src/
  Tickets.Api/
  Tickets.Application/
  Tickets.Domain/
  Tickets.Infrastructure/
tests/
  Tickets.Domain.Tests/
```

## Build and Test

```bash
dotnet build Tickets.slnx
dotnet test Tickets.slnx
```
