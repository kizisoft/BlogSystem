namespace BlogSystem.Common.FileUploadServices.ImgurFileUpload
{
    using System;

    internal class TokenDataModel : ITokenDataModel
    {
        public TokenDataModel()
        {
            this.CreatedOn = DateTime.Now;
        }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public int ExpiresIn { get; set; }

        public string TokenType { get; set; }

        public string AccountId { get; set; }

        public string AccountUsername { get; set; }

        public string Scope { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}