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
using Unity.Lifetime;

namespace DesktopWeeabo2 {

	public partial class App : Application {

		private void Application_Startup(object sender, StartupEventArgs e) {
			IUnityContainer container = new UnityContainer();

			container.RegisterType<EntriesContext>(new TransientLifetimeManager());

			container.RegisterType<IAnimeRepository, AnimeRepository>(new TransientLifetimeManager());
			container.RegisterType<IMangaRepository, MangaRepository>(new TransientLifetimeManager());

			container.RegisterType<IAnimeService, AnimeService>(new TransientLifetimeManager());
			container.RegisterType<IMangaService, MangaService>(new TransientLifetimeManager());

			container.RegisterType<IRunJobs<DWOneImportJob>, DWOneImportJob>(new TransientLifetimeManager());
			container.RegisterType<IRunJobs<UpdateDbEntries>, UpdateDbEntries>(new TransientLifetimeManager());

			container.RegisterSingleton<MainWindowViewModel>();
			container.RegisterSingleton<AnimeViewModel>();
			container.RegisterSingleton<MangaViewModel>();
			container.RegisterSingleton<SettingsViewModel>();

			container.Resolve<MainWindow>().Show();
		}
	}
}