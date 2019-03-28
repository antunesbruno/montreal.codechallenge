using Montreal.Challenge.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Montreal.Challenge.Core.Base
{
    public class BaseApiCore
    {
        /// <summary>
        /// Fixes relative paths so that the ui can use the url seamlessly
        /// </summary>
        /// <param name="results"></param>
        protected void UpdateRelativePaths(List<MovieEntity> results)
        {
            foreach (MovieEntity movie in results)
            {
                movie.BackdropPath = "https://image.tmdb.org/t/p/w780" + movie.BackdropPath;
                movie.PosterPath = "https://image.tmdb.org/t/p/w500" + movie.PosterPath;
            }
        }
    }
}
