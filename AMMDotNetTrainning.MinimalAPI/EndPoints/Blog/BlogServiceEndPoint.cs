using AMMDotNetCoreTrainning.Domain.Features.Blog;

namespace AMMDotNetTrainning.MinimalAPI.EndPoints.Blog;

public static class BlogServiceEndPoint
{

    public static void MapBlogServiceEndPoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blogs", () =>
        {
            BlogService service = new BlogService();
            var blogs = service.GetTblblogs();

            return Results.Ok(blogs);
        })
            .WithName("GetBlogs")
            .WithOpenApi();

        app.MapPost("/blogs", (TblBlog blog) =>
        {
            BlogService service = new BlogService();
            var createdBlog = service.CreateBlog(blog);

            return Results.Ok(createdBlog);
        })
            .WithName("CreateBlog")
            .WithOpenApi();

        app.MapGet("/blogs/{id}", (int id) =>
        {
            BlogService service = new BlogService();
            var blog = service.GetTblBlog(id);

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
            BlogService service = new BlogService();
            var oldBlog = service.UpdateBlog(id, blog);

            if (oldBlog is null)
            {
                return Results.NotFound("Blog is not found!");
            }

            //if (result == 0)
            //{
            //    return Results.StatusCode(StatusCodes.Status500InternalServerError);
            //}

            return Results.Ok("Blog Updated");
        })
            .WithName("UpdateBlog")
            .WithOpenApi();

        app.MapPatch("/blogs/{id}", (int id, TblBlog blog) =>
        {
            BlogService service = new BlogService();
            var oldBlog = service.EditBlog(id, blog);

            if (oldBlog is null)
            {
                return Results.NotFound("Blog is not found!");
            }

            //if (result == 0)
            //{
            //    return Results.StatusCode(StatusCodes.Status500InternalServerError);
            //}
            return Results.Ok("Blog Updated");
        })
            .WithName("EditBlog")
            .WithOpenApi();

        app.MapDelete("/blogs/{id}", (int id) =>
        {
            BlogService service = new BlogService();
            var blog = service.DeleteBlog(id);

            if (blog is null)
            {
                return Results.NotFound("Blog Not Found!");
            }

            if (blog == false)
            {
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Results.Ok("Blog Deleted!");
        });
    }
}
