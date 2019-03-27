using Montreal.Challenge.Datasource.Entity;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Montreal.Challenge.ViewModels
{
    public class NowPlayingPageViewModel : ViewModelBase
    {
        #region Fields        

        ObservableCollection<Movie> _moviesNowList;
        bool _isBoxSearchBarVisible = true;
        bool _isSearchBarVisible = true;
        string _searchText = string.Empty;

        #endregion

        #region Properties       

        public ObservableCollection<Movie> MoviesNowList
        {
            get { return _moviesNowList; }
            set
            {
                _moviesNowList = value;
                RaisePropertyChanged("MoviesNowList");
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
            //set title
            Title = "Now Playing Movies";

            //binding delegates
            BindingDelegateCommands();

            var movie = new Movie() { BackdropPath = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/AtsgWhDnHTq68L0lLsUrCnM7TjG.jpg", Title = "Capita Marvel", ReleaseDate = DateTime.Now };

            var listmovies = new List<Movie>();
            listmovies.Add(movie);

            MoviesNowList = new ObservableCollection<Movie>(listmovies);
        }

        #endregion

        #region Delegate Commands

        public DelegateCommand SearchBarCommand { get; set; }

        #endregion

        #region Methods

        private void BindingDelegateCommands()
        {
            //products button action
            SearchBarCommand = new DelegateCommand(ExecuteSearch);
        }

        private void ExecuteSearch()
        {
            var x = SearchText;
        }


        #endregion
    }
}
