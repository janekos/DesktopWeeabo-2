using DesktopWeeabo2.Helpers;
using DesktopWeeabo2.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Data.Repositories.Shared {
    public interface IRepo<T> where T : BaseModel{
		void Add(T item);
		void Update(T item);
		void Delete(int id);
        Task<T> Get(int id);
		IEnumerable<T> FindSet(Func<T, bool> expression, Func<T, object> orderBy, bool isDescending);
    }
}
