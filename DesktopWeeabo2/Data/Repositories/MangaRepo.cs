﻿using DesktopWeeabo2.Data.Repositories.Shared;
using DesktopWeeabo2.Helpers.Enums;
using DesktopWeeabo2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Data.Repositories
{
    class MangaRepo: IRepo<MangaModel>, IDisposable {

		private readonly EntriesContext _db = new EntriesContext();

		public Task<RepoResponse> AddOrUpdate(MangaModel item) {
			throw new NotImplementedException();
		}

		public Task<RepoResponse> Delete(int id) {
			throw new NotImplementedException();
		}

		public void Dispose() {
			throw new NotImplementedException();
		}

		public IEnumerable<MangaModel> FindEnumerable(Func<MangaModel, bool> expression) {
			throw new NotImplementedException();
		}

		public Task<MangaModel> Get(int id) {
			throw new NotImplementedException();
		}

		public Task<MangaModel> Get(MangaModel entity) {
			throw new NotImplementedException();
		}
	}
}
