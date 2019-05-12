using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CustomHtmlHelpers {
    public static class HtmlHelperExtensions {

        public static HtmlString MultiSelect<TModel, T> (
            this IHtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, IEnumerable<T>>> expression,
            IEnumerable<SelectListItem> items, 
            object htmlAttributes, 
            int cols = 1) {

            string cboxName = ExpressionHelper.GetExpressionText(expression);
            
            IEnumerable<T> results = ExpressionMetadataProvider.FromLambdaExpression(expression, htmlHelper.ViewData, htmlHelper.MetadataProvider).Model as IEnumerable<T>;
            

            foreach(SelectListItem item in items) {
                TagBuilder checkbox = new TagBuilder("input");
                Dictionary<string, string> attributes = new Dictionary<string, string>() {
                    { "type", "checkbox" },
                    { "name", cboxName }
                };

                if(results.Any(r => r.ToString().Equals(item.Value))) {
                    attributes.Add("checked", "checked");
                }
                foreach(string key in attributes.Keys) { checkbox.Attributes.Add(key, attributes[key]); }

                TagBuilder span = new TagBuilder("span");
                span.InnerHtml.AppendLine(item.Text);
            }



            return null;
        }
    }
}
