# HR Management System

## Project Overview
This project implements a comprehensive model of a Human Resources Management System tailored for a small organization. The system includes functionalities to manage employees, roles, departments, and payroll computation. It integrates key programming concepts such as Object-Oriented Programming (OOP), file handling, and MVC architecture.

The project demonstrates robust design principles and practical application of advanced programming techniques, providing a complete solution for managing HR operations efficiently.

## Project Core Functionalities

### Department Management
- Add, update, and manage departmental information.
  
### Employee Management
- Handle data for part-time and full-time employees (e.g., teachers and staff).
- Assign roles and responsibilities to employees.
  
### Payroll Computation
- Calculate salaries based on job roles, workload, and additional criteria.
- Utilizes logic for accurate and dynamic payroll processing.

### File Handling
- Store and retrieve data for departments, employees, and payrolls for persistence.

### MVC Framework
- Implements the Model-View-Controller pattern for separation of concerns.
- Razor pages (`.cshtml`) provide a dynamic and responsive UI.

### GUI Features
- User-friendly interface for data input, display, and management.

### Exception Handling
- Manage edge cases and provide user-friendly error feedback.

### Unit Testing
- Comprehensive test suite to ensure correctness of methods and features.

## Technologies Used
- **Programming Language**: C# with Razor Pages (`.cshtml`)
- **Framework**: ASP.NET Core MVC
- **Frontend**: HTML, CSS, and optional Bootstrap for styling
- **Database**: SQL Server for data storage
- **Configuration**: JSON for application settings (`appsettings.json`)
- **Development Environment**: Visual Studio 2022 or newer

## Database Contents
Data is managed using SQL Server, with tables for:
1. **Departments**:
   - Stores names, IDs, and associated details.
2. **Employees**:
   - Includes fields for name, role, department, workload, and salary.
3. **Payroll Records**:
   - Stores computed salary data for employees.

## Implementation Details
- **Model Classes**: Define entities like `Employee`, `Department`, and `Payroll`.
- **Controller Logic**: Handles business operations and connects the model with views.
- **Views**: Razor Pages (`.cshtml`) for rendering dynamic content.
- **Database Interaction**: Uses Entity Framework for ORM to interact with SQL Server.
- **Exception Handling**: Ensures graceful error handling.
- **File Handling**: Provides fallback file storage for backup purposes.

## Real-Life Uses
- **Educational Institutions**:
  - Manage HR operations for schools, colleges, and universities.
- **Payroll Management Systems**:
  - Automate salary calculations for employees.
- **Small and Medium Businesses**:
  - Organize and streamline HR processes efficiently.

## Design Requirements
- Clear and intuitive GUI for user interaction.
- Modular design for scalability and maintainability.
- Comprehensive testing to validate all functionalities.
- MVC pattern for clean separation of concerns and ease of development.

## Contributor
Sajjan Gautam
