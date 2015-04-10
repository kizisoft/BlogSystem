namespace BlogSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ImgurToken
    {
        [Key]
        [Required]
        public string ClientId { get; set; }

        [Required]
        public string AccessToken { get; set; }

        [Required]
        public string RefreshToken { get; set; }

        [Required]
        public int ExpiresIn { get; set; }

        public string TokenType { get; set; }

        public string AccountId { get; set; }

        public string AccountUsername { get; set; }

        public string Scope { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}