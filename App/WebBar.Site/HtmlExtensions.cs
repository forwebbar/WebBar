using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebBar.Site.Models;

namespace WebBar.Site
{
    public static class HtmlExtensions
    {
        public static IHtmlString GetBackgroundImageUrl(this HtmlHelper html)
        {
            var url = string.Empty;
            if (html.ViewData.Model is VerifyCodeViewModel || html.ViewData.Model is LoginViewModel || html.ViewData.Model == null)
            url = "url('../Images/beer-pattern.jpg')";

            return new HtmlString(url);
        }
        
        public static MvcHtmlString NoEncodeActionLink(this HtmlHelper htmlHelper,
                                             string text, string title, string action,
                                             string controller,
                                             object routeValues = null,
                                             object htmlAttributes = null)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            var builder = new TagBuilder("a") {InnerHtml = text};
            builder.Attributes["title"] = title;
            builder.Attributes["href"] = urlHelper.Action(action, controller, routeValues);
            builder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)));

            return MvcHtmlString.Create(builder.ToString());
        }
    }
}