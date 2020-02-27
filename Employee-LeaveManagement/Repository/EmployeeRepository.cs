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

        public Employee FindById(Guid Id)
        {
            return _context.Employees.Find(Id);
        }

        public bool Create(Employee entity)
        {
            _context.Employees.Add(entity);

            return (_context.SaveChanges() > 0);
        }

        public bool Update(Employee entity)
        {
            _context.Employees.Update(entity);

            return (_context.SaveChanges() > 0);
        }

        public bool Delete(Employee entity)
        {
            _context.Employees.Remove(entity);

            return (_context.SaveChanges() > 0);
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
