using DesktopWeeabo2.API;
using DesktopWeeabo2.Data;
using DesktopWeeabo2.Data.Repositories;
using DesktopWeeabo2.Data.Repositories.Shared;
using DesktopWeeabo2.Helpers;
using DesktopWeeabo2.Models;
using DesktopWeeabo2.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DesktopWeeabo2.ViewModels {
	class AnimeViewModel : BaseItemViewModel {

		private AnimeAPIEnumerator aae = new AnimeAPIEnumerator("", true, "");
		public ObservableCollection<AnimeModel> AnimeItems { get; set; } = new ObservableCollection<AnimeModel>();

		private AnimeModel _SelectedItem = null;
		public AnimeModel SelectedItem {
			get { return _SelectedItem; }
			set { if (_SelectedItem != value) { _SelectedItem = value; RaisePropertyChanged("SelectedItem"); } }
		}

		// stick personal review etc in selected item

		public AnimeViewModel() : base() { BindingOperations.EnableCollectionSynchronization(AnimeItems, _CollectionLock); }

		protected override void Property_Changed(object sender, PropertyChangedEventArgs e) {
			switch (e.PropertyName) {
				case "SearchText":
					RenewView();
					if (_CurrentView.Equals(StatusView.Online)) AddOnlineItemsToView.Execute(null);
					else AddLocalItemsToView();
					break;
				default:
					break;
			}
		}

		protected override void RenewView() {
			AnimeItems.Clear();
			SelectedItem = null;
			TotalItems = 0;
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

		public DelegateCommand ChangeItemSource => new DelegateCommand(
			new Action<object>((e) => {

				CurrentView = e as string;
				RenewView();

				if (e.Equals(StatusView.Online)) { AddOnlineItemsToView.Execute(null); }
				else { AddLocalItemsToView(); }
			})
		);

		public DelegateCommand AddOnlineItemsToView => new DelegateCommand(
			new Action(async () => {
				aae.SearchString = SearchText;
				IsContentLoading = true;
				try {
					foreach (var item in await aae.GetCurrentSet()) {
						AnimeItems.Add(item);
						TotalItems += 1;
					}

					APIEnumeratorHasNextPage = aae.HasNextPage;

				} catch (ArgumentNullException ex) {
					System.Diagnostics.Debug.WriteLine(ex.StackTrace);
				}
				IsContentLoading = false;
			})
		);

		public DelegateCommand SaveItemToDb => new DelegateCommand(
			new Action<object>(async (e) => {
				try {
					using (var repo = new AnimeRepo()) {
						SelectedItem.ViewingStatus = e.ToString();
						if (await repo.Get(_SelectedItem.Id) == null) {
							repo.Add(_SelectedItem);
							ToastService.ShowToast($"Succesfully saved '{StringHelpers.GetFirstNotNullItemTitle(_SelectedItem)}' in '{_SelectedItem.ViewingStatus}' view!", "success");
						} else {
							repo.Update(_SelectedItem);
							ToastService.ShowToast($"Succesfully moved '{StringHelpers.GetFirstNotNullItemTitle(_SelectedItem)}' in '{_SelectedItem.ViewingStatus}' view!", "success");
						}

						if (CurrentView != StatusView.Online) {
							RenewView();
							AddLocalItemsToView();
						}
					}						
				} catch (Exception ex) {
					ToastService.ShowToast($"Something went wrong: {ex.Message}.", "danger");
				}
			}),
			(e) => { return true; }
		);

		public DelegateCommand DeleteItemFromDb => new DelegateCommand(
			new Action<object>(async (e) => {
				try {
					using (var repo = new AnimeRepo()) {
						if (await repo.Get(_SelectedItem.Id) != null) {
							repo.Delete(_SelectedItem.Id);
							ToastService.ShowToast($"Anime '{StringHelpers.GetFirstNotNullItemTitle(_SelectedItem)}' in '{_SelectedItem.ViewingStatus}' view was deleted succesfully!", "success");
						} else {
							ToastService.ShowToast($"Anime '{StringHelpers.GetFirstNotNullItemTitle(_SelectedItem)}' doesn't exist in '{_SelectedItem.ViewingStatus}' view.", "danger");
						}

						if (CurrentView != StatusView.Online) {
							RenewView();
							AddLocalItemsToView();
						}
					}
				} catch (Exception ex) {
					ToastService.ShowToast($"Something went wrong: {ex.Message}.", "danger");
				}
			}),
			(e) => { return true; }
		);
	}
}
