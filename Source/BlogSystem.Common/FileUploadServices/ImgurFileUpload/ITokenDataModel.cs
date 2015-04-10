namespace BlogSystem.Common.FileUploadServices.ImgurFileUpload
{
    using System;

    using Newtonsoft.Json;

    public interface ITokenDataModel
    {
        [JsonProperty("access_token")]
        string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        string RefreshToken { get; set; }

        [JsonProperty("expires_in")]
        int ExpiresIn { get; set; }

        [JsonProperty("token_type")]
        string TokenType { get; set; }

        [JsonProperty("account_id")]
        string AccountId { get; set; }

        [JsonProperty("account_username")]
        string AccountUsername { get; set; }

        [JsonProperty("scope")]
        string Scope { get; set; }

        DateTime CreatedOn { get; set; }
    }
}