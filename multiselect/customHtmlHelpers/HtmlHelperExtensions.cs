using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;

namespace CustomHtmlHelpers {
    public static class HtmlHelperExtensions {

        public static HtmlString MultiSelect<TModel, T> (
            this IHtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, IEnumerable<T>>> expression,
            IEnumerable<SelectListItem> items, 
            object htmlAttributes, 
            int cols = 1) {
            
            ModelExplorer modelExplorer = ExpressionMetadataProvider.FromLambdaExpression(expression, htmlHelper.ViewData, htmlHelper.MetadataProvider);
            StringBuilder strBuilder = new StringBuilder(modelExplorer.Metadata.Name);
            
            while(modelExplorer.Container.Metadata.Name != null) {
                modelExplorer = modelExplorer.Container;
                strBuilder.Insert(0, ".");
                strBuilder.Insert(0, modelExplorer.Metadata.Name);
            }

            string cboxName = strBuilder.ToString();

            TagBuilder mainContainer = new TagBuilder("div");
            if (htmlAttributes != null) {
                foreach (PropertyInfo prop in htmlAttributes.GetType().GetProperties()) {
                    mainContainer.Attributes.Add(prop.Name, prop.GetValue(htmlAttributes).ToString());
                }
            }

            int totalRows = items.Count() / cols + 1;
            TagBuilder grid = new TagBuilder("div");
            grid.Attributes.Add("style", $"display: grid; grid-template-columns: {cols}; grid-template-rows: {totalRows}");

            int index = 0;
            foreach(SelectListItem item in items) {
                int currentCol = index % cols + 1;
                int currentRow = index / cols + 1;

                TagBuilder cell = new TagBuilder("div");
                cell.Attributes.Add("style", $"grid-column-start: {currentCol}; grid-row-start: {currentRow}");

                TagBuilder checkbox = new TagBuilder("input");
                Dictionary<string, string> attributes = new Dictionary<string, string>() {
                    { "type", "checkbox" },
                    { "name", cboxName },
                    { "value", item.Value }
                };

                //if(results.Any(r => r.ToString().Equals(item.Value))) {
                //    attributes.Add("checked", "checked");
                //}
                if(item.Selected) {
                    attributes.Add("checked", "checked");
                }

                checkbox.GenerateId($"{cboxName}_{item.Value}", "_");
                foreach(string key in attributes.Keys) { checkbox.Attributes.Add(key, attributes[key]); }

                TagBuilder span = new TagBuilder("span");
                span.InnerHtml.AppendLine(item.Text);

                cell.InnerHtml.AppendLine(checkbox.RenderSelfClosingTag());
                cell.InnerHtml.AppendHtml(span.RenderStartTag());
                cell.InnerHtml.AppendHtml(span.RenderBody());
                cell.InnerHtml.AppendLine(span.RenderEndTag());

                grid.InnerHtml.AppendHtml(cell.RenderStartTag());
                grid.InnerHtml.AppendHtml(cell.RenderBody());
                grid.InnerHtml.AppendLine(cell.RenderEndTag());

                index++;
            }

            mainContainer.InnerHtml.AppendHtml(grid.RenderStartTag());
            mainContainer.InnerHtml.AppendHtml(grid.RenderBody());
            mainContainer.InnerHtml.AppendLine(grid.RenderEndTag());

            string result;
            using(TextWriter textWriter = new StringWriter()) {
                mainContainer.WriteTo(textWriter, HtmlEncoder.Default);
                result = textWriter.ToString();
            }

            return new HtmlString(result);
        }

        public static HtmlString MultiSelect<TModel, T>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, IEnumerable<T>>> expression,
            IEnumerable<SelectListItem> items,
            int cols = 1) { return MultiSelect(htmlHelper, expression, items, null, cols); }
    }
}
