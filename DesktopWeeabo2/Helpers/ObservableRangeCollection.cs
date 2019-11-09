using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace DesktopWeeabo2.Helpers {

	public class ObservableRangeCollection<T> : ObservableCollection<T> {

		public void AddRange(IEnumerable<T> newRange) {
			if (newRange != null) {
				foreach (T item in newRange)
					Items.Add(item);

				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
			}
		}
	}
}