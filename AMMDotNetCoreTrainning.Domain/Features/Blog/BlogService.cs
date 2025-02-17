﻿using AMMDotNetCoreTrainning.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMMDotNetCoreTrainning.Domain.Features.Blog
{
    public class BlogService : IBlogService
    {
        private readonly EfCoreDbContext _db;

        public BlogService(EfCoreDbContext db)
        {
            _db = db;
        }

        public List<TblBlog> GetTblblogs()
        {
            var list = _db.TblBlogs.AsNoTracking().Where(x => x.DeleteFlag == false).ToList();
            return list;
        }

        public TblBlog? GetTblBlog(int id)
        {
            var blog = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            return blog;
        }

        public TblBlog CreateBlog(TblBlog blog)
        {
            _db.TblBlogs.Add(blog);
            _db.SaveChanges();
            return blog;
        }

        public TblBlog? UpdateBlog(int id, TblBlog blog)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            if (blog is null || item is null)
            {
                return null;
            }

            item = blog;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            return blog;
        }

        public TblBlog? EditBlog(int id, TblBlog blog)
        {
            var item = GetTblBlog(id);
            if (blog is null || item is null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle.Trim();
            }

            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor.Trim();
            }

            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent = blog.BlogContent.Trim();
            }

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            return blog;
        }

        public bool? DeleteBlog(int id)
        {
            var blog = GetTblBlog(id);
            if (blog is null)
            {
                return null;
            }

            blog.DeleteFlag = true;
            _db.Entry(blog).State = EntityState.Deleted;
            int result = _db.SaveChanges();

            return result > 0;
        }
    }
}
