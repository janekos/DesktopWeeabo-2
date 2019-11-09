using DesktopWeeabo2.Core.Entities.Shared;
using DesktopWeeabo2.Core.Enums;
using DesktopWeeabo2.Core.Interfaces.Repositories.Shared;
using DesktopWeeabo2.Core.Interfaces.Services.Shared;
using DesktopWeeabo2.Core.Models;
using DesktopWeeabo2.Core.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Infrastructure.Services.Shared {

	public abstract class BaseService<T, U> : IDefineServices<T, U> where T : BaseModel where U : BaseEntity {
		private readonly IDefineRepositories<U> repo;

		public BaseService(IDefineRepositories<U> repo) {
			this.repo = repo;
		}

		public async Task<DBResponse> AddOrUpdate(T model) {
			var entity = await repo.Get(model.Id);

			if (entity == null) {
				await repo.Add((U) Cast(model));
				return DBResponse.ADDED;
			}
			if (entity != null) {
				await repo.Update(entity, (U) Cast(model));
				return DBResponse.UPDATED;
			}

			return DBResponse.NOCHANGES;
		}

		public async Task<DBResponse> AddOrUpdateRange(IEnumerable<T> entities) {
			var allIds = entities.Select(e => e.Id);
			var existingIds = repo.Find(e => allIds.Contains(e.Id)).Select(e => e.Id);
			if (existingIds.Count() > 0) {
				foreach (U entity in entities.Where(e => existingIds.Contains(e.Id)).Select(e => (U) Cast(e))) {
					await repo.Update(await repo.Get(entity.Id), entity);
				}

				await repo.AddRange(entities.Where(e => !existingIds.Contains(e.Id)).Select(e => (U) Cast(e)));

				return DBResponse.UPDATED;
			}

			if (entities.Count() > 0) {
				await repo.AddRange(entities.Cast<U>());

				return DBResponse.ADDED;
			}

			return DBResponse.NOCHANGES;
		}

		public async Task<DBResponse> Delete(int id) {
			var rowsChanged = await repo.Delete(id);
			return rowsChanged > 0 ? DBResponse.DELETED : DBResponse.NOCHANGES;
		}

		public IEnumerable<T> GetBySearchModelAndCurrentView(SearchModel search, string currentView) {
			IEnumerable<string> selectedGenres = search.GenresList.Where(genre => genre.IsSelected).Select(genre => genre.Name);

			return repo.Find(
				expression: item => IsAdultCondition(search, item) && ContainsGenre(selectedGenres, item) && ContainsSearchTextCondition(search, item) && IsCorrectView(currentView, item),
				orderBy: item => item.GetType().GetProperty(search.SelectedSort.LocalValue).GetValue(item),
				isDescending: search.IsDescending
			).Select(e => (T) Cast(e));
		}

		public IEnumerable<T> GetCustom(Func<U, bool> condition) =>
			repo.Find(condition, item => item.Id).Select(e => (T) Cast(e));

		protected virtual bool IsAdultCondition(SearchModel search, U item) =>
			throw new NotSupportedException("This function has to be overriden");

		protected virtual bool ContainsSearchTextCondition(SearchModel search, U item) =>
			throw new NotSupportedException("This function has to be overriden");

		protected virtual bool ContainsGenre(IEnumerable<string> selectedGenres, U item) =>
			throw new NotSupportedException("This function has to be overriden");

		protected virtual bool IsCorrectView(string currentView, U item) =>
			throw new NotSupportedException("This function has to be overriden");

		protected virtual U Cast(T model) =>
			throw new NotSupportedException("This function has to be overriden");

		protected virtual T Cast(U model) =>
			throw new NotSupportedException("This function has to be overriden");
	}
}