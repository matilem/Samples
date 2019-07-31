using System;
using System.IO;
using System.Web.Mvc;
using System.Web.Routing;

namespace Aafp.EmailSender.Api.Helpers
{
    public class RazorViewGenerator : ControllerBase
    {
        protected override void ExecuteCore() { }

        public static string RenderView(string controllerName, string viewName, object viewData, RequestContext rctx)
        {
            try
            {
                using (var writer = new StringWriter())
                {
                    var routeData = new RouteData();
                    routeData.Values.Add("controller", controllerName);

                    var controllerContext = new ControllerContext(rctx, new RazorViewGenerator());
                    controllerContext.RouteData = routeData;
                    var razorViewEngine = new RazorViewEngine();
                    var razorViewResult = razorViewEngine.FindView(controllerContext, viewName, string.Empty, false);
                    var viewContext = new ViewContext(controllerContext, razorViewResult.View, new ViewDataDictionary(viewData), new TempDataDictionary(), writer);

                    razorViewResult.View.Render(viewContext, writer);

                    return writer.GetStringBuilder().ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);

                return string.Empty;
            }
        }

        public static string RenderPlainTextView(string controllerName, string viewName, object viewData, RequestContext rctx)
        {
            try
            {
                using (var writer = new StringWriter())
                {
                    var routeData = new RouteData();
                    routeData.Values.Add("controller", controllerName);

                    var controllerContext = new ControllerContext(rctx, new RazorViewGenerator());
                    controllerContext.RouteData = routeData;
                    var razorViewEngine = new RazorViewEngine();
                    var razorViewResult = razorViewEngine.FindView(controllerContext, viewName, string.Empty, false);
                    var viewContext = new ViewContext(controllerContext, razorViewResult.View, new ViewDataDictionary(viewData), new TempDataDictionary(), writer);

                    razorViewResult.View.Render(viewContext, writer);

                    return writer.GetStringBuilder().ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);

                return string.Empty;
            }
        }
    }
}