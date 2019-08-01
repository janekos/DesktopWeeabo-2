using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DesktopWeeabo2.ViewModels.Shared {
	public class NotifyUIBase : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] String propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
