using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace Montreal.Challenge.Datasource.Api
{
    public interface IMoviesApi
    {
        [Get("/movie/now_playing")]
        Task<HttpResponseMessage> GetNowPlayingMovies([AliasAs("api_key")] string apiKey, [AliasAs("language")] string language, [AliasAs("page")] int page);

        [Get("/search/movie")]
        Task<HttpResponseMessage> GetMoviesByQuery([AliasAs("api_key")] string apiKey, [AliasAs("language")] string language, [AliasAs("query")] string query, [AliasAs("page")] int page);
    }
}
