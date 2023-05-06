﻿using BlogSite.Core.Entities.Transaction;
using BlogSite.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogSite.Repository.Repositories
{
    public class BlogRepository : GenericRepository<TBlog>, IBlogRepository
    {
        public BlogRepository(BlogSiteContext context) : base(context)
        {
        }

        public IQueryable<TBlog> GetByUserIdAsync(int userId)
        {
            return _context.TBlogs.Where(x => x.User_ID == userId);
        }

        public int GetTotalViewCountByUserId(int userId)
        {
            return _context.TBlogs.Where(x => x.User_ID == userId).Sum(x => x.ViewNumber);
        }

        public int GetTotalViewCount()
        {
            return _context.TBlogs.Where(x => x.IsActive).Sum(x => x.ViewNumber);
        }

        public IQueryable<TBlog> GetAllWithUser(int page, int pageSize)
        {
            return _context.TBlogs
                 .Include(s => s.User)
                 .Where(s => s.IsActive)
                 .OrderBy(s => s.CreatedDate)
                 .Skip((page - 1) * pageSize)
                 .Take(pageSize);
        }
    }
}
