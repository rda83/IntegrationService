using IntegrationService.Helpers;
using IntegrationService.Model;
using IntegrationService.ResourceParameters;

namespace IntegrationService.MessageFormatManager
{
    public interface IMessageFormatManager
    {
        public MessageFormat GetMessageFormat(long Id);
        public void AddMessageFormat(MessageFormat messageFormat);
        public PageList<Data.Entities.MessageFormat> GetMessageFormats(SimpleObjectResourceParameter request);
    }
}
