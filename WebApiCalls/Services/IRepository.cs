using System.Collections.Generic;
using System.Linq;

namespace WebApiCalls.Services
{
    public interface IRepository<T, TId>
    {
        IEnumerable<T> GetAll();

        T GetById(TId id);

        int Create(T data);

        void Update(T data);

        void Delete(TId id);
    }
}
