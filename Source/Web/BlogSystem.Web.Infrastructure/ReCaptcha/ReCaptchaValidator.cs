namespace BlogSystem.Web.Infrastructure.ReCaptcha
{
    using System.Collections.Generic;
    using System.Net;

    using Newtonsoft.Json;

    public class ReCaptchaValidator
    {
        private const string GoogleReCaptchaString = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";

        public ReCaptchaValidator(string recaptchaResponse, string recaptchaSecret)
            : this()
        {
            if (recaptchaResponse != null && recaptchaSecret != null)
            {
                var client = new WebClient();
                var googleReply = client.DownloadString(string.Format(GoogleReCaptchaString, recaptchaSecret, recaptchaResponse));
                var obj = JsonConvert.DeserializeObject<ReCaptchaValidator>(googleReply);
                this.Success = obj.Success;
                this.ErrorCodes = obj.ErrorCodes;
            }
        }

        private ReCaptchaValidator()
        {
            this.Success = false;
            this.ErrorCodes = new List<string>();
        }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}