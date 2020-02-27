using System;
using System.Collections.Generic;

namespace Employee_LeaveManagement.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        ICollection<T> FindAll();
        T FindById(Guid Id);
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool Save();

    }
}
