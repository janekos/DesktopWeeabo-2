using DesktopWeeabo2.Core.Enums;
using DesktopWeeabo2.Core.Interfaces.Misc;
using DesktopWeeabo2.Core.Interfaces.Services;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace DesktopWeeabo2.Helpers {

	public class CanvasRoutines : IDefineCanvasRoutines<Chart> {
		private readonly IAnimeService animeService;
		private readonly IMangaService mangaService;

		public CanvasRoutines(IAnimeService animeService, IMangaService mangaService) {
			this.animeService = animeService;
			this.mangaService = mangaService;
		}

		public Chart GetRoutineData(CanvasRoutine routine) {
			switch (routine) {
				case CanvasRoutine.MEDIA_CONSUMED_PER_MONTH:
					return MediaConsumedPerMonth();
			}

			throw new ArgumentException("Routine either null or incorrect", nameof(routine));
		}

		private Chart MediaConsumedPerMonth() {
			var chart = new CartesianChart {
				DisableAnimations = true
			};

			var animeStats = animeService.GetCustom(a => a.ViewingStatus.Equals(StatusView.VIEWED) && a.DateAdded != null)
				.GroupBy(a => new { GroupDate = a.DateAdded?.ToString("MMMM yyyy") })
				.Select(a => new {
					Label = a.Key.GroupDate,
					Items = a.Count()
				});

			var mangaStats = mangaService.GetCustom(a => a.ReadingStatus.Equals(StatusView.READ) && a.DateAdded != null)
				.GroupBy(a => new { GroupDate = a.DateAdded?.ToString("MMMM yyyy") })
				.Select(a => new {
					Label = a.Key.GroupDate,
					Items = a.Count()
				});

			var earliestDate = animeStats.Select(aS => aS.Label)
						.Concat(mangaStats.Select(ms => ms.Label))
						.Select(l => DateTime.Parse(l))
						.DefaultIfEmpty(DateTime.Now)
						.Min();

			var maxDate = DateTime.Parse(DateTime.Now.ToString("MMMM yyyy")).AddMonths(1);

			var animes = new ChartValues<double>();
			var mangas = new ChartValues<double>();
			var labels = new List<string>();

			for (var currDate = earliestDate; currDate < maxDate; currDate = currDate.AddMonths(1)) {
				var currStringDate = currDate.ToString("MMMM yyyy");
				animes.Add(animeStats.FirstOrDefault(aS => aS.Label.Equals(currStringDate))?.Items ?? 0);
				mangas.Add(mangaStats.FirstOrDefault(ms => ms.Label.Equals(currStringDate))?.Items ?? 0);
				labels.Add(currStringDate);
			}

			chart.AxisX.Add(new Axis {
				Foreground = Brushes.Black,
				FontSize = 14,
				Labels = labels,
				Separator = new Separator { Step = 1 },
				LabelsRotation = 45
			});

			chart.AxisY.Add(new Axis {
				MinValue = 0,
				Foreground = Brushes.Black,
				FontSize = 14
			});

			chart.Series.Add(new ColumnSeries {
				Values = animes,
				Title = "Animes viewed",
				ColumnPadding = 5
			});

			chart.MinWidth = labels.Count / 2 * 100;

			chart.Series.Add(new ColumnSeries {
				Values = mangas,
				Title = "Mangas read",
				ColumnPadding = 5
			});

			return chart;
		}
	}
}