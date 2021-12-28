using Microsoft.AspNetCore.Mvc;

namespace IntegrationService.ResourceParameters
{
    public static class ControllerNavigationExtensions
    {
        public static string GetResourceUri<T>(this T resourceParameter,
            ControllerBase controller,
            string methodName,
            ResourceUriType uriType) where T : ICommonResourceParameter
        {
            var routeObject = resourceParameter.GetRouteObject(uriType);
            var result = controller.Url.Link(
                methodName,
                routeObject
                );

            return result;
        }
    }
}
