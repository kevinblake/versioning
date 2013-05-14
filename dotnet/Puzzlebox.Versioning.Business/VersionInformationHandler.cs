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

	        string jsoncallback = context.Request.QueryString.Get("callback");

			var json = VersionInformation.GetVersionInformation().ToJson();

			if (string.IsNullOrEmpty(jsoncallback))
			{
				context.Response.Write(json);
			}
			else
			{
				context.Response.Write(string.Format("{0}({1})", jsoncallback, json));
			}
		}

		public bool IsReusable { get; private set; }
	}
}