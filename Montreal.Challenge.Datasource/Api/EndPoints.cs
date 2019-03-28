using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Montreal.Challenge.Datasource.Api
{
    public static class Endpoints
    {
        //TheMovieDB - API
        //https://api.themoviedb.org/3/movie/550?api_key=f2dc3e1e5744cb77b6c069d191016e25
        public static string BaseTheMovieDbApiKey => "f2dc3e1e5744cb77b6c069d191016e25";

        public static string BaseTheMovieDbUrl => "https://api.themoviedb.org/3";
       
        public static HttpClient BaseEirpointHttpClient()
        {
            var _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseTheMovieDbUrl);
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.Timeout = TimeSpan.FromSeconds(15);

            return _httpClient;
        }
    }
}
