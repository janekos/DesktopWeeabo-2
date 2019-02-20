using DesktopWeeabo2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static DesktopWeeabo2.Models.SearchModel;

namespace DesktopWeeabo2.CustomControls {
	public class AdvancedSearch : Control {
		static AdvancedSearch() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(AdvancedSearch), new FrameworkPropertyMetadata(typeof(AdvancedSearch)));
		}

		public bool ASIsVisible {
			get { return (bool)GetValue(ASIsVisibleProperty); }
			set { SetValue(ASIsVisibleProperty, value); }
		}
		public static readonly DependencyProperty ASIsVisibleProperty = DependencyProperty.Register("ASIsVisible", typeof(bool), typeof(AdvancedSearch), new PropertyMetadata(null));

		public bool ASIsDescending {
			get { return (bool)GetValue(ASIsDescendingProperty); }
			set { SetValue(ASIsDescendingProperty, value); }
		}
		public static readonly DependencyProperty ASIsDescendingProperty = DependencyProperty.Register("ASIsDescending", typeof(bool), typeof(AdvancedSearch), new PropertyMetadata(null));

		public bool ASIsAdult {
			get { return (bool)GetValue(ASIsAdultProperty); }
			set { SetValue(ASIsAdultProperty, value); }
		}
		public static readonly DependencyProperty ASIsAdultProperty = DependencyProperty.Register("ASIsAdult", typeof(bool), typeof(AdvancedSearch), new PropertyMetadata(null));

		public SortObject ASSelectedSort {
			get { return (SortObject)GetValue(ASSelectedSortProperty); }
			set { SetValue(ASSelectedSortProperty, value); }
		}
		public static readonly DependencyProperty ASSelectedSortProperty = DependencyProperty.Register("ASSelectedSort", typeof(SortObject), typeof(AdvancedSearch), new PropertyMetadata(null));

		public string ASSelectedGenres {
			get { return (string)GetValue(ASSelectedGenresProperty); }
			set { SetValue(ASSelectedGenresProperty, value); }
		}
		public static readonly DependencyProperty ASSelectedGenresProperty = DependencyProperty.Register("ASSelectedGenres", typeof(string), typeof(AdvancedSearch), new PropertyMetadata(null));

		public string ASCurrentView {
			get { return (string)GetValue(ASCurrentViewProperty); }
			set { SetValue(ASCurrentViewProperty, value); }
		}
		public static readonly DependencyProperty ASCurrentViewProperty = DependencyProperty.Register("ASCurrentView", typeof(string), typeof(AdvancedSearch), new PropertyMetadata(null));

		public SortObject[] ASSortsList {
			get { return (SortObject[])GetValue(ASSortsListProperty); }
			set { SetValue(ASSortsListProperty, value); }
		}
		public static readonly DependencyProperty ASSortsListProperty = DependencyProperty.Register("ASSortsList", typeof(SortObject[]), typeof(AdvancedSearch), new PropertyMetadata(null));

		public GenreObject[] ASGenresList {
			get { return (GenreObject[])GetValue(ASGenresListProperty); }
			set { SetValue(ASGenresListProperty, value); }
		}
		public static readonly DependencyProperty ASGenresListProperty = DependencyProperty.Register("ASGenresList", typeof(GenreObject[]), typeof(AdvancedSearch), new PropertyMetadata(null));

		public ICommand ASSetChecked {
			get { return (ICommand)GetValue(ASSetCheckedProperty); }
			set { SetValue(ASSetCheckedProperty, value); }
		}
		public static readonly DependencyProperty ASSetCheckedProperty = DependencyProperty.Register("ASSetChecked", typeof(ICommand), typeof(AdvancedSearch), new PropertyMetadata(null));

		public ICommand ASHideComponent {
			get { return (ICommand)GetValue(ASHideComponentProperty); }
			set { SetValue(ASHideComponentProperty, value); }
		}
		public static readonly DependencyProperty ASHideComponentProperty = DependencyProperty.Register("ASHideComponent", typeof(ICommand), typeof(AdvancedSearch), new PropertyMetadata(null));

		public ICommand ASClearFilter {
			get { return (ICommand)GetValue(ASClearFilterProperty); }
			set { SetValue(ASClearFilterProperty, value); }
		}
		public static readonly DependencyProperty ASClearFilterProperty = DependencyProperty.Register("ASClearFilter", typeof(ICommand), typeof(AdvancedSearch), new PropertyMetadata(null));
	}
}
