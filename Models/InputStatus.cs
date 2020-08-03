using System.ComponentModel.DataAnnotations;
using System;

namespace IntegrationService.Models
{
    public class InputStatus
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public long StatusId { get; set; }

        [Required]
        public long UpackageId { get; set; }

        [Required]
        public string Message { get; set; }
    }
}