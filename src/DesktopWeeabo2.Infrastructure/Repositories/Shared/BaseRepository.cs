using DesktopWeeabo2.Core.Interfaces.Repositories.Shared;
using DesktopWeeabo2.Core.Models.Shared;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DesktopWeeabo2.Infrastructure.Repositories.Shared {

	public abstract class BaseRepository<T> : IDefineRepositories<T> where T : BaseModel {
		protected virtual LiteCollection<T> collection { get; }

		protected BaseRepository() {
		}

		public int Add(T item) =>
			collection.Insert(item);

		public int AddRange(IEnumerable<T> items) =>
			collection.InsertBulk(items);

		public int UpdateRange(IEnumerable<T> items) =>
			collection.Update(items);

		public int Update(T item) =>
			collection.Update(item) ? 1 : 0;

		public int Upsert(T item) =>
			collection.Upsert(item) ? 1 : 0;

		public int UpsertRange(IEnumerable<T> items) =>
			collection.Upsert(items);

		public int Delete(int id) =>
			collection.Delete(id) ? 1 : 0;

		public int Delete(T item) =>
			Delete(item.Id);

		public int DeleteRange(IEnumerable<T> items) {
			var ids = items.Select(item => item.Id);
			return collection.Delete(q => ids.Contains(q.Id));
		}

		public T Get(int id) =>
			 collection.FindById(id);

		public IEnumerable<T> Find(Expression<Func<T, bool>> expression) =>
			collection.Find(expression);

		public IEnumerable<T> Find(Expression<Func<T, bool>> expression, Func<T, object> orderBy, bool isDescending = false) =>
			isDescending
				? Find(expression).OrderByDescending(orderBy)
				: Find(expression).OrderBy(orderBy);

		public IEnumerable<T> GetAll() =>
			collection.FindAll();
	}
}