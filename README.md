# My Web API Template

## Starter template for all sorts of REST APIs with .NET

This is my opinionated, constantly work in progress, starter template, for building REST APIs with ASP.NET.  

**This template is for you if** you just want to get started with coding, want a already thought out project structure, that is quite simple and fast to learn, and works on smaller and bigger projects just fine.

## Details

### Technologies, frameworks and libraries
  - Programming language: C#
  - Framework: ASP.NET 5
  - Database: SQL Server (also easy to change)
  - ORM: Entity Framework
  - Testing: XUnit, Moq & FluentAssertions
  
### Architecture, features and patterns
  - Monolithic ()
  - Clean/Onion Architecture 
  - Well thought and production ready project structure
  - Development/QA/Production configurations (work in progress)
  - Repository pattern
  - Specification pattern (work in progress)
  - Swagger/OpenAPI 
  - Database seeding
  - Global error handling
  - Logging (work in progress)
  - Validations
  - Integration and unit tests
  - Model Converters

## Usage
  1. Download latest .NET SDK
  2. Launch
     1. With Visual Studio **OR**
     2. Command line
        1. dotnet run
   
  Initialize database (from Web folder CLI)
  ``` bash
  dotnet ef database update -c ApplicationDbContext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj
  ```
  Migrate database (from Web folder CLI)
  ``` bash
  dotnet ef migrations add InitialModel --context ApplicationDbContext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj -o Database/Migrations
  ```

## Motivation 

This project started as my way to handle the information flow of everything that needs to be done when building APIs with single monolithic manner. There are so many things to remember and there are vast amount of choices to be made. Therefore, I made this template, for myself, to store all the practices that I have found practical and I hope this template might also help someone else to learn a thing or two.

In this template, I have collected my knowledge of API building in a practical format. The architectural preferences, library selections and other choices are made, based on best practices, popularity and my own opinionated preferences. The purpose of this template, is to provide a good basic example on how to build Rest APIs with ASP.NET that are scalable, easy to mantain and contain necessary configurations and other goodies. This template is inspired by [Microsoft eShopOnWeb reference application](https://github.com/dotnet-architecture/eShopOnWeb) and many others.

### Goals
- To keep it "monolithic" (read [here](https://www.martinfowler.com/bliki/MonolithFirst.html) for thoughts why it might be wiser to start from here) 
  - (I have also other plans TODO)
- To keep the structure simple, but not too simple. This project structure might seem a bit overkill for TODO app, but the structure is here to show one way to handle code structure on bigger projects.
- To only use concepts or technologies that I have already used in some of my previous projects, so I know if those are worth it. This project is not for tinkering experimental stuff. (I have other plans to do experimental stuff)
- To have opinions, but to be ready to change anything if there is better way of doing


## Contributing and license
This project is [MIT](https://choosealicense.com/licenses/mit/) licensed - So feel free to use it anyway you like. Suggestions and help are welcomed. 🙂

