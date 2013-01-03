using System.Web;
using Newtonsoft.Json;
using Puzzlebox.Versioning.Business.Extensions;

namespace Puzzlebox.Versioning.Business
{
	public class VersionInformationHandler : IHttpHandler
	{
		public void ProcessRequest(HttpContext context)
		{
			var settings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.None, NullValueHandling = NullValueHandling.Ignore };
			context.Response.ContentType = "application/json";
			context.Response.Write(VersionInformation.GetVersionInformation().ToJson());
		}

		public bool IsReusable { get; private set; }
	}
}
