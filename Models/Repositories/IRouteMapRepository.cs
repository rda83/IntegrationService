namespace IntegrationService.Models.Repositories
{
    public interface IRouteMapRepository
    {
        RouteMap GetRouteMap(string routeMapId);
    }
}