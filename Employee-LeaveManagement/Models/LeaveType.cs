using System;
using System.ComponentModel.DataAnnotations;

namespace Employee_LeaveManagement.Models
{
    public class LeaveType
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
