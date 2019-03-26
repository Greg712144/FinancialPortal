using System.Web.Mvc;

namespace FinancialPortal.Helpers
{
    public class AllowHtmlHelper : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            var name = bindingContext.ModelName;

            return request.Unvalidated[name];
        }
    }
}