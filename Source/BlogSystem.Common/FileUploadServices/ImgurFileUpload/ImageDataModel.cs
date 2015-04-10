namespace BlogSystem.Common.FileUploadServices.ImgurFileUpload
{
    internal class ImageDataModel : IImageDataModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Datetime { get; set; }

        public string Type { get; set; }

        public bool Animated { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Size { get; set; }

        public int Views { get; set; }

        public int Bandwidth { get; set; }

        public string Ddeletehash { get; set; }

        public string Section { get; set; }

        public string Link { get; set; }

        public string AccountUrl { get; set; }

        public int AaccountId { get; set; }
    }
}