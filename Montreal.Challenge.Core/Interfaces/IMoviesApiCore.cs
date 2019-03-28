using Montreal.Challenge.Shared.DTO;
using Montreal.Challenge.Shared.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Montreal.Challenge.Core.Interfaces
{
    public interface IMoviesApiCore
    {
        Task<ResultDTO<MovieEntity>> GetNowPlayingMovies(string language, int page);

        Task<List<MovieEntity>> GetSearchedMovies(string textSearch, string language, int page);
    }
}
