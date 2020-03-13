using Employee_LeaveManagement.Contracts;
using Employee_LeaveManagement.Data;
using Employee_LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Employee_LeaveManagement.Repository
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public LeaveRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public ICollection<LeaveRequest> FindAll()
        {
            return _context.LeaveRequests
                .Include(x => x.RequestingEmployee)
                .Include(x => x.ApprovedBy)
                .Include(x => x.LeaveType)
                .Where(x => x.IsDeleted != true)
                .ToList();
        }

        public LeaveRequest FindById(Guid id)
        {
            return FindAll()
                .FirstOrDefault(x => x.Id == id);
        }

        public bool Create(LeaveRequest entity)
        {
            _context.LeaveRequests.Add(entity);
            return Save();
        }

        public bool Update(LeaveRequest entity)
        {
            _context.LeaveRequests.Update(entity);
            return Save();
        }

        public bool Delete(LeaveRequest entity)
        {
            _context.LeaveRequests.Remove(entity);
            return Save();
        }

        public bool Save()
        {
            return (_context.SaveChanges() > 0);
        }
    }
}
