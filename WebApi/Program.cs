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
app.MapGet("/animals/list/all", GetAll);
app.MapGet("/animals/list/single/{index}", GetSingle);

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

app.Run();
