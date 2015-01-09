namespace BlogSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public abstract class CommentForBase
    {
        [Key]
        public int Id { get; set; }

        public int CommentId { get; set; }

        public virtual Comment Comment { get; set; }
    }
}