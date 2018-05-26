// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var movieDbResult = MovieDbResult.FromJson(jsonString);

namespace QuickType
{
	using System;
	using System.Collections.Generic;

	using System.Globalization;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Converters;

	public partial class MovieDbResult
	{
		[JsonProperty("page")]
		public long Page { get; set; }

		[JsonProperty("total_results")]
		public long TotalResults { get; set; }

		[JsonProperty("total_pages")]
		public long TotalPages { get; set; }

		[JsonProperty("results")]
		public MovieDBItem[] MovieDbItems { get; set; }
	}

	public partial class MovieDBItem
	{
		[JsonProperty("original_name")]
		public string OriginalName { get; set; }

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("poster_path")] private string _PosterPath;
		public string PosterPath
		{
			get
			{
				if (!string.IsNullOrEmpty(_PosterPath))
				{
					return string.Concat("http://image.tmdb.org/t/p/w185/", _PosterPath);
				}

				//	return val;
				return "";
			}
			set { _PosterPath = value; }
		}

		[JsonProperty("overview")]
		public string Overview { get; set; }

		[JsonProperty("origin_country")]
		public string[] OriginCountry { get; set; }
	}

	public partial class MovieDbResult
	{
		public static MovieDbResult FromJson(string json) => JsonConvert.DeserializeObject<MovieDbResult>(json, QuickType.Converter.Settings);
	}

	public static class Serialize
	{
		public static string ToJson(this MovieDbResult self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
	}

	internal static class Converter
	{
		public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
		{
			MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
			DateParseHandling = DateParseHandling.None,
			Converters = {
				new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
			},
		};
	}
}
