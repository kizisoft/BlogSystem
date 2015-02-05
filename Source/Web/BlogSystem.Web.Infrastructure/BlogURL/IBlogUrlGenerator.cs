namespace BlogSystem.Web.Infrastructure.BlogURL
{
    using System;

    public interface IBlogUrlGenerator
    {
        string GenerateUrl(int id, string title, DateTime createdOn);
    }
}