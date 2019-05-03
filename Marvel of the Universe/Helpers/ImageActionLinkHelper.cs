using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;

namespace Marvel_of_the_Universe.Helpers
{
    public static class ImageActionLinkHelper
    {
        public static IHtmlString ImageActionLink(this AjaxHelper helper, string imageUrl,string classAtributues, string height,string width, string actionName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes = null)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", imageUrl);
            builder.MergeAttribute("class", classAtributues);
            builder.MergeAttribute("width", width);
            builder.MergeAttribute("height", height);
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            var link = helper.ActionLink("[replaceme]", actionName, routeValues, ajaxOptions).ToHtmlString();
            return MvcHtmlString.Create(link.Replace("[replaceme]", builder.ToString(TagRenderMode.SelfClosing)));
        }
    }
}