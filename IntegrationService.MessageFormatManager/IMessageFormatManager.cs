using IntegrationService.Model;
using IntegrationService.ResourceParameters;
using System.Collections.Generic;
using System.Dynamic;

namespace IntegrationService.MessageFormatManager
{
    public interface IMessageFormatManager
    {
        public MessageFormat GetMessageFormat(long Id);
        public void AddMessageFormat(MessageFormat messageFormat);
        public IEnumerable<ExpandoObject> GetMessageFormats(SimpleObjectResourceParameter request);
    }
}
