using DesktopWeeabo2.Core.Enums;
using DesktopWeeabo2.Core.Interfaces.Services;
using DesktopWeeabo2.Core.Models;
using DesktopWeeabo2.Infrastructure.API;
using DesktopWeeabo2.Infrastructure.Events;
using DesktopWeeabo2.Infrastructure.Jobs.Shared;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Infrastructure.Jobs {
	public class UpdateDbEntries : BaseJob<UpdateDbEntries> {
		private readonly int EntriesPerRequest = 50;
		private readonly IAnimeService animeService;
		private readonly IMangaService mangaService;

		private IEnumerable<AnimeModel> animes;
		private IEnumerable<MangaModel> mangas;

		public UpdateDbEntries(IAnimeService animeService, IMangaService mangaService) {
			this.animeService = animeService;
			this.mangaService = mangaService;
		}

		protected override void PrepareJob(object[] args) {
			var updateOnlyUnfinishedEntries = args.Length != 0 && args[0] != null && args[0].GetType() == typeof(bool) && (bool) args[0] == true;

			if (updateOnlyUnfinishedEntries) {
				animes = animeService.GetCustom(e => e.Status != (int)ContentStatus.FINISHED);
				mangas = mangaService.GetCustom(e => e.Status != (int)ContentStatus.FINISHED);
			} else {
				animes = animeService.GetAll();
				mangas = mangaService.GetAll();
			}

			jobMaxProgress = (animes.Count() + mangas.Count()) * 2;
			jobDescription = "Updating saved animes and mangas with latest info from anilst";
			jobTitle = "Animes and mangas update";
		}

		protected override async Task ExecuteJob() {
			
			if (animes.Count() > 0) {
				ConcurrentBag<AnimeModel> updatedAnimeEntries = new ConcurrentBag<AnimeModel>();
				AnimeAPIEnumerator animeApi = new AnimeAPIEnumerator();
				List<Task> animeRequests = new List<Task>();

				JobEvent.NotifyJobProgressChange(0, "Querying animes");

				for (int i = 0; i < animes.Count(); i = i + EntriesPerRequest) {
					var currEntries = animes.Skip(i).Take(EntriesPerRequest);
					animeRequests.Add(
						Task.Run(async () => {
							var requestResult = await animeApi.GetByIdSet(currEntries.Select(e => e.Id).ToArray());
							
							foreach (AnimeModel entry in requestResult) {
								updatedAnimeEntries.Add(entry);
								JobEvent.NotifyJobProgressChange(1, isIncremental: true);
							}
						}));
				}

				await Task.WhenAll(animeRequests);

				JobEvent.NotifyJobProgressChange(0, "Updating animes", true);

				animeService.AddOrUpdateRange(updatedAnimeEntries, (progress) => {
					JobEvent.NotifyJobProgressChange((int) progress, isIncremental: true);
				});

				await Task.Delay(100);
			}

			if (mangas.Count() > 0) {
				ConcurrentBag<MangaModel> updatedMangaEntries = new ConcurrentBag<MangaModel>();
				MangaAPIEnumerator mangaApi = new MangaAPIEnumerator();
				List<Task> mangaRequests = new List<Task>();

				JobEvent.NotifyJobProgressChange(0, "Querying mangas", true);

				for (int i = 0; i < mangas.Count(); i = i + EntriesPerRequest) {
					var currEntries = mangas.Skip(i).Take(EntriesPerRequest);
					mangaRequests.Add(
						Task.Run(async () => {
							var requestResult = await mangaApi.GetByIdSet(currEntries.Select(e => e.Id).ToArray(), false);

							foreach (MangaModel entry in requestResult) {
								updatedMangaEntries.Add(entry);
								JobEvent.NotifyJobProgressChange(1, isIncremental: true);
							}
						}));
				}

				await Task.WhenAll(mangaRequests);

				JobEvent.NotifyJobProgressChange(0, "Updating mangas", true);

				mangaService.AddOrUpdateRange(updatedMangaEntries, (progress) => {
					JobEvent.NotifyJobProgressChange((int) progress, isIncremental: true);
				});
			}
		}
	}
}
