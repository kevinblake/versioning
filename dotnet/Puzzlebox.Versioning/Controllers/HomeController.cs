using System.Web.Mvc;
using Puzzlebox.Versioning.Business;
using Puzzlebox.Versioning.Business.Extensions;

namespace Puzzlebox.Versioning.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
			return Content(VersionInformation.GetVersionInformation().ToJson(), "application/json");
        }

    }
}
