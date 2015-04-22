namespace BlogSystem.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using BlogSystem.Data.Common.Repository;
    using BlogSystem.Data.Models;

    public class BlogSystemData : IBlogSystemData
    {
        private readonly DbContext context;
        private readonly IDictionary<Type, object> repositories;

        public BlogSystemData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<BlogPost> BlogPosts
        {
            get { return this.GetRepository<BlogPost>(); }
        }

        public IRepository<Tag> Tags
        {
            get { return this.GetRepository<Tag>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}