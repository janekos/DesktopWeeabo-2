using DesktopWeeabo2.Core.Enums;
using DesktopWeeabo2.Core.Interfaces.Repositories.Shared;
using DesktopWeeabo2.Core.Interfaces.Services.Shared;
using DesktopWeeabo2.Core.Models;
using DesktopWeeabo2.Core.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DesktopWeeabo2.Infrastructure.Services.Shared {

	public abstract class BaseService<T> : IDefineServices<T> where T : BaseModel {
		private readonly IDefineRepositories<T> repo;

		public BaseService(IDefineRepositories<T> repo) {
			this.repo = repo;
		}

		public DBResponse AddOrUpdate(T model) {
			if (repo.Update(model) == 0) {
				repo.Add(model);
				return DBResponse.ADDED;
			} else {
				return DBResponse.UPDATED;
			}
		}

		public DBResponse AddOrUpdateRange(IEnumerable<T> entities, Action<object> onActionCallback = null) {
			var allIds = entities.Select(e => e.Id).ToList();
			var existingIds = repo.Find(e => allIds.Contains(e.Id)).Select(e => e.Id).ToList();

			if (existingIds.Count() > 0) {
				// update existing entites
				var dbEntities = repo.Find(e => existingIds.Contains(e.Id));
				var entitiesToUpdate = entities.Where(e => existingIds.Contains(e.Id)).ToList();

				CopyPersonalVariablesToNewEntites(dbEntities, ref entitiesToUpdate);

				onActionCallback(entitiesToUpdate.Count());
				repo.UpdateRange(entitiesToUpdate);

				// add new entites
				var entitiesToAdd = entities.Where(e => !existingIds.Contains(e.Id));
				onActionCallback(entitiesToAdd.Count());
				repo.AddRange(entitiesToAdd);

				return DBResponse.UPDATED;
			}

			if (entities.Count() > 0) {

				onActionCallback(entities.Count());
				repo.AddRange(entities);

				return DBResponse.ADDED;
			}

			return DBResponse.NOCHANGES;
		}

		public DBResponse Delete(int id) =>
			repo.Delete(id) > 0
				? DBResponse.DELETED
				: DBResponse.NOCHANGES;

		public IEnumerable<T> GetBySearchModelAndCurrentView(SearchModel search, string currentView) {
			IEnumerable<string> selectedGenres = search.GenresList.Where(genre => genre.IsSelected).Select(genre => genre.Name);

			return repo.Find(
				expression: item => IsAdultCondition(search, item)
							&& ContainsGenre(selectedGenres, item)
							&& ContainsSearchTextCondition(search, item)
							&& IsCorrectView(currentView, item),
				orderBy: item => item.GetType().GetProperty(search.SelectedSort.LocalValue).GetValue(item),
				isDescending: search.IsDescending
			);
		}

		public IEnumerable<T> GetCustom(Expression<Func<T, bool>> condition) =>
			repo.Find(condition, item => item.Id);

		public IEnumerable<T> GetAll() =>
			repo.GetAll();

		protected bool IsAdultCondition(SearchModel search, T item) =>
			!search.IsAdult
				? !item.IsAdult ?? true
				: true;

		protected bool ContainsSearchTextCondition(SearchModel search, T item) =>
			!string.IsNullOrWhiteSpace(search.SearchText)
				? ((item.Title.English?.ToLower().Contains(search.SearchText.ToLower()) ?? false)
					|| (item.Title.Native?.ToLower().Contains(search.SearchText.ToLower()) ?? false)
					|| (item.Title.Romaji?.ToLower().Contains(search.SearchText.ToLower()) ?? false))
				: true;

		protected bool ContainsGenre(IEnumerable<string> selectedGenres, T item) =>
			selectedGenres.Count() > 0
				? !selectedGenres.Except(item.Genres).Any()
				: true;

		protected abstract bool IsCorrectView(string currentView, T item);

		private void CopyPersonalVariablesToNewEntites(IEnumerable<T> dbEntities, ref List<T> entities) {
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

							if (typeof(T) == typeof(AnimeModel)) {
								var newAEn = newEn as AnimeModel;
								var oldAEn = oldEn as AnimeModel;

								if (oldAEn.ViewingStatus != null)
									newAEn.ViewingStatus = oldAEn.ViewingStatus;

								if (oldAEn.WatchPriority != null)
									newAEn.WatchPriority = oldAEn.WatchPriority;

								if (oldAEn.RewatchCount != null)
									newAEn.RewatchCount = oldAEn.RewatchCount;

								if (oldAEn.CurrentEpisode != null)
									newAEn.CurrentEpisode = oldAEn.CurrentEpisode;

								return newAEn as T;
							} else {
								var newMEn = newEn as MangaModel;
								var oldMEn = oldEn as MangaModel;

								if (oldMEn.ReadingStatus != null)
									newMEn.ReadingStatus = oldMEn.ReadingStatus;

								if (oldMEn.ReadPriority != null)
									newMEn.ReadPriority = oldMEn.ReadPriority;

								if (oldMEn.RereadCount != null)
									newMEn.RereadCount = oldMEn.RereadCount;

								if (oldMEn.CurrentChapter != null)
									newMEn.CurrentChapter = oldMEn.CurrentChapter;

								return newMEn as T;
							}
						}).ToList();
		}
	}
}