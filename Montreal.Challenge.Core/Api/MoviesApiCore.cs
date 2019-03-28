using Montreal.Challenge.Core.Base;
using Montreal.Challenge.Core.Interfaces;
using Montreal.Challenge.Datasource.Api;
using Montreal.Challenge.Shared.DTO;
using Montreal.Challenge.Shared.Entity;
using Newtonsoft.Json;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Montreal.Challenge.Core.Api
{
    public class MoviesApiCore : BaseApiCore, IMoviesApiCore
    {
        public async Task<List<MovieEntity>> GetNowPlayingMovies(string language, int page)
        {
            var moviesNowPlayingAPI = RestService.For<IMoviesApi>(Endpoints.BaseEirpointHttpClient());
            var moviesNowPlayingResponse = await moviesNowPlayingAPI.GetNowPlayingMovies(Endpoints.BaseTheMovieDbApiKey, language, page);

            if (moviesNowPlayingResponse.IsSuccessStatusCode)
            {
                var response = await moviesNowPlayingResponse.Content.ReadAsStringAsync();
                var json = await Task.Run(() => JsonConvert.DeserializeObject<ResultDTO<MovieEntity>>(response));

                //update paths
                UpdateRelativePaths(json.Results);

                return json.Results;
            }

            return new List<MovieEntity>();
        }

        public async Task<List<MovieEntity>> GetSearchedMovies(string textSearch, string language, int page)
        {
            var query = System.Net.WebUtility.UrlEncode(textSearch);
            var moviesQueryAPI = RestService.For<IMoviesApi>(Endpoints.BaseEirpointHttpClient());
            var moviesQueryResponse = await moviesQueryAPI.GetMoviesByQuery(Endpoints.BaseTheMovieDbApiKey, language, query, page);

            if (moviesQueryResponse.IsSuccessStatusCode)
            {
                var response = await moviesQueryResponse.Content.ReadAsStringAsync();
                var json = await Task.Run(() => JsonConvert.DeserializeObject<ResultDTO<MovieEntity>>(response));

                //update paths
                UpdateRelativePaths(json.Results);

                return json.Results;
            }

            return new List<MovieEntity>();
        }
    }
}
