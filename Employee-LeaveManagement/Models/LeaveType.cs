using System;

namespace Employee_LeaveManagement.Models
{
    public class LeaveType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
