using DesktopWeeabo2.Core.Enums;
using DesktopWeeabo2.Core.Interfaces.Repositories;
using DesktopWeeabo2.Core.Interfaces.Services;
using DesktopWeeabo2.Core.Models;
using DesktopWeeabo2.Infrastructure.API;
using DesktopWeeabo2.Infrastructure.Events;
using DesktopWeeabo2.Infrastructure.Jobs.Shared;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DesktopWeeabo2.Infrastructure.Jobs {
	public class DWOneImportJob : BaseJob<DWOneImportJob> {
		private IEnumerable<XElement> entries;

		private readonly int EntriesPerRequest = 50;
		private readonly IAnimeService _animeService;

		public DWOneImportJob(IAnimeService animeService) {
			_animeService = animeService;
		}

		/// <summary>
		/// args[0] = (string) path to mainentries.xml
		/// </summary>
		/// <param name="args"></param>
		protected override void PrepareJob(object[] args) {
			if (args.Length == 0 || args[0] == null || args[0].GetType() != typeof(string))
				throw new ArgumentNullException("Path to MainEntries.xml wasn't provided or is incorrect.");

			entries = XElement.Load((string) args[0]).Elements();
			jobMaxProgress = entries.Count() * 2;
			jobDescription = "Importing anime entries from DesktopWeeabo 1";
			jobTitle = "DW 1 Entry import";
		}

		protected override async Task ExecuteJob() {
			List<Task> requests = new List<Task>();
			AnimeAPIEnumerator api = new AnimeAPIEnumerator();
			ConcurrentBag<AnimeModel> persistableEntries = new ConcurrentBag<AnimeModel>();
			
			JobEvent.NotifyJobProgressChange(0, "Querying API");

			for (int i = 0; i < entries.Count(); i = i + EntriesPerRequest) {
				var currEntries = entries.Skip(i).Take(EntriesPerRequest);
				requests.Add(
					Task.Run(async () => {
						var requestResult = await api.GetByMalIdSet(currEntries.Select(entry => int.Parse(entry.Element("id").Value)).ToArray());

						foreach (AnimeModel entry in requestResult) {
							var currEntry = currEntries.Where(e => e.Element("id").Value == entry.IdMal.ToString()).FirstOrDefault();

							entry.DateAdded = DateTime.Now;

							var viewingStatus = currEntry.Element("viewingstatus").Value;

							switch (viewingStatus) {
								case "Watched":
									viewingStatus = StatusView.VIEWED;
									break;

								case "Dropped":
									viewingStatus = StatusView.DROPPEDANIME;
									break;

								case "Watching":
									viewingStatus = StatusView.WATCHING;
									break;

								default:
								case "To Watch":
									viewingStatus = StatusView.TOWATCH;
									break;
							}

							entry.ViewingStatus = viewingStatus;

							string review = currEntry.Element("review").Value;
							bool isReviewEmpty = string.IsNullOrEmpty(review);
							string dropReason = currEntry.Element("dropreason").Value;
							bool isDropReasonEmpty = string.IsNullOrEmpty(dropReason);

							if (!isReviewEmpty && isDropReasonEmpty)
								entry.PersonalReview = review;
							else if (isReviewEmpty && !isDropReasonEmpty)
								entry.PersonalReview = dropReason;
							else if (!isReviewEmpty && !isDropReasonEmpty)
								entry.PersonalReview = $"--- DW1 REVIEW ---{Environment.NewLine}{Environment.NewLine}{review}{Environment.NewLine}{Environment.NewLine}--- DW1 DROP REASON ---{Environment.NewLine}{Environment.NewLine}{dropReason}";

							if (double.TryParse(currEntry.Element("personal_score").Value, out double personalScore) && personalScore != -1)
								entry.PersonalScore = (int) (personalScore * 10);

							if (int.TryParse(currEntry.Element("currepisode").Value, out int currentEpisode))
								entry.CurrentEpisode = currentEpisode;

							if (int.TryParse(currEntry.Element("watch_priority").Value, out int watchPriority) && watchPriority != -1)
								entry.WatchPriority = watchPriority;

							persistableEntries.Add(entry);
							JobEvent.NotifyJobProgressChange(1, isIncremental: true);
						}
					}));
			}

			await Task.WhenAll(requests);

			JobEvent.NotifyJobProgressChange(0, "Saving results", true);

			await _animeService.AddOrUpdateRange(persistableEntries, (progress) => {
				JobEvent.NotifyJobProgressChange((int)progress, isIncremental: true);
			});
		}
	}
}
