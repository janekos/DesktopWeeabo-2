using DesktopWeeabo2.Core.Entities;
using DesktopWeeabo2.Core.Enums;
using DesktopWeeabo2.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DesktopWeeabo2.Infrastructure.Tests.Services {

	public class ServiceUtilitiesTests {

		private readonly List<AnimeEntity> oldAnimeEntities = new List<AnimeEntity>() {
			new AnimeEntity {
				Id = 1,
				IdMal = 1,
				AverageScore = 1,
				Type = 0,
				Format = 0,
				Status = 0,
				Description = "old description",
				IsAdult = false,
				Genres = "genre1",
				Synonyms = "synonym1",
				TitleEnglish = "title1",
				TitleRomaji  = "title1",
				TitleNative  = "title1",
				CoverImage = "img1",
				ExternalLinks = "linklist1",
				PersonalScore  = 1,
				PersonalReview= "rev1",
				DateAdded = DateTime.Today,
				Episodes = 1,
				Duration = 1,
				StartDate = DateTime.Today,
				EndDate = DateTime.Today,
				NextAiringEpisodeAiringAt = DateTime.Today,
				NextAiringEpisodeEpisode = 1,
				ViewingStatus = "status1",
				WatchPriority = 1,
				RewatchCount = 1,
				CurrentEpisode = 1
			}
		};

		private readonly List<AnimeEntity> newAnimeEntities = new List<AnimeEntity>() {
			new AnimeEntity {
				Id = 1,
				IdMal = 1,
				AverageScore = 2,
				Type = 0,
				Format = 0,
				Status = 1,
				Description = "new description",
				IsAdult = true,
				Genres = "genre2",
				Synonyms = "synonym2",
				TitleEnglish = "title2",
				TitleRomaji  = "title2",
				TitleNative  = "title2",
				CoverImage = "img2",
				ExternalLinks = "linklist2",
				PersonalScore  = null,
				PersonalReview= "rev2",
				DateAdded = DateTime.Today.AddDays(1),
				Episodes = 2,
				Duration = 2,
				StartDate = DateTime.Today.AddDays(1),
				EndDate = DateTime.Today.AddDays(1),
				NextAiringEpisodeAiringAt = DateTime.Today.AddDays(1),
				NextAiringEpisodeEpisode = 2,
				ViewingStatus = "status2",
				WatchPriority = 2,
				RewatchCount = 2,
				CurrentEpisode = 2
			}
		};

		private readonly List<MangaEntity> oldMangaEntities = new List<MangaEntity>() {
			new MangaEntity {
				Id = 1,
				IdMal = 1,
				AverageScore = 1,
				Type = 0,
				Format = 0,
				Status = 0,
				Description = "old description",
				IsAdult = false,
				Genres = "genre1",
				Synonyms = "synonym1",
				TitleEnglish = "title1",
				TitleRomaji  = "title1",
				TitleNative  = "title1",
				CoverImage = "img1",
				ExternalLinks = "linklist1",
				PersonalScore  = 1,
				PersonalReview= "prev1",
				DateAdded = DateTime.Today,
				Volumes = 1,
				Chapters = 1,
				ReadingStatus = "status1",
				ReadPriority = 1,
				RereadCount = 1,
				CurrentChapter = 1,
			}
		};

		private readonly List<MangaEntity> newMangaEntities = new List<MangaEntity>() {
			new MangaEntity {
				Id = 1,
				IdMal = 1,
				AverageScore = 2,
				Type = 0,
				Format = 0,
				Status = 1,
				Description = "new description",
				IsAdult = true,
				Genres = "genre2",
				Synonyms = "synonym2",
				TitleEnglish = "title2",
				TitleRomaji  = "title2",
				TitleNative  = "title2",
				CoverImage = "img2",
				ExternalLinks = "linklist2",
				PersonalScore  = null,
				PersonalReview= "prev2",
				DateAdded = DateTime.Today.AddDays(1),
				Volumes = 2,
				Chapters = 2,
				ReadingStatus = "status2",
				ReadPriority = 2,
				RereadCount = 2,
				CurrentChapter = 2,
			}
		};

		//[Fact]
		//public void PersonalAnimePropertiesWillUpdateCorrectly() {
		//	IEnumerable<AnimeEntity> dbEntities = oldAnimeEntities.Select(x => x);
		//	IEnumerable<AnimeEntity> newEntities = newAnimeEntities.Select(x => x);

		//	ServiceUtilities.UpdateByMethod(dbEntities, newEntities, UpdateMethod.ONLY_PERSONAL_PROPERTIES);

		//	var assertEntity = newEntities.ToList()[0];

		//	Assert.Equal("rev1\r\n\r\n----- IMPORTED PERSONAL REVIEW -----\r\n\r\nrev2", assertEntity.PersonalReview);
		//	Assert.Equal("status1", assertEntity.ViewingStatus);
		//	Assert.Equal(1, assertEntity.PersonalScore);
		//	Assert.Equal(1, assertEntity.RewatchCount);
		//	Assert.Equal(1, assertEntity.WatchPriority);
		//	Assert.Equal(1, assertEntity.CurrentEpisode);
		//	Assert.Equal(DateTime.Today, assertEntity.DateAdded);
		//}

		//[Fact]
		//public void PersonalMangaPropertiesWillUpdateCorrectly() {
		//	IEnumerable<MangaEntity> dbEntities = oldMangaEntities.Select(x => x);
		//	IEnumerable<MangaEntity> newEntities = newMangaEntities.Select(x => x);

		//	ServiceUtilities.UpdateByMethod(dbEntities, newEntities, UpdateMethod.ONLY_PERSONAL_PROPERTIES);

		//	var assertEntity = newEntities.ToList()[0];

		//	Assert.Equal("prev1\r\n\r\n----- IMPORTED PERSONAL REVIEW -----\r\n\r\nprev2", assertEntity.PersonalReview);
		//	Assert.Equal("status1", assertEntity.ReadingStatus);
		//	Assert.Equal(1, assertEntity.PersonalScore);
		//	Assert.Equal(1, assertEntity.RereadCount);
		//	Assert.Equal(1, assertEntity.ReadPriority);
		//	Assert.Equal(1, assertEntity.CurrentChapter);
		//	Assert.Equal(DateTime.Today, assertEntity.DateAdded);
		//}

		//[Fact]
		//public void ModelAnimePropertiesWillUpdateCorrectly() {
		//	List<AnimeEntity> dbEntities = oldAnimeEntities.Select(x => x).ToList();
		//	List<AnimeEntity> newEntities = newAnimeEntities.Select(x => x).ToList();

		//	ServiceUtilities.UpdateByMethod(dbEntities, newEntities, UpdateMethod.ONLY_MODEL_PROPERTIES);
		//}

		//[Fact]
		//public void ModelMangaPropertiesWillUpdateCorrectly() {
		//	List<MangaEntity> dbEntities = oldMangaEntities.Select(x => x).ToList();
		//	List<MangaEntity> newEntities = newMangaEntities.Select(x => x).ToList();

		//	ServiceUtilities.UpdateByMethod(dbEntities, newEntities, UpdateMethod.ONLY_MODEL_PROPERTIES);
		//}
	}
}