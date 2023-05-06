﻿using BlogSite.Core.Entities.Transaction;

namespace BlogSite.Core.Repositories
{
    public interface ICommentRepository : IGenericRepository<TComment>
    {
        IQueryable<TComment> GetAllByBlogId(int blogId);
    }
}
