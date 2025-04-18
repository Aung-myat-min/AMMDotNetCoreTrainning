﻿using AMMDotNetCoreTrainning.RestAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AMMDotNetCoreTrainning.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        private readonly String _connectionString = "Data Source=DESKTOP-6KTIH2K\\SQLEXPRESS;Initial Catalog=DotNetTrainning;User Id=sa;Password=sasa@123;TrustServerCertificate=True;";

        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogViewModel> blogs = new List<BlogViewModel>();
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT [BlogId]
                              ,[BlogTitle]
                              ,[BlogAuthor]
                              ,[BlogContent]
                              ,[DeleteFlag]
                          FROM [dbo].[Tbl_Blog] WHERE DeleteFlag = 0";

            SqlCommand cmd = new SqlCommand(query, connection);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                blogs.Add(new BlogViewModel
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"])
                });
            }
            connection.Close();

            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogById(int id)
        {
            if (id == 0 || id < 0)
            {
                return BadRequest("Invalid Id");
            }

            BlogViewModel blog = new BlogViewModel();

            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            string query = @"SELECT [BlogId]
                              ,[BlogTitle]
                              ,[BlogAuthor]
                              ,[BlogContent]
                              ,[DeleteFlag]
                          FROM [dbo].[Tbl_Blog] WHERE DeleteFlag = 0 AND BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);


            connection.Close();

            if (dt.Rows.Count == 0)
            {
                return NotFound("Blog Not Found!");
            }

            DataRow dr = dt.Rows[0];

            blog = new BlogViewModel
            {
                Id = Convert.ToInt32(dr["BlogId"]),
                Title = Convert.ToString(dr["BlogTitle"]),
                Author = Convert.ToString(dr["BlogAuthor"]),
                Content = Convert.ToString(dr["BlogContent"]),
                DeleteFlag = Convert.ToBoolean(dr["DeleteFlag"]),
            };

            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogViewModel blog)
        {
            if (String.IsNullOrEmpty(blog.Title))
            {
                return BadRequest("Title can't be null or empty!");
            }

            if (String.IsNullOrEmpty(blog.Author))
            {
                return BadRequest("Author can't be null or empty!");
            }

            if (String.IsNullOrEmpty(blog.Content))
            {
                return BadRequest("Contant can't be null or empty!");
            }

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                               ([BlogTitle]
                               ,[BlogAuthor]
                               ,[BlogContent]
                               ,[DeleteFlag])
                         VALUES
                               (@BlogTitle
                               ,@BlogAuthor
                               ,@BlogContent
                               ,0)";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            cmd.Parameters.AddWithValue("@BlogContent", blog.Content);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            if (result == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An Error Occured!");
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogViewModel blog)
        {
            if (int.IsNegative(id))
            {
                return BadRequest("Id can't be a negative number");
            }
            if (String.IsNullOrEmpty(blog.Title))
            {
                return BadRequest("Blog Title can't be null or empty.");
            }
            if (String.IsNullOrEmpty(blog.Author))
            {
                return BadRequest("Blog Author can't be null or empty.");
            }
            if (String.IsNullOrEmpty(blog.Content))
            {
                return BadRequest("Blog Content can't be null or empty.");
            }

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @title
                              ,[BlogAuthor] = @author
                              ,[BlogContent] = @content
                              ,[DeleteFlag] = @deleteflag
                         WHERE BlogId = @id";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@title", blog.Title);
            cmd.Parameters.AddWithValue("@author", blog.Author);
            cmd.Parameters.AddWithValue("@content", blog.Content);
            cmd.Parameters.AddWithValue("@deleteflag", blog.DeleteFlag);
            cmd.Parameters.AddWithValue("@id", id);

            int result = cmd.ExecuteNonQuery();
            connection.Close();

            if (result == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error Occured!");
            }

            return Ok("Blog Updated!");
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogViewModel blog)
        {
            string condition = "";

            if (id < 0 || id == 0)
            {
                return BadRequest("Blod Id can't be 0 or negative.");
            }
            if (!string.IsNullOrEmpty(blog.Title))
            {
                condition += " BlogTitle = @title, ";
            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                condition += " BlogContent = @content, ";
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                condition += " BlogAuthor = @author, ";
            }

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            condition = condition.Substring(0, condition.Length - 2);

            string query = @$"UPDATE [dbo].[Tbl_Blog]
                           SET {condition}
                         WHERE DeleteFlag = 0 AND BlogId = @id";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);

            if (!string.IsNullOrEmpty(blog.Title))
            {
                cmd.Parameters.AddWithValue("@title", blog.Title);
            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                cmd.Parameters.AddWithValue("@content", blog.Content);
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                cmd.Parameters.AddWithValue("@author", blog.Author);
            }

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            if (result == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }

            return Ok("Blog Updated!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            if (id == 0 || int.IsNegative(id))
            {
                return BadRequest("Id can't be 0 or Negative");
            }

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Blog]
                           SET [DeleteFlag] = 1
                         WHERE BlogId = @id";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            if (result == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error!");
            }

            return Ok("Blog Deleted!");
        }
    }
}
