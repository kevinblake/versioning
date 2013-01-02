using System.Web.Mvc;
using Puzzlebox.Versioning.Business;

namespace Puzzlebox.Versioning.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
			return Json(VersionInformation.GetVersionInformation(), JsonRequestBehavior.AllowGet);
        }

    }
}
