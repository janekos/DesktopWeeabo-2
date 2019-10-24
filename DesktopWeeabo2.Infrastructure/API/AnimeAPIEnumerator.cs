using DesktopWeeabo2.Core.API.Models;
using DesktopWeeabo2.Core.API.Models.Shared;
using DesktopWeeabo2.Core.Models;
using DesktopWeeabo2.Infrastructure.API.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DesktopWeeabo2.Infrastructure.API {
	public class AnimeAPIEnumerator : APIEnumerator<AnimeModel> {

		public AnimeAPIEnumerator() {
			Type = true;
		}

		protected override AnimeModel[] GetItems(string result) {

			var apiResult = JsonConvert.DeserializeObject<ApiReturnValue<AnimeApiModel>>(result);

			if (apiResult.Data == null) throw new ArgumentException("Server returned nothing.");

			HasNextPage = apiResult.Data.Page?.PageInfo?.HasNextPage ?? false;
			TotalItems = apiResult.Data.Page?.PageInfo?.Total ?? 0;

			if (HasNextPage) CurrentPage = CurrentPage + 1;

			var returnable = apiResult.Data.Page.Media;

			AdjustResult(returnable);

			return Array.ConvertAll(returnable, r => (AnimeModel)r);
		}

		private void AdjustResult(IEnumerable<AnimeApiModel> apiModelList) {
			foreach(AnimeApiModel model in apiModelList) {
				// add mal and anilist links to externallink list
				model.ExternalLinks.AddRange(new Core.API.Models.JsonTypes.ExternalLink[] {
					new Core.API.Models.JsonTypes.ExternalLink {
						Url = $"https://myanimelist.net/{model.Type.ToLower()}/{model.IdMal}",
						Site = "MAL"
					},
					new Core.API.Models.JsonTypes.ExternalLink {
						Url = model.siteUrl,
						Site = "Anilist"
					}
				});
			}
		}
	}
}
