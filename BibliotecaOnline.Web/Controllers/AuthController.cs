using BibliotecaOnline.Web.ViewModel;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace BibliotecaOnline.Web.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        public ActionResult Login(string returnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            
            if (model.Email == "admin@admin.com" && model.Senha == "senha")
            {
                var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, "Admin"),
                    new Claim(ClaimTypes.Email, "samuel@gmail.com"),
                    new Claim(ClaimTypes.Country, "Brazil"),
                },
                "ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;
                authManager.SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));

            }

            ModelState.AddModelError("", "Email ou Senha inválidos");
            return View();

        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("ListarLivros", "Livro");
            }

            return returnUrl;
        }

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var AuthManager = ctx.Authentication;
            AuthManager.SignOut("ApplicationCookie");

            return RedirectToAction("Login", "Auth");
        }
    }
}