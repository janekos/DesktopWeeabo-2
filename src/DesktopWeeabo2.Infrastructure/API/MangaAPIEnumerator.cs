using DesktopWeeabo2.Core.API.Models;
using DesktopWeeabo2.Core.API.Models.JsonTypes;
using DesktopWeeabo2.Core.API.Models.Shared;
using DesktopWeeabo2.Core.Models;
using DesktopWeeabo2.Infrastructure.API.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DesktopWeeabo2.Infrastructure.API {

	public class MangaApiEnumerator : ApiEnumerator<MangaModel> {

		public MangaApiEnumerator() {
			IsAnimeType = false;
		}

		protected override MangaModel[] GetItems(string result, bool autoIncrementPage = true) {
			var apiResult = JsonConvert.DeserializeObject<ApiReturnValue<MangaApiModel>>(result);

			if (apiResult.Data == null)
				throw new ArgumentException("Server returned nothing.");

			HasNextPage = apiResult.Data.Page?.PageInfo?.HasNextPage ?? false;
			TotalItems = apiResult.Data.Page?.PageInfo?.Total ?? 0;
			LastPage = apiResult.Data.Page?.PageInfo?.LastPage ?? 0;

			if (autoIncrementPage && HasNextPage)
				CurrentPage += 1;

			var returnable = apiResult.Data.Page.Media;

			AdjustResult(returnable);

			return Array.ConvertAll(returnable, r => (MangaModel) r);
		}

		private void AdjustResult(IEnumerable<MangaApiModel> apiModelList) {
			foreach (MangaApiModel model in apiModelList) {
				// add mal and anilist links to externallink list
				model.ExternalLinks.AddRange(new ExternalLink[] {
					new ExternalLink {
						Url = $"https://myanimelist.net/{model.Type.ToLower()}/{model.IdMal}",
						Site = "MAL"
					},
					new ExternalLink {
						Url = model.siteUrl,
						Site = "Anilist"
					}
				});
			}
		}
	}
}