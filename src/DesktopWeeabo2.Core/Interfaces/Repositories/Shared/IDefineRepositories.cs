using DesktopWeeabo2.Core.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DesktopWeeabo2.Core.Interfaces.Repositories.Shared {

	public interface IDefineRepositories<T> where T : BaseModel {

		int Add(T item);

		int AddRange(IEnumerable<T> items);

		int Update(T item);

		int UpdateRange(IEnumerable<T> items);

		int Upsert(T item);

		int UpsertRange(IEnumerable<T> items);

		int Delete(int id);

		int Delete(T item);

		int DeleteRange(IEnumerable<T> items);

		T Get(int id);

		IEnumerable<T> GetAll();

		IEnumerable<T> Find(Expression<Func<T, bool>> expression);

		IEnumerable<T> Find(Expression<Func<T, bool>> expression, Func<T, object> orderBy, bool isDescending = false);
	}
}