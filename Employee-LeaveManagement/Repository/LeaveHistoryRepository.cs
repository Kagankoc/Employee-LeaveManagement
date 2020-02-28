using Employee_LeaveManagement.Contracts;
using Employee_LeaveManagement.Data;
using Employee_LeaveManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Employee_LeaveManagement.Repository
{
    public class LeaveHistoryRepository : ILeaveHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public LeaveHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public ICollection<LeaveHistory> FindAll()
        {
            return _context.LeaveHistories.ToList();
        }

        public LeaveHistory FindById(Guid id)
        {
            return _context.LeaveHistories.Find(id);
        }

        public bool Create(LeaveHistory entity)
        {
            _context.LeaveHistories.Add(entity);
            return Save();
        }

        public bool Update(LeaveHistory entity)
        {
            _context.LeaveHistories.Add(entity);
            return Save();
        }

        public bool Delete(LeaveHistory entity)
        {
            _context.LeaveHistories.Add(entity);
            return Save();
        }

        public bool Save()
        {
            return (_context.SaveChanges() > 0);
        }
    }
}
