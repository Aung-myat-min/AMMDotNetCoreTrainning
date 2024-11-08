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

app.Run();
