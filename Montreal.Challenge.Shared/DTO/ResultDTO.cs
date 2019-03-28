using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Montreal.Challenge.Shared.DTO
{
    /// <summary>
    /// Wraps the result json for movies queries
    /// </summary>
    public class ResultDTO<T> where T : class
    {
        [JsonProperty("results")]
        public List<T> Results { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("total_results")]
        public int TotalResults { get; set; }

        [JsonProperty("dates")]
        public object Dates { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

    }
}
