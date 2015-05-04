namespace BlogSystem.Common.FileUploadServices.ImgurFileUpload
{
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    using Newtonsoft.Json;

    public class UploadService
    {
        private static string clientId;
        private static string clientSecret;
        private static ITokenDataModel tokenInfo;

        public UploadService()
        {
            if (!UploadService.IsInitialized)
            {
                throw new NullReferenceException("Initialize the UploadService before instantiate it!");
            }
        }

        public static bool IsInitialized
        {
            get { return UploadService.tokenInfo != null; }
        }

        public static void Initialize(string clientId, string clientSecret, ITokenDataModel tokenInfo)
        {
            if (string.IsNullOrWhiteSpace(clientId))
            {
                throw new ArgumentNullException("Client ID must not be empty!");
            }

            if (string.IsNullOrWhiteSpace(clientSecret))
            {
                throw new ArgumentNullException("Client Secret must not be empty!");
            }

            UploadService.clientId = clientId;
            UploadService.clientSecret = clientSecret;
            if (tokenInfo == null)
            {
                throw new ArgumentNullException("Token Info object must not be null!");
            }

            UploadService.tokenInfo = tokenInfo;
        }

        public static string GetRedirect(string clientId)
        {
            return "https://api.imgur.com/oauth2/authorize?client_id=" + clientId + "&response_type=code";
        }

        public static async Task<ITokenDataModel> GenerateToken(string clientId, string clientSecret, string code)
        {
            byte[] response;
            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values.Add("code", code);
                values.Add("client_id", clientId);
                values.Add("client_secret", clientSecret);
                values.Add("grant_type", "authorization_code");
                response = await client.UploadValuesTaskAsync("https://api.imgur.com/oauth2/token", values);
            }

            var result = JsonConvert.DeserializeObject<TokenDataModel>(Encoding.ASCII.GetString(response));
            return result;
        }

        public async Task<IImageDataModel> Upload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                throw new ArgumentNullException("File must not be null!");
            }

            string fileBase64String;
            using (var binaryReader = new BinaryReader(file.InputStream))
            {
                fileBase64String = Convert.ToBase64String(binaryReader.ReadBytes(file.ContentLength));
            }

            return await this.Upload(fileBase64String);
        }

        public async Task<IImageDataModel> Upload(string sourceUrl)
        {
            byte[] response;
            using (var client = new WebClient())
            {
                string token = await this.GetToken();
                client.Headers.Add("Authorization", "Bearer " + token);
                var values = new NameValueCollection { { "image", sourceUrl } };
                response = await client.UploadValuesTaskAsync("https://api.imgur.com/3/upload", values);
            }

            BaseDataModel baseModel = this.GetResult<BaseDataModel>(response, "File Upload");
            baseModel.Validate();
            baseModel.Data.Link = baseModel.Data.Link.Replace("http", "https");
            return baseModel.Data;
        }

        private T GetResult<T>(byte[] response, string message) where T : class
        {
            var responseString = Encoding.ASCII.GetString(response);
            T model;
            try
            {
                model = JsonConvert.DeserializeObject<T>(responseString);
            }
            catch (Exception)
            {
                throw new ArgumentException("Unsuccessful operation: " + message);
            }

            return model;
        }

        private async Task<string> GetToken()
        {
            if (!this.IsTokenValid(UploadService.tokenInfo))
            {
                UploadService.tokenInfo = await this.RefreshToken(UploadService.tokenInfo);
            }

            return UploadService.tokenInfo.AccessToken;
        }

        private async Task<ITokenDataModel> RefreshToken(ITokenDataModel info)
        {
            byte[] response;
            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values.Add("refresh_token", info.RefreshToken);
                values.Add("client_id", UploadService.clientId);
                values.Add("client_secret", UploadService.clientSecret);
                values.Add("grant_type", "refresh_token");
                response = await client.UploadValuesTaskAsync("https://api.imgur.com/oauth2/token", values);
            }

            TokenDataModel tokenModel = this.GetResult<TokenDataModel>(response, "Refresh Token");
            return tokenModel;
        }

        private bool IsTokenValid(ITokenDataModel info)
        {
            if (info.CreatedOn.AddSeconds(info.ExpiresIn) > DateTime.Now)
            {
                return true;
            }

            return false;
        }
    }
}