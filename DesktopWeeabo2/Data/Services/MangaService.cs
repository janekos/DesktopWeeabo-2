﻿using DesktopWeeabo2.Data.Repositories;
using DesktopWeeabo2.Data.Repositories.Shared;
using DesktopWeeabo2.Data.Services.Shared;
using DesktopWeeabo2.Helpers;
using DesktopWeeabo2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Data.Services {
	public class MangaService : IService<MangaModel> {
		private readonly IRepo<MangaModel> _repo;

		public MangaService(IRepo<MangaModel> repo) {
			_repo = repo;
		}

		public async Task<DBResponse> AddOrUpdate(MangaModel entity) {
			try {
				var dbEntity = await _repo.Get(entity.Id);
				if (dbEntity == null) {
					_repo.Add(entity);
					return DBResponse.ADDED;
				} else {
					if (ReflectionHelpers.CompareObjectValues(dbEntity, entity)) {
						_repo.Update(dbEntity, entity);
						return DBResponse.UPDATED;
					}
					return DBResponse.NOCHANGES;
				}

			}
			catch (Exception) { return DBResponse.ERROR; }
		}

		public DBResponse Delete(int id) {
			try {
				_repo.Delete(id);
				return DBResponse.DELETED;
			}
			catch (Exception) { return DBResponse.ERROR; }
		}

		private bool IsAdultCondition(SearchModel search, MangaModel item) =>
			!search.IsAdult
				? !item.IsAdult
				: true;

		private bool ContainsSearchTextCondition(SearchModel search, MangaModel item) =>
			!string.IsNullOrWhiteSpace(search.SearchText)
				? (item.TitleEnglish ?? item.TitleRomaji ?? item.TitleNative).ToLower().Contains(search.SearchText)
				: true;

		private bool ContainsGenre(IEnumerable<string> selectedGenres, MangaModel item) =>
			selectedGenres.Count() > 0
				? !selectedGenres.Except(item.Genres.Replace(" ", string.Empty).Split(',')).Any()
				: true;

		private bool IsCorrectView(string currentView, MangaModel item) => item.ReadingStatus.Equals(currentView);

		public IEnumerable<MangaModel> GetBySearchModelAndCurrentView(SearchModel search, string currentView) {
			IEnumerable<string> selectedGenres = search.GenresList.Where(genre => genre.IsSelected).Select(genre => genre.Name);
			return _repo.FindSet(
				item => IsAdultCondition(search, item) && ContainsGenre(selectedGenres, item) && ContainsSearchTextCondition(search, item) && IsCorrectView(currentView, item),
				item => item.GetType().GetProperty(search.SelectedSort.LocalValue).GetValue(item),
				search.IsDescending
			);
		}

		public IEnumerable<MangaModel> GetCustom(Func<MangaModel, bool> condition) =>
			_repo.FindSet(condition, item => item.Id);
	}
}
