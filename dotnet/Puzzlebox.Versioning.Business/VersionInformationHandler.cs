using System.Web;
using Newtonsoft.Json;

namespace Puzzlebox.Versioning.Business
{
	public class VersionInformationHandler : IHttpHandler
	{
		public void ProcessRequest(HttpContext context)
		{
			var settings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.None, NullValueHandling = NullValueHandling.Ignore };
			context.Response.ContentType = "application/json";
			context.Response.Write(JsonConvert.SerializeObject(VersionInformation.GetVersionInformation(), Formatting.Indented, settings));
		}

		public bool IsReusable { get; private set; }
	}
}
