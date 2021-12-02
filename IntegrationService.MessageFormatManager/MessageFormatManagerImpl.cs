using AutoMapper;
using IntegrationService.Model;
using IntegrationService.Data.Services;
using System;
using System.Collections.Generic;

namespace IntegrationService.MessageFormatManager
{
    public class MessageFormatManagerImpl : IMessageFormatManager
    {
        private readonly IMapper _mapper;
        private IMessageFormatRepository _messageFormatRepository;

        public MessageFormatManagerImpl(IMessageFormatRepository messageFormatRepository,
            IMapper mapper)
        {
            _messageFormatRepository = messageFormatRepository ?? 
                throw new ArgumentNullException(nameof(messageFormatRepository));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

        public IEnumerable<MessageFormat> GetMessageFormats(string name, string searchQuery)
        {
            var messageFormatEntities = _messageFormatRepository.GetMessageFormats();

            var result = _mapper.Map<IEnumerable<MessageFormat>>(messageFormatEntities);
            return result;
        }
    }
}
