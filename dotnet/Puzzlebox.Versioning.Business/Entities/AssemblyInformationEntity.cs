﻿namespace Puzzlebox.Versioning.Business.Entities
{
	public class AssemblyInformationEntity
	{
		public string Name { get; set; }

		public string VersionNumber { get; set; }

		public string BuildDate { get; set; }

		public bool Gac { get; set; }

        public string MachineName { get; set; }
	}
}
