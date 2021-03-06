﻿using DesktopWeeabo2.Core.Enums;
using DesktopWeeabo2.Core.Interfaces.Services;
using DesktopWeeabo2.Core.Models;
using DesktopWeeabo2.Helpers;
using DesktopWeeabo2.Infrastructure.API;
using DesktopWeeabo2.Infrastructure.Events;
using DesktopWeeabo2.ViewModels.Shared;
using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DesktopWeeabo2.ViewModels {

	public class MangaViewModel : BaseItemViewModel {
		private readonly IMangaService _mangaService;
		private readonly MangaApiEnumerator _mangaAPIEnumerator;

		private bool LocalStateHelper = false;
		public ObservableRangeCollection<MangaModel> MangaItems { get; set; } = new ObservableRangeCollection<MangaModel>();

		private MangaModel _SelectedItem = null;

		public MangaModel SelectedItem {
			get { return _SelectedItem; }
			set {
				if (_SelectedItem?.Id != value?.Id) {
					_SelectedItem = value;
					if (_SelectedItem != null) {
						SelectedItemPersonalReview = _SelectedItem.PersonalReview;
						SelectedItemPersonalScore = _SelectedItem.PersonalScore;
						SelectedItemReadingStatus = _SelectedItem.ReadingStatus;
					}
					RaisePropertyChanged("SelectedItem");
				}
			}
		}

		public string SelectedItemTitle { get { return _SelectedItem == null ? null : _SelectedItem.Title.GetFirstNonNullTitle(); } }

		public string SelectedItemPersonalReview {
			get { return _SelectedItem?.PersonalReview; }
			set {
				if (_SelectedItem.PersonalReview != value)
					_SelectedItem.PersonalReview = value;
				RaisePropertyChanged("SelectedItemPersonalReview");
			}
		}

		public int? SelectedItemPersonalScore {
			get { return (_SelectedItem == null || _SelectedItem.PersonalScore == null) ? 0 : _SelectedItem.PersonalScore; }
			set {
				if (_SelectedItem.PersonalScore != value)
					_SelectedItem.PersonalScore = value;
				RaisePropertyChanged("SelectedItemPersonalScore");
			}
		}

		public string SelectedItemReadingStatus {
			get { return _SelectedItem?.ReadingStatus; }
			set {
				if (_SelectedItem != null && _SelectedItem?.ReadingStatus != value)
					_SelectedItem.ReadingStatus = value;
				RaisePropertyChanged("SelectedItemReadingStatus");
			}
		}

		public MangaViewModel(IMangaService mangaService) : base() {
			_mangaService = mangaService;
			_mangaAPIEnumerator = new MangaApiEnumerator();

			BindingOperations.EnableCollectionSynchronization(MangaItems, _CollectionLock);
		}

		protected override void Property_Changed(object sender, PropertyChangedEventArgs e) {
			switch (e.PropertyName) {
				case "SearchChanged":
					if (!DontTriggerSearchChanged) {
						RenewView();
						if (_CurrentView.Equals(StatusView.ONLINE))
							AddOnlineItemsToView.Execute(null);
						else
							AddLocalItemsToView();
					}
					break;
				case "SelectedPageIndex":
					if (!LocalStateHelper) {
						if (_CurrentView.Equals(StatusView.ONLINE))
							AddOnlineItemsToView.Execute(PaginationCommandType.OTHER);
						else
							AddLocalItemsToView();
					}
					break;
				default:
					break;
			}
		}

		protected override void RenewView(bool isActionOnline = false) {
			if (!isActionOnline) {
				MangaItems.Clear();
				SelectedItem = null;
				TotalAPIPages = 1;
				_mangaAPIEnumerator.CurrentPage = 1;
			}
			IsContentLoading = false;
			PressedTransferButton = "";
		}

		protected async override void AddLocalItemsToView() {
			await Task.Run(() => {
				if (LocalStateHelper)
					return;
				LocalStateHelper = true;
				IsContentLoading = true;
				lock (_CollectionLock) {
					MangaItems.AddRange(_mangaService.GetBySearchModelAndCurrentView(SearchModel, CurrentView));
				};
				LocalStateHelper = false;
				IsContentLoading = false;
			});
		}

		public DelegateCommand TriggerSearch => new DelegateCommand(new Action(() => {
			RenewView();
			if (CurrentView.Equals(StatusView.ONLINE))
				AddOnlineItemsToView.Execute(null);
			else
				AddLocalItemsToView();
		}));

		public DelegateCommand ClearFilterTriggered => new DelegateCommand(new Action(() => {
			DontTriggerSearchChanged = true;

			SelectedSort = SearchModel.SortsList[0];
			IsAdult = true;
			IsDescending = false;
			SearchGenres = null;
			SearchGenres = SearchModel.InitGenresList();
			SelectedGenres = "All";

			DontTriggerSearchChanged = false;
			RaisePropertyChanged("SearchChanged");
		}));

		public DelegateCommand TriggerAdvancedWasClicked => new DelegateCommand(new Action(() => {
			IsAdvancedVisible = !IsAdvancedVisible;
			if (IsAdvancedVisible)
				SelectedGenres = Utilities.GenreHelper(SearchModel.GenresList);
		}));

		public DelegateCommand ChangeItemSource => new DelegateCommand(
			new Action<object>((e) => {
				if (CurrentView.Equals(e as string))
					return;

				DontTriggerSearchChanged = true;
				if ((SelectedSort.VisibleIn == SortLocation.MANGA || SelectedSort.VisibleIn == SortLocation.LOCAL) && (e as string) == StatusView.ONLINE)
					SelectedSort = SearchModel.SortsList[0];
				DontTriggerSearchChanged = false;

				CurrentView = e as string;
				RenewView();

				if (!e.Equals(StatusView.ONLINE))
					AddLocalItemsToView();
				else if (e.Equals(StatusView.ONLINE) && SearchText.Length > 0)
					AddOnlineItemsToView.Execute(null);
			})
		);

		public DelegateCommand AddOnlineItemsToView => new DelegateCommand(
			new Action<object>(async (paginationType) => {
				if (LocalStateHelper)
					return;
				LocalStateHelper = true;
				IsContentLoading = true;

				var castPaginationType = (PaginationCommandType) paginationType;

				switch (castPaginationType) {
					case PaginationCommandType.FIRST:
						_mangaAPIEnumerator.CurrentPage = 1;
						break;
					case PaginationCommandType.LAST:
						_mangaAPIEnumerator.CurrentPage = _mangaAPIEnumerator.LastPage;
						break;
					case PaginationCommandType.NEXT:
						_mangaAPIEnumerator.CurrentPage += 1;
						break;
					case PaginationCommandType.PREVIOUS:
						_mangaAPIEnumerator.CurrentPage -= 1;
						break;
					case PaginationCommandType.OTHER:
						_mangaAPIEnumerator.CurrentPage = SelectedPageIndex + 1;
						break;
				}

				_mangaAPIEnumerator.SearchString = SearchText;
				_mangaAPIEnumerator.SortBy = IsDescending ? $"{SelectedSort.APIValue}_DESC" : SelectedSort.APIValue;
				_mangaAPIEnumerator.IsAdult = IsAdult;
				_mangaAPIEnumerator.Genres = SearchModel.GenresList.Where(item => item.IsSelected).Select(item => item.Name).ToArray();
				try {
					var onlineItems = await _mangaAPIEnumerator.GetCurrentSearchSet();
					var onlineItemsIds = onlineItems.Select(onlineItem => onlineItem.Id);

					foreach (MangaModel localItem in _mangaService.GetCustom(localItem => onlineItemsIds.Contains(localItem.Id))) {
						var currentItem = onlineItems.FirstOrDefault(onlineItem => onlineItem.Id == localItem.Id);
						currentItem.DateAdded = localItem.DateAdded;
						currentItem.RereadCount = localItem.RereadCount;
						currentItem.ReadPriority = localItem.ReadPriority;
						currentItem.PersonalScore = localItem.PersonalScore;
						currentItem.ReadingStatus = localItem.ReadingStatus;
						currentItem.PersonalReview = localItem.PersonalReview;
						currentItem.CurrentChapter = localItem.CurrentChapter;
					}

					MangaItems.Clear();
					MangaItems.AddRange(onlineItems);

					LastItemsPage = _mangaAPIEnumerator.LastPage;
					ItemPageList = Enumerable.Range(1, LastItemsPage).ToList();

					if (castPaginationType != PaginationCommandType.OTHER)
						SelectedPageIndex = _mangaAPIEnumerator.CurrentPage - 1;

					TotalAPIPages = (int) Math.Ceiling(((decimal) _mangaAPIEnumerator.TotalItems / 50));
				} catch (ArgumentNullException ex) {
					ToastEvent.ShowToast(ex.Message, ToastType.DANGER);
				} catch (ArgumentOutOfRangeException ex) {
					ToastEvent.ShowToast(ex.Message, ToastType.DANGER);
				} catch (HttpRequestException) {
					ToastEvent.ShowToast("The server isn't responding.", ToastType.DANGER);
				}

				LocalStateHelper = false;
				IsContentLoading = false;
			})
		);

		public DelegateCommand SaveItemToDb => new DelegateCommand(
			new Action<object>((e) => {
				if (e.ToString().Equals("PersonalEditStart"))
					return;

				try {
					SelectedItemReadingStatus = PressedTransferButton.Length > 0 ? PressedTransferButton : SelectedItemReadingStatus;
					if (_SelectedItem.DateAdded == null)
						_SelectedItem.DateAdded = DateTime.Now;
					var itemWorkResponse = _mangaService.AddOrUpdate(_SelectedItem);

					if (itemWorkResponse == DBResponse.ADDED)
						ToastEvent.ShowToast($"Succesfully saved '{_SelectedItem.Title.GetFirstNonNullTitle()}' in '{SelectedItemReadingStatus}' view!", ToastType.SUCCESS);
					else if (itemWorkResponse == DBResponse.UPDATED)
						ToastEvent.ShowToast($"Succesfully updated '{_SelectedItem.Title.GetFirstNonNullTitle()}'!", ToastType.SUCCESS);
					else if (itemWorkResponse == DBResponse.ERROR)
						throw new Exception("Repo returned ERROR");

					if (CurrentView == StatusView.ONLINE || e.ToString().Equals("PersonalEditComplete")) {
						RenewView(true);
					} else {
						RenewView();
						AddLocalItemsToView();
					}
				} catch (Exception ex) {
					ToastEvent.ShowToast($"Something went wrong: {ex.Message}.", ToastType.DANGER);
				}
			})
		);

		public DelegateCommand DeleteItemFromDb => new DelegateCommand(
			new Action(() => {
				try {
					var itemDeleteResponse = _mangaService.Delete(_SelectedItem.Id);
					if (itemDeleteResponse == DBResponse.DELETED)
						ToastEvent.ShowToast($"Manga '{_SelectedItem.Title.GetFirstNonNullTitle()}' in '{SelectedItemReadingStatus}' view was deleted succesfully!", ToastType.SUCCESS);
					else
						throw new Exception($"Manga '{_SelectedItem.Title.GetFirstNonNullTitle()}' doesn't exist in '{SelectedItemReadingStatus}' view.");

					if (CurrentView == StatusView.ONLINE) {
						SelectedItemReadingStatus = null;
						RenewView(true);
					} else {
						RenewView();
						AddLocalItemsToView();
					}
				} catch (Exception ex) {
					ToastEvent.ShowToast($"Something went wrong: {ex.Message}.", ToastType.DANGER);
				}
			})
		);
	}
}