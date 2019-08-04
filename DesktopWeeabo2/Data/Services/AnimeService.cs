﻿using DesktopWeeabo2.Data.Repositories;
using DesktopWeeabo2.Data.Repositories.Shared;
using DesktopWeeabo2.Data.Services.Shared;
using DesktopWeeabo2.Helpers;
using DesktopWeeabo2.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Data.Services {
	public class AnimeService : IService<AnimeModel> {
		private readonly IRepo<AnimeModel> _repo;

		public AnimeService(IRepo<AnimeModel> repo) {
			_repo = repo;
		}

		public async Task<DBResponse> AddOrUpdate(AnimeModel entity) {
			try {
				var dbEntity = await _repo.Get(entity.Id);
				if (dbEntity == null) {
					_repo.Add(entity);
					return DBResponse.ADDED;
				}
				else {
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

		private bool IsAdultCondition(SearchModel search, AnimeModel item) =>
			!search.IsAdult
				? !item.IsAdult
				: true;
		private bool ContainsSearchTextCondition(SearchModel search, AnimeModel item) =>
			search.SearchText.Length > 0
				? (item.TitleEnglish.ToLower().Contains(search.SearchText) || item.TitleRomaji.ToLower().Contains(search.SearchText) || item.TitleNative.ToLower().Contains(search.SearchText))
				: true;
		private bool ContainsGenre(string[] selectedGenres, AnimeModel item) =>
			selectedGenres.Length > 0
				? !selectedGenres.Except(item.Genres.Replace(" ", string.Empty).Split(',')).Any()
				: true;
		private bool IsCorrectView(string currentView, AnimeModel item) => item.ViewingStatus.Equals(currentView);

		public AnimeModel[] GetBySearchModelAndCurrentView(SearchModel search, string currentView) {
			string[] selectedGenres = search.GenresList.Where(genre => genre.IsSelected).Select(genre => genre.Name).ToArray();
			return _repo.FindSet(
				item => IsAdultCondition(search, item) && ContainsGenre(selectedGenres, item) && ContainsSearchTextCondition(search, item) && IsCorrectView(currentView, item),
				item => item.GetType().GetProperty(search.SelectedSort.LocalValue).GetValue(item),
				search.IsDescending
			).ToArray();
		}

		public AnimeModel[] GetCustom(Func<AnimeModel, bool> condition) =>
			_repo.FindSet(condition, item => item.Id).ToArray();
	}
}
