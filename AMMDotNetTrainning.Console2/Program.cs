using AMMDotNetCoreTrainning.Database.Models;
using Microsoft.EntityFrameworkCore;

//EfCoreDbContext db = new EfCoreDbContext(opt =>
//{
//    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
//});
//var list = db.TblBlogs.Where(x => x.DeleteFlag == false).ToList();

//foreach (var item in list)
//{
//    Console.Write($"Blog Id: {item.BlogId}\nBlog Title: {item.BlogTitle}\nBlog Content: {item.BlogContent}\nWritten By: {item.BlogAuthor}\n\n");
//}