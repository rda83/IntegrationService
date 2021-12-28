using AutoMapper;
using IntegrationService.Helpers;
using IntegrationService.MessageFormatManager;
using IntegrationService.Model;
using IntegrationService.PropertyCheckerService;
using IntegrationService.PropertyMappingService;
using IntegrationService.ResourceParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace IntegrationService.api.Controllers
{

    /// <summary>
    /// Интерфейс для работы с форматами сообщений. 
    /// </summary>
    [ApiExplorerSettings(GroupName = "Конфигурирование приложения")]
    [ApiController]
    [Route("api/configuration/messageFormats")]
    public class MessageFormatController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IMessageFormatManager _manager;
        private IPropertyCheckerService _propertyCheckerService;
        private IPropertyMappingService _propertyMappingService;

        public MessageFormatController(IMessageFormatManager manager,
            IPropertyCheckerService propertyCheckerService,
            IPropertyMappingService propertyMappingService,
            IMapper mapper)
        {
            _manager = manager
                ?? throw new NullReferenceException(nameof(manager));

            _propertyCheckerService = propertyCheckerService ??
                throw new ArgumentNullException(nameof(propertyCheckerService));

            _propertyMappingService = propertyMappingService ??
                throw new ArgumentNullException(nameof(propertyMappingService));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET api/configuration/messageFormats
        // GET api/configuration/messageFormats/{messageFormatId}
        // GET api/configuration/messageFormats?orderby=name


        ///**
        // GET api/configuration/messageFormats/{messageFormatId}/schemes
        // GET api/configuration/schemes{schemaId}

        // RPC-style
        // GET api/configuration/messageFormats/{messageFormatId}/pagetotals
        // GET api/configuration/messageFormatsPageTotals/{id}
        // GET api/configuration/messageFormats/{messageFormatId}/totalAmountOfPages


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="request">Параметры запроса </param>
        ///// <returns>Пустой ответ в случае успеха</returns>
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Consumes("application/json")]
        //[HttpPost]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MessageFormat>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet(Name = "GetMessageFormats")]
        public ActionResult<IEnumerable<MessageFormat>> GetMessageFormats([FromQuery] SimpleObjectResourceParameter request)
        {
            if (!_propertyMappingService.ValidMappingExistsFor<MessageFormat, Data.Entities.MessageFormat>(
                request.OrderBy))
            {
                return BadRequest();
            }

            if (!_propertyCheckerService.TypeHasProperties<MessageFormat>(
                request.Fields))
            {
                return BadRequest();
            }

            var messageFormatEntities = _manager.GetMessageFormats(request);

            var result = _mapper
                .Map<IEnumerable<MessageFormat>>(messageFormatEntities)
                .ShapeData(request.Fields);

            var previousPageLink = messageFormatEntities.HasPrevious
                ? request.GetResourceUri(this, "GetMessageFormats", ResourceUriType.PreviousPage)
                : null;
            var nextPageLink = messageFormatEntities.HasNext
                ? request.GetResourceUri(this, "GetMessageFormats", ResourceUriType.NextPage)
                : null;

            var paginationMetadata = new
            {
                totalCount = messageFormatEntities.TotalCount,
                pageSize = messageFormatEntities.PageSize,
                currentPage = messageFormatEntities.CurrentPage,
                totalPages = messageFormatEntities.TotalPages,
                previousPageLink,
                nextPageLink
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(
                paginationMetadata,
                new JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                }));

            return Ok(result);
        }

        [HttpGet("{Id}")]
        public ActionResult GetMessageFormat(long Id)
        {
            var result = _manager.GetMessageFormat(Id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
