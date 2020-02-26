using System;

namespace Employee_LeaveManagement.Models
{
    public class LeaveHistory
    {
        public Guid Id { get; set; }
        public bool Approved { get; set; }
        public Guid ApprovedById { get; set; }
        public DateTime ActionDate { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveType LeaveType { get; set; }
        public Guid RequestingEmployeeId { get; set; }
        public DateTime StartDate { get; set; }
    }


}
