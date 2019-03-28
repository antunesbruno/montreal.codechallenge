using Newtonsoft.Json;
using System;

namespace Montreal.Challenge.Shared.Entity
{
    /// <summary>
    /// Base entity for Movie entity and it's annotation for Json deserialization
    /// </summary>
    public class MovieEntity
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("genre_ids")]
        public int[] GenreIds { get; set; }

        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonProperty("overview")]
        public string OverView { get; set; }

        [JsonProperty("release_date")]
        public DateTime? ReleaseDate { get; set; }

    }
}
