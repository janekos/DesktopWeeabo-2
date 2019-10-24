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
			var entity = Cast(await repo.Get(model.Id));

			if (entity == null) {
				await repo.Add(Cast(model));
				return DBResponse.ADDED;
			} else {
				await repo.Update(Cast(model));

			}


			return DBResponse.NOCHANGES;
		}

		public async Task<DBResponse> AddOrUpdateRange(IEnumerable<T> entities) {
			var rowsChanged = await repo.AddRange(entities.Cast<U>());

			return rowsChanged > 0 ? DBResponse.UPDATED : DBResponse.NOCHANGES;
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
			).Cast<T>();
		}

		public IEnumerable<T> GetCustom(Func<U, bool> condition) =>
			repo.Find(condition, item => item.Id).Cast<T>();

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
