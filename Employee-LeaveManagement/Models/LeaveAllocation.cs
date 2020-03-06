using System;

namespace Employee_LeaveManagement.Models
{
    public class LeaveAllocation
    {
        public Guid Id { get; set; }
        public LeaveType LeaveType { get; set; }
        public Guid LeaveTypeId { get; set; }
        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }
        public Employee Employee { get; set; }
        public string EmployeeId { get; set; }
        public int Period { get; set; }
    }
}
