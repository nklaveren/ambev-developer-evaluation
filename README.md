# Developer Evaluation Project

## Overview
This project is a .NET-based evaluation application demonstrating clean architecture, domain-driven design, and best practices in software development.

## Prerequisites
- Docker Desktop
- .NET 8 SDK
- Git

## Getting Started

1. Clone the repository:
```bash
git clone https://github.com/nklaveren/ambev-developer-evaluation.git
```

2. Navigate to the project directory:
```bash
cd <project-directory>
```

3. Start the application using Docker Compose:
```bash
docker-compose up -d
```

4. Generate test coverage report:
```bash
coverage-report.bat 
```

## Project Structure
```
src/
├── Ambev.DeveloperEvaluation.WebApi/                # API entry point
├── Ambev.DeveloperEvaluation.Application/           # Application layer (use cases)
├── Ambev.DeveloperEvaluation.Domain/                # Domain layer (entities, value objects)
├── Ambev.DeveloperEvaluation.ORM/                   # ORM layer (persistence)
├── Ambev.DeveloperEvaluation.IoC/                   # IoC container
├── Ambev.DeveloperEvaluation.Common/                # Common classes

Tests/
├── Ambev.DeveloperEvaluation.Integration/           # Integration tests
├── Ambev.DeveloperEvaluation.Functional/            # Functional tests
├── Ambev.DeveloperEvaluation.Unit/                  # Unit tests
```

## Testing
The project includes:
- Unit tests
- Integration tests
- End-to-end tests

To view test coverage, run the coverage-report script after running the tests.

## API Documentation
Once the application is running, you can access the Swagger documentation at:
http://localhost:5000/swagger