using System;
using System.ComponentModel.DataAnnotations;

namespace IntegrationService.Models
{
    public class UpackageStatus
    {
        public long Id { get; set; }

        [Required]
        public DateTime Date  { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public Status Status{ get; set; }

        [Required]
        public Upackage Upackage{ get; set; }

        [Required]
        public long UpackageId{ get; set; }

        public UpackageStatus(){}

        public UpackageStatus(DateTime date, Status status, Upackage upackage, string message)
        {
            Date = date;
            Status = status;
            Upackage = upackage;
            Message = message;
        }
    }
}