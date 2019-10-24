using DesktopWeeabo2.Core.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Core.Interfaces.Repositories.Shared {
	public interface IDefineRepositories<T> where T : BaseEntity {
		Task<int> Add(T item);
		Task<int> Update(T item);
		Task<int> AddRange(IEnumerable<T> items);
		Task<int> Delete(int id);
		Task<T> Get(int id);
		IEnumerable<T> Find(Func<T, bool> expression);
		IEnumerable<T> Find(Func<T, bool> expression, Func<T, object> orderBy, bool isDescending = false);
	}
}
