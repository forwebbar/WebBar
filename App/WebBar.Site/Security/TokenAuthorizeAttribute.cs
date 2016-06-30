using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebBar.Site.Controllers;

namespace WebBar.Site.Security
{
    public class TokenAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["ConfirmToken"] == null ||
                HttpContext.Current.Session[ControllerExtentions.UserTokenParamName] == null)
            {
                FormsAuthentication.SignOut();
            }

           base.OnAuthorization(filterContext);
        }
    }
}