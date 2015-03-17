namespace BlogSystem.Web.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using BlogSystem.Data.Common.Repository;
    using BlogSystem.Data.Models;
    using BlogSystem.Web.Infrastructure.Identity;
    using BlogSystem.Web.ViewModels.Users;

    [Authorize]
    public class UsersController : BaseController
    {
        private const string IncorrectRequestParameter = "Incorrect request parameter(s)!";
        private const string UnsupportedFileFormat = "Unsupported file format. Only .jpg, .png and .bmp are allowed.";
        private const string ForbiddenRequest = "Unauthorized request! You can only update your profile.";
        private const string InvalidUrlAddress = "URL address is invalid or not correct!URL address is invalid or not correct!";

        private readonly IRepository<ApplicationUser> users;
        private readonly ApplicationUser currentUser;

        public UsersController(ICurrentUser currentUser, IRepository<ApplicationUser> users)
        {
            this.users = users;
            this.currentUser = currentUser.Get();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Info(string userName)
        {
            UserViewModel model;
            if (this.TryParseUser(userName, out model))
            {
                return this.View(model);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, IncorrectRequestParameter);
        }

        [HttpGet]
        public ActionResult Edit(string userName)
        {
            UserInputModel model;
            if (this.TryParseUser(userName, out model))
            {
                if (model.Id == this.currentUser.Id || model.UserName == this.currentUser.UserName)
                {
                    return this.View(model);
                }

                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, ForbiddenRequest);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, IncorrectRequestParameter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserInputModel model)
        {
            if (ModelState.IsValid)
            {
                var userDb = this.users.GetById(model.Id);
                if (userDb.Id == this.currentUser.Id && userDb.UserName == this.currentUser.UserName)
                {
                    userDb.Email = model.Email;
                    userDb.RealName = model.RealName;
                    userDb.AvatarUrl = model.AvatarUrl;

                    this.users.Update(userDb);
                    this.users.SaveChanges();

                    return this.RedirectToAction("Info");
                }

                return new HttpUnauthorizedResult(ForbiddenRequest);
            }

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(string id, string source, string url, HttpPostedFileBase file)
        {
            if (id == null || source == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, IncorrectRequestParameter);
            }

            string fileExtention;
            if (source == "url")
            {
                if (url == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, IncorrectRequestParameter);
                }

                var client = new WebClient();
                try
                {
                    client.OpenRead(url);
                }
                catch (WebException)
                {
                    return new HttpNotFoundResult(InvalidUrlAddress);
                }

                var contentType = client.ResponseHeaders["content-type"];
                if (this.IsContentTypeAllowed(contentType) == false)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, UnsupportedFileFormat);
                }

                fileExtention = "." + contentType.Substring(contentType.IndexOf('/') + 1);
                client.DownloadFile(url, Server.MapPath("~/Content/Users/") + id + fileExtention);
            }
            else
            {
                if (file == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, IncorrectRequestParameter);
                }

                if (this.IsContentTypeAllowed(file.ContentType) == false)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, UnsupportedFileFormat);
                }

                fileExtention = Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath("~/Content/Users/") + id + fileExtention);
            }

            return this.Content("/Content/Users/" + id + fileExtention);
        }

        private bool TryParseUser<T>(string userName, out T model)
        {
            model = this.users.All().Where(x => x.UserName == userName).Project().To<T>().FirstOrDefault();
            return model != null;
        }

        private bool IsContentTypeAllowed(string contentType)
        {
            switch (contentType.ToLower())
            {
                case "image/png":
                case "image/jpeg":
                case "image/pjpeg":
                case "image/bmp": return true;
                default: return false;
            }
        }
    }
}