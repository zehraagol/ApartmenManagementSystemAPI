# Apartment Management System API

## Overview
In this project, an apartment management system has been implemented to streamline the management of apartment-related tasks through a user-friendly API. This system facilitates various functions for apartment managers and residents, including managing invoices, apartments, and resident information.

<img width="406" alt="Ekran görüntüsü 2024-02-11 223030" src="https://github.com/zehraagol/ApartmenManagementSystemAPI/assets/72569851/4518ed81-bf37-42ef-ad0b-65dcc76ed32a">

## Technologies

- **Backend:** .NET Core / ASP.NET Core
- **Database:** Microsoft SQL Server
- **Authentication:** Identity Framework for secure user management

## Getting Started

### Prerequisites

- .NET Core SDK
- Microsoft SQL Server

### Installation

1. **Clone the repository.**
   - Use `git clone <repository-url>` to clone the project to your local machine.

2. **Configure the connection strings in `appsettings.json`.**
   - Navigate to the `appsettings.json` file and modify the connection strings to match your database configuration.

3. **Create a new migration (if necessary).**
   - If you've empty database, open your package manager console terminal navigate to the project directory, and run `add-migration NameOfYourMigration` to scaffold a new migration. Replace `NameOfYourMigration` with a descriptive name for the migration.

4. **Apply database migrations to set up the Microsoft SQL Server database.**
   - Run `uptade-database` to apply the existing migrations and set up the database in Microsoft SQL Server.

### Entities Managed
- **Apartment (MainBuildings)**
- **Flats**
- **Users and Admin (Users)**
- **Invoices and Payments (Payments)**

### User Roles
- **Flat Owner or Tenant (User role)**
- **Administrator (Admin role)**

## Entity Relationships
The system features several key relationships:
- **User - Flat:** One-to-many. A user can have multiple flats, but a flat is associated with one user.
- **User - Payment:** One-to-many. A user can have multiple payments, but a payment is associated with one user.
- **Flat - Payment:** One-to-many. A flat can have multiple payments.
- **User - Apartment (MainBuilding):** One-to-one. An apartment building has one manager, and a manager manages one apartment.

![Entity Relationship Diagram](https://github.com/zehraagol/ApartmenManagementSystemAPI/assets/72569851/e466475e-98bc-4d5c-8335-7bb7e740bae8)

## Roles and Responsibilities
### Administrator (Role: `admin`)
- Manage user information (create, update, delete).
- Manage flats and assign users.
- Assign and manage payments for flats and apartments.
- Monitor payment statuses and overall debt on a monthly and annual basis.
- Generate tokens for authentication.

### Users (Role: `user`)
- View and pay invoices and dues.
- Obtain tokens for authentication using their ID number and phone number.

## How to Use
### For Admins
1. Generate an admin token (`Tokens->CreateTokenForAdmin`).
2. Authorize via Swagger or Postman.
3. Access and use all system functions.

### For Users (`role: "user"`)
1. Obtain an admin token.
2. Assign the "user" role with `tokens/AssignRoleToUser`.
3. Authorize the selected user with the obtained token.
4. Access authorized functions as user.

## Making Payments
- **Late Payments:** Incur a 10% increase if not paid within the same month.
- **Regular Payments:** Benefit from a 10% discount on dues if payments were consistently made on time over the last year.

## Getting Started
Administrators and users should follow the respective guides above to interact with the system effectively.



