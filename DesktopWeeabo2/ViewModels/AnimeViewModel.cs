using DesktopWeeabo2.Core.Enums;
using DesktopWeeabo2.Core.Interfaces.Services;
using DesktopWeeabo2.Core.Models;
using DesktopWeeabo2.Helpers;
using DesktopWeeabo2.Infrastructure.API;
using DesktopWeeabo2.Infrastructure.DomainServices;
using DesktopWeeabo2.ViewModels.Shared;
using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DesktopWeeabo2.ViewModels {

	public class AnimeViewModel : BaseItemViewModel {
		private readonly IAnimeService _animeService;
		private readonly AnimeAPIEnumerator _animeAPIEnumerator;

		private bool LocalHelper = false;
		public ObservableRangeCollection<AnimeModel> AnimeItems { get; set; } = new ObservableRangeCollection<AnimeModel>();

		private AnimeModel _SelectedItem = null;

		public AnimeModel SelectedItem {
			get { return _SelectedItem; }
			set {
				if (_SelectedItem?.Id != value?.Id) {
					_SelectedItem = value;
					if (_SelectedItem != null) {
						SelectedItemPersonalReview = _SelectedItem.PersonalReview;
						SelectedItemPersonalScore = _SelectedItem.PersonalScore;
						SelectedItemViewingStatus = _SelectedItem.ViewingStatus;
					}
					RaisePropertyChanged("SelectedItem");
				}
			}
		}

		public string SelectedItemTitle { get { return _SelectedItem?.Title.GetFirstNonNullTitle(); } }

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

		public string SelectedItemViewingStatus {
			get { return _SelectedItem?.ViewingStatus; }
			set {
				if (_SelectedItem != null && _SelectedItem?.ViewingStatus != value)
					_SelectedItem.ViewingStatus = value;
				RaisePropertyChanged("SelectedItemViewingStatus");
			}
		}

		public AnimeViewModel(IAnimeService animeService, AnimeAPIEnumerator animeAPIEnumerator) : base() {
			_animeService = animeService;
			_animeAPIEnumerator = animeAPIEnumerator;

			BindingOperations.EnableCollectionSynchronization(AnimeItems, _CollectionLock);
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

				default:
					break;
			}
		}

		protected override void RenewView(bool isActionOnline = false) {
			if (!isActionOnline) {
				AnimeItems.Clear();
				TotalItems = 0;
				SelectedItem = null;
				TotalAPIItems = "";
				APIHasNextPage = false;
				APICurrentPage = 1;
				TotalAPIPages = 1;
				_animeAPIEnumerator.CurrentPage = 1;
			}
			IsContentLoading = false;
			PressedTransferButton = "";
		}

		protected override void AddLocalItemsToView() {
			Task.Run(() => {
				if (LocalHelper)
					return;
				LocalHelper = true;
				IsContentLoading = true;
				lock (_CollectionLock) {
					var items = _animeService.GetBySearchModelAndCurrentView(SearchModel, CurrentView);
					AnimeItems.AddRange(items);
					TotalItems = AnimeItems.Count;
				};
				LocalHelper = false;
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
				SelectedGenres = StringHelpers.GenreHelper(SearchModel.GenresList);
		}));

		public DelegateCommand ChangeItemSource => new DelegateCommand(
			new Action<object>((e) => {
				if (CurrentView.Equals(e as string))
					return;

				DontTriggerSearchChanged = true;
				if ((SelectedSort.VisibleIn == SortLocation.ANIME || SelectedSort.VisibleIn == SortLocation.LOCAL) && (string) e == StatusView.ONLINE)
					SelectedSort = SearchModel.SortsList[0];
				DontTriggerSearchChanged = false;

				CurrentView = e as string;
				RenewView();

				if (!e.Equals(StatusView.ONLINE)) {
					AddLocalItemsToView();
				} else if (e.Equals(StatusView.ONLINE) && SearchText.Length > 0) {
					AddOnlineItemsToView.Execute(null);
				}
			})
		);

		public DelegateCommand AddOnlineItemsToView => new DelegateCommand(
			new Action(async () => {
				if (LocalHelper)
					return;
				LocalHelper = true;
				IsContentLoading = true;
				await Task.Run(async () => {
					_animeAPIEnumerator.SearchString = SearchText;
					_animeAPIEnumerator.SortBy = IsDescending ? $"{SelectedSort.APIValue}_DESC" : SelectedSort.APIValue;
					_animeAPIEnumerator.IsAdult = IsAdult;
					_animeAPIEnumerator.Genres = SearchModel.GenresList.Where(item => item.IsSelected).Select(item => item.Name).ToArray();
					try {
						var onlineItems = await _animeAPIEnumerator.GetCurrentSearchSet();
						var onlineItemsIds = onlineItems.Select(onlineItem => onlineItem.Id);

						foreach (AnimeModel localItem in _animeService.GetCustom(localItem => onlineItemsIds.Contains(localItem.Id))) {
							var currentItem = onlineItems.FirstOrDefault(onlineItem => onlineItem.Id == localItem.Id);
							currentItem.DateAdded = localItem.DateAdded;
							currentItem.RewatchCount = localItem.RewatchCount;
							currentItem.PersonalScore = localItem.PersonalScore;
							currentItem.ViewingStatus = localItem.ViewingStatus;
							currentItem.WatchPriority = localItem.WatchPriority;
							currentItem.PersonalReview = localItem.PersonalReview;
							currentItem.CurrentEpisode = localItem.CurrentEpisode;
						}

						AnimeItems.AddRange(onlineItems);
						TotalItems = AnimeItems.Count;

						APIHasNextPage = _animeAPIEnumerator.HasNextPage;
						TotalAPIItems = $" / {_animeAPIEnumerator.TotalItems}";
						APICurrentPage = _animeAPIEnumerator.CurrentPage - 1;
						TotalAPIPages = (int) Math.Ceiling(((decimal) _animeAPIEnumerator.TotalItems / 50));
					} catch (ArgumentNullException ex) { ToastService.ShowToast(ex.Message, "danger"); } catch (ArgumentOutOfRangeException ex) { ToastService.ShowToast(ex.Message, "danger"); } catch (HttpRequestException) { ToastService.ShowToast("The server isn't responding.", "danger"); }
				});
				LocalHelper = false;
				IsContentLoading = false;
			})
		);

		public DelegateCommand SaveItemToDb => new DelegateCommand(
			new Action<object>((e) => {
				if (e.ToString().Equals("PersonalEditStart"))
					return;
				Task.Run(async () => {
					try {
						SelectedItemViewingStatus = PressedTransferButton.Length > 0 ? PressedTransferButton : SelectedItemViewingStatus;
						if (_SelectedItem.DateAdded == null)
							_SelectedItem.DateAdded = DateTime.Now;
						var itemWorkResponse = await _animeService.AddOrUpdate(_SelectedItem);

						if (itemWorkResponse == DBResponse.ADDED)
							ToastService.ShowToast($"Succesfully saved '{_SelectedItem.Title.GetFirstNonNullTitle()}' in '{_SelectedItem.ViewingStatus}' view!", "success");
						else if (itemWorkResponse == DBResponse.UPDATED)
							ToastService.ShowToast($"Succesfully updated '{_SelectedItem.Title.GetFirstNonNullTitle()}'!", "success");
						else if (itemWorkResponse == DBResponse.ERROR)
							throw new Exception("Repo returned ERROR");

						if (CurrentView == StatusView.ONLINE || e.ToString().Equals("PersonalEditComplete")) {
							RenewView(true);
						} else {
							RenewView();
							AddLocalItemsToView();
						}
					} catch (Exception ex) {
						ToastService.ShowToast($"Something went wrong: {ex.Message}.", "danger");
					}
				});
			})
		);

		public DelegateCommand DeleteItemFromDb => new DelegateCommand(
			new Action(async () => {
				await Task.Run(async () => {
					try {
						var itemDeleteResponse = await _animeService.Delete(_SelectedItem.Id);
						if (itemDeleteResponse == DBResponse.DELETED)
							ToastService.ShowToast($"Anime '{_SelectedItem.Title.GetFirstNonNullTitle()}' in '{_SelectedItem.ViewingStatus}' view was deleted succesfully!", "success");
						else
							throw new Exception($"Anime '{_SelectedItem.Title.GetFirstNonNullTitle()}' doesn't exist in '{_SelectedItem.ViewingStatus}' view.");

						if (CurrentView == StatusView.ONLINE) {
							SelectedItemViewingStatus = null;
							RenewView(true);
						} else {
							RenewView();
							AddLocalItemsToView();
						}
					} catch (Exception ex) {
						ToastService.ShowToast($"Something went wrong: {ex.Message}.", "danger");
					}
				});
			})
		);
	}
}