namespace BlogSystem.Data
{
    using BlogSystem.Data.Common.Repository;
    using BlogSystem.Data.Models;

    public interface IBlogSystemData
    {
        IRepository<BlogPost> BlogPosts { get; }

        IRepository<Tag> Tags { get; }

        int SaveChanges();
    }
}