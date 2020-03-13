using Employee_LeaveManagement.Contracts;
using Employee_LeaveManagement.Data;
using Employee_LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Employee_LeaveManagement.Repository
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _context;

        public LeaveAllocationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public ICollection<LeaveAllocation> FindAll()
        {
            return _context.LeaveAllocations.ToList();
        }

        public LeaveAllocation FindById(Guid id)
        {
            return _context.LeaveAllocations
                .Include(x => x.LeaveType)
                .Include(x => x.Employee)
                .FirstOrDefault(x => x.Id == id);
        }

        public bool Create(LeaveAllocation entity)
        {
            _context.LeaveAllocations.Add(entity);
            return Save();
        }

        public bool Update(LeaveAllocation entity)
        {
            _context.LeaveAllocations.Update(entity);
            return Save();
        }

        public bool Delete(LeaveAllocation entity)
        {
            _context.LeaveAllocations.Remove(entity);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool CheckAllocation(Guid leaveTypeId, string employeeId)
        {
            var period = DateTime.Now.Year;
            return _context.LeaveAllocations.Any(x => x.EmployeeId == employeeId && x.LeaveTypeId == leaveTypeId && x.Period == period);
        }

        public ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(string id)
        {
            var datePeriod = DateTime.Now.Year;
            return _context.LeaveAllocations.Include(x => x.LeaveType).ToList().FindAll(x => x.EmployeeId == id && x.Period == datePeriod);
        }

        public Employee GetEmployeeById(string id)
        {
            return _context.Employees.Find(id);
        }

        public LeaveType GetLeaveTypeById(Guid id)
        {
            return _context.LeaveTypes.Find(id);
        }

        public LeaveAllocation GetLeaveAllocationByEmployeeAndLeaveType(string employeeId, Guid leaveTypeId)
        {
            var datePeriod = DateTime.Now.Year;
            return _context.LeaveAllocations.Include(x => x.LeaveType).ToList()
                .FirstOrDefault(x => x.EmployeeId == employeeId && x.LeaveTypeId == leaveTypeId && x.Period == datePeriod);
        }
    }
}
