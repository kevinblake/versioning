using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Caching;
using Puzzlebox.Versioning.Business.CacheHelpers;
using Puzzlebox.Versioning.Business.Configuration;
using Puzzlebox.Versioning.Business.Entities;

namespace Puzzlebox.Versioning.Business
{
	public static class VersionInformation
	{
		public static VersionInformationEntity GetVersionInformation()
		{
			if (!VersionInformationConfiguration.Settings.Enabled)
			{
				return null;
			}

			if (HttpContext.Current != null && VersionInformationConfiguration.Settings.LocalOnly &&
				!HttpContext.Current.Request.IsLocal)
			{
				return null;
			}

			var cachedVersionInformationEntity = new InMemoryCache().Get("VersionInformation", null, Cache.NoAbsoluteExpiration, TimeSpan.FromDays(30), () =>
				{
					var myAssemblies = Thread.GetDomain().GetAssemblies();
					var applicationAssembly = GetWebEntryAssembly();

					var versionInformationEntity = new VersionInformationEntity();
					versionInformationEntity.WebApplicationVersion = GetAssemblyInformationFromAssembly(applicationAssembly);

					if (!VersionInformationConfiguration.Settings.IncludeWebApplicationName)
					{
						versionInformationEntity.WebApplicationVersion.Name = null;
					}

                    if (!VersionInformationConfiguration.Settings.IncludeMachineName)
                    {
                        versionInformationEntity.WebApplicationVersion.MachineName = null;
                    }

                    if (VersionInformationConfiguration.Settings.AllAssemblies)
					{
						versionInformationEntity.Assemblies =
							myAssemblies.Where(
								t => !t.IsDynamic && (!t.GlobalAssemblyCache || VersionInformationConfiguration.Settings.IncludeGac))
										.Select(
											GetAssemblyInformationFromAssembly)
										.OrderBy(t => t.Name).ToList();
					}
					return versionInformationEntity;
				});

			return cachedVersionInformationEntity;
		}

		private static AssemblyInformationEntity GetAssemblyInformationFromAssembly(Assembly applicationAssembly)
		{
			var entity = new AssemblyInformationEntity
				{
					VersionNumber = applicationAssembly.GetName().Version.ToString()
				};

			if (!VersionInformationConfiguration.Settings.ExcludeBuildDate)
			{
				entity.BuildDate =
					RetrieveLinkerTimestamp(applicationAssembly).GetValueOrDefault().ToString(CultureInfo.InvariantCulture);
			}

		    if (VersionInformationConfiguration.Settings.IncludeMachineName)
		    {
		        entity.MachineName = Environment.MachineName;
		    }

			entity.Name = applicationAssembly.GetName().Name;

			entity.Gac = applicationAssembly.GlobalAssemblyCache;

		    


            return entity;
		}

		private static Assembly GetWebEntryAssembly()
		{
			if (HttpContext.Current == null ||
				HttpContext.Current.ApplicationInstance == null)
			{
				return null;
			}

			var type = HttpContext.Current.ApplicationInstance.GetType();
			while (type != null && type.Namespace == "ASP")
			{
				type = type.BaseType;
			}

			return type == null ? null : type.Assembly;
		}

		// Stolen from Joe Spivey (http://stackoverflow.com/questions/1600962/displaying-the-build-date)
		private static DateTime? RetrieveLinkerTimestamp(Assembly t)
		{
			if (t.IsDynamic) return null;
			var filePath = t.Location;
			if (String.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
			{
				return null;
			}

			const int cPeHeaderOffset = 60;
			const int cLinkerTimestampOffset = 8;
			var b = new byte[2048];
			Stream s = null;

			try
			{
				s = new FileStream(filePath, FileMode.Open, FileAccess.Read);
				s.Read(b, 0, 2048);
			}
			finally
			{
				if (s != null)
				{
					s.Close();
				}
			}

			var i = BitConverter.ToInt32(b, cPeHeaderOffset);
			var secondsSince1970 = BitConverter.ToInt32(b, i + cLinkerTimestampOffset);
			var dt = new DateTime(1970, 1, 1, 0, 0, 0);
			dt = dt.AddSeconds(secondsSince1970);
			dt = dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours);
			return dt;
		}
	}
}