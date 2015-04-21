namespace BlogSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Tag
    {
        private ICollection<BlogPost> blogPosts;

        public Tag()
        {
            this.blogPosts = new HashSet<BlogPost>();
        }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<BlogPost> BlogPosts
        {
            get { return this.blogPosts; }
            set { this.blogPosts = value; }
        }
    }
}