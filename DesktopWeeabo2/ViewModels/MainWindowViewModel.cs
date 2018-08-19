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
    class MainWindowViewModel : BaseViewModel {

        public MainWindowViewModel() {
            ViewModels = new ObservableCollection<BaseViewModel>(){
                new AnimeViewModel(),
                new MangaViewModel(),
                new SettingsViewModel()
            };
            ViewModelsView = CollectionViewSource.GetDefaultView(ViewModels);
        }

        private ICollectionView _ViewModelsView;
        public ICollectionView ViewModelsView {
            get { return _ViewModelsView; }
            set {
                _ViewModelsView = value;
                RaisePropertyChanged("ViewModelsView");
            }
        }

        private ObservableCollection<BaseViewModel> _ViewModels;
        public ObservableCollection<BaseViewModel> ViewModels {
            get { return _ViewModels; }
            set {
                _ViewModels = value;
                RaisePropertyChanged("ViewModels");
            }
        }

        public DelegateCommand ChangeView => new DelegateCommand(
                 new Action<object>(
                  (e) => {
                      if (ViewModelsView != null) {
                          switch (e) {
                              case "AnimeView":
                                  ViewModelsView.MoveCurrentToPosition(0);
                                  break;
                              case "MangaView":
                                  ViewModelsView.MoveCurrentToPosition(1);
                                  break;
                              case "SettingsView":
                                  ViewModelsView.MoveCurrentToPosition(2);
                                  break;
                              default:
                                  ViewModelsView.MoveCurrentToPosition(0);
                                  break;
                          }
                      }
                  }),
                 (e) => { return true; });
    }
}
