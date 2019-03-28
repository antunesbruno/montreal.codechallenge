using Acr.UserDialogs;
using Montreal.Challenge.Core.Interfaces;
using Montreal.Challenge.Ioc;
using Montreal.Challenge.Items;
using Montreal.Challenge.Shared.Entity;
using Montreal.Challenge.Shared.NativeInterfaces;
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
        int _currentPage = 1;
        int _totalPages = 0;
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
        public DelegateCommand LoadMoreCommand { get; set; }
        public DelegateCommand UnfocusedCommand { get; set; }        

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
            //verify intenet connection
            if (!IsConnected())
                return;

            if (Injector.Resolver<IConnectivity>().IsConnected())
            {
                using (var loading = UserDialogs.Instance.Loading("Loading...", null, null, true, MaskType.Gradient))
                {
                    //request api
                    var moviesApiCore = Injector.Resolver<IMoviesApiCore>();

                    //set page
                    _currentPage = 1;

                    //perfomr request
                    var moviesListDTO = await moviesApiCore.GetNowPlayingMovies("pt-br", _currentPage);

                    //add in observable list
                    if (moviesListDTO != null && moviesListDTO.Results?.Count > 0)
                    {
                        //set page property
                        _totalPages = moviesListDTO.TotalPages;

                        //append items
                        AppendMovieItems(moviesListDTO.Results);
                    }
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
            LoadMoreCommand = new DelegateCommand(ExecuteLoadMoreItems);
            UnfocusedCommand = new DelegateCommand(ExecuteUnfocus);
        }

        private async void ExecuteImageSearchBar()
        {
            //show search bar
            IsBoxSearchBarVisible = IsSearchBarVisible = !IsSearchBarVisible;

            //reset search
            SearchText = string.Empty;
        }

        private async void ExecuteSearchBar()
        {
            //verify internet connection
            if (!IsConnected())
                return;

            //execute search
            if (!string.IsNullOrEmpty(SearchText) && SearchText.Length >= 3)
            {
                using (var searching = UserDialogs.Instance.Loading("Searching...", null, null, true, MaskType.Gradient))
                {
                    //request api
                    var moviesApiCore = Injector.Resolver<IMoviesApiCore>();

                    //set page
                    _currentPage = 1;

                    //perfomr request
                    var moviesList = await moviesApiCore.GetSearchedMovies(SearchText, "pt-br", _currentPage);

                    //add in observable list
                    if (moviesList?.Count > 0)
                    {
                        //clear lists
                        ClearLists();

                        //append items
                        AppendMovieItems(moviesList);
                    }                
                }
            }
        }

        private async void ExecuteLoadMoreItems()
        {
            //verify intenet connection
            if (!IsConnected())
                return;

            if (_currentPage <= _totalPages && !_isSearchBarVisible)
            {
                using (var searching = UserDialogs.Instance.Loading("Loading More Items...", null, null, true, MaskType.Gradient))
                {
                    //request api
                    var moviesApiCore = Injector.Resolver<IMoviesApiCore>();

                    //set page
                    _currentPage++;

                    //perfomr request
                    var moviesListDTO = await moviesApiCore.GetNowPlayingMovies("pt-br", _currentPage);

                    //add in observable list
                    if (moviesListDTO != null && moviesListDTO.Results?.Count > 0)
                    {
                        //set page property
                        _totalPages = moviesListDTO.TotalPages;

                        //append items
                        AppendMovieItems(moviesListDTO.Results);
                    }
                }
            }
        }

        private void ExecuteUnfocus()
        {
            //show search bar
            IsBoxSearchBarVisible = IsSearchBarVisible = false;

            //reset search
            SearchText = string.Empty;
        }

        private void ClearLists()
        {
            _listmovies.Clear();
            MoviesNowList.Clear();
        }

        #endregion
    }
}
