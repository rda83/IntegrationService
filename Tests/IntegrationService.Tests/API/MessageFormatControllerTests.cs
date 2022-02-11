using AutoMapper;
using IntegrationService.api.Controllers;
using IntegrationService.MessageFormatManager;
using IntegrationService.PropertyCheckerService;
using IntegrationService.PropertyMappingService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;
using FluentAssertions;
using IntegrationService.ResourceParameters;
using IntegrationService.Application.PropertyMapping;
using IntegrationService.Helpers;
using System.Collections.Generic;
using IntegrationService.Application.Mapping.Configuration;
using Microsoft.AspNetCore.Http;
using System.Dynamic;
using System.Linq;

namespace IntegrationService.Tests
{
    public class MessageFormatControllerTests
    {
        private readonly Random random = new Random();

        [Fact]
        public void GetMessageFormat_WithUnexistingItem_ReturnsNotFound()
        {
            // Arrange
            long unexistingId = 1;

            var mapperStub = new Mock<IMapper>();
            var messageFormatManagerStub = new Mock<IMessageFormatManager>();
            var propertyCheckerServiceStub = new Mock<IPropertyCheckerService>();
            var propertyMappingServiceStub = new Mock<IPropertyMappingService>();

            var messageFormatControllerStub = new MessageFormatController(messageFormatManagerStub.Object, 
                propertyCheckerServiceStub.Object, 
                propertyMappingServiceStub.Object, 
                mapperStub.Object);

            // Act
            var result = messageFormatControllerStub.GetMessageFormat(unexistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetMessageFormat_WithExistingItem_ReturnsMessageFormatObject()
        {
            // Arrange

            var randomMessageFormatModel = GetRandomMessageFormatModel();

            var mapperStub = new Mock<IMapper>();
            var messageFormatManagerStub = new Mock<IMessageFormatManager>();
            messageFormatManagerStub.Setup(x => x.GetMessageFormat(It.IsAny<long>()))
                .Returns(randomMessageFormatModel);

            var propertyCheckerServiceStub = new Mock<IPropertyCheckerService>();
            var propertyMappingServiceStub = new Mock<IPropertyMappingService>();

            var messageFormatControllerStub = new MessageFormatController(messageFormatManagerStub.Object,
                propertyCheckerServiceStub.Object,
                propertyMappingServiceStub.Object,
                mapperStub.Object);

            // Act
            var actionResult = messageFormatControllerStub.GetMessageFormat(randomMessageFormatModel.Id);
            var result = actionResult.Result as OkObjectResult;

            // Assert
            result.Value.Should().BeEquivalentTo(randomMessageFormatModel);
        }

        [Fact]
        public void GetMessageFormats_UnValidMappingExistsFor_OrderBy()
        {
            var mapperStub = new Mock<IMapper>();
            var messageFormatManagerStub = new Mock<IMessageFormatManager>();

            IPropertyMappingService propertyMappingService = PropertyMappingServiceBuilder.Create().AddAllMappings().Build();
            IPropertyCheckerService propertyCheckerService = new PropertyCheckerService.PropertyCheckerService(); 

            var messageFormatControllerStub = new MessageFormatController(messageFormatManagerStub.Object,
                propertyCheckerService,
                propertyMappingService,
                mapperStub.Object);

            var queryParsmeters = new SimpleObjectResourceParameter();
            queryParsmeters.OrderBy = "unknownFieldName";

            // Act
            var actionResult = messageFormatControllerStub.GetMessageFormats(queryParsmeters);

            // Assert
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void GetMessageFormats_UnValidShapeData()
        {
            // Arrange
            var mapperStub = new Mock<IMapper>();
            var messageFormatManagerStub = new Mock<IMessageFormatManager>();

            IPropertyMappingService propertyMappingService = PropertyMappingServiceBuilder.Create().AddAllMappings().Build();
            IPropertyCheckerService propertyCheckerService = new PropertyCheckerService.PropertyCheckerService();

            var messageFormatControllerStub = new MessageFormatController(messageFormatManagerStub.Object,
                propertyCheckerService,
                propertyMappingService,
                mapperStub.Object);

            var queryParsmeters = new SimpleObjectResourceParameter();
            queryParsmeters.Fields = "unknownFieldName";

            // Act
            var actionResult = messageFormatControllerStub.GetMessageFormats(queryParsmeters);

            // Assert
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void GetMessageFormats_ShapeData()
        {
            // Arrange
            var items = new List<Data.Entities.MessageFormat>() 
            { 
                GetRandomMessageFormatEntity(),
                GetRandomMessageFormatEntity(),
                GetRandomMessageFormatEntity(),
                GetRandomMessageFormatEntity(),
                GetRandomMessageFormatEntity(),
                GetRandomMessageFormatEntity(),
                GetRandomMessageFormatEntity(),
                GetRandomMessageFormatEntity(),
                GetRandomMessageFormatEntity(),
                GetRandomMessageFormatEntity()
            };
            int count = 10, pageNumber = 1, pageSize = 10;
            var messageFormats = new PageList<Data.Entities.MessageFormat>(items, count, pageNumber, pageSize);
            

            var queryParsmeters = new SimpleObjectResourceParameter();
            queryParsmeters.Fields = "Id,Name,FormatType";

            var messageFormatManagerStub = new Mock<IMessageFormatManager>();
            messageFormatManagerStub.Setup(x => x.GetMessageFormats(queryParsmeters))
                           .Returns(messageFormats);

            IPropertyMappingService propertyMappingService = PropertyMappingServiceBuilder.Create().AddAllMappings().Build();
            IPropertyCheckerService propertyCheckerService = new PropertyCheckerService.PropertyCheckerService();


            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MessageFormatProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var messageFormatControllerStub = new MessageFormatController(messageFormatManagerStub.Object,
                propertyCheckerService,
                propertyMappingService,
                mapper);

            messageFormatControllerStub.ControllerContext = ctx;

            // Act
            var actionResult = messageFormatControllerStub.GetMessageFormats(queryParsmeters);
            var result = actionResult.Result as OkObjectResult;

            var values = result.Value as System.Collections.Generic.List<ExpandoObject>;

            string keys = "";
            if (values.Count > 0)
            {
                var properties = values[0] as IDictionary<String, Object>;
                keys = string.Join(",", properties.Keys.ToArray());
            }

             
            var resultObjects = values.Select(item =>
            {
                var tmp = item as dynamic;

                var messageFormat = new Data.Entities.MessageFormat()
                {
                    Id = tmp.Id,
                    FormatType = tmp.FormatType,
                    Name = tmp.Name
                };

                return messageFormat;
            }).ToList();


            // Assert
            Assert.Equal(items.Count, values.Count);
            Assert.Equal(queryParsmeters.Fields, keys);

            items.Select(i => new { i.Id, i.Name, i.FormatType }).ToList()
                .Should().BeEquivalentTo(resultObjects.Select(i => new { i.Id, i.Name, i.FormatType }).ToList());

        }

        [Fact]
        public void GetMessageFormats_PaginationHeaders()
        {
            // Arrange
            var items = new List<Data.Entities.MessageFormat>()
            {
                GetRandomMessageFormatEntity(),
                GetRandomMessageFormatEntity(),
                GetRandomMessageFormatEntity(),
                GetRandomMessageFormatEntity(),
                GetRandomMessageFormatEntity(),
                GetRandomMessageFormatEntity(),
                GetRandomMessageFormatEntity(),
                GetRandomMessageFormatEntity(),
                GetRandomMessageFormatEntity(),
                GetRandomMessageFormatEntity()
            };

            var routeObjectStubPreviousPage = new RouteObject()
            {
                Fields = "Id,Name,FormatType",
                OrderBy = "Name",
                PageNumber = 0,
                PageSize = 10
            };

            var routeObjectStubNextPage = new RouteObject()
            {
                Fields = "Id,Name,FormatType",
                OrderBy = "Name",
                PageNumber = 2,
                PageSize = 10
            };

            int count = 10, pageNumber = 1, pageSize = 10;
            
            var messageFormats = new PageList<Data.Entities.MessageFormat>(items, count, pageNumber, pageSize);
            var queryParsmeters = new SimpleObjectResourceParameter();
            queryParsmeters.Fields = routeObjectStubPreviousPage.Fields;

            var messageFormatManagerStub = new Mock<IMessageFormatManager>();
            messageFormatManagerStub.Setup(x => x.GetMessageFormats(queryParsmeters))
                           .Returns(messageFormats);

            IPropertyMappingService propertyMappingService = PropertyMappingServiceBuilder.Create().AddAllMappings().Build();
            IPropertyCheckerService propertyCheckerService = new PropertyCheckerService.PropertyCheckerService();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MessageFormatProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var messageFormatControllerStub = new MessageFormatController(messageFormatManagerStub.Object,
                propertyCheckerService,
                propertyMappingService,
                mapper);
            
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            messageFormatControllerStub.ControllerContext = ctx;

            // Act
            var actionResult = messageFormatControllerStub.GetMessageFormats(queryParsmeters);
  
            var RouteObjectPreviousPage = queryParsmeters.GetRouteObject(ResourceUriType.PreviousPage);
            var RouteObjectNextPage = queryParsmeters.GetRouteObject(ResourceUriType.NextPage);

            routeObjectStubPreviousPage.Should().BeEquivalentTo(RouteObjectPreviousPage);
            routeObjectStubNextPage.Should().BeEquivalentTo(RouteObjectNextPage);

        }

        private Model.MessageFormat GetRandomMessageFormatModel()
        {
            var result = new Model.MessageFormat()
            {
                Id = random.Next(1000),
                Name = Guid.NewGuid().ToString(),
                FormatType = Model.MessageFormatType.PLAIN_TEXT,
                Scheme = Guid.NewGuid().ToString()
            };

            return result;
        }

        private Data.Entities.MessageFormat GetRandomMessageFormatEntity()
        {
            var result = new Data.Entities.MessageFormat()
            {
                Id = random.Next(1000),
                Name = Guid.NewGuid().ToString(),
                FormatType = Model.MessageFormatType.PLAIN_TEXT,
                Scheme = Guid.NewGuid().ToString()
            };

            return result;
        }
    }
}
