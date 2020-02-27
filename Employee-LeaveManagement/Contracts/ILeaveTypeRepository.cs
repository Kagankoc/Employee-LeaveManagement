using Employee_LeaveManagement.Models;
using System;
using System.Collections.Generic;

namespace Employee_LeaveManagement.Contracts
{
    public interface ILeaveTypeRepository : IRepositoryBase<LeaveType>
    {
        ICollection<Employee> GetEmployeesByLeaveType(Guid Id);
    }
}
