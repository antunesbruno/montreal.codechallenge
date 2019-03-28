using Acr.UserDialogs;
using Montreal.Challenge.Core.Interfaces;
using Montreal.Challenge.Ioc;
using Montreal.Challenge.Items;
using Montreal.Challenge.Shared.Entity;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace Montreal.Challenge.ViewModels
{
    public class NowPlayingPageViewModel : ViewModelBase
    {
        #region Fields        

        ObservableCollection<MovieItem> _moviesNowList;
        bool _isBoxSearchBarVisible = false;
        bool _isSearchBarVisible = false;
        string _searchText = string.Empty;
        List<MovieItem> _listmovies = new List<MovieItem>();
        MovieItem _selectedItem;
        MovieItem _itemSelectedList;        
        INavigationService _navigationService;
        #endregion

        #region Properties       

        public ObservableCollection<MovieItem> MoviesNowList
        {
            get { return _moviesNowList; }
            set
            {
                _moviesNowList = value;
                RaisePropertyChanged("MoviesNowList");
            }
        }

        public MovieItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        public MovieItem ItemSelectedList
        {
            get { return _itemSelectedList; }
            set
            {
                _itemSelectedList = value;
                RaisePropertyChanged("ItemSelectedList");
            }
        }        

        public bool IsBoxSearchBarVisible
        {
            get { return _isBoxSearchBarVisible; }
            set
            {
                _isBoxSearchBarVisible = value;
                RaisePropertyChanged("IsBoxSearchBarVisible");
            }
        }

        public bool IsSearchBarVisible
        {
            get { return _isSearchBarVisible; }
            set
            {
                _isSearchBarVisible = value;
                RaisePropertyChanged("IsSearchBarVisible");
            }
        }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                RaisePropertyChanged("SearchText");
            }
        }

        #endregion

        #region Constructors        

        public NowPlayingPageViewModel(INavigationService navigationService)
        : base(navigationService)
        {
            //set navigation
            _navigationService = navigationService;

            //set title
            Title = "Now Playing Movies";

            //binding delegates
            BindingDelegateCommands();

            //load movies
            LoadNowPlayingMovies();
        }

        #endregion

        #region Delegate Commands

        public DelegateCommand ImageSearchBarCommand { get; set; }
        public DelegateCommand SearchBarCommand { get; set; }
        #endregion

        #region Methods

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName.Equals(nameof(SelectedItem)))
            {
                OnSelectedItem();
            }
        }

        private void OnSelectedItem()
        {
            if (SelectedItem != null)
            {
                //declare parameters
                var navigationParams = new NavigationParameters();

                //add parameter
                navigationParams.Add("movieItemSelected", SelectedItem);

                //redirect
                _navigationService.NavigateAsync("DetailsPage", navigationParams);
            }
        }

        private async void LoadNowPlayingMovies()
        {
            using (var loading = UserDialogs.Instance.Loading("Loading...", null, null, true, MaskType.Gradient))
            {
                //request api
                var moviesApiCore = Injector.Resolver<IMoviesApiCore>();

                //perfomr request
                var moviesList = await moviesApiCore.GetNowPlayingMovies("pt-br", 1);

                //add in observable list
                if (moviesList?.Count > 0)
                {
                    //append items
                    AppendMovieItems(moviesList);
                }
            }           
        }

        private void AppendMovieItems(List<MovieEntity> movies)
        {
            foreach (MovieEntity movie in movies)
                _listmovies.Add(new MovieItem(movie));

            MoviesNowList = new ObservableCollection<MovieItem>(_listmovies);
        }
      
        private void BindingDelegateCommands()
        {
            ImageSearchBarCommand = new DelegateCommand(ExecuteImageSearchBar);
            SearchBarCommand = new DelegateCommand(ExecuteSearchBar);
        }

        private void ExecuteImageSearchBar()
        {
            //show search bar
            IsBoxSearchBarVisible = IsSearchBarVisible = !IsSearchBarVisible;

            //reset search
            SearchText = string.Empty;
        }

        private async void ExecuteSearchBar()
        {
            using (var searching = UserDialogs.Instance.Loading("Searching...", null, null, true, MaskType.Gradient))
            {
                if (!string.IsNullOrEmpty(SearchText) && SearchText.Length >= 5)
                {
                    //request api
                    var moviesApiCore = Injector.Resolver<IMoviesApiCore>();

                    //perfomr request
                    var moviesList = await moviesApiCore.GetSearchedMovies(SearchText, "pt-br", 1);

                    //add in observable list
                    if (moviesList?.Count > 0)
                    {
                        //clear lists
                        ClearLists();

                        //append items
                        AppendMovieItems(moviesList);
                    }

                    //hide and clear search
                    ExecuteImageSearchBar();
                }
                else
                {
                    //show message
                    UserDialogs.Instance.Alert("O termo informado deve ter 5 caracteres ou mais !", "Pesquisa");
                }
            }
        }

        private void ClearLists()
        {
            _listmovies.Clear();
            MoviesNowList.Clear();
        }


        #endregion
    }
}
