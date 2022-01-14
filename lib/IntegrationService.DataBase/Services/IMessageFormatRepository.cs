using IntegrationService.Data.Entities;
using IntegrationService.Helpers;
using System.Collections.Generic;

namespace IntegrationService.Data.Services
{
    public interface IMessageFormatRepository
    {
        public PageList<MessageFormat> GetMessageFormatsPages(string name, int pageNumber, int pageSize, string orderBy);
        public IEnumerable<MessageFormat> GetMessageFormats(string name, string orderBy);
        public MessageFormat GetMessageFormat(long Id);
        void UpdateMessageFormat(MessageFormat messageFormat);
        public void AddMessageFormat(MessageFormat messageFormat);
        public void DeleteMessageFormat(long Id);

        public void BeginTransaction();
        public void CommitTransaction();
        public void RollbackTransaction();
        public bool Save();
    }
}
