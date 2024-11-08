using AMMDotNetCoreTrainning.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

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

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();

app.MapGet("/blogs", () =>
{
    EfCoreDbContext db = new EfCoreDbContext();
    var blogs = db.TblBlogs.AsNoTracking().ToList().Where(x => x.DeleteFlag == false);

    return Results.Ok(blogs);
})
    .WithName("GetBlogs")
    .WithOpenApi();

app.MapPost("/blogs", (TblBlog blog) =>
{
    EfCoreDbContext db = new EfCoreDbContext();
    db.TblBlogs.Add(blog);
    db.SaveChanges();

    return Results.Ok("Blog Created!");
})
    .WithName("CreateBlog")
    .WithOpenApi();

app.MapGet("/blogs/{id}", (int id) =>
{
    EfCoreDbContext db = new EfCoreDbContext();
    var blog = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);

    if (blog is null)
    {
        return Results.NotFound("Blog Not Found!");
    }

    return Results.Ok(blog);
})
    .WithName("GetBlogById")
    .WithOpenApi();

app.MapPut("/blogs/{id}", (int id, TblBlog blog) =>
{
    EfCoreDbContext db = new EfCoreDbContext();
    var oldBlog = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);

    if (oldBlog is null)
    {
        return Results.NotFound("Blog is not found!");
    }

    oldBlog.BlogTitle = blog.BlogTitle;
    oldBlog.BlogAuthor = blog.BlogAuthor;
    oldBlog.BlogContent = blog.BlogContent;

    db.Entry(oldBlog).State = EntityState.Modified;

    int result = db.SaveChanges();

    if (result == 0)
    {
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }
    return Results.Ok("Blog Updated");
})
    .WithName("UpdateBlog")
    .WithOpenApi();

app.MapPatch("/blogs/{id}", (int id, TblBlog blog) =>
{
    EfCoreDbContext db = new EfCoreDbContext();
    var oldBlog = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);

    if (oldBlog is null)
    {
        return Results.NotFound("Blog is not found!");
    }

    if (!blog.BlogTitle.IsNullOrEmpty())
    {
        oldBlog.BlogTitle = blog.BlogTitle;
    }
    if (!blog.BlogAuthor.IsNullOrEmpty())
    {
        oldBlog.BlogAuthor = blog.BlogAuthor;
    }
    if (!blog.BlogContent.IsNullOrEmpty())
    {
        oldBlog.BlogContent = blog.BlogContent;
    }

    db.Entry(oldBlog).State = EntityState.Modified;

    int result = db.SaveChanges();

    if (result == 0)
    {
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }
    return Results.Ok("Blog Updated");
})
    .WithName("EditBlog")
    .WithOpenApi();

app.MapDelete("/blogs/{id}", (int id) =>
{
    EfCoreDbContext db = new EfCoreDbContext();
    var blog = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);

    if (blog is null)
    {
        return Results.NotFound("Blog Not Found!");
    }

    blog.DeleteFlag = true;

    db.Entry(blog).State = EntityState.Modified;
    int result = db.SaveChanges();

    if (result == 0)
    {
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }
    return Results.Ok("Blog Deleted!");
});

app.Run();


