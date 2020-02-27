using Employee_LeaveManagement.Contracts;
using Employee_LeaveManagement.Data;
using Employee_LeaveManagement.Models;
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

        public LeaveAllocation FindById(Guid Id)
        {
            return _context.LeaveAllocations.Find(Id);
        }

        public bool Create(LeaveAllocation entity)
        {
            _context.LeaveAllocations.Add(entity);
            return (_context.SaveChanges() > 0);
        }

        public bool Update(LeaveAllocation entity)
        {
            _context.LeaveAllocations.Update(entity);
            return (_context.SaveChanges() > 0);
        }

        public bool Delete(LeaveAllocation entity)
        {
            _context.LeaveAllocations.Remove(entity);
            return (_context.SaveChanges() > 0);
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
