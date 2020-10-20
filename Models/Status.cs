using System.ComponentModel.DataAnnotations;

namespace IntegrationService.Models
{
    public class Status
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Presentation { get; set; }
    }
}