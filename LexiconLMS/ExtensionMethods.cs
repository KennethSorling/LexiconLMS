using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LexiconLMS.Interfaces;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace LexiconLMS
{
    public static class ExtensionMethods
    {
        public static MvcHtmlString HiddenForReferrer<TModel>(this HtmlHelper<TModel> htmlHelper) where TModel : IReferrer
        {
            var str = htmlHelper.HiddenFor(_ => _.Referrer);
            var referrer = HttpContext.Current.Request.UrlReferrer.AbsoluteUri;
            return new MvcHtmlString(str.ToHtmlString().Replace("value=\"\"", String.Format("value=\"{0}\"", referrer)));
        }
    }
}