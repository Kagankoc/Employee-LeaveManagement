﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Employee_LeaveManagement.Models.ViewModels
{
    public class LeaveHistoryViewModel
    {
        public Guid Id { get; set; }

        public EmployeeViewModel RequestingEmployee { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }


        public DetailsLeaveTypeViewModel LeaveType { get; set; }
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ActionDate { get; set; }
        public bool? Approved { get; set; }



        public EmployeeViewModel ApprovedBy { get; set; }



    }
}
