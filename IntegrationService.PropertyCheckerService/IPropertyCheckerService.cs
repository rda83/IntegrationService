
namespace IntegrationService.PropertyCheckerService
{
    public interface IPropertyCheckerService
    {
        public bool TypeHasProperties<T>(string fieldsNames);
    }
}
