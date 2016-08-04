using System.Configuration;

namespace Puzzlebox.Versioning.Business.Configuration
{
	public class VersionInformationConfiguration : ConfigurationSection
	{
		private const string enabled = "enabled";
		private const string localOnly = "localOnly";
		private const string httpHandler = "httpHandler";
		private const string allAssemblies = "allAssemblies";
		private const string includeWebApplicationName = "includeWebApplicationName";
		private const string excludeBuildDate = "excludeBuildDate";
		private const string includeGac = "includeGAC";
	    private const string includeMachineName = "includeMachineName";


        private static readonly VersionInformationConfiguration ConfigurationSettings;


		static VersionInformationConfiguration()
		{
			ConfigurationSettings = ConfigurationManager.GetSection("version.info") as VersionInformationConfiguration;
		}

		public static VersionInformationConfiguration Settings
		{
			get { return ConfigurationSettings; }
		}

		[ConfigurationProperty(enabled, DefaultValue = false, IsRequired = false)]
		public bool Enabled
		{
			get { return (bool) this[enabled]; }
			set { this[enabled] = value; }
		}

		[ConfigurationProperty(httpHandler, DefaultValue = false, IsRequired = false)]
		public bool HttpHandler
		{
			get { return (bool) this[httpHandler]; }
			set { this[httpHandler] = value; }
		}

		[ConfigurationProperty(localOnly, DefaultValue = true, IsRequired = false)]
		public bool LocalOnly
		{
			get { return (bool) this[localOnly]; }
			set { this[localOnly] = value; }
		}

		[ConfigurationProperty(allAssemblies, DefaultValue = false, IsRequired = false)]
		public bool AllAssemblies
		{
			get { return (bool) this[allAssemblies]; }
			set { this[allAssemblies] = value; }
		}

		[ConfigurationProperty(includeWebApplicationName, DefaultValue = false, IsRequired = false)]
		public bool IncludeWebApplicationName
		{
			get { return (bool) this[includeWebApplicationName]; }
			set { this[includeWebApplicationName] = value; }
		}

		[ConfigurationProperty(excludeBuildDate, DefaultValue = false, IsRequired = false)]
		public bool ExcludeBuildDate
		{
			get { return (bool) this[excludeBuildDate]; }
			set { this[excludeBuildDate] = value; }
		}


		[ConfigurationProperty(includeGac, DefaultValue = false, IsRequired = false)]
		public bool IncludeGac
		{
			get { return (bool)this[includeGac]; }
			set { this[includeGac] = value; }
		}

        [ConfigurationProperty(includeMachineName, DefaultValue = false, IsRequired = false)]
        public bool IncludeMachineName
        {
            get { return (bool)this[includeMachineName]; }
            set { this[includeMachineName] = value; }
        }

        
    }
}