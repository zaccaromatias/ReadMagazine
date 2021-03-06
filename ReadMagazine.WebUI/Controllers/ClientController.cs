﻿using System;
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
                //if (TempData["ModelErrors"] == null)
                //    TempData.Add("ModelErrors", new List<string>());
                //foreach (var obj in ModelState.Values)
                //{
                //    foreach (var error in obj.Errors)
                //    {
                //        if (!string.IsNullOrEmpty(error.ErrorMessage))
                //            ((List<string>)TempData["ModelErrors"]).Add(error.ErrorMessage);
                //    }
                //}

                //Url.Action("Index", new { client = clientExtended, page = 1 });
                //var url = Url.Action("index", new { client = clientExtended, page = 1 });
                //return RedirectToAction("index",new {client=clientExtended,page=1 });
                ViewBag.Page = "1";
                //return View("index", clientExtended);
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
    }
}
