using System;
using System.ComponentModel.DataAnnotations;

namespace IntegrationService.Models.UpackageViewModel
{
    public class CreateUpackageViewModel
    {
		    public long Id { get; set; }

            [Required]
			public string IntegrationId { get; set; }

            [Required]
			public string Data { get; set; }
    }
}