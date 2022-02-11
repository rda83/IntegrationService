
namespace IntegrationService.ResourceParameters
{
    public abstract class CommonResourceParameter : ICommonResourceParameter
    {
        protected int _pageSize = DefaultSettings.GetDefaultPageSize();

        public virtual RouteObject GetRouteObject(ResourceUriType uriType)
        {
            return new RouteObject();
        }
    }
}
