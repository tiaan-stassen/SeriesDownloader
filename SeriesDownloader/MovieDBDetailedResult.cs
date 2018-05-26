// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var movieDbDetailedResult = MovieDbDetailedResult.FromJson(jsonString);

namespace QuickType
{
	using System;
	using System.Collections.Generic;

	using System.Globalization;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Converters;

	public partial class MovieDbDetailedResult
	{
		[JsonProperty("imdb_id")]
		public string ImdbId { get; set; }

		[JsonProperty("freebase_mid")]
		public string FreebaseMid { get; set; }

		[JsonProperty("freebase_id")]
		public string FreebaseId { get; set; }

		[JsonProperty("tvdb_id")]
		public long TvdbId { get; set; }

		[JsonProperty("tvrage_id")]
		public long TvrageId { get; set; }

		[JsonProperty("facebook_id")]
		public string FacebookId { get; set; }

		[JsonProperty("instagram_id")]
		public string InstagramId { get; set; }

		[JsonProperty("twitter_id")]
		public string TwitterId { get; set; }
	}

	public partial class MovieDbDetailedResult
	{
		public static MovieDbDetailedResult FromJson(string json) => JsonConvert.DeserializeObject<MovieDbDetailedResult>(json, QuickType.Converter.Settings);
	}

}
