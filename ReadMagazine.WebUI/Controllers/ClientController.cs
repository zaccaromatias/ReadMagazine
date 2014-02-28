using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReadMagazine.Domain.Abstract;
using ReadMagazine.Domain.Concrete;
using ReadMagazine.Domain.Entities;
using ReadMagazine.WebUI.Infrastructure.Abstract;
using ReadMagazine.WebUI.Models;
using ORM = ReadMagazine.Domain.Concrete.ORM;
using System.Web.Security;
namespace ReadMagazine.WebUI.Controllers
{
    public class ClientController : Controller
    {
        private IClientRepository _clientRepository;

        //
        // GET: /Client/

        public ActionResult Index()
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
                    TempData["messageError"] = string.Format("Error when registering, try later!", client.UserName);
                    ViewBag.Page = "1";
                    return View("index", clientViewModelAndLogOnViewModel);
                    //throw;
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



        public ClientController(IClientRepository repository)
        {
            _clientRepository = repository;
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
                    return RedirectToAction("channels", "channel");
                }
                else
                {
                    TempData["errorUserOrPasword"] = "UserName or Password Incorrect!";
                    return View("index", clientViewModelAndLogOnViewModel);
                }


            }
            else
            {
                ViewBag.Page = "0";
                return View("index", clientViewModelAndLogOnViewModel);
            }
        }
    }
}
