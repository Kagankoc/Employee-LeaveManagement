using System;
using System.ComponentModel.DataAnnotations;

namespace Employee_LeaveManagement.Models.ViewModels
{
    public class CreateLeaveTypeViewModel
    {
        [Required]
        public string Name { get; set; }

    }

    public class DetailsLeaveTypeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
