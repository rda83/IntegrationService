
using IntegrationService.Model;

namespace IntegrationService.Data.Entities
{
    public class MessageFormat
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public MessageFormatType FormatType { get; set; }
        public string Scheme { get; set; }
    }
}
