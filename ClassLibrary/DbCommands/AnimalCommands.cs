using ClassLibrary.DbAccess;
using ClassLibrary.Models;

namespace ClassLibrary.DbCommands;

public class AnimalCommands
{
    private readonly ISqlDataAccess _db;

    public AnimalCommands(ISqlDataAccess db)
    {
        _db = db;
    }

    // Create (Crud) a single new item in database
    public async Task<int> InsertSingle(Animal animal)
    {
        return await _db.SaveData<Animal>("insert into Animal.Animals values (@Name, @LegCount)", animal);
    }

    // Create (Crud) many items into database
    public async Task<int> InsertMany(IEnumerable<Animal> animals)
    {
        return await _db.SaveData("insert into Animal.Animals values (@Name, @LegCount)", animals);
    }

    // Read (cRud) all items from database
    public async Task<Animal?> GetSingle(int index)
    {
        return (await (_db.LoadData<Animal, dynamic>("select * from Animal.Animals where Id = @Id", new {Id = index }))).FirstOrDefault();
    }

    // Read (cRud) all items from database
    public async Task<IEnumerable<Animal>> GetAll()
    {
        return await _db.LoadData<Animal, dynamic>("select * from Animal.Animals", new { });
    }

    // Update (crUd) a single item in database
    public async Task<int> UpdateSingle(Animal animal)
    {
        return await _db.SaveData<Animal>("update Animal.Animals set Name = @Name, LegCount = @LegCount where Id = @Id)", animal);
    }

    // Delete (cruD) a single item in database
    public async Task<int> DeleteSingle(Animal animal)
    {
         return await _db.SaveData<Animal>("delete from Animal.Animals where Id = @Id", animal);
    }

    // Delete (cruD) all items from database
    public async Task<int> DeleteAll(Animal animal)
    {
         return await _db.SaveData("delete from Animal.Animals", new { });
    }
}
