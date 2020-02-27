using System;

namespace Employee_LeaveManagement.Models
{
    public class LeaveAllocation
    {
        public Guid Id { get; set; }
        public LeaveType LeaveType { get; set; }
        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }
        public Employee Employee { get; set; }
    }
}
