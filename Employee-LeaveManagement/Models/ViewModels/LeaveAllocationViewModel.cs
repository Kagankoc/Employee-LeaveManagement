using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Employee_LeaveManagement.Models.ViewModels
{
    public class LeaveAllocationViewModel
    {
        public Guid Id { get; set; }
        public DetailsLeaveTypeViewModel LeaveType { get; set; }
        [Required]

        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }
        public EmployeeViewModel Employee { get; set; }

        public IEnumerable<SelectListItem> Employees { get; set; }
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }

    }
}
