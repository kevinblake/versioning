using System.Web;
using Puzzlebox.Versioning.Business.Configuration;
using Puzzlebox.Versioning.Business.Extensions;

namespace Puzzlebox.Versioning.Business
{
	public class VersionInformationHandler : IHttpHandler
	{
		public void ProcessRequest(HttpContext context)
		{
			if (!VersionInformationConfiguration.Settings.HttpHandler)
			{
				return;
			}

			context.Response.ContentType = "application/json";
			context.Response.Write(VersionInformation.GetVersionInformation().ToJson());
		}

		public bool IsReusable { get; private set; }
	}
}