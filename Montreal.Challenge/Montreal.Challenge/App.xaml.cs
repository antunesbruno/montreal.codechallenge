using Prism;
using Prism.Ioc;
using Montreal.Challenge.ViewModels;
using Montreal.Challenge.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Montreal.Challenge.Ioc;
using Montreal.Challenge.Datasource.Interfaces;
using Montreal.Challenge.Shared;
using Montreal.Challenge.Datasource.Helpers;
using Montreal.Challenge.Shared.NativeInterfaces;
using Montreal.Challenge.Core.Api;
using Montreal.Challenge.Core.Interfaces;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Montreal.Challenge
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/NowPlayingPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //register platform dependencies and all mobile dependencies
            RegisterDependencies.BuildDependencies(RegisterMobileDependencies);

            //register navigations
            RegisterNavigations(containerRegistry);

            //create database
            Injector.Resolver<IDatabaseHelper>().CreateDatabase();

            //create tables
            Injector.Resolver<IDatabaseHelper>().CreateTables();
           
        }

        /// <summary>
        /// MVVM dependencies (Views / ViewModels)
        /// </summary>
        /// <param name="containerRegistry"></param>
        private void RegisterNavigations(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<NowPlayingPage, NowPlayingPageViewModel>();
            containerRegistry.RegisterForNavigation<DetailsPage, DetailsPageViewModel>();
        }

        /// <summary>
        /// Dependencies of application
        /// </summary>
        private void RegisterMobileDependencies()
        {
            //datasource
            Injector.RegisterType<DatabaseHelper, IDatabaseHelper>();
          
            //core api
            Injector.RegisterType<MoviesApiCore, IMoviesApiCore>();
            
            //core bll
            //Injector.RegisterType<ProductsBll, IProductsBll>();
            //Injector.RegisterType<BarcodesBll, IBarcodesBll>();

            //native interfaces
            //Injector.RegisterType<NativeConnectivity, IConnectivity>();
            //Injector.RegisterType<MonitorService, IMonitorService>();
            //Injector.RegisterType<ConfigurationApp, IConfiguration>();
        }
    }
}
