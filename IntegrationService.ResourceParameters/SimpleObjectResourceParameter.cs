
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

        #region ShapeData

        public string Fields { get; set; }

        #endregion

        public override object GetRouteObject(ResourceUriType uriType)
        {
            switch (uriType)
            {
                case ResourceUriType.PreviousPage:
                    return new
                    {
                        fields = Fields,
                        orderBy = OrderBy,
                        pageNumber = PageNumber - 1,
                        pageSize = PageSize,
                        Name
                    };
                case ResourceUriType.NextPage:
                    return new
                    {
                        orderBy = OrderBy,
                        pageNumber = PageNumber - 1,
                        pageSize = PageSize,
                        Name
                    };
                default:
                    return new
                    {
                        orderBy = OrderBy,
                        pageNumber = PageNumber,
                        pageSize = PageSize,
                        Name
                    };
            }
        }

    }
}
