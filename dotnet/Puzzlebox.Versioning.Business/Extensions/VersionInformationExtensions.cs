using Newtonsoft.Json;
using Puzzlebox.Versioning.Business.Entities;

namespace Puzzlebox.Versioning.Business.Extensions
{
	public static class VersionInformationExtensions
	{
		public static string ToJson(this VersionInformationEntity entity, Formatting formatting)
		{
			if (entity == null)
			{
				return string.Empty;
			}

			var settings = new JsonSerializerSettings
				{
					PreserveReferencesHandling = PreserveReferencesHandling.None,
					NullValueHandling = NullValueHandling.Ignore
				};

			return JsonConvert.SerializeObject(entity, formatting, settings);
		}

		public static string ToJson(this VersionInformationEntity entity)
		{
			return entity.ToJson(Formatting.Indented);
		}
	}
}