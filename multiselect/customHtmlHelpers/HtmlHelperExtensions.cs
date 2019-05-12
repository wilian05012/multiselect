using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomHtmlHelpers {
    public static class HtmlHelperExtensions {
        public static HtmlString MultiSelect (this IHtmlHelper htmlHelper, IEnumerable<SelectListItem> items, object htmlAttributes, int cols = 1) {
            return null;
        }
    }
}
