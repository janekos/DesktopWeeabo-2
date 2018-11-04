using DesktopWeeabo2.API;
using DesktopWeeabo2.Data;
using DesktopWeeabo2.Data.Repositories;
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
        private AnimeModel _SelectedItem = null;

        public ObservableCollection<AnimeModel> AnimeItems { get; set; } = new ObservableCollection<AnimeModel>();

		public AnimeModel SelectedItem {
            get { return _SelectedItem; }
            set {
                if (_SelectedItem != value) {
                    _SelectedItem = value;
                    RaisePropertyChanged("SelectedItem");
                }
            }
        }

        public AnimeViewModel() : base() {
            BindingOperations.EnableCollectionSynchronization(AnimeItems, _CollectionLock);
        }

        protected override void Property_Changed(object sender, PropertyChangedEventArgs e) {
            switch (e.PropertyName) {
                case "SearchText":

                    RenewView();

                    if (_CurrentView.Equals(StatusView.Online)) { AddOnlineItemsToView(); }
                    else { AddLocalItemsToView(); }
                    break;

                case "SelectedItem":
                    if (_SelectedItem != null) {

                    }
                    break;

                default:
                    break;
            }
        }

        protected override void RenewView() {
			AnimeItems.Clear();
            _SelectedItem = null;
            TotalItems = 0;
			IsContentLoading = false;
        }

        protected override void AddLocalItemsToView() {

            lock (_CollectionLock) {
                Task.Run(() => {
					IsContentLoading = true;
                    using (var db = new EntriesContext()) {

                        IQueryable entries;

                        if (_SearchText.Length > 0) { entries = db.AnimeItems.Where(entry => entry.ViewingStatus.Equals(_CurrentView) && (entry.TitleEnglish.ToLower().Contains(_SearchText) || entry.TitleRomaji.ToLower().Contains(_SearchText) || entry.TitleNative.ToLower().Contains(_SearchText))); }
                        else { entries = db.AnimeItems.Where(entry => entry.ViewingStatus.Equals(_CurrentView)); }

                        foreach (AnimeModel item in entries) { AnimeItems.Add(item); TotalItems += 1; }
                    }
					IsContentLoading = false;
                });
            };
        }

        protected override async void AddOnlineItemsToView() {

            aae.SearchString = _SearchText;
			IsContentLoading = true;
            try {
                foreach (var item in await aae.GetCurrentSet()) {
                    AnimeItems.Add(item);
                    TotalItems += 1;
                }

                while (aae.TryMoveToNextSet()) {
                    foreach (var item in await aae.GetCurrentSet()) {
                        AnimeItems.Add(item);
                        TotalItems += 1;
                    }
                }

            }
            catch (ArgumentNullException ex) {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
			IsContentLoading = false;
		}
        public DelegateCommand SaveItemToDb => new DelegateCommand(
            new Action<object>(async (e) => {
                bool didUpdate = await Task.Run(() => {
                    var repo = new AnimeRepo(new EntriesContext());
					try {
						_SelectedItem.ViewingStatus = e.ToString();
						if (repo.GetById(_SelectedItem.Id) == null) {
							repo.Add(_SelectedItem);
							ToastService.ShowToast("Succesfully saved \"" + (_SelectedItem.TitleEnglish?.Length > 0 ?
																		   _SelectedItem.TitleEnglish :
																		   _SelectedItem.TitleRomaji?.Length > 0 ?
																		   _SelectedItem.TitleRomaji :
																		   _SelectedItem.TitleNative) + "\" in \"" + _SelectedItem.ViewingStatus + "\" view!", "success");
						} else {
							repo.Update(_SelectedItem);
							ToastService.ShowToast("Succesfully moved \"" + (_SelectedItem.TitleEnglish?.Length > 0 ?
																			 _SelectedItem.TitleEnglish :
																			 _SelectedItem.TitleRomaji?.Length > 0 ?
																			 _SelectedItem.TitleRomaji :
																			 _SelectedItem.TitleNative) + "\" to \"" + _SelectedItem.ViewingStatus + "\" view!", "success");
						}

						return true;
					} catch (Exception ex) {
						ToastService.ShowToast("Something went wrong: " + ex.Message, "danger");
						return false;
					}
                });

                if (didUpdate) {
                    if (_CurrentView != StatusView.Online) {
                        RenewView();
                        AddLocalItemsToView();
                    }
				}

            }),
            (e) => { return true; }
        );

		public DelegateCommand DeleteItemFromDb => new DelegateCommand(
			new Action<object>(async (e) => {
				bool didUpdate = await Task.Run(() => {
					var repo = new AnimeRepo(new EntriesContext());
					try {

					if (repo.GetById(_SelectedItem.Id) != null) {
						repo.Delete(_SelectedItem.Id);
						ToastService.ShowToast("Anime \"" + (_SelectedItem.TitleEnglish?.Length > 0 ?
														   _SelectedItem.TitleEnglish :
														   _SelectedItem.TitleRomaji?.Length > 0 ?
														   _SelectedItem.TitleRomaji :
														   _SelectedItem.TitleNative) + "\" in \"" + _SelectedItem.ViewingStatus + "\" was deleted succesfully!", "success");
					} else {
						ToastService.ShowToast("Anime \"" + (_SelectedItem.TitleEnglish?.Length > 0 ?
														   _SelectedItem.TitleEnglish :
														   _SelectedItem.TitleRomaji?.Length > 0 ?
														   _SelectedItem.TitleRomaji :
														   _SelectedItem.TitleNative) + "\" doesn't exist in \"" + _SelectedItem.ViewingStatus + "\" view.", "danger");
					}

					return true;
					} catch (Exception ex) {
						ToastService.ShowToast("Something went wrong: " + ex.Message, "danger");
						return false;
					}
				});

				if (didUpdate) {
					if (_CurrentView != StatusView.Online) {
						RenewView();
						AddLocalItemsToView();
					}
				}

			}),
			(e) => { return true; }
		);
	}
}
