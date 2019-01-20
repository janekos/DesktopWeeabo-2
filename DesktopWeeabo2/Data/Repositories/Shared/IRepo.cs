using DesktopWeeabo2.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Data.Repositories.Shared {
    public interface IRepo<T> where T : BaseModel{
        void Add(T item);
        void Delete(int id);
        void Update(T item);
        Task<T> Get(int id);
		Task<T> Get(T entity);
		IEnumerable<T> FindEnumerable(Func<T, bool> expression);
    }
}
