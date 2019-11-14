using DesktopWeeabo2.Core.Entities;
using DesktopWeeabo2.Core.Entities.Shared;
using DesktopWeeabo2.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesktopWeeabo2.Infrastructure.Services {

	public static class ServiceUtilities {

		public static void UpdateByMethod<T>(IEnumerable<T> dbEntities, IEnumerable<T> entities, UpdateMethod updateMethod) where T : BaseEntity {
			switch (updateMethod) {
				// add new info to existing entites with personal data
				case UpdateMethod.ONLY_PERSONAL_PROPERTIES:
					entities = entities
						.Join(dbEntities, newEn => newEn.Id, oldEn => oldEn.Id, (newEn, oldEn) => new { newEn, oldEn })
						.Select(both => {
							var newEn = both.newEn;
							var oldEn = both.oldEn;

							if (newEn.PersonalScore == null && oldEn.PersonalScore != null)
								newEn.PersonalScore = oldEn.PersonalScore;

							if (newEn.PersonalReview == null && oldEn.PersonalReview != null)
								newEn.PersonalReview = oldEn.PersonalReview;
							else if (newEn.PersonalReview != null && oldEn.PersonalReview != null)
								newEn.PersonalReview = $"{oldEn.PersonalReview}{Environment.NewLine}{Environment.NewLine}----- IMPORTED PERSONAL REVIEW -----{Environment.NewLine}{Environment.NewLine}{newEn.PersonalReview}";

							if ((newEn.DateAdded == null && oldEn.DateAdded != null)
								|| (newEn.DateAdded != null && newEn.DateAdded != null && newEn.DateAdded > oldEn.DateAdded))
								newEn.DateAdded = oldEn.DateAdded;

							if (typeof(T) == typeof(AnimeEntity)) {
								var newAEn = newEn as AnimeEntity;
								var oldAEn = oldEn as AnimeEntity;



							} else {
								var newMEn = newEn as MangaEntity;
								var oldMEn = oldEn as MangaEntity;
							}

							return both.newEn;
						});
					break;

				// add personal stuff to new (entities) incoming things
				case UpdateMethod.ONLY_MODEL_PROPERTIES:
					entities = entities
						.Join(dbEntities, e => e.Id, dbe => dbe.Id, (es, dbes) => new { es, dbes })
						.Select(all => {
							return all.es;
						});
					break;

				case UpdateMethod.ALL:
				default:
					break;
			}
		}
	}
}