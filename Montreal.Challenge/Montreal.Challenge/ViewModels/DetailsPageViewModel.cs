using Montreal.Challenge.Items;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Montreal.Challenge.ViewModels
{
    public class DetailsPageViewModel : ViewModelBase
    {
        #region Fields        

        MovieItem _movieDetailItem;

        #endregion

        #region Properties       

        public MovieItem MovieDetailItem
        {
            get { return _movieDetailItem; }
            set
            {
                _movieDetailItem = value;
                RaisePropertyChanged("MovieDetailItem");
            }
        }

        #endregion

        #region Constructor      

        public DetailsPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        #endregion

        #region Methods        

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.Keys.Contains("movieItemSelected"))
            {
                var movieItem = parameters.GetValue<MovieItem>("movieItemSelected");
                if (movieItem != null)
                {
                    MovieDetailItem = movieItem;
                }
            }
        }

        #endregion
    }
}
