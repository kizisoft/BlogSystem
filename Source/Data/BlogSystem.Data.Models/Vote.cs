namespace BlogSystem.Data.Models
{
    public abstract class Vote
    {
        public int Id { get; set; }

        public int CommentId { get; set; }

        public string ApplicationUserId { get; set; }
    }
}