var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
// builder.Services.AddSingleton<IDeviceData, DeviceData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

IResult AwesomeFunction()
{
     return Results.Ok("hi");
}

app.MapGet("/test", AwesomeFunction);

app.Run();
