using AutoMapper;
using IntegrationService.Model;
using IntegrationService.Data.Services;
using System;
using IntegrationService.ResourceParameters;
using IntegrationService.PropertyCheckerService;
using IntegrationService.PropertyMappingService;
using IntegrationService.Helpers;
using Microsoft.AspNetCore.JsonPatch;

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

        public PageList<Data.Entities.MessageFormat> GetMessageFormats(SimpleObjectResourceParameter request)
        {

            var messageFormatEntities = _messageFormatRepository
                .GetMessageFormatsPages(request.Name, request.PageNumber, request.PageSize, request.OrderBy);

            return messageFormatEntities;
        }

        public MessageFormat UpdateMessageFormat(long Id, JsonPatchDocument<MessageFormat> patchDocument)
        {
            MessageFormat messageFormat;

            IntegrationService.Data.Entities.MessageFormat messageFormatEntity =
                _messageFormatRepository.GetMessageFormat(Id);         

            if (messageFormatEntity == null)
            {
                messageFormat = new MessageFormat();

                patchDocument.ApplyTo(messageFormat);

                messageFormatEntity = _mapper.Map<IntegrationService.Data.Entities.MessageFormat>(messageFormat);
                _messageFormatRepository.AddMessageFormat(messageFormatEntity);
            }
            else
            {
                messageFormat = _mapper.Map<MessageFormat>(messageFormatEntity);

                patchDocument.ApplyTo(messageFormat);

                _mapper.Map(messageFormat, messageFormatEntity);
                _messageFormatRepository.UpdateMessageFormat(messageFormatEntity);
            }

            _messageFormatRepository.Save();
            _mapper.Map(messageFormatEntity, messageFormat); //??

            return messageFormat;
        }
    }
}
