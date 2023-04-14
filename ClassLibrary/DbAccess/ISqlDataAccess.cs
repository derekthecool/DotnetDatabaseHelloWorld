namespace ClassLibrary.DbAccess;

public interface ISqlDataAccess
{
    Task<IEnumerable<T>> LoadData<T, U>(
        string sqlCommand,
        U parameters,
        string connectionId = "Default"
    );

    Task<int> SaveData<T>(string sqlCommand, T parameters, string connectionId = "Default");
}
