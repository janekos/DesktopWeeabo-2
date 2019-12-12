using DesktopWeeabo2.Core.Interfaces.Jobs;
using DesktopWeeabo2.Core.Interfaces.Repositories;
using DesktopWeeabo2.Core.Interfaces.Services;
using DesktopWeeabo2.Infrastructure.Database;
using DesktopWeeabo2.Infrastructure.Jobs;
using DesktopWeeabo2.Infrastructure.Repositories;
using DesktopWeeabo2.Infrastructure.Services;
using DesktopWeeabo2.ViewModels;
using System.Windows;
using Unity;

namespace DesktopWeeabo2 {

	public partial class App : Application {

		private void Application_Startup(object sender, StartupEventArgs e) {
			IUnityContainer container = new UnityContainer();
			
			//container.RegisterType<EntriesContext>(TypeLifetime.Scoped);

			container.RegisterType<IAnimeRepository, AnimeRepository>(TypeLifetime.Scoped);
			container.RegisterType<IMangaRepository, MangaRepository>(TypeLifetime.Scoped);

			container.RegisterType<IAnimeService, AnimeService>(TypeLifetime.Scoped);
			container.RegisterType<IMangaService, MangaService>(TypeLifetime.Scoped);

			container.RegisterType<IRunJobs<DWOneImportJob>, DWOneImportJob>(TypeLifetime.Scoped);
			container.RegisterType<IRunJobs<UpdateDbEntries>, UpdateDbEntries>(TypeLifetime.Scoped);

			container.RegisterSingleton<MainWindowViewModel>();
			container.RegisterSingleton<AnimeViewModel>();
			container.RegisterSingleton<MangaViewModel>();
			container.RegisterSingleton<SettingsViewModel>();

			container.Resolve<MainWindow>().Show();
		}
	}
}