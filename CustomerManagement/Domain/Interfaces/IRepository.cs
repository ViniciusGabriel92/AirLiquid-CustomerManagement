using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerManagement.Domain.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAll();
        Task<bool> Insert(T item);
        Task<bool> Update(T item);
        Task<bool> Delete(Guid id);
    }
}