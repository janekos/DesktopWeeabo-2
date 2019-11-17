using DesktopWeeabo2.Core.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Core.Interfaces.Repositories.Shared {

	public interface IDefineRepositories<T> where T : BaseEntity {

		Task<int> Add(T item);

		Task<int> AddRange(IEnumerable<T> items);

		Task<int> Update(T dbItem, T item);

		Task<int> UpdateRange(IEnumerable<T> dbItems, IEnumerable<T> items);

		Task<int> Delete(int id);

		Task<int> Delete(T item);

		Task<int> DeleteRange(IEnumerable<T> items);

		T Get(int id);
		IEnumerable<T> GetAll();

		IEnumerable<T> Find(Func<T, bool> expression, bool isNoTracking = true);

		IEnumerable<T> Find(Func<T, bool> expression, Func<T, object> orderBy, bool isDescending = false, bool isNoTracking = true);
	}
}