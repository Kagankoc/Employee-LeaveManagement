using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Employee_LeaveManagement.Models.ViewModels
{
    public class LeaveRequestViewModel
    {
        public Guid Id { get; set; }

        public EmployeeViewModel RequestingEmployee { get; set; }
        public string RequestingEmployeeId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }


        public LeaveTypeViewModel LeaveType { get; set; }
        public Guid LeaveTypeId { get; set; }

        public DateTime RequestDate { get; set; }
        public DateTime ActionDate { get; set; }
        public bool? Approved { get; set; }



        public EmployeeViewModel ApprovedBy { get; set; }



    }
    public class EmployeeLeaveRequestViewModel
    {
        public IEnumerable<LeaveRequest> leaveRequests { get; set; }
        public IEnumerable<LeaveAllocation> leaveAllocations { get; set; }
    }

    public class AdminLeaveRequestViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Total Requests")]
        public int TotalRequests { get; set; }
        [Display(Name = "Approved Requests")]
        public int ApprovedRequests { get; set; }
        [Display(Name = "Pending Requests")]
        public int PendingRequests { get; set; }
        [Display(Name = "Rejected Requests")]
        public int RejectedRequest { get; set; }

        public List<LeaveRequestViewModel> LeaveRequests { get; set; }

    }

    public class CreateLeaveRequestViewModel
    {
        [Display(Name = "Start Date")]
        [Required]

        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [Required]

        public DateTime EndDate { get; set; }
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }
        [Display(Name = "Leave Type")]
        public Guid LeaveTypeId { get; set; }

        public IEnumerable<SelectListItem> Employees { get; set; }
        [Display(Name = "Employee")]
        public string EmployeeId { get; set; }
    }
}
