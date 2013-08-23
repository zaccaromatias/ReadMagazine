using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReadMagazine.WebUI.HtmlHelpers
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString CustomInput(this HtmlHelper helper, System.Linq.Expressions.Expression expression)
        {
            var p = helper.ViewData.ModelMetadata.Properties;
            var html = string.Empty;
            //html = String.Format("<input class=\"span3\" placeholder=\"{0}\" type=\"{1}\" name=\"{0}\" id=\"{0}\">",helper.ViewData.ModelMetadata.Properties.FirstOrDefault(p=>p.DisplayName==name),"popo");

            return MvcHtmlString.Create(html);
        }
    }
}