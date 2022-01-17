
using IntegrationService.DataValidatorService;
using IntegrationService.DataValidatorService.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IntegrationService.Model
{
    public class MessageFormat : IValidatableObject
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
        public MessageFormatType FormatType { get; set; }
        public string Scheme { get; set; }

        #region IValidatableObject impl

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<IntegrationServiceValidationError>();

            switch (FormatType)
            {
                case MessageFormatType.JSON_DRAFT_4:

                    var service = validationContext.GetService(typeof(IJSONDataValidator)) as IJSONDataValidator;
                    errors = (List<IntegrationServiceValidationError>)service.ValidateMetaSchemaJSONDraft4(Scheme);
                    break;
                case MessageFormatType.PLAIN_TEXT:

                    if(!string.IsNullOrEmpty(Scheme))
                    {
                        errors.Add(
                            new IntegrationServiceValidationError("Для формата с типом: 'PLAIN_TEXT' - значение 'Scheme' не указывается."));
                    }
                    break;
                default:

                    errors.Add(
                            new IntegrationServiceValidationError("Неизвестный формат сообщения."));
                    break;
            }

            if (errors.Count > 0)
            {
                foreach (var item in errors)
                {
                    yield return new ValidationResult(item.Message, new[] { nameof(Scheme) });
                }
            }
        }

        #endregion
    }
}
