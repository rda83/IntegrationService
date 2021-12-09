using IntegrationService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationService.Data.Services
{
    public interface IMessageFormatRepository
    {
        public MessageFormat GetMessageFormat(long Id);
        public IEnumerable<MessageFormat> GetMessageFormats();
        public IEnumerable<MessageFormat> GetMessageFormats(string name);
        public void AddMessageFormat(MessageFormat messageFormat);

        //public void MessageFormatAdd(MessageFormat messageFormat);

        //Создание объекта
        //Обновление объекта (обновление измененных, полное обновление, контроль версии объекта)
        //Получение объекта по идентификатору
        //Получение списка объектов (пагинация, условия, сортировка)

        public void BeginTransaction();
        public void CommitTransaction();
        public void RollbackTransaction();
        public bool Save();
    }
}
