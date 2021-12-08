using System;

namespace IntegrationService.PropertyCheckerService
{
    public class PropertyCheckerService : IPropertyCheckerService
    {
        public bool TypeHasProperties<T>(string fieldsNames)
        {
            bool result = true;

            var fieldsArray = fieldsNames.Split(',');

            foreach (var fieldName in fieldsArray)
            {
                var current = fieldName.Trim();

                if(typeof(T).GetProperty(current, System.Reflection.BindingFlags.Public 
                    | System.Reflection.BindingFlags.IgnoreCase 
                    | System.Reflection.BindingFlags.Instance) == null)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }
}
