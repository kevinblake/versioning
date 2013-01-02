using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Puzzlebox.Versioning.Business.Entities;

namespace Puzzlebox.Versioning.Business
{
	public static class VersionInformation
	{
		public static VersionInformationEntity GetVersionInformation()
		{
			var myAssemblies = Thread.GetDomain().GetAssemblies();

			return new VersionInformationEntity
				{
					Assemblies = myAssemblies.Where(t => !t.IsDynamic && !t.GlobalAssemblyCache).Select(t => new AssemblyInformationEntity
						{
							Name = t.GetName().Name,
							VersionNumber = t.GetName().Version.ToString(),
							BuildDate = RetrieveLinkerTimestamp(t).GetValueOrDefault().ToString(CultureInfo.InvariantCulture)	
						}).OrderBy(t => t.Name).ToList()
				};
		}

		// Stolen from Joe Spivey (http://stackoverflow.com/questions/1600962/displaying-the-build-date)
		private static DateTime? RetrieveLinkerTimestamp(Assembly t)
		{
			if (t.IsDynamic) return null;
			var filePath = t.Location;
			const int cPeHeaderOffset = 60;
			const int cLinkerTimestampOffset = 8;
			var b = new byte[2048];
			Stream s = null;

			try
			{
				s = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
				s.Read(b, 0, 2048);
			}
			finally
			{
				if (s != null)
				{
					s.Close();
				}
			}

			var i = System.BitConverter.ToInt32(b, cPeHeaderOffset);
			var secondsSince1970 = System.BitConverter.ToInt32(b, i + cLinkerTimestampOffset);
			var dt = new DateTime(1970, 1, 1, 0, 0, 0);
			dt = dt.AddSeconds(secondsSince1970);
			dt = dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours);
			return dt;
		}

	}
}
