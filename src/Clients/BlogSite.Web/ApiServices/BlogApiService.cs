﻿using BlogSite.Common.DTOs;
using BlogSite.Common.DTOs.Transaction;

namespace BlogSite.Web.ApiServices
{
    public class BlogApiService
    {
        private readonly HttpClient _httpClient;

        public BlogApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetTotalCountAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<BlogSiteResponseDto<int>>("blog/GetTotalCount");
            return response.Data;
        }

        public async Task<int> GetTotalViewCountAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<BlogSiteResponseDto<int>>("blog/GetTotalViewCount");
            return response.Data;
        }

        public async Task<List<TBlogDto>> GetByUserIdAsync(int userId)
        {
            var response = await _httpClient.GetFromJsonAsync<BlogSiteResponseDto<List<TBlogDto>>>($"blog/GetByUserId/{userId}");
            return response.Data;
        }

        public async Task<BlogSiteResponseDto<TBlogDto>> SaveAsync(TBlogDto blogDto)
        {
            var response = await _httpClient.PostAsJsonAsync("blog/save", blogDto);
            return await response.Content.ReadFromJsonAsync<BlogSiteResponseDto<TBlogDto>>();
        }
    }
}
