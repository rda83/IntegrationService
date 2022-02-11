using FluentAssertions;
using IntegrationService.ResourceParameters;
using Xunit;

namespace IntegrationService.Tests.Core
{
    public class SimpleObjectResourceParameterTests
    {
        [Fact]
        public void GetRouteObject_Should()
        {
            // Arrange

            int currentPageNumber = 2, pageSize = 10;

            var routeObjectStubPreviousPage = new RouteObject()
            {
                Fields = "Id,Name,FormatType",
                OrderBy = "Name",
                PageNumber = 1,
                PageSize = 10
            };

            var routeObjectStubNextPage = new RouteObject()
            {
                Fields = "Id,Name,FormatType",
                OrderBy = "Name",
                PageNumber = 3,
                PageSize = 10
            };

            var queryParsmeters = new SimpleObjectResourceParameter();

            queryParsmeters.Fields = routeObjectStubPreviousPage.Fields;
            queryParsmeters.PageNumber = currentPageNumber;
            queryParsmeters.PageSize = pageSize;

            // Act
            var RouteObjectPreviousPage = queryParsmeters.GetRouteObject(ResourceUriType.PreviousPage);
            var RouteObjectNextPage = queryParsmeters.GetRouteObject(ResourceUriType.NextPage);

            // Assert
            routeObjectStubPreviousPage.Should().BeEquivalentTo(RouteObjectPreviousPage);
            routeObjectStubNextPage.Should().BeEquivalentTo(RouteObjectNextPage);

        }
    }
}
