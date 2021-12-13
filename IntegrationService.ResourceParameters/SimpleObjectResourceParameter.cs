
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

        #endregion

        /// <summary>
        /// Поля для сортировки
        /// Для сортировки по убыванию необходимо использовать ключевое слово DESC
        /// </summary>
        public string OrderBy { get; set; } = "Name";
    }
}
