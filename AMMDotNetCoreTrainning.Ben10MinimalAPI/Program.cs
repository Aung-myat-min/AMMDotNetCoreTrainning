using AMMDotNetCoreTrainning.Ben10MinimalAPI.Actions;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
const string filePath = "Data/ben10.json";

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/ben10", () =>
{
    var data = File.ReadAllText(filePath);
    var result = JsonConvert.DeserializeObject<Ben10ResponseModel>(data);
    return Results.Ok(result.Tbl_Ben10);
})
    .WithName("GetAliens")
    .WithOpenApi();

app.MapGet("/ben10/{id}", (int id) =>
{
    var data = File.ReadAllText(filePath);
    var result = JsonConvert.DeserializeObject<Ben10ResponseModel>(data);
    if (result.Tbl_Ben10.Length == 0)
    {
        return Results.NotFound("The database is empty!");
    }

    Tbl_Ben10? targetAlien = Actions.findById(id, result);

    if (targetAlien is null)
    {
        return Results.NotFound("Alien not found!");
    }

    return Results.Ok(targetAlien);
})
    .WithName("GetAlienByName")
    .WithOpenApi();

app.MapPost("/ben10", (Tbl_Ben10 alien) =>
{
    var data = File.ReadAllText(filePath);
    var result = JsonConvert.DeserializeObject<Ben10ResponseModel>(data);
    if (result == null || result.Tbl_Ben10 == null)
    {
        return Results.Problem("Error reading data from the file.");
    }

    var list = result.Tbl_Ben10.ToList();
    int newId = list.Count + 1;
    alien.id = newId;
    list.Add(alien);

    result.Tbl_Ben10 = list.ToArray();
    var updatedData = JsonConvert.SerializeObject(result, Formatting.Indented);

    File.WriteAllText(filePath, updatedData);

    return Results.Ok($"Your Alien is created with id: {newId}");
})
    .WithName("CreateAlien")
    .WithOpenApi();

app.MapPut("/ben10/{id}", (int id, Tbl_Ben10 alien) =>
{
    var data = File.ReadAllText(filePath);
    var result = JsonConvert.DeserializeObject<Ben10ResponseModel>(data);
    if (result == null || result.Tbl_Ben10 == null)
    {
        return Results.Problem("Error reading data from the file.");
    }

    var targetAlien = Actions.findById(id, result);
    if (targetAlien is null)
    {
        return Results.NotFound("Alien With That Id not found!");
    }

    targetAlien.name = alien.name;
    targetAlien.description = alien.description;
    targetAlien.power = alien.power;
    targetAlien.rating = alien.rating;
    targetAlien.color = alien.color;

    result = Actions.replaceAlien(targetAlien, result);
    if (result == null)
    {
        return Results.Problem("Error replacing alien.");
    }

    var updatedData = JsonConvert.SerializeObject(result, Formatting.Indented);

    File.WriteAllText(filePath, updatedData);

    return Results.Ok("Alien Updated!");
})
    .WithName("UpdateAlien")
    .WithOpenApi();

app.MapPatch("/ben10/{id}", (int id, Tbl_Ben10 alien) =>
{
    var data = File.ReadAllText(filePath);
    var result = JsonConvert.DeserializeObject<Ben10ResponseModel>(data);
    if (result == null || result.Tbl_Ben10 == null)
    {
        return Results.Problem("Error reading data from the file.");
    }

    var targetAlien = Actions.findById(id, result);
    if (targetAlien is null)
    {
        return Results.NotFound("Alien With That Id not found!");
    }

    if(alien.name.Length != 0)
    {
        targetAlien.name = alien.name;
    }
    if (alien.description.Length != 0)
    {
        targetAlien.description = alien.description;
    }
    if (alien.color.Length != 0)
    {
        targetAlien.color = alien.color;
    }
    if (alien.rating >= 0)
    {
        targetAlien.rating = alien.rating;
    }
    if (alien.power.Length != 0)
    {
        targetAlien.power = alien.power;
    }

    result = Actions.replaceAlien(targetAlien, result);
    if (result == null)
    {
        return Results.Problem("Error replacing alien.");
    }

    var updatedData = JsonConvert.SerializeObject(result, Formatting.Indented);

    File.WriteAllText(filePath, updatedData);

    return Results.Ok("Alien Updated!");
})
    .WithName("EditAlien")
    .WithOpenApi();
app.Run();
