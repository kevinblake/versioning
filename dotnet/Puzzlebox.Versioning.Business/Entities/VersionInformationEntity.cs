using System.Collections.Generic;
using Newtonsoft.Json;

namespace Puzzlebox.Versioning.Business.Entities
{
	public class VersionInformationEntity
	{
		public VersionInformationEntity()
		{
		}

		public AssemblyInformationEntity WebApplicationVersion { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public IList<AssemblyInformationEntity> Assemblies { get; set; }
	}

}