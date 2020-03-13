using System;

namespace Employee_LeaveManagement.Models
{
    public class LeaveRequest
    {
        public Guid Id { get; set; }
        public bool? Approved { get; set; }
        public Employee RequestingEmployee { get; set; }
        public Employee ApprovedBy { get; set; }
        public DateTime ActionDate { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveType LeaveType { get; set; }
        public DateTime StartDate { get; set; }

        public bool IsDeleted { get; set; }
    }


}
