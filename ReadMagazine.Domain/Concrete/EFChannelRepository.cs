using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using ReadMagazine.Domain.Abstract;
using ReadMagazine.Domain.Concrete.ORM;
using entitie = ReadMagazine.Domain.Entities;

namespace ReadMagazine.Domain.Concrete
{
    public class EFChannelRepository : IChannelRepository
    {

        #region Metodos para extraer urlImagen
        /// <summary>Default Uri</summary>
        private const string TEMPURI = "http://tempuri.org";
        private const string STR_IMGTAG_SRC_EXP = @"<img\s+[^>]*\bsrc\s*\=\s*[\x27\x22](?<Url>[^\x27\x22]*)[\x27\x22]";

        /// <summary>
        /// Extracts the first image Url from a html string
        /// </summary>
        /// <param name="htmlString">A string containing html code</param>
        /// <returns>a string with the Url or first image in the htmlString parameter</returns>
        /// <remarks>This method uses regular expressions,so using System.Text.RegularExpressions; must be addeed</remarks>
        public static string ExtractFirstHtmlImage(string htmlString)
        {
            string respuesta = TEMPURI;
            try
            {
                var rgx = new Regex(
                    STR_IMGTAG_SRC_EXP,
                                RegexOptions.IgnoreCase | RegexOptions.Multiline);

                var match = rgx.Match(htmlString);

                respuesta = match.Groups["Url"].Value;

                if (respuesta == "")
                    respuesta = TEMPURI;
            }
            catch { respuesta = TEMPURI; }

            return respuesta;
        }
        #endregion


        private ReadMagazineEntities context = new ReadMagazineEntities();
        #region IChannelRepository Members

        public IQueryable<ORM.Noticia> Noticias
        {
            get { return context.Noticias; }
        }

        public entitie.ChannelRss GetNoticias(ORM.Channel channelView)
        {
            if (channelView != null)
            {
                var urlXml = channelView.UrlXml;

                XmlDocument doc = new XmlDocument();
                XmlTextReader reader = new XmlTextReader(urlXml);
                doc.Load(reader);
                //doc.Save("c:\\reduser.xml");
                //NameSpace para leer el content
                XmlNamespaceManager namespaceManager = new XmlNamespaceManager(doc.NameTable);
                namespaceManager.AddNamespace("content", doc.DocumentElement.GetNamespaceOfPrefix("content"));

                var tituloChanel = doc.SelectSingleNode("/rss/channel/title");
                var linkChanel = doc.SelectSingleNode("/rss/channel/link");
                var descriptionChannel = doc.SelectSingleNode("/rss/channel/description");
                var channel = new entitie.ChannelRss();

                if (tituloChanel != null && linkChanel != null)
                {
                    channel.TituloChannel = tituloChanel.InnerText;
                    channel.LinkChannel = linkChanel.InnerText;
                }
                if (descriptionChannel != null)
                    channel.DescriptionChannel = descriptionChannel.InnerText;
                XmlNodeList items = doc.GetElementsByTagName("item");
                channel.Items = new List<entitie.Noticia>();


                foreach (XmlNode item in items)
                {
                    var noticia = new entitie.Noticia();
                    var titulo = item.SelectSingleNode("title");
                    var desc = item.SelectSingleNode("description");
                    var link = item.SelectSingleNode("link");
                    var contenido = item.SelectSingleNode("content:encoded", namespaceManager);

                    noticia.Title = titulo.InnerText;
                    noticia.Link = link.InnerText;
                    noticia.Descripcion = desc.InnerText;
                    if (contenido != null)
                    {
                        noticia.Contenido = contenido.InnerText;
                    }

                    noticia.UrlImage = ExtractFirstHtmlImage(noticia.Descripcion);

                    channel.Items.Add(noticia);
                }
                return channel;

            }
            else
                return null;
        }

        #endregion
    }


}
