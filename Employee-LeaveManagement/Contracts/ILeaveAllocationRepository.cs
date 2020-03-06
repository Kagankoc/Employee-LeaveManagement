using Employee_LeaveManagement.Models;
using System;
using System.Collections.Generic;

namespace Employee_LeaveManagement.Contracts
{
    public interface ILeaveAllocationRepository : IRepositoryBase<LeaveAllocation>
    {
        bool CheckAllocation(Guid leaveTypeId, string employeeId);
        ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(string id);
        public Employee GetEmployeeById(string id);
        public LeaveType GetLeaveTypeById(Guid id);
    }
}
