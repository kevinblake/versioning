versioning
==========

Get version information about assemblies in current project visible on the URL /version

{ "WebApplicationVersion": { "VersionNumber": "1.0.0.0", "BuildDate": "01/04/2013 12:34:55" } }

Read more at: http://www.kevinblake.co.uk/development/every-site-needs-a-version/

Configuration
=============

enabled=true|false

Enable or disable the version info output, in all areas.

httpHandler=true|false

Determines whether version information is visible on /version, of your web site.

allAssemblies=true|false

When set to true, version information for all loaded assemblies are included.

includeWebApplicationName=true|false

Hide the web applcation name, when set to false.

includeMachineName=true|false

Hide the web machine name, when set to false.

excludeBuildDate=true|false

Do not show the build date (true).  Helpful if you'd prefer to hide this information from your "visitors".

localOnly=true|false

localOnly will only allow access to your version information if you’re on the local machine, and not publicly accessible to the rest of the world.  Highly recommended for public sites (you should also consider disabling completely on production with enabled="false").
