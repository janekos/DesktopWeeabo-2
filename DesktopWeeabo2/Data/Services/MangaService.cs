﻿using DesktopWeeabo2.Data.Repositories;
using DesktopWeeabo2.Data.Services.Shared;
using DesktopWeeabo2.Helpers;
using DesktopWeeabo2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Data.Services {
	public class MangaService : IService<MangaModel> {
		public async Task<DBResponse> AddOrUpdate(MangaModel entity) {
			try {
				using (var repo = new MangaRepo()) {
					var dbEntity = await repo.Get(entity.Id);
					if (dbEntity == null) {
						repo.Add(entity);
						return DBResponse.ADDED;
					}
					else {
						if (ReflectionHelpers.CompareObjectValues(dbEntity, entity)) {
							repo.Update(dbEntity, entity);
							return DBResponse.UPDATED;
						}
						return DBResponse.NOCHANGES;
					}
				}
			}
			catch (Exception) { return DBResponse.ERROR; }
		}

		public DBResponse Delete(int id) {
			try {
				using (var repo = new MangaRepo()) {
					repo.Delete(id);
					return DBResponse.DELETED;
				}
			}
			catch (Exception) { return DBResponse.ERROR; }
		}

		private bool IsAdultCondition(SearchModel search, MangaModel item) =>
			!search.IsAdult
				? !item.IsAdult
				: true;
		private bool ContainsSearchTextCondition(SearchModel search, MangaModel item) =>
			search.SearchText.Length > 0
				? (item.TitleEnglish.ToLower().Contains(search.SearchText) || item.TitleRomaji.ToLower().Contains(search.SearchText) || item.TitleNative.ToLower().Contains(search.SearchText))
				: true;
		private bool ContainsGenre(string[] selectedGenres, MangaModel item) =>
			selectedGenres.Length > 0
				? !selectedGenres.Except(item.Genres.Replace(" ", string.Empty).Split(',')).Any()
				: true;
		private bool IsCorrectView(string currentView, MangaModel item) => item.ReadingStatus.Equals(currentView);

		public MangaModel[] GetBySearchModelAndCurrentView(SearchModel search, string currentView) {
			string[] selectedGenres = search.GenresList.Where(genre => genre.IsSelected).Select(genre => genre.Name).ToArray();
			using (var repo = new MangaRepo()) {
				return repo.FindSet(
					item => IsAdultCondition(search, item) && ContainsGenre(selectedGenres, item) && ContainsSearchTextCondition(search, item) && IsCorrectView(currentView, item),
					item => item.GetType().GetProperty(search.SelectedSort.LocalValue).GetValue(item),
					search.IsDescending
				).ToArray();
			}
		}

		public MangaModel[] GetCustom(Func<MangaModel, bool> condition) {
			using (var repo = new MangaRepo()) {
				return repo.FindSet(condition, item => item.Id).ToArray();
			}
		}
	}
}
