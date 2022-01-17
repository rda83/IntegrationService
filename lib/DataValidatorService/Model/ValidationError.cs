
namespace IntegrationService.DataValidatorService.Model
{
    public class IntegrationServiceValidationError
    {
        public string Message { get; private set; }

        public IntegrationServiceValidationError(string msg)
        {
            Message = msg;
        }
    }
}
