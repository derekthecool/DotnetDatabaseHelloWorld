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

# Lastly, verify the web API works

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
