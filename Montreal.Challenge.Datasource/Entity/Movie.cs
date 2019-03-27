using System;
using System.Collections.Generic;
using System.Text;

namespace Montreal.Challenge.Datasource.Entity
{
    /// <summary>
    /// Base entity for Movie entity and it's annotation for Json deserialization
    /// </summary>
    public class Movie
    {
        public string Title { get; set; }

        public string PosterPath { get; set; }

        public int[] GenreIds { get; set; }

        public string BackdropPath { get; set; }

        public string OverView { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
