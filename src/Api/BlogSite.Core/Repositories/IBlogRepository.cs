﻿using BlogSite.Core.Entities.Transaction;

namespace BlogSite.Core.Repositories
{
    public interface IBlogRepository : IGenericRepository<TBlog>
    {
        int GetTotalViewCount();
        int GetTotalViewCountByUserId(int userId);

        IQueryable<TBlog> GetByUserIdAsync(int userId);
    }
}
