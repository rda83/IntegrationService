using IntegrationService.Data.Entities;
using System.Collections.Generic;

namespace IntegrationService.Data.Services
{
    public interface IMessageFormatRepository
    {
        public IEnumerable<MessageFormat> GetMessageFormatsPages(string name, int pageNumber, int pageSize, string orderBy);
        public IEnumerable<MessageFormat> GetMessageFormats(string name, string orderBy);
        public MessageFormat GetMessageFormat(long Id);

        // bool MessageFormatExist(long Id);
        //void BatchInsertOrUpdateMessageFormats(List<MessageFormat> MessageFormats); // POST и PUT 

        public void AddMessageFormat(MessageFormat messageFormat);

        

        public void BeginTransaction();
        public void CommitTransaction();
        public void RollbackTransaction();
        public bool Save();
    }
}
