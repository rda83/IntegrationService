using IntegrationService.DataValidatorService.Model;
using NJsonSchema;
using NJsonSchema.Validation;
using System.Collections.Generic;

namespace IntegrationService.DataValidatorService
{
    public class JSONSchemaValidator : IJSONDataValidator
    {

        private readonly JsonSchema _metaSchemaDraft4;
        private readonly JsonSchemaValidator schemaValidator;

        public JSONSchemaValidator()
        {
            _metaSchemaDraft4 = JsonSchema.FromJsonAsync(JSONSchemaDrafts.Draft4).Result;
            schemaValidator = new JsonSchemaValidator();
        }

        public IEnumerable<IntegrationServiceValidationError> ValidateMetaSchemaJSONDraft4(string jsonData)
        {
            var result = new List<IntegrationServiceValidationError>();

            try
            {
                var validatorResult = schemaValidator.Validate(jsonData, _metaSchemaDraft4);
                foreach (var item in validatorResult)
                {
                    var msg =
                         $"Kind: {item.Kind}, Property: {item.Property}, Path: {item.Path}, LineNumber: {item.LineNumber}, LinePosition: {item.LinePosition}";

                    result.Add(new IntegrationServiceValidationError(msg));
                }
            }
            catch (System.Exception e)
            {
                result.Add(new IntegrationServiceValidationError(e.Message));
            }
            return result;
        }
    }
}
