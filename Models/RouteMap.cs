using System.ComponentModel.DataAnnotations;

namespace IntegrationService.Models
{
    public class RouteMap
    {
        [Required]
        public string IntegrationId { get; set; }

        [Required]
        public string SystemId { get; set; }
    }
}