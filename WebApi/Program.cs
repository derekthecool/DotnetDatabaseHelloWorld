using ClassLibrary.DbAccess;
using ClassLibrary.DbCommands;
using ClassLibrary.Models;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();

var app = builder.Build();

// Awesome API debug tool
app.UseSwagger();
app.UseSwaggerUI();

var db = (ISqlDataAccess)app.Services.GetService(typeof(ISqlDataAccess));
var command = new AnimalCommands(db);
app.MapGet("/animals/list/single/{index}", GetSingle);
app.MapGet("/animals/list/all", GetAll);

// TODO: fix issue with json naming parsing
app.MapPost("/animals/insert/single", InsertSingle);
app.MapPost("/animals/insert/many", InsertMany);

async Task<IResult> GetSingle(int index)
{
    Animal animal = await (command.GetSingle(index));
    if (animal != null)
    {
        return Results.Ok(animal);
    }
    else
    {
        return Results.Problem($"Animal with index {index} not found");
    }
}

async Task<IResult> GetAll()
{
    IEnumerable<Animal> animals = await (command.GetAll());
    return Results.Ok(animals);
}

async Task<IResult> InsertSingle(Animal animal)
{
    var linesChanged = await command.InsertSingle(animal);
    return Results.Ok($"Lines changed: {linesChanged}");
}

async Task<IResult> InsertMany(List<Animal> animals)
{
    var linesChanged = await command.InsertMany(animals);
    return Results.Ok($"Lines changed: {linesChanged}");
}

app.Run();
