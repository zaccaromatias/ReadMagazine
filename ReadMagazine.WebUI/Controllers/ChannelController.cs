using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReadMagazine.Domain.Abstract;
using ReadMagazine.Domain.Concrete.ORM;

namespace ReadMagazine.WebUI.Controllers
{
    public class ChannelController : Controller
    {
        private IChannelRepository _channerlRepository;
        //
        // GET: /Channel/

        public ChannelController(IChannelRepository repository)
        {
            _channerlRepository = repository;
        }

        public ActionResult Index(string id)
        {
            ViewBag.id = id;
            //http://feeds.feedburner.com/redusers/internacional
            var channel = new Channel() { UrlXml = "http://feeds.feedburner.com/redusers/internacional" };
            var model = _channerlRepository.GetNoticias(channel);
            return View(model);
        }

    }
}
