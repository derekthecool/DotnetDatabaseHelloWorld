using MySqlConnector;
using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper;

namespace DataAccess.DbAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(
        string sqlCommand,
        U parameters,
        string connectionId = "Default"
    )
    {
        string connectionString = _config.GetConnectionString(connectionId);
        using IDbConnection connection = new MySqlConnection(connectionString);

        return await connection.QueryAsync<T>(
            sqlCommand,
            parameters,
            commandType: CommandType.Text
        );
    }

    public async Task SaveData<T>(string sqlCommand, T parameters, string connectionId = "Default")
    {
        string connectionString = _config.GetConnectionString(connectionId);
        using IDbConnection connection = new MySqlConnection(connectionString);

        await connection.ExecuteAsync(sqlCommand, parameters, commandType: CommandType.Text);
    }
}

