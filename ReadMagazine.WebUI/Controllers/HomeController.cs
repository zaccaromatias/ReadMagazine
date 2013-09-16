using System;
using System.Web.Mvc;
using System.Web.Security;
using ReadMagazine.Domain.Abstract;
using ReadMagazine.WebUI.Models;
using ORM = ReadMagazine.Domain.Concrete.ORM;


namespace ReadMagazine.Controllers
{
    public class HomeController : Controller
    {
        #region Propiedades
        private IClientRepository _clientRepository;

        #endregion

        #region Contructor

        public HomeController(IClientRepository repository)
        {
            _clientRepository = repository;
        }
        #endregion

        #region Actions

        public ViewResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SignUp(ClientViewModelAndLogOnViewModel clientViewModelAndLogOnViewModel)
        {
            var clientExtended = clientViewModelAndLogOnViewModel.ClientExtendedView;

            if (ModelState.IsValid)
            {
                var client = new ORM.Client()
                {
                    ClientId = clientExtended.ClientId,
                    UserName = clientExtended.UserName,
                    Password = clientExtended.Password,
                    Email = clientExtended.Email
                };
                try
                {
                    _clientRepository.SaveClient(client);
                }
                catch (Exception)
                {
                    //TODO: Manejo de errores
                    TempData["messageError"] = string.Format("Error when registering, try later!", client.UserName);
                    ViewBag.Page = "1";
                    return View("index", clientViewModelAndLogOnViewModel);
                }

                TempData["message"] = string.Format("{0} is now a member of ReadMagazine!", client.UserName);
                ViewBag.Page = "0";
                return View("index", clientViewModelAndLogOnViewModel);
            }
            else
            {
                ViewBag.Page = "1";
                return View("index", clientViewModelAndLogOnViewModel);
            }

        }

        [HttpPost]
        public ActionResult Login(ClientViewModelAndLogOnViewModel clientViewModelAndLogOnViewModel)
        {
            var login = clientViewModelAndLogOnViewModel.Login;
            if (ModelState.IsValid)
            {
                var client = new ORM.Client() { UserName = login.UserName, Password = login.Password };
                var clientValid = _clientRepository.GetUser(client);
                if (clientValid != null)
                {
                    TempData["errorUserOrPasword"] = "Logeado con exito";
                    FormsAuthentication.SetAuthCookie(clientValid.UserName, login.Remember);
                }
                else
                {
                    TempData["errorUserOrPasword"] = "UserName or Password Incorrect!";
                }
                return View("index", clientViewModelAndLogOnViewModel);
            }
            else
            {
                ViewBag.Page = "0";
                return View("index", clientViewModelAndLogOnViewModel);
            }
        }

        #endregion
    }
}