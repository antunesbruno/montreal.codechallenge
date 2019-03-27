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

        //general endpoints
        public const string CUSTOMERS = "/customers";
        public const string DEPARTMENT = "/departments";
        public const string DISCOUNT = "/discounts";
        public const string PAYMENT_CATEGORY = "/paymentcategories";
        public const string GROUP_LIST = "/groups";
        public const string POS_STATION = "/posstations";
        public const string PRODUCT = "/products";
        public const string PRODUCT_BARCODE = "/productbarcodes";
        public const string PROMOTIONS = "/promotions";
        public const string REASON = "/reasons";
        public const string RECEIPT = "/receipttemplates";
        public const string STOCKING_ATTRIBUTE_TYPE = "/stockingattributetypes";
        public const string STOCK_LOCATION = "/stocklocations";
        public const string USER_LIST = "/users";


        public static HttpClient BaseEirpointHttpClient()
        {
            var _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseTheMovieDbUrl + "?api_key=" + BaseTheMovieDbApiKey);
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.Timeout = TimeSpan.FromSeconds(15);

            return _httpClient;
        }
    }
}
