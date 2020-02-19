using LiveCharts.Wpf.Charts.Base;

namespace DesktopWeeabo2.ViewModels {
	public class StatisticsViewModel {
		public string HoursSpentWatchingAnimes { get; set; } = "Not calculated";
		public string HoursNeededToWatchRemainingAnimes { get; set; } = "Not calculated";
		public string AnimesToWatch { get; set; } = "Not calculated";
		public string AnimesDropped { get; set; } = "Not calculated";
		public string AnimesViewed { get; set; } = "Not calculated";

		public string HoursSpentReadingMangas { get; set; } = "Not calculated";
		public string HoursNeededToReadRemainingMangas { get; set; } = "Not calculated";
		public string MangasToRead { get; set; } = "Not calculated";
		public string MangasDropped { get; set; } = "Not calculated";
		public string MangasRed { get; set; } = "Not calculated";

		public Chart Chart { get; set; }
	}
}
