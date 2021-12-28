
namespace IntegrationService.ResourceParameters
{
    public abstract class CommonResourceParameter : ICommonResourceParameter
    {
        protected int _pageSize = DefaultSettings.GetDefaultPageSize();

        public virtual object GetRouteObject(ResourceUriType uriType)
        {
            return new { };
        }
    }
}
