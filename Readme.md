## EvolentTestApp

## Contact Information

This repository contains the Contact Information Web API. This API deals with database for contact information crud operations. I have used repository pattern. It restricts us to work directly with the data in the application and creates new layers for database operations, business logic, and the application's UI.

## Organization of Application

A typical abstract directory layout

```bash
|--EvolentTest.API
   |--Controller
      |--ContactController.cs
   |--Migration
   |--appsetting.json
   |--Startup.cs
|--EvolentTest.Common
   |--Constants
      |--ContactConstant.cs
      |--MessageConstant.cs
   |--Models
      |--ContactModel.cs
|--EvolentTest.Datbase
   |--Entities
      |--Contact.cs
      |--ApplicationUser.cs
   |--EvolentTestDBContext.cs
   |--BaseEntity.cs
   |--CustomExtensionClass.cs
   |--PagedResult.cs
   |--PagedResultBase.cs
|--EvolentTest.Services
   |--Services
      |--ContactService.cs
      |--Interfaces
         |--IContactService.cs
   |--Repositories
      |--ContactRepository.cs
      |--Interfaces
         |--IContactRepository.cs
```

## Prerequisites

```bash
1. Should have .net core 3.1 installed on your system
2. Should have SQL Server installed on you system configured with database engine.
3. Should have Visual Studio 2017 or above installed to run this project
```

## Steps to Run Application
```bash
1. Download the project
2. Run "update-database" from package manager console.
2. Run the web api project first from visual studio
3. You are all good with project setup. Play with the api.
```

## Versioning
This is Version v1.

## Author
Sanket Marathe
