﻿using BlogSite.Core.DTOs.UserBase;
using BlogSite.Core.Entities.UserBase;

namespace BlogSite.Core.Services
{
    public interface IUserService : IService<User, UserDto>
    {
    }
}
