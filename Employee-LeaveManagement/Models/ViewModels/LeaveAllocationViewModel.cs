using System;
using System.Collections.Generic;

namespace Employee_LeaveManagement.Models.ViewModels
{
    public class LeaveAllocationViewModel
    {
        public Guid Id { get; set; }
        public LeaveTypeViewModel LeaveType { get; set; }
        public Guid LeaveTypeViewModelId { get; set; }
        public int Period { get; set; }

        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }
        public EmployeeViewModel Employee { get; set; }
        public string EmployeeViewModelId { get; set; }



    }

    public class CreateLeaveAllocationViewModel
    {
        public int? NumberUpdated { get; set; }
        public List<LeaveTypeViewModel> LeaveTypes { get; set; }
    }
    public class EditLeaveAllocationViewModel
    {
        public Guid Id { get; set; }
        public EmployeeViewModel Employee { get; set; }
        public string EmployeeId { get; set; }

        public int NumberOfDays { get; set; }
        public LeaveTypeViewModel LeaveType { get; set; }
        public Guid LeaveTypeId { get; set; }
    }

    public class ViewAllocationsViewModel
    {
        public EmployeeViewModel Employee { get; set; }
        public string EmployeeId { get; set; }
        public List<LeaveAllocationViewModel> LeaveAllocations { get; set; }

    }
}
