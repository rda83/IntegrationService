using IntegrationService.Model;
using IntegrationService.PropertyMappingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationService.Application.PropertyMapping
{
    public class PropertyMappingServiceBuilder
    {
        private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        private PropertyMappingServiceBuilder()
        {
        }

        public static PropertyMappingServiceBuilder Create()
        {
            return new PropertyMappingServiceBuilder();
        }

        public PropertyMappingServiceBuilder AddAllMappings()
        {
            AddMessageFormatMapping();
            return this;
        }

        public PropertyMappingService.PropertyMappingService Build()
        {
            return new PropertyMappingService.PropertyMappingService(_propertyMappings);
        }

        public PropertyMappingServiceBuilder AddMessageFormatMapping()
        {
            var messageFormatsPropertyMapping =
                new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
                {
                                { "Id", new PropertyMappingValue(new List<string>() { "Id" })},
                                { "Name", new PropertyMappingValue(new List<string>() { "Name" })},
                };
            _propertyMappings.Add(new PropertyMapping<MessageFormat, Data.Entities.MessageFormat>(messageFormatsPropertyMapping));

            return this;
        }
    }
}
