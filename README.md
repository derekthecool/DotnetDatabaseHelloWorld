# Dotnet Database Hello World With Docker

This guide is a simple sketch of a basic setup using the following components:

- Dotnet minimal web API
- Dotnet class library for database class models and database access
- Docker for application containerization:
    - [Dockerfile](./Dockerfile) for building all the dotnet components into an image
    - compose.yaml for the docker compose file for easy docker setup
    - [./MariaDB](https://hub.docker.com/_/mariadb) database. Easily initialize
      databases and tables by creating a docker volume mapping to the containers
      `/docker-entrypoint-initdb.d` directory. Any `.sh`, `.sql`, `.sql.gz`,
      `.sql.xz`, and `.sql.zst` files will be executed.
- [adminer](https://hub.docker.com/_/adminer/) for easy database web debugging
