using DesktopWeeabo2.Data;
using DesktopWeeabo2.Data.Repositories;
using DesktopWeeabo2.Data.Repositories.Shared;
using DesktopWeeabo2.Data.Services;
using DesktopWeeabo2.Data.Services.Shared;
using DesktopWeeabo2.Models;
using DesktopWeeabo2.ViewModels;
using System.Windows;
using Unity;
using Unity.Lifetime;

namespace DesktopWeeabo2 {
	public partial class App : Application {
		protected override void OnStartup(StartupEventArgs e) {
			base.OnStartup(e);

			IUnityContainer container = new UnityContainer();

			container.RegisterType<EntriesContext>(new ContainerControlledTransientManager());

			container.RegisterType<IRepo<AnimeModel>, AnimeRepo>(new ContainerControlledTransientManager());
			container.RegisterType<IRepo<MangaModel>, MangaRepo>(new ContainerControlledTransientManager());

			container.RegisterType<IService<AnimeModel>, AnimeService>(new ContainerControlledTransientManager());
			container.RegisterType<IService<MangaModel>, MangaService>(new ContainerControlledTransientManager());
			container.RegisterSingleton<IOService>();

			container.RegisterSingleton<MainWindowViewModel>();
			container.RegisterSingleton<AnimeViewModel>();
			container.RegisterSingleton<MangaViewModel>();
			container.RegisterSingleton<SettingsViewModel>();
			
			container.Resolve<MainWindow>().Show();
		}
	}
}
