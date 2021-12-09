using IntegrationService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationService.Data.Services
{
    public class MessageFormatRepository : CommonRepository, IMessageFormatRepository
    {
        public MessageFormatRepository(DBContext context):base(context) { }

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

        public IEnumerable<MessageFormat> GetMessageFormats()
        {
            IEnumerable<MessageFormat> result = _context.MessageFormats.ToList<MessageFormat>();
            return result;
        }

        public IEnumerable<MessageFormat> GetMessageFormats(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return GetMessageFormats();
            }

            var messageFormats = _context.MessageFormats.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                messageFormats = messageFormats.Where(i => i.Name == name);
            }

            IEnumerable<MessageFormat> result = messageFormats
                .ToList<MessageFormat>();

            return result;
        }
    }
}
