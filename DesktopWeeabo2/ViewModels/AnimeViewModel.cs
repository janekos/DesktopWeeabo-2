using DesktopWeeabo2.API;
using DesktopWeeabo2.Data;
using DesktopWeeabo2.Data.Repositories;
using DesktopWeeabo2.Data.Repositories.Shared;
using DesktopWeeabo2.Helpers;
using DesktopWeeabo2.Helpers.Enums;
using DesktopWeeabo2.Models;
using DesktopWeeabo2.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DesktopWeeabo2.ViewModels {
	class AnimeViewModel : BaseItemViewModel {

		//private string _testval { get; set; } = "All";
		//public string testval {
		//	get { return _testval; }
		//	set {
		//		if ((value as string).Length < 1) _testval = "All";
		//		else _testval = value;
		//		RaisePropertyChanged("testval");
		//	}
		//}
		//public DelegateCommand SetChecked => new DelegateCommand(new Action(() => {testval = string.Join(", ", testsrc.Where(g => g.IsChecked).Select(g => g.Name).ToArray())}));

		private AnimeAPIEnumerator aae = new AnimeAPIEnumerator();
		public ObservableCollection<AnimeModel> AnimeItems { get; set; } = new ObservableCollection<AnimeModel>();

		private AnimeModel _SelectedItem = null;
		public AnimeModel SelectedItem {
			get { return _SelectedItem; }
			set {
				if (_SelectedItem != value) {
					_SelectedItem = value;
					RaisePropertyChanged("SelectedItem");
					if (_SelectedItem != null) {
						SelectedItemPersonalReview = _SelectedItem.PersonalReview;
						SelectedItemPersonalScore = _SelectedItem.PersonalScore;
					}
				}
			}
		}

		public string SelectedItemTitle { get { return _SelectedItem == null ? null : StringHelpers.GetFirstNotNullItemTitle(_SelectedItem); } }

		public string SelectedItemPersonalReview {
			get { return _SelectedItem?.PersonalReview; }
			set { if (_SelectedItem.PersonalReview != value) { _SelectedItem.PersonalReview = value; RaisePropertyChanged("SelectedItem"); } }
		}

		public int SelectedItemPersonalScore {
			get { return _SelectedItem == null ? 0 : _SelectedItem.PersonalScore; }
			set { if (_SelectedItem.PersonalScore != value) { _SelectedItem.PersonalScore = value; RaisePropertyChanged("SelectedItem"); } }
		}

		public AnimeViewModel() : base() { BindingOperations.EnableCollectionSynchronization(AnimeItems, _CollectionLock); }

		protected override void Property_Changed(object sender, PropertyChangedEventArgs e) {
			switch (e.PropertyName) {
				case "SearchText":
					RenewView();
					if (_CurrentView.Equals(StatusView.ONLINE)) AddOnlineItemsToView.Execute(null);
					else AddLocalItemsToView();
					break;
				default:
					break;
			}
		}

		protected override void RenewView(bool isOnline = false) {
			if (!isOnline) {
				AnimeItems.Clear();
				SelectedItem = null;
				TotalItems = 0;
			}
			APIEnumeratorHasNextPage = false;
			IsContentLoading = false;
			PressedTransferButton = "";
		}

		protected override void AddLocalItemsToView() {
			lock (_CollectionLock) {
				Task.Run(() => {
					IsContentLoading = true;
					using (var repo = new AnimeRepo()) {
						foreach (AnimeModel item in repo.FindEnumerable(entry => _SearchText.Length > 0 ? entry.ViewingStatus.Equals(_CurrentView) && (entry.TitleEnglish.ToLower().Contains(_SearchText) || entry.TitleRomaji.ToLower().Contains(_SearchText) || entry.TitleNative.ToLower().Contains(_SearchText)) : entry.ViewingStatus.Equals(_CurrentView)).ToArray()) {
							AnimeItems.Add(item);
							TotalItems += 1;
						}
					}
					IsContentLoading = false;
				});
			};
		}

		public DelegateCommand TriggerViewStatusWasChanged => new DelegateCommand(new Action<object>(e => PressedTransferButton = e as string));
		public DelegateCommand TriggerAdvancedWasClicked => new DelegateCommand(new Action(() => IsAdvancedVisible = !IsAdvancedVisible));

		public DelegateCommand ChangeItemSource => new DelegateCommand(
			new Action<object>((e) => {

				CurrentView = e as string;
				RenewView();

				if (e.Equals(StatusView.ONLINE)) { AddOnlineItemsToView.Execute(null); }
				else { AddLocalItemsToView(); }
			})
		);

		public DelegateCommand AddOnlineItemsToView => new DelegateCommand(
			new Action(async () => {
				aae.SearchString = SearchText;
				IsContentLoading = true;
				try {
					using (var repo = new AnimeRepo()) {
						var onlineItems = await aae.GetCurrentSet();
						var onlineItemsIds = onlineItems.Select(onlineItem => onlineItem.Id);

						var localItems = repo.FindEnumerable(localItem => onlineItemsIds.Contains(localItem.Id)).ToArray();

						for (int i = 0; i < localItems.Length; i++) {
							var currentItem = onlineItems.FirstOrDefault(onlineItem => onlineItem.Id == localItems[i].Id);
							currentItem.DateAdded = localItems[i].DateAdded;
							currentItem.RewatchCount = localItems[i].RewatchCount;
							currentItem.PersonalScore = localItems[i].PersonalScore;
							currentItem.ViewingStatus = localItems[i].ViewingStatus;
							currentItem.WatchPriority = localItems[i].WatchPriority;
							currentItem.PersonalReview = localItems[i].PersonalReview;
							currentItem.CurrentEpisode = localItems[i].CurrentEpisode;
						}

						for (int i = 0; i < onlineItems.Length; i++) {
							AnimeItems.Add(onlineItems[i]);
							TotalItems += 1;
						}

						APIEnumeratorHasNextPage = aae.HasNextPage;
					}
				} catch (ArgumentNullException ex) { ToastService.ShowToast(ex.Message, "danger");}
				catch (HttpRequestException) { ToastService.ShowToast("The server isn't responding.", "danger");}

				IsContentLoading = false;
			})
		);

		public DelegateCommand SaveItemToDb => new DelegateCommand(
			new Action(async () => {
				try {
					using (var repo = new AnimeRepo()) {
						SelectedItem.ViewingStatus = PressedTransferButton;
						_SelectedItem.DateAdded = DateTime.Now;
						var itemWorkResponse = await repo.AddOrUpdate(_SelectedItem);

						if (itemWorkResponse == RepoResponse.ADDED) ToastService.ShowToast($"Succesfully saved '{StringHelpers.GetFirstNotNullItemTitle(_SelectedItem)}' in '{_SelectedItem.ViewingStatus}' view!", "success");
						else if (itemWorkResponse == RepoResponse.UPDATED) ToastService.ShowToast($"Succesfully moved '{StringHelpers.GetFirstNotNullItemTitle(_SelectedItem)}' in '{_SelectedItem.ViewingStatus}' view!", "success");
						else throw new Exception("Repo returned ERROR");

						if (CurrentView == StatusView.ONLINE) {
							RenewView(true);
						} else {
							RenewView();
							AddLocalItemsToView();
						}
					}
				}
				catch (Exception ex) {
					ToastService.ShowToast($"Something went wrong: {ex.Message}.", "danger");
				}
			})
		);

		public DelegateCommand DeleteItemFromDb => new DelegateCommand(
			new Action(async () => {
				try {
					using (var repo = new AnimeRepo()) {
						var itemDeleteResponse = await repo.Delete(_SelectedItem.Id);
						if (itemDeleteResponse == RepoResponse.DELETED) ToastService.ShowToast($"Anime '{StringHelpers.GetFirstNotNullItemTitle(_SelectedItem)}' in '{_SelectedItem.ViewingStatus}' view was deleted succesfully!", "success");
						else throw new Exception($"Anime '{StringHelpers.GetFirstNotNullItemTitle(_SelectedItem)}' doesn't exist in '{_SelectedItem.ViewingStatus}' view.");

						if (CurrentView == StatusView.ONLINE) {
							RenewView(true);
						} else {
							RenewView();
							AddLocalItemsToView();
						}
					}
				} catch (Exception ex) {
					ToastService.ShowToast($"Something went wrong: {ex.Message}.", "danger");
				}
			})
		);
	}
}
