using System.Web.Mvc;
namespace ReadMagazine.Controllers {
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}