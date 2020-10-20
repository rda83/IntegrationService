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

        public UpackageCurrentStatusViewModel(){}
        public UpackageCurrentStatusViewModel(long id, DateTime date, long statusId, string presentation, string message)
        {    
            UpackageId = id;
            Date = date;
            StatusId = statusId;
            Presentation = presentation;
            Message = message; 
        }
    }
}