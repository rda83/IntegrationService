using IntegrationService.Data.Entities;
using IntegrationService.Data.Helpers;
using IntegrationService.Helpers;
using IntegrationService.PropertyMappingService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationService.Data.Services
{
    public class MessageFormatRepository : CommonRepository, IMessageFormatRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;

        public MessageFormatRepository(DBContext context, 
            IPropertyMappingService propertyMappingService) :base(context)
        {
            _propertyMappingService = propertyMappingService ??
                            throw new ArgumentNullException(nameof(propertyMappingService));
        }

        public MessageFormat GetMessageFormat(long Id)
        {
            MessageFormat result = _context.MessageFormats.FirstOrDefault(i => i.Id == Id);
            return result;
        }

        public void AddMessageFormat(MessageFormat messageFormat)
        {
            if (messageFormat == null)
            {
                throw new ArgumentNullException(nameof(messageFormat));
            }
            _context.MessageFormats.Add(messageFormat);
        }

        public PageList<MessageFormat> GetMessageFormatsPages(string name, int pageNumber, int pageSize, string orderBy)
        {
            var result = GetMessageFormatsAsQueryable(name, orderBy);
            return PageList<MessageFormat>.Create(result, pageNumber, pageSize);
        }

        public IEnumerable<MessageFormat> GetMessageFormats(string name, string orderBy)
        {
            var result = GetMessageFormatsAsQueryable(name, orderBy);
            return result;
        }

        #region Service

        private IQueryable<MessageFormat> GetMessageFormatsAsQueryable(string name, string orderBy)
        {
            var messageFormats = _context.MessageFormats.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                messageFormats = messageFormats.Where(i => i.Name == name);
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                var messageFormatMappingDictionary = _propertyMappingService.GetPropertyMapping<IntegrationService.Model.MessageFormat, MessageFormat>();
                messageFormats = messageFormats.ApplySort(orderBy, messageFormatMappingDictionary);
            }
            else
            {
                messageFormats = messageFormats.OrderBy(t => t.Name);
            }

            return messageFormats;
        }

        #endregion    
    }
}
