using System;
using System.ComponentModel.DataAnnotations;

namespace Employee_LeaveManagement.Models.ViewModels
{
    public class LeaveTypeViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Date Created")]
        public DateTime? DateCreated { get; set; }
    }
}
