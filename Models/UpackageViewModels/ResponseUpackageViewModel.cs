using System;
using System.ComponentModel.DataAnnotations;

namespace IntegrationService.Models.UpackageViewModel
{
    public class ResponseUpackageViewModel
    {
        [Required]
        public long RequestId { get; set; }
    }
}