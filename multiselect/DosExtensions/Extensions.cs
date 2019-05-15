using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace DosExtensions {
    public static class Extensions {
        public static MvcHtmlString MultiSelectFor<TModel, T>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, IEnumerable<T>>> expression, 
            IEnumerable<SelectListItem> items, 
            object htmlAttributes, 
            int cols = 1 ) {

            cols = (cols < 1 || cols > 12) ? 1 : cols;
            int rows = items.Count() % cols == 0 ? items.Count() / cols : 1 + items.Count() / cols;

            TagBuilder mainContainer = new TagBuilder("div");
            if(htmlAttributes != null) {
                foreach(PropertyInfo prop in htmlAttributes.GetType().GetProperties()) {
                    mainContainer.Attributes.Add(prop.Name, prop.GetValue(htmlAttributes).ToString());
                }
            }

            TagBuilder grid = new TagBuilder("div");
            grid.Attributes.Add("style", $"display: grid; grid.template-columns: {cols}; grid-template-rows: {rows}");

            string cboxName = ExpressionHelper.GetExpressionText(expression);
            ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            int index = 0;
            StringBuilder gridContentBuilder = new StringBuilder();
            foreach(SelectListItem item in items) {
                int currentCol = 1 + index % cols;
                int currentRow = 1 + index / cols;

                TagBuilder div = new TagBuilder("div");
                div.Attributes.Add("style", $"grid-column-start: {currentCol}; grid-row-start: {currentRow}");

                TagBuilder checkbox = new TagBuilder("input");
                checkbox.Attributes.Add("type", "checkbox");
                checkbox.Attributes.Add("name", cboxName);
                checkbox.Attributes.Add("value", item.Value);
                if(item.Selected) {
                    checkbox.Attributes.Add("checked", "checked");
                }
                checkbox.GenerateId($"{cboxName}_{item.Value}");

                TagBuilder label = new TagBuilder("label");
                label.Attributes.Add("for", checkbox.Attributes["id"]);
                label.SetInnerText(item.Text);

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine(checkbox.ToString(TagRenderMode.SelfClosing));
                stringBuilder.AppendLine(label.ToString(TagRenderMode.Normal));

                div.InnerHtml = stringBuilder.ToString();

                gridContentBuilder.AppendLine(div.ToString(TagRenderMode.Normal));

                index++;
            }

            grid.InnerHtml = gridContentBuilder.ToString();
            mainContainer.InnerHtml = grid.ToString(TagRenderMode.Normal);

            return new MvcHtmlString(mainContainer.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString MultiSelectFor<TModel, T>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, IEnumerable<T>>> expression, 
            IEnumerable<SelectListItem> items, 
            int cols = 1) {

            return Extensions.MultiSelectFor(htmlHelper, expression, items, null, cols);
        }
    }
}
