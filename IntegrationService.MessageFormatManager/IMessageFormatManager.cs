using IntegrationService.Helpers;
using IntegrationService.Model;
using IntegrationService.ResourceParameters;
using Microsoft.AspNetCore.JsonPatch;

namespace IntegrationService.MessageFormatManager
{
    public interface IMessageFormatManager
    {
        public MessageFormat GetMessageFormat(long Id);
        public void AddMessageFormat(MessageFormat messageFormat);
        public PageList<Data.Entities.MessageFormat> GetMessageFormats(SimpleObjectResourceParameter request);
        public MessageFormat UpdateMessageFormat(long Id, JsonPatchDocument<MessageFormat> patchDocument);
        public void UpdateMessageFormat(long Id, MessageFormat messageFormat);
    }
}
