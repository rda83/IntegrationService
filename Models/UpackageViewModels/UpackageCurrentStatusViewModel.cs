using System;
using System.ComponentModel.DataAnnotations;

namespace IntegrationService.Models.UpackageViewModel
{
    public class UpackageCurrentStatusViewModel
    {
        [Required]
        public long UpackageId{ get; set; }
        
        [Required]
        public DateTime Date  { get; set; }

        [Required]
        public long StatusId { get; set; }

        [Required]
        public string Presentation { get; set; }

        [Required]
        public string Message { get; set; }

    }
}