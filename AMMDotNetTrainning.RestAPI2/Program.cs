using AMMDotNetTrainning.RestAPI2.Models.RefitInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton(client => new HttpClient()
{
    BaseAddress = new Uri(builder.Configuration.GetSection("AppDomain").Value!)
});

builder.Services.AddSingleton(client => 
    new RestClient(builder.Configuration.GetSection("AppDomain").Value!)
);

builder.Services.AddRefitClient<IPickAPile>().ConfigureHttpClient(client => client.BaseAddress = new Uri(builder.Configuration.GetSection("AppDomain").Value!));

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
