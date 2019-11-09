using System.Windows;
using System.Windows.Controls;

namespace DesktopWeeabo2.CustomControls {
	public class JobDialog : Control {
		static JobDialog() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(JobDialog), new FrameworkPropertyMetadata(typeof(JobDialog)));
		}

		public bool IsJobRunning {
			get { return (bool) GetValue(IsJobRunningProperty); }
			set { SetValue(IsJobRunningProperty, value); }
		}

		public static readonly DependencyProperty IsJobRunningProperty = DependencyProperty.Register("IsJobRunning", typeof(bool), typeof(JobDialog), new PropertyMetadata(null));

		public int JobProgressMinimum {
			get { return (int) GetValue(JobProgressMinimumProperty); }
			set { SetValue(JobProgressMinimumProperty, value); }
		}

		public static readonly DependencyProperty JobProgressMinimumProperty = DependencyProperty.Register("JobProgressMinimum", typeof(int), typeof(JobDialog), new PropertyMetadata(0));

		public int JobProgressMaximum {
			get { return (int) GetValue(JobProgressMaximumProperty); }
			set { SetValue(JobProgressMaximumProperty, value); }
		}

		public static readonly DependencyProperty JobProgressMaximumProperty = DependencyProperty.Register("JobProgressMaximum", typeof(int), typeof(JobDialog), new PropertyMetadata(null));

		public int JobProgressCurrent {
			get { return (int) GetValue(JobProgressCurrentProperty); }
			set { SetValue(JobProgressCurrentProperty, value); }
		}

		public static readonly DependencyProperty JobProgressCurrentProperty = DependencyProperty.Register("JobProgressCurrent", typeof(int), typeof(JobDialog), new PropertyMetadata(null));

		public string JobDescription {
			get { return (string) GetValue(JobDescriptionProperty); }
			set { SetValue(JobDescriptionProperty, value); }
		}

		public static readonly DependencyProperty JobDescriptionProperty = DependencyProperty.Register("JobDescription", typeof(string), typeof(JobDialog), new PropertyMetadata(null));
	}
}
