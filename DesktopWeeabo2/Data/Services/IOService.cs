using DesktopWeeabo2.API;
using DesktopWeeabo2.Data.Services.Shared;
using DesktopWeeabo2.Helpers;
using DesktopWeeabo2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DesktopWeeabo2.Data.Services {
	public class IOService {
		private readonly IService<AnimeModel> _animeService;
		private readonly int EntriesPerRequest = 50;

		public IOService(IService<AnimeModel> animeService) {
			_animeService = animeService;
		}

		public async void ImportDW1Data(string path) {
			IEnumerable<XElement> entries = XElement.Load(path).Elements();
			List<AnimeModel> persistableEntries = new List<AnimeModel>();
			List<Task> requests = new List<Task>();
			AnimeAPIEnumerator AnimeAPIEnumerator = new AnimeAPIEnumerator();

			for (int i = 0; i < entries.Count(); i = i + EntriesPerRequest) {
				var currEntries = entries.Skip(i).Take(EntriesPerRequest);
				requests.Add(Task.Run(async () => {
					var requestResult = await AnimeAPIEnumerator.GetByMalIdSet(currEntries.Select(entry => int.Parse(entry.Element("id").Value)).ToArray());

					foreach (AnimeModel entry in requestResult) {
						var currEntry = currEntries.Where(e => e.Element("id").Value == entry.IdMal.ToString()).FirstOrDefault();

						entry.DateAdded = DateTime.Now;

						var viewingStatus = currEntry.Element("viewingstatus").Value;

						switch (viewingStatus) {
							case "Watched": viewingStatus = StatusView.VIEWED; break;
							case "Dropped": viewingStatus = StatusView.DROPPEDANIME; break;
							case "Watching": viewingStatus = StatusView.WATCHING; break;
							default:
							case "To Watch": viewingStatus = StatusView.TOWATCH; break;
						}

						entry.ViewingStatus = viewingStatus;

						string review = currEntry.Element("review").Value;
						bool isReviewEmpty = string.IsNullOrEmpty(review);
						string dropReason = currEntry.Element("dropreason").Value;
						bool isDropReasonEmpty = string.IsNullOrEmpty(dropReason);

						if (!isReviewEmpty && isDropReasonEmpty) entry.PersonalReview = review;
						else if (isReviewEmpty && !isDropReasonEmpty) entry.PersonalReview = dropReason;
						else if (!isReviewEmpty && !isDropReasonEmpty) entry.PersonalReview = $"--- DW1 REVIEW ---\n\n{review}\n\n--- DW1 DROP REASON ---\n\n{dropReason}";

						if (double.TryParse(currEntry.Element("personal_score").Value, out double personalScore) && personalScore != -1)
							entry.PersonalScore = (int)(personalScore * 10);

						if (int.TryParse(currEntry.Element("currepisode").Value, out int currentEpisode))
							entry.CurrentEpisode = currentEpisode;

						if (int.TryParse(currEntry.Element("watch_priority").Value, out int watchPriority) && watchPriority != -1)
							entry.WatchPriority = watchPriority;

						persistableEntries.Add(entry);
					}
				}));
			}

			await Task.WhenAll(requests);

			foreach (AnimeModel a in persistableEntries) {
				await _animeService.AddOrUpdate(a);
			}
		}
	}
}
