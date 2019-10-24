using DesktopWeeabo2.Core.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DesktopWeeabo2.CustomControls {
	public class AdvancedSearch : Control {
		static AdvancedSearch() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(AdvancedSearch), new FrameworkPropertyMetadata(typeof(AdvancedSearch)));
		}

		public new bool IsVisible {
			get { return (bool)GetValue(IsVisibleProperty); }
			set { SetValue(IsVisibleProperty, value); }
		}
		public new static readonly DependencyProperty IsVisibleProperty = DependencyProperty.Register("IsVisible", typeof(bool), typeof(AdvancedSearch), new PropertyMetadata(null));

		public bool IsDescending {
			get { return (bool)GetValue(IsDescendingProperty); }
			set { SetValue(IsDescendingProperty, value); }
		}
		public static readonly DependencyProperty IsDescendingProperty = DependencyProperty.Register("IsDescending", typeof(bool), typeof(AdvancedSearch), new PropertyMetadata(null));

		public bool IsAdult {
			get { return (bool)GetValue(IsAdultProperty); }
			set { SetValue(IsAdultProperty, value); }
		}
		public static readonly DependencyProperty IsAdultProperty = DependencyProperty.Register("IsAdult", typeof(bool), typeof(AdvancedSearch), new PropertyMetadata(null));

		public SortObject SelectedSort {
			get { return (SortObject)GetValue(SelectedSortProperty); }
			set { SetValue(SelectedSortProperty, value); }
		}
		public static readonly DependencyProperty SelectedSortProperty = DependencyProperty.Register("SelectedSort", typeof(SortObject), typeof(AdvancedSearch), new PropertyMetadata(null));

		public string SelectedGenres {
			get { return (string)GetValue(SelectedGenresProperty); }
			set { SetValue(SelectedGenresProperty, value); }
		}
		public static readonly DependencyProperty SelectedGenresProperty = DependencyProperty.Register("SelectedGenres", typeof(string), typeof(AdvancedSearch), new PropertyMetadata(null));

		public string CurrentView {
			get { return (string)GetValue(CurrentViewProperty); }
			set { SetValue(CurrentViewProperty, value); }
		}
		public static readonly DependencyProperty CurrentViewProperty = DependencyProperty.Register("CurrentView", typeof(string), typeof(AdvancedSearch), new PropertyMetadata(null));

		public SortObject[] SortsList {
			get { return (SortObject[])GetValue(SortsListProperty); }
			set { SetValue(SortsListProperty, value); }
		}
		public static readonly DependencyProperty SortsListProperty = DependencyProperty.Register("SortsList", typeof(SortObject[]), typeof(AdvancedSearch), new PropertyMetadata(null));

		public GenreObject[] GenresList {
			get { return (GenreObject[])GetValue(GenresListProperty); }
			set { SetValue(GenresListProperty, value); }
		}
		public static readonly DependencyProperty GenresListProperty = DependencyProperty.Register("GenresList", typeof(GenreObject[]), typeof(AdvancedSearch), new PropertyMetadata(null));

		public ICommand SetChecked {
			get { return (ICommand)GetValue(SetCheckedProperty); }
			set { SetValue(SetCheckedProperty, value); }
		}
		public static readonly DependencyProperty SetCheckedProperty = DependencyProperty.Register("SetChecked", typeof(ICommand), typeof(AdvancedSearch), new PropertyMetadata(null));

		public ICommand HideComponent {
			get { return (ICommand)GetValue(HideComponentProperty); }
			set { SetValue(HideComponentProperty, value); }
		}
		public static readonly DependencyProperty HideComponentProperty = DependencyProperty.Register("HideComponent", typeof(ICommand), typeof(AdvancedSearch), new PropertyMetadata(null));

		public ICommand ClearFilter {
			get { return (ICommand)GetValue(ClearFilterProperty); }
			set { SetValue(ClearFilterProperty, value); }
		}
		public static readonly DependencyProperty ClearFilterProperty = DependencyProperty.Register("ClearFilter", typeof(ICommand), typeof(AdvancedSearch), new PropertyMetadata(null));
	}
}
