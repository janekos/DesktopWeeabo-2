using DesktopWeeabo2.API;
using DesktopWeeabo2.Data.Services;
using DesktopWeeabo2.Helpers;
using DesktopWeeabo2.Models;
using DesktopWeeabo2.ViewModels.Shared;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DesktopWeeabo2.ViewModels {
	class MangaViewModel : BaseItemViewModel {
		private bool LocalHelper = false;
		private MangaService MangaService = new MangaService();
		private MangaAPIEnumerator MangaAPIEnumerator = new MangaAPIEnumerator();
		public ObservableCollection<MangaModel> MangaItems { get; set; } = new ObservableCollection<MangaModel>();

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

		public string SelectedItemTitle { get { return _SelectedItem == null ? null : StringHelpers.GetFirstNotNullItemTitle(_SelectedItem); } }

		public string SelectedItemPersonalReview {
			get { return _SelectedItem?.PersonalReview; }
			set {
				if (_SelectedItem.PersonalReview != value) _SelectedItem.PersonalReview = value;
				RaisePropertyChanged("SelectedItemPersonalReview");
			}
		}

		public int? SelectedItemPersonalScore {
			get { return (_SelectedItem == null || _SelectedItem.PersonalScore == null) ? 0 : _SelectedItem.PersonalScore; }
			set {
				if (_SelectedItem.PersonalScore != value) _SelectedItem.PersonalScore = value;
				RaisePropertyChanged("SelectedItemPersonalScore");
			}
		}

		public string SelectedItemReadingStatus {
			get { return _SelectedItem?.ReadingStatus; }
			set {
				if (_SelectedItem != null && _SelectedItem?.ReadingStatus != value) _SelectedItem.ReadingStatus = value;
				RaisePropertyChanged("SelectedItemReadingStatus");
			}
		}

		public MangaViewModel() : base() { BindingOperations.EnableCollectionSynchronization(MangaItems, _CollectionLock); }

		protected override void Property_Changed(object sender, PropertyChangedEventArgs e) {
			switch (e.PropertyName) {
				case "SearchChanged":
					if (!DontTriggerSearchChanged) {
						RenewView();
						if (_CurrentView.Equals(StatusView.ONLINE)) AddOnlineItemsToView.Execute(null);
						else AddLocalItemsToView();
					}
					break;
				default:
					break;
			}
		}

		protected override void RenewView(bool isActionOnline = false) {
			if (!isActionOnline) {
				MangaItems.Clear();
				TotalItems = 0;
				SelectedItem = null;
				TotalAPIItems = "";
				APIHasNextPage = false;
				APICurrentPage = 1;
				TotalAPIPages = 1;
				MangaAPIEnumerator.CurrentPage = 1;
			}
			IsContentLoading = false;
			PressedTransferButton = "";
		}

		protected async override void AddLocalItemsToView() {
			await Task.Run(() => {
				if (LocalHelper) return;
				LocalHelper = true;
				IsContentLoading = true;
				lock (_CollectionLock) {
					foreach (MangaModel item in MangaService.GetBySearchModelAndCurrentView(SearchModel, CurrentView)) {
						MangaItems.Add(item);
						TotalItems += 1;
					}
				};
				LocalHelper = false;
				IsContentLoading = false;
			});
		}

		public DelegateCommand TriggerSearch => new DelegateCommand(new Action(() => {
			RenewView();
			if (CurrentView.Equals(StatusView.ONLINE)) AddOnlineItemsToView.Execute(null);
			else AddLocalItemsToView();
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
			if (IsAdvancedVisible) SelectedGenres = StringHelpers.GenreHelper(SearchModel.GenresList);
		}));

		public DelegateCommand ChangeItemSource => new DelegateCommand(
			new Action<object>((e) => {
				if (CurrentView.Equals(e as string)) return;

				DontTriggerSearchChanged = true;
				if ((SelectedSort.VisibleIn == SortLocation.MANGA || SelectedSort.VisibleIn == SortLocation.LOCAL) && (e as string) == StatusView.ONLINE) SelectedSort = SearchModel.SortsList[0];
				DontTriggerSearchChanged = false;

				CurrentView = e as string;
				RenewView();

				if (!e.Equals(StatusView.ONLINE)) AddLocalItemsToView();
				else if (e.Equals(StatusView.ONLINE) && SearchText.Length > 0) AddOnlineItemsToView.Execute(null);
			})
		);

		public DelegateCommand AddOnlineItemsToView => new DelegateCommand(
			new Action(async () => {
				if (LocalHelper) return;
				LocalHelper = true;
				IsContentLoading = true;
				await Task.Run(async () => {
					MangaAPIEnumerator.SearchString = SearchText;
					MangaAPIEnumerator.SortBy = IsDescending ? $"{SelectedSort.APIValue}_DESC" : SelectedSort.APIValue;
					MangaAPIEnumerator.IsAdult = IsAdult;
					MangaAPIEnumerator.Genres = SearchModel.GenresList.Where(item => item.IsSelected).Select(item => item.Name).ToArray();
					try {
						var onlineItems = await MangaAPIEnumerator.GetCurrentSearchSet();
						var onlineItemsIds = onlineItems.Select(onlineItem => onlineItem.Id);

						var localItems = MangaService.GetCustom(localItem => onlineItemsIds.Contains(localItem.Id));

						for (int i = 0; i < localItems.Length; i++) {
							var currentItem = onlineItems.FirstOrDefault(onlineItem => onlineItem.Id == localItems[i].Id);
							currentItem.DateAdded = localItems[i].DateAdded;
							currentItem.RereadCount = localItems[i].RereadCount;
							currentItem.PersonalScore = localItems[i].PersonalScore;
							currentItem.ReadingStatus = localItems[i].ReadingStatus;
							currentItem.ReadPriority = localItems[i].ReadPriority;
							currentItem.PersonalReview = localItems[i].PersonalReview;
							currentItem.CurrentChapter = localItems[i].CurrentChapter;
						}

						for (int i = 0; i < onlineItems.Length; i++) {
							MangaItems.Add(onlineItems[i]);
							TotalItems += 1;
						}

						APIHasNextPage = MangaAPIEnumerator.HasNextPage;
						TotalAPIItems = $" / {MangaAPIEnumerator.TotalItems}";
						APICurrentPage = MangaAPIEnumerator.CurrentPage - 1;
						TotalAPIPages = (int)Math.Ceiling(((decimal)MangaAPIEnumerator.TotalItems / 50));
					}
					catch (ArgumentNullException ex) { ToastService.ShowToast(ex.Message, "danger"); }
					catch (ArgumentOutOfRangeException ex) { ToastService.ShowToast(ex.Message, "danger"); }
					catch (HttpRequestException) { ToastService.ShowToast("The server isn't responding.", "danger"); }

				});
				LocalHelper = false;
				IsContentLoading = false;
			})
		);

		public DelegateCommand SaveItemToDb => new DelegateCommand(
			new Action<object>((e) => {
				if (e.ToString().Equals("PersonalEditStart")) return;
				Task.Run(async () => {
					try {
						SelectedItemReadingStatus = PressedTransferButton.Length > 0 ? PressedTransferButton : SelectedItemReadingStatus;
						if (_SelectedItem.DateAdded == null) _SelectedItem.DateAdded = DateTime.Now;
						var itemWorkResponse = await MangaService.AddOrUpdate(_SelectedItem);

						if (itemWorkResponse == DBResponse.ADDED) ToastService.ShowToast($"Succesfully saved '{StringHelpers.GetFirstNotNullItemTitle(_SelectedItem)}' in '{SelectedItemReadingStatus}' view!", "success");
						else if (itemWorkResponse == DBResponse.UPDATED) ToastService.ShowToast($"Succesfully updated '{StringHelpers.GetFirstNotNullItemTitle(_SelectedItem)}'!", "success");
						else if (itemWorkResponse == DBResponse.ERROR) throw new Exception("Repo returned ERROR");

						if (CurrentView == StatusView.ONLINE || e.ToString().Equals("PersonalEditComplete")) {
							RenewView(true);
						}
						else {
							RenewView();
							AddLocalItemsToView();
						}

					}
					catch (Exception ex) {
						ToastService.ShowToast($"Something went wrong: {ex.Message}.", "danger");
					}
				});
			})
		);

		public DelegateCommand DeleteItemFromDb => new DelegateCommand(
			new Action(async () => {
				await Task.Run(() => {
					try {
						var itemDeleteResponse = MangaService.Delete(_SelectedItem.Id);
						if (itemDeleteResponse == DBResponse.DELETED) ToastService.ShowToast($"Manga '{StringHelpers.GetFirstNotNullItemTitle(_SelectedItem)}' in '{SelectedItemReadingStatus}' view was deleted succesfully!", "success");
						else throw new Exception($"Manga '{StringHelpers.GetFirstNotNullItemTitle(_SelectedItem)}' doesn't exist in '{SelectedItemReadingStatus}' view.");

						if (CurrentView == StatusView.ONLINE) {
							SelectedItemReadingStatus = null;
							RenewView(true);
						}
						else {
							RenewView();
							AddLocalItemsToView();
						}
					}
					catch (Exception ex) {
						ToastService.ShowToast($"Something went wrong: {ex.Message}.", "danger");
					}
				});
			})
		);
	}
}
