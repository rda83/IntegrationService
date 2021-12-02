using IntegrationService.Model;
using System.Collections.Generic;

namespace IntegrationService.MessageFormatManager
{
    public interface IMessageFormatManager
    {
        public MessageFormat GetMessageFormat(long Id);
        public void AddMessageFormat(MessageFormat messageFormat);
        public IEnumerable<MessageFormat> GetMessageFormats(string name, string searchQuery);
    }
}
