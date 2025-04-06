using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace YOGBIS.UI.Extensions
{
    public static class ControllerExtensions
    {
        public static async Task<string> RenderViewComponentToStringAsync(this Controller controller, string componentName, object parameters = null)
        {
            var context = controller.ControllerContext;
            var viewComponentHelper = context.HttpContext.RequestServices
                .GetRequiredService<IViewComponentHelper>();

            ((IViewContextAware)viewComponentHelper).Contextualize(new ViewContext(
                context,
                new NullView(),
                controller.ViewData,
                controller.TempData,
                TextWriter.Null,
                new HtmlHelperOptions()));

            var viewComponentResult = await viewComponentHelper.InvokeAsync(componentName, parameters);
            using var writer = new StringWriter();
            viewComponentResult.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
            return writer.ToString();
        }

        private class NullView : IView
        {
            public string Path => "";
            public async Task RenderAsync(ViewContext context)
            {
                await Task.CompletedTask;
            }
        }
    }
}
