using AMMDotNetCoreTrainning.Database.Models;
using AMMDotNetCoreTrainning.RestAPI.ViewModel;
using Microsoft.AspNetCore.Http;
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
        private readonly String _connectionString = "DataSource=DESKTOP-KPCHONN\\SQLEXPRESS;Initial Catalog=DotNetTrainning;User Id=sa;Password=sasa@123;TrustServerCertificate=True;";

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
                    Id = Convert.ToInt32(reader["BlodId"]),
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
            if (id == 0 || id < 0) { 
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
            cmd.Parameters.AddWithValue("BlodId", id);

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            adapter.Fill(dt);


            connection.Close();

            if (dt.Rows.Count == 0) {
                return NotFound("Blog Not Found!");
            }

            DataRow dr = dt.Rows[0];

            blog = new BlogViewModel { 
                Id = Convert.ToInt32(dr["BlogId"]),
                Title = Convert.ToString(dr["BlogTitle"]),
                Author = Convert.ToString(dr["BlogAuthor"]),
                Content = Convert.ToString(dr["BlogContent"]),
                DeleteFlag = Convert.ToBoolean(dr["DeleteFlag"]),
            };

            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {

        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, TblBlog blog)
        {

        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, TblBlog blog)
        {

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {

        }
    }
}
