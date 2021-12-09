using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationService.PropertyMappingService
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private readonly IList<IPropertyMapping> _propertyMappings;

        public PropertyMappingService(IList<IPropertyMapping> propertyMappings)
        {
            _propertyMappings = propertyMappings;
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {

            var matchingMapping = _propertyMappings
                .OfType<PropertyMapping<TSource, TDestination>>();

            var propertyMappings = matchingMapping.ToList();
            if (propertyMappings.Count() == 1)
            {
                return propertyMappings.First()._mappingDictionary;
            }

            throw new Exception($"Cannot find exact property mapping instance "
                + $"for <{typeof(TSource)},{typeof(TDestination)}");

        }

        public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
        {

            var propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var fieldsAfterSplit = fields.Split(',');

            foreach (var field in fieldsAfterSplit)
            {
                var trimmedField = field.Trim();

                var indexOfFirstSpace = trimmedField.IndexOf(" ", StringComparison.Ordinal);
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedField : trimmedField.Remove(indexOfFirstSpace);

                if (!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
