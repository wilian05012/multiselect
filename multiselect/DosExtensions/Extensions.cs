using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc {
    public static class Extensions {
        /// <summary>
        /// Generates the HTML markup corresponding to a list of checkboxes.
        /// </summary>
        /// <typeparam name="TModel">The type of the model</typeparam>
        /// <typeparam name="T">The type of the elements of the enumeration. Use simple types</typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression">The lambda expression that yields an enumeration of Ts</param>
        /// <param name="namePrefix">Prefix to be used by the name for the generated checkboxes</param>
        /// <param name="items">List of available options to be displayed</param>
        /// <param name="htmlAttributes">An object whose properties will become attributes for the HTML main container element</param>
        /// <param name="cols">Number of columns in which the list will be arranged. Valid values range lies between 1 and 12</param>
        /// <returns></returns>
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


            ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string prefix = htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix;

            string cboxName = String.IsNullOrWhiteSpace(prefix) ? 
                modelMetadata.PropertyName : 
                $"{prefix}.{modelMetadata.PropertyName}";

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
