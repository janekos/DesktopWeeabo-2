using DesktopWeeabo2.Core.Entities.Shared;
using DesktopWeeabo2.Core.Interfaces.Repositories.Shared;
using DesktopWeeabo2.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Infrastructure.Repositories.Shared {
	public abstract class BaseRepository<T> : IDefineRepositories<T> where T : BaseEntity {

		private readonly EntriesContext _db;

		public BaseRepository(EntriesContext db) {
			_db = db;
		}

		public async Task<int> Add(T item) {
			_db.Set<T>().Add(item);
			return await _db.SaveChangesAsync();
		}

		public async Task<int> AddRange(IEnumerable<T> items) {
			_db.Set<T>().AddRange(items);
			return await _db.SaveChangesAsync();
		}

		public async Task<int> Update(T item) {
			_db.Entry(item).CurrentValues.SetValues(item);
			return await _db.SaveChangesAsync();
		}

		public async Task<int> Delete(int id) {
			var item = await Get(id);
			if (item == null) return 0;

			_db.Set<T>().Remove(item);
			return await _db.SaveChangesAsync();
		}

		public async Task<T> Get(int id) =>
			 await _db.Set<T>().FindAsync((int)id);


		public IEnumerable<T> Find(Func<T, bool> expression) {

			return _db.Set<T>().Where(expression);
		}

		public IEnumerable<T> Find(Func<T, bool> expression, Func<T, object> orderBy, bool isDescending = false) =>
			isDescending
				? _db.Set<T>().Where(expression).OrderByDescending(orderBy)
				: _db.Set<T>().Where(expression).OrderBy(orderBy);
	}
}
