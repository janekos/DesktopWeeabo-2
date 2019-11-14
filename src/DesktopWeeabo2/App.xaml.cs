using DesktopWeeabo2.Core.Interfaces.Jobs;
using DesktopWeeabo2.Core.Interfaces.Repositories;
using DesktopWeeabo2.Core.Interfaces.Services;
using DesktopWeeabo2.Infrastructure.Database;
using DesktopWeeabo2.Infrastructure.DomainServices;
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

			container.RegisterType<EntriesContext>(new ContainerControlledTransientManager());

			container.RegisterType<IAnimeRepository, AnimeRepository>(new ContainerControlledTransientManager());
			container.RegisterType<IMangaRepository, MangaRepository>(new ContainerControlledTransientManager());

			container.RegisterType<IAnimeService, AnimeService>(new ContainerControlledTransientManager());
			container.RegisterType<IMangaService, MangaService>(new ContainerControlledTransientManager());
			container.RegisterType<IHandleIO, IOService>(new ContainerControlledTransientManager());

			container.RegisterType<IRunJobs<DWOneImportJob>, DWOneImportJob>(new ContainerControlledTransientManager());

			container.RegisterSingleton<MainWindowViewModel>();
			container.RegisterSingleton<AnimeViewModel>();
			container.RegisterSingleton<MangaViewModel>();
			container.RegisterSingleton<SettingsViewModel>();

			container.Resolve<MainWindow>().Show();
		}
	}
}