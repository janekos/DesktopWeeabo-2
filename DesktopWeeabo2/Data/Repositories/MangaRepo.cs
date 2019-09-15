using DesktopWeeabo2.Data.Repositories.Shared;
using DesktopWeeabo2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Data.Repositories {
	class MangaRepo : IRepo<MangaModel> {
		private readonly EntriesContext _db;

		public MangaRepo(EntriesContext db) {
			_db = db;
		}

		public async void Add(MangaModel item) {
			_db.MangaItems.Add(item);
			await _db.SaveChangesAsync();
		}

		public async void Delete(int id) {
			var item = await Get(id);
			if (item == null) return;

			_db.MangaItems.Remove(item);
			await _db.SaveChangesAsync();
		}

		public IEnumerable<MangaModel> FindSet(Func<MangaModel, bool> expression, Func<MangaModel, object> orderBy, bool isDescending = false) =>
			isDescending
				? _db.MangaItems.Where(expression).OrderByDescending(orderBy)
				: _db.MangaItems.Where(expression).OrderBy(orderBy);

		public async Task<MangaModel> Get(int id) => await _db.MangaItems.FindAsync((int)id);

		public async void Update(MangaModel dbItem, MangaModel newItem) {
			_db.Entry(dbItem).CurrentValues.SetValues(newItem);
			await _db.SaveChangesAsync();
		}
	}
}
