namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using BlogSystem.Common.FileUploadServices.ImgurFileUpload;
    using BlogSystem.Data.Common.Repository;
    using BlogSystem.Data.Models;
    using BlogSystem.Web.ViewModels.Users;

    public class ImgurAutenticationController : AdminBaseController
    {
        private readonly IRepository<ImgurToken> tokens;

        public ImgurAutenticationController(IRepository<ImgurToken> tokens)
        {
            this.tokens = tokens;
        }

        // GET: Administration/ImgurAutentication
        public ActionResult Index()
        {
            string clientId = this.ViewBag.Settings.Get["Imgur Client ID"];
            ITokenDataModel token = this.tokens.All().Where(x => x.ClientId == clientId).Project().To<ImgurTokenViewModel>().FirstOrDefault();
            return this.View(token);
        }

        public ActionResult Generate()
        {
            var clientId = this.ViewBag.Settings.Get["Imgur Client ID"];
            string redirectAddress = UploadService.GetRedirect(clientId);
            return this.Redirect(redirectAddress);
        }

        public async Task<ActionResult> Callback(string code)
        {
            var clientId = this.ViewBag.Settings.Get["Imgur Client ID"];
            var clientSecret = this.ViewBag.Settings.Get["Imgur Client Secret"];
            var tokenDb = this.tokens.GetById(clientId);
            ITokenDataModel info = await UploadService.GenerateToken(clientId, clientSecret, code);

            if (tokenDb == null)
            {
                var token = new ImgurToken
                {
                    ClientId = clientId,
                    AccessToken = info.AccessToken,
                    RefreshToken = info.RefreshToken,
                    ExpiresIn = info.ExpiresIn,
                    AccountId = info.AccountId,
                    AccountUsername = info.AccountUsername,
                    TokenType = info.TokenType,
                    Scope = info.Scope,
                    CreatedOn = info.CreatedOn
                };
                this.tokens.Add(token);
            }
            else
            {
                tokenDb.AccessToken = info.AccessToken;
                tokenDb.RefreshToken = info.RefreshToken;
                tokenDb.ExpiresIn = info.ExpiresIn;
                tokenDb.AccountId = info.AccountId;
                tokenDb.AccountUsername = info.AccountUsername;
                tokenDb.TokenType = info.TokenType;
                tokenDb.Scope = info.Scope;
                tokenDb.CreatedOn = info.CreatedOn;
                this.tokens.Update(tokenDb);
            }

            this.tokens.SaveChanges();
            return this.RedirectToAction("Index", "Home");
        }
    }
}