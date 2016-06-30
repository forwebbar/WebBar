using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Contracts.Common;
using WebBar.Site.Models;

namespace WebBar.Site.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        #region Constructors
        public AccountController(IAuthorize authorizeClient)
        {
            _authorizeClient = authorizeClient;
        }

        #endregion

        #region Fields

        readonly IAuthorize _authorizeClient;

        #endregion

        #region Methods

        #region Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _authorizeClient.Login(model.Username, Encoding.UTF8.GetBytes(model.Password));
            if (result == null)
            {
                ModelState.AddModelError("", "Попытка входа неудачна.");
                return View("Error");
            }

            if (HttpContext.Session != null) HttpContext.Session["ConfirmToken"] = result;

            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
        }

        #endregion

        #region SendCode

        // GET: /Account/SendCode
        [AllowAnonymous]
        public ActionResult SendCode(string returnUrl, bool rememberMe)
        {
            return RedirectToAction("VerifyCode", new
            {
                ReturnUrl = returnUrl,
                RememberMe = rememberMe
            });
        }

        #endregion

        #region VerifyCode

        [AllowAnonymous]
        public ActionResult VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            return View(new VerifyCodeViewModel {ReturnUrl = returnUrl});
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (HttpContext.Session != null)
            {
                var token = HttpContext.Session["ConfirmToken"] as ConfirmToken;

                if (token != null)
                {
                    var confirmOperation = _authorizeClient.ConfirmOperation(token, Encoding.UTF8.GetBytes(model.Code));

                    if (confirmOperation != null)
                    {
                        HttpContext.Session[ControllerExtentions.UserTokenParamName] = confirmOperation;

                        FormsAuthentication.SetAuthCookie(token.Username, false);
                        return RedirectToLocal(model.ReturnUrl);
                    }
                }
            }

            ModelState.AddModelError("", "Неверный код.");
            return View(model);
        }

        #endregion

        #region ForgotPassword

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = await UserManager.FindByNameAsync(model.Email);
                //if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                //{
                //    // Don't reveal that the user does not exist or is not confirmed
                //    return View("ForgotPasswordConfirmation");
                //}

                //string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                //var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                //await UserManager.SendEmailAsync(user.Id, "Reset Password",
                //   "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                //TempData["ViewBagLink"] = callbackUrl;
                //return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            ViewBag.Link = TempData["ViewBagLink"];
            return View();
        }

        #endregion

        #region ResetPassword

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //var user = await UserManager.FindByNameAsync(model.Email);
            //if (user == null)
            //{
            //    // Don't reveal that the user does not exist
            //    return RedirectToAction("ResetPasswordConfirmation", "Account");
            //}
            //var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            //if (result.Succeeded)
            //{
            //    return RedirectToAction("ResetPasswordConfirmation", "Account");
            //}
            //AddErrors(result);
            return View();
        }

        #endregion

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            if (HttpContext.Session != null)
            {
                HttpContext.Session["ConfirmToken"] = null;
                HttpContext.Session[ControllerExtentions.UserTokenParamName] = null;
            }

            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}