using IntegrationService.DataValidatorService.Model;
using System.Collections.Generic;

namespace IntegrationService.DataValidatorService
{
    public interface IJSONDataValidator
    {
        IEnumerable<IntegrationServiceValidationError> ValidateMetaSchemaJSONDraft4(string jsonData);
    }
}
