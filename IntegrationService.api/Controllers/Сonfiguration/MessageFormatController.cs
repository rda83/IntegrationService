using IntegrationService.MessageFormatManager;
using IntegrationService.Model;
using IntegrationService.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        private readonly IMessageFormatManager _manager;

        public MessageFormatController(IMessageFormatManager manager)
        {
            _manager = manager
                ?? throw new NullReferenceException(nameof(manager));
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

        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<MessageFormat>> GetMessageFormats([FromQuery] SimpleObjectResourceParameter request)
        {
            //throw new Exception();
            //var result = _manager.GetMessageFormats(name, searchQuery);
            return Ok();
        }

        [HttpGet("{Id}")]
        public ActionResult<MessageFormat> GetMessageFormat(long Id)
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
