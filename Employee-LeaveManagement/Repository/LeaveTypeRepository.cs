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

        public LeaveType FindById(Guid Id)
        {
            return _context.LeaveTypes.Find(Id);
        }

        public bool Create(LeaveType entity)
        {
            _context.LeaveTypes.Add(entity);
            return _context.SaveChanges() > 0;
        }

        public bool Update(LeaveType entity)
        {
            _context.LeaveTypes.Update(entity);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(LeaveType entity)
        {
            _context.LeaveTypes.Remove(entity);
            return _context.SaveChanges() > 0;
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public ICollection<Employee> GetEmployeesByLeaveType(Guid Id)
        {
            var allocations = _context.LeaveAllocations.Where(x => x.LeaveType.Id == Id).ToList();

            return allocations.Select(item => item.Employee).ToList();
        }
    }
}
