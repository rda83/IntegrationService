using AutoMapper;
using IntegrationService.Model;
using IntegrationService.Data.Services;
using System;
using System.Collections.Generic;
using IntegrationService.ResourceParameters;
using IntegrationService.PropertyCheckerService;
using IntegrationService.PropertyMappingService;
using IntegrationService.Helpers;
using System.Dynamic;

namespace IntegrationService.MessageFormatManager
{
    public class MessageFormatManagerImpl : IMessageFormatManager
    {
        private readonly IMapper _mapper;
        private IMessageFormatRepository _messageFormatRepository;
        private IPropertyCheckerService _propertyCheckerService;
        private IPropertyMappingService _propertyMappingService;

        public MessageFormatManagerImpl(IMessageFormatRepository messageFormatRepository,
            IPropertyCheckerService propertyCheckerService,
            IPropertyMappingService propertyMappingService,
            IMapper mapper)
        {
            _messageFormatRepository = messageFormatRepository ?? 
                throw new ArgumentNullException(nameof(messageFormatRepository));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            _propertyCheckerService = propertyCheckerService ??
                throw new ArgumentNullException(nameof(propertyCheckerService));

            _propertyMappingService = propertyMappingService ??
                throw new ArgumentNullException(nameof(propertyMappingService));
        }

        public MessageFormat GetMessageFormat(long Id)
        {
            IntegrationService.Data.Entities.MessageFormat resultEntity = 
                _messageFormatRepository.GetMessageFormat(Id);

            var result = _mapper.Map<MessageFormat>(resultEntity);
            return result;
        }

        public void AddMessageFormat(MessageFormat messageFormat)
        {

            var messageFormatEntity = _mapper.Map<IntegrationService.Data.Entities.MessageFormat>(messageFormat);

            _messageFormatRepository.AddMessageFormat(messageFormatEntity);
            _messageFormatRepository.Save();
        }

        public IEnumerable<ExpandoObject> GetMessageFormats(SimpleObjectResourceParameter request)
        {
            if (!_propertyMappingService.ValidMappingExistsFor<MessageFormat, Data.Entities.MessageFormat>(
                request.OrderBy))
            {
                //return BadRequest();
                var i = 0;
            }

            if (!_propertyCheckerService.TypeHasProperties<MessageFormat>(
                request.Fields))
            {
                var i = 0;
                //return BadRequest();
            }

            var messageFormatEntities = _messageFormatRepository
                .GetMessageFormatsPages(request.Name, request.PageNumber, request.PageSize, request.OrderBy);

            var result = _mapper
                .Map<IEnumerable<MessageFormat>>(messageFormatEntities)
                .ShapeData(request.Fields);

            return result;
        }
    }
}
