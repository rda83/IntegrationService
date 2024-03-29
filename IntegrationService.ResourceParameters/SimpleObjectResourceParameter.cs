﻿
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

        public override RouteObject GetRouteObject(ResourceUriType uriType)
        {
            switch (uriType)
            {
                case ResourceUriType.PreviousPage:
                    return new RouteObject()
                    {
                        Fields = Fields,
                        OrderBy = OrderBy,
                        PageNumber = PageNumber - 1,
                        PageSize = PageSize
                    };
                case ResourceUriType.NextPage:
                    return new RouteObject()
                    {
                        Fields = Fields,
                        OrderBy = OrderBy,
                        PageNumber = PageNumber + 1,
                        PageSize = PageSize,
                    };
                default:
                    return new RouteObject()
                    {
                        OrderBy = OrderBy,
                        PageNumber = PageNumber,
                        PageSize = PageSize,
                    };
            }
        }
    }
}
