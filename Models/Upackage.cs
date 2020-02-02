using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace IntegrationService.Models
{
    public class Upackage
    {
        public long Id { get; set; }

        [Required]
        public string IntegrationId { get; set; }

        [Required]
        public string SystemId { get; set; }

        [Required]
        public string Data { get; set; }

        [Required]
        public DateTime Date  { get; set; }

        public List<UpackageStatus> UpackageStatuses { get; set; }
    }
}