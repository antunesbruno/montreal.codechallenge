using Montreal.Challenge.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Montreal.Challenge.Items
{
    public class MovieItem
    {
        public MovieItem(MovieEntity movie)
        {
            Movie = movie;
        }
       
        public MovieEntity Movie { get; }
     
        public string ReleaseDate
        {
            get
            {
                return (Movie.ReleaseDate.HasValue) ? Movie.ReleaseDate.Value.Date.GetDateTimeFormats()[0] : new DateTime(0001, 01, 01).Date.GetDateTimeFormats()[0];
            }
        }     
    }
}
