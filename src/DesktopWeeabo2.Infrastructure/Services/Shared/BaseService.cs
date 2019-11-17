using DesktopWeeabo2.Core.Entities;
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
			var entity = repo.Get(model.Id);

			if (entity == null) {
				await repo.Add((U) Cast(model));
				return DBResponse.ADDED;
			} else {
				await repo.Update(entity, (U) Cast(model));
				return DBResponse.UPDATED;
			}
		}

		public async Task<DBResponse> AddOrUpdateRange(IEnumerable<T> entities, Action<object> onActionCallback = null) {
			var allIds = entities.Select(e => e.Id).ToList();
			var existingIds = repo.Find(e => allIds.Contains(e.Id)).Select(e => e.Id).ToList();

			if (existingIds.Count() > 0) {
				// update existing entites
				IEnumerable<U> dbEntities = repo.Find(e => existingIds.Contains(e.Id), isNoTracking: false);
				List<U> entitiesToUpdate = entities.Where(e => existingIds.Contains(e.Id)).Select(e => (U) Cast(e)).ToList();

				CopyPersonalVariablesToNewEntites(dbEntities, ref entitiesToUpdate);
				
				onActionCallback(entitiesToUpdate.Count());
				await repo.UpdateRange(dbEntities, entitiesToUpdate);

				// add new entites
				var entitiesToAdd = entities.Where(e => !existingIds.Contains(e.Id)).Select(e => (U) Cast(e));
				onActionCallback(entitiesToAdd.Count());
				await repo.AddRange(entitiesToAdd);

				return DBResponse.UPDATED; 
			}

			if (entities.Count() > 0) {

				onActionCallback(entities.Count());
				await repo.AddRange(entities.Select(e => (U) Cast(e)));

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

		public IEnumerable<T> GetAll() =>
			repo.GetAll().Select(e => (T) Cast(e));

		protected abstract bool IsAdultCondition(SearchModel search, U item);

		protected abstract bool ContainsSearchTextCondition(SearchModel search, U item);

		protected abstract bool ContainsGenre(IEnumerable<string> selectedGenres, U item);

		protected abstract bool IsCorrectView(string currentView, U item);

		protected abstract U Cast(T model);

		protected abstract T Cast(U model);

		private void CopyPersonalVariablesToNewEntites(IEnumerable<U> dbEntities, ref List<U> entities) {
			entities = entities
						.Join(dbEntities, newEn => newEn.Id, oldEn => oldEn.Id, (newEn, oldEn) => new { newEn, oldEn })
						.Select(both => {
							var newEn = both.newEn;
							var oldEn = both.oldEn;

							if (newEn.PersonalScore == null && oldEn.PersonalScore != null)
								newEn.PersonalScore = oldEn.PersonalScore;

							if (newEn.PersonalReview == null && oldEn.PersonalReview != null)
								newEn.PersonalReview = oldEn.PersonalReview;
							else if (newEn.PersonalReview != null && oldEn.PersonalReview != null)
								newEn.PersonalReview = $"{oldEn.PersonalReview}{Environment.NewLine}{Environment.NewLine}----- OLDER PERSONAL REVIEW -----{Environment.NewLine}{Environment.NewLine}{newEn.PersonalReview}";

							if ((newEn.DateAdded == null && oldEn.DateAdded != null)
								|| (newEn.DateAdded != null && newEn.DateAdded != null && newEn.DateAdded > oldEn.DateAdded))
								newEn.DateAdded = oldEn.DateAdded;

							if (typeof(U) == typeof(AnimeEntity)) {
								var newAEn = newEn as AnimeEntity;
								var oldAEn = oldEn as AnimeEntity;

								if (oldAEn.ViewingStatus != null)
									newAEn.ViewingStatus = oldAEn.ViewingStatus;

								if (oldAEn.WatchPriority != null)
									newAEn.WatchPriority = oldAEn.WatchPriority;

								if (oldAEn.RewatchCount != null)
									newAEn.RewatchCount = oldAEn.RewatchCount;

								if (oldAEn.CurrentEpisode != null)
									newAEn.CurrentEpisode = oldAEn.CurrentEpisode;

								return newAEn as U;
							} else {
								var newMEn = newEn as MangaEntity;
								var oldMEn = oldEn as MangaEntity;

								if (oldMEn.ReadingStatus != null)
									newMEn.ReadingStatus = oldMEn.ReadingStatus;

								if (oldMEn.ReadPriority != null)
									newMEn.ReadPriority = oldMEn.ReadPriority;

								if (oldMEn.RereadCount != null)
									newMEn.RereadCount = oldMEn.RereadCount;

								if (oldMEn.CurrentChapter != null)
									newMEn.CurrentChapter = oldMEn.CurrentChapter;

								return newMEn as U;
							}
						}).ToList();
		}
	}
}