
namespace IntegrationService.ResourceParameters
{
    /// <summary>
    /// Параметры ресурсов для простого объекта
    /// </summary>
    public class SimpleObjectResourceParameter : CommonResourceParameter
    {
        #region FilteringFields

        public string Name { get; set; }

        #endregion

        #region Pagination
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > DefaultSettings.GetMaxPageSize()) ? DefaultSettings.GetMaxPageSize() : value;
        }
        #endregion

        /// <summary>
        /// Поля для сортировки
        /// Для сортировки по убыванию необходимо использовать ключевое слово DESC
        /// </summary>
        public string OrderBy { get; set; } = "Name";
    }
}
