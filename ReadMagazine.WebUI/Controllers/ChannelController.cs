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
            var urlXml = string.Empty;
            switch (id)
            {   
                case "1": urlXml="http://feeds.feedburner.com/redusers/internacional";
                    break;
                case "2": urlXml = "http://eltreboldigital.com.ar/rss/ult_noticias.xml";
                    break;
                case "3": urlXml = "http://feeds.feedburner.com/CssTricks";
                    break;
                case "4": urlXml = "http://radioeltrebol.com.ar/feed";
                    break;
                case "5": urlXml = "http://www.lacapital.com.ar/rss/home.xml";
                    break;
                case "6": urlXml = "http://feeds.feedburner.com/alistapart/main";
                    break;
                case "7": urlXml = "http://clarin.feedsportal.com/c/33088/f/577681/index.rss";
                    break;
                default:
                    break;
            }
            var channel = new ChannelDB() { UrlXml = urlXml };

            var model = _channerlRepository.GetNoticias(channel);
            return View(model);
        }

    }
}
