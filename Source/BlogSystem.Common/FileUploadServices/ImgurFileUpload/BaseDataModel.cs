namespace BlogSystem.Common.FileUploadServices.ImgurFileUpload
{
    using Newtonsoft.Json;

    internal class BaseDataModel
    {
        [JsonProperty("data")]
        public ImageDataModel Data { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        public void Validate()
        {
            if (!this.Success)
            {
                // TODO: Add error verification
                throw new System.NotImplementedException();
            }
        }
    }
}