using System.Collections.Generic;

namespace Puzzlebox.Versioning.Business.Entities
{
	public class VersionInformationEntity
	{
		public VersionInformationEntity()
		{
			Assemblies = new List<AssemblyInformationEntity>();
		}

		public AssemblyInformationEntity WebApplicationVersion { get; set; }

		public IList<AssemblyInformationEntity> Assemblies { get; set; }
	}

}