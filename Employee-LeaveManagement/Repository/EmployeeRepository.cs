using Employee_LeaveManagement.Contracts;
using Employee_LeaveManagement.Data;
using Employee_LeaveManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Employee_LeaveManagement.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public ICollection<Employee> FindAll()
        {
            return _context.Employees.ToList();
        }

        public Employee FindById(Guid id)
        {
            return _context.Employees.FirstOrDefault(x => x.Id == id.ToString());
        }

        public bool Create(Employee entity)
        {
            _context.Employees.Add(entity);

            return Save();
        }

        public bool Update(Employee entity)
        {
            _context.Employees.Update(entity);

            return Save();
        }

        public bool Delete(Employee entity)
        {
            _context.Employees.Remove(entity);

            return Save();
        }

        public bool Save()
        {
            return (_context.SaveChanges() > 0);
        }
    }
}
