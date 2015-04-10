namespace BlogSystem.Common.FileUploadServices.ImgurFileUpload
{
    using Newtonsoft.Json;

    public interface IImageDataModel
    {
        [JsonProperty("id")]
        string Id { get; set; }

        [JsonProperty("title")]
        string Title { get; set; }

        [JsonProperty("description")]
        string Description { get; set; }

        [JsonProperty("datetime")]
        string Datetime { get; set; }

        [JsonProperty("type")]
        string Type { get; set; }

        [JsonProperty("animated")]
        bool Animated { get; set; }

        [JsonProperty("width")]
        int Width { get; set; }

        [JsonProperty("height")]
        int Height { get; set; }

        [JsonProperty("size")]
        int Size { get; set; }

        [JsonProperty("views")]
        int Views { get; set; }

        [JsonProperty("bandwidth")]
        int Bandwidth { get; set; }

        [JsonProperty("deletehash")]
        string Ddeletehash { get; set; }

        [JsonProperty("section")]
        string Section { get; set; }

        [JsonProperty("link")]
        string Link { get; set; }

        [JsonProperty("account_url")]
        string AccountUrl { get; set; }

        [JsonProperty("account_id")]
        int AaccountId { get; set; }
    }
}