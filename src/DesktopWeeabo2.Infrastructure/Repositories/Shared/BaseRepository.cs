using DesktopWeeabo2.Core.Entities.Shared;
using DesktopWeeabo2.Core.Enums;
using DesktopWeeabo2.Core.Interfaces.Repositories.Shared;
using DesktopWeeabo2.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Infrastructure.Repositories.Shared {

	public abstract class BaseRepository<T> : IDefineRepositories<T> where T : BaseEntity {
		protected readonly EntriesContext context;

		public BaseRepository(EntriesContext db) {
			context = db;
		}

		public async Task<int> Add(T item) {
			context.Set<T>().Add(item);
			return await context.SaveChangesAsync();
		}

		public async Task<int> AddRange(IEnumerable<T> items) {
			var a = items.ToList();
			context.Set<T>().AddRange(a);
			return await context.SaveChangesAsync();
		}

		public async Task<int> UpdateRange(IEnumerable<T> dbItems, IEnumerable<T> items) {
			await DeleteRange(dbItems);
			return await AddRange(items);
		}

		public async Task<int> Update(T dbItem, T item) {
			await Delete(dbItem);
			return await Add(item);
		}

		public async Task<int> Delete(int id) {
			var item = Get(id);
			if (item == null)
				return 0;

			return await Delete(item);
		}

		public async Task<int> Delete(T item) {
			context.Set<T>().Remove(item);
			return await context.SaveChangesAsync();
		}

		public async Task<int> DeleteRange(IEnumerable<T> items) {
			context.Set<T>().RemoveRange(items);
			return await context.SaveChangesAsync();
		}

		public T Get(int id) =>
			 context.Set<T>().Where(q => q.Id == id).FirstOrDefault();

		public IEnumerable<T> Find(Func<T, bool> expression, bool isNoTracking = true) {
			if(isNoTracking) return context.Set<T>().AsNoTracking().Where(expression);
			return context.Set<T>().Where(expression);
		}

		public IEnumerable<T> Find(Func<T, bool> expression, Func<T, object> orderBy, bool isDescending = false, bool isNoTracking = true) =>
			isDescending
				? Find(expression, isNoTracking).OrderByDescending(orderBy)
				: Find(expression, isNoTracking).OrderBy(orderBy);

		public IEnumerable<T> GetAll() =>
			context.Set<T>();
	}
}