# Dotnet Database Hello World With Docker

This guide is a simple sketch of a basic setup using the following components:

- Dotnet minimal web API which uses asp.net
- Dotnet class library for database class models and database access
- Dotnet cli for project generation and running code
- Docker for application containerization:
    - [Dockerfile](./Dockerfile) for building all the dotnet components into an image
    - compose.yaml for the docker compose file for easy docker setup
    - [./MariaDB](https://hub.docker.com/_/mariadb) database. Easily initialize
      databases and tables by creating a docker volume mapping to the containers
      `/docker-entrypoint-initdb.d` directory. Any `.sh`, `.sql`, `.sql.gz`,
      `.sql.xz`, and `.sql.zst` files will be executed.
- [adminer](https://hub.docker.com/_/adminer/) for easy database web debugging

## Guide To Creating This Project

### Dotnet CLI Project Setup

These commands will create the project files we need to get started.

```sh
# Create a solution file. Not technically required but is good practice working
# with C# code. Note this should be in the project root.
dotnet new sln

# Create the web API project
# Change the argument to the -o to change the project name
dotnet new webapi -o WebApi

# Create the class library
# Change the argument to the -o to change the project name
dotnet new classlib -o ClassLibrary

# Connect all the projects
# 1. Load all csharp projects into the solution file (substitute any changed filenames
dotnet sln add ./WebApi/WebApi.csproj ./ClassLibrary/ClassLibrary.csproj

# 2. In the web API add a reference to the class library project
cd WebApi
dotnet add reference ../ClassLibrary/ClassLibrary.csproj

# Note the contents of the file ./WebApi/WebApi.csproj
# This is where the reference is stored
cat ./WebApi/WebApi.csproj

# 3. Make a change the to ./WebApi/Program.cs file to disable HTTPS
#    delete the lines matching this:
#    app.UseHttpsRedirection();
#    app.UseAuthorization();
```

Lastly, verify the web API works

```sh
dotnet run
```

You should see output like this

```text
dotnet run
Building...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7169
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5149
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: D:\Programming\dotnet\DotnetDatabaseHelloWorld\DotnetDatabaseHelloWorld\WebApi\
```

View the web API with Swagger/Open API by going to a modified version of the
second localhost listed in the dotnet run output. So in this case:

http://localhost:5149/swagger/index.html

(if already running in docker it should be http://localhost:8080/swagger/index.html)
assuming the local port is mapped to 8080.

### Customize ClassLibrary Project

- Delete Class1.cs
- Add required [NuGet](https://www.nuget.org/) packages using dotnet cli
```sh
cd ClassLibrary

# Database ORM (object relational mapper)
dotnet add package Dapper

# Required for dapper to "talk" to mysql or mariadb databases
dotnet add package MySqlConnector

# Needed to reed the appSettings.json file from a non asp.net project
dotnet add package Microsoft.Extensions.Configuration.Abstractions
```
- Create the database access classes in new directory ./ClassLibrary/DbAccess:
    - ISqlDataAccess.cs interface
    - SqlDataAccess.cs implementation
- Create the model classes to match database tables in ./ClassLibrary/Models:
    - Names will be decided on your database setup. This example uses the
      Animal.cs file to declare the animal.
- Create new directory DbCommands for class library commands to be used by the
  web API. Methods demonstrated in this example project include:
  - InsertSingle
  - InsertMany
  - GetSingle
  - GetAll
  - UpdateSingle
  - DeleteSingle
  - DeleteAll

  All of these methods will be used in the Program.cs web API file latter.

### Customize WebApi Project

- Delete Controllers directory
- Edit Program.cs:
    - Setup dependency injection (DI) in the WebApi's Program.cs to use the
      classes for sql access. DI helps because the class library does not need
      to read the appSettings.json file directly.
    - Start mapping API endpoints for `Get`, `Post`, `Delete` API calls. These
      mappings are easy and map    ```cs
    app.MapGet("/animals/list/single/{index}", GetSingle);
    app.MapGet("/animals/list/all", GetAll);
    ```
