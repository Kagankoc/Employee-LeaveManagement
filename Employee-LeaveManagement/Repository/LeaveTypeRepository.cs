using Employee_LeaveManagement.Contracts;
using Employee_LeaveManagement.Data;
using Employee_LeaveManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Employee_LeaveManagement.Repository
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public LeaveTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public ICollection<LeaveType> FindAll()
        {
            return _context.LeaveTypes.ToList();
        }

        public LeaveType FindById(Guid id)
        {
            return _context.LeaveTypes.Find(id);
        }

        public bool Create(LeaveType entity)
        {
            _context.LeaveTypes.Add(entity);
            return Save();
        }

        public bool Update(LeaveType entity)
        {
            _context.LeaveTypes.Update(entity);
            return Save();
        }

        public bool Delete(LeaveType entity)
        {
            _context.LeaveTypes.Remove(entity);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public ICollection<Employee> GetEmployeesByLeaveType(Guid id)
        {
            var allocations = _context.LeaveAllocations.Where(x => x.LeaveType.Id == id).ToList();

            return allocations.Select(item => item.Employee).ToList();
        }
    }
}
