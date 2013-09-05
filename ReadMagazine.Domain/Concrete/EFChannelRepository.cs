using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using ReadMagazine.Domain.Abstract;
using ReadMagazine.Domain.Concrete.ORM;
using ReadMagazine.Domain.Entities;
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
        private XmlNamespaceManager NamespaceManager { get; set; }
        private static int Contador { get; set; }

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
                this.NamespaceManager = new XmlNamespaceManager(doc.NameTable);
                this.NamespaceManager.AddNamespace("content", doc.DocumentElement.GetNamespaceOfPrefix("content"));

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
                channel.Paginas = CrearLayoutsParaCadaPagina(items);
                var pp = "";
                //foreach (XmlNode item in items)
                //{
                //    var noticia = new entitie.Noticia();
                //    var titulo = item.SelectSingleNode("title");
                //    var desc = item.SelectSingleNode("description");
                //    var link = item.SelectSingleNode("link");
                //    var contenido = item.SelectSingleNode("content:encoded", this.NamespaceManager);

                //    noticia.Title = titulo.InnerText;
                //    noticia.Link = link.InnerText;
                //    noticia.Descripcion = desc.InnerText;
                //    if (contenido != null)
                //    {
                //        noticia.Contenido = contenido.InnerText;
                //    }

                //    noticia.UrlImage = ExtractFirstHtmlImage(noticia.Descripcion);

                //    channel.Items.Add(noticia);
                //}
                return channel;

            }
            else
                return null;
        }

        private List<Pagina> CrearLayoutsParaCadaPagina(XmlNodeList items)
        {
            List<int> valoresPosible = new List<int>(new int[] { 25, 30, 40, 50, 60, 70, 75, 100 });
            var totalDeNoticias = items.Count;
            var quedanNoticias = totalDeNoticias;

            List<Pagina> paginas = new List<Pagina>();
            var ramdom = new Random();


            while (quedanNoticias > 0)
            {
                var minimoNoticiasPorPagina = 2;
                while (quedanNoticias % minimoNoticiasPorPagina == 1)
                {
                    minimoNoticiasPorPagina++;
                }
                var maximoNoticiasPorPagina = quedanNoticias >= 8 ? 8 : quedanNoticias <= minimoNoticiasPorPagina ? minimoNoticiasPorPagina : quedanNoticias;
                Pagina pagina = new Pagina() { Noticias = new List<entitie.Noticia>() };
                var cantidadEnEstaPagina = ramdom.Next(minimoNoticiasPorPagina, maximoNoticiasPorPagina);
                List<int> listaValoresAtomarFinalWidth;
                List<int> listaValoresAtomarFinalHeigth;
                Contador = 0;
                var bit = ramdom.Next(0, 1);
                if (bit == 0)
                {
                    //Cargar primero los anchos
                    listaValoresAtomarFinalWidth = GetValoresFinales(valoresPosible, ramdom, cantidadEnEstaPagina, false);
                    listaValoresAtomarFinalHeigth = GetValoresFinales(valoresPosible, ramdom, cantidadEnEstaPagina, true);
                }
                else
                {
                    //Cargar Primero las alturas
                    listaValoresAtomarFinalHeigth = GetValoresFinales(valoresPosible, ramdom, cantidadEnEstaPagina, false);
                    listaValoresAtomarFinalWidth = GetValoresFinales(valoresPosible, ramdom, cantidadEnEstaPagina, true);
                }
                var indiceListaValores = 0;
                for (int i = totalDeNoticias - quedanNoticias; i < (totalDeNoticias - quedanNoticias) + cantidadEnEstaPagina; i++, indiceListaValores++)
                {
                    var noticia = new entitie.Noticia();
                    var titulo = items[i].SelectSingleNode("title");
                    var desc = items[i].SelectSingleNode("description");
                    var link = items[i].SelectSingleNode("link");
                    var contenido = items[i].SelectSingleNode("content:encoded", this.NamespaceManager);

                    noticia.Title = titulo.InnerText;
                    noticia.Link = link.InnerText;
                    noticia.Descripcion = desc.InnerText;
                    if (contenido != null)
                    {
                        noticia.Contenido = contenido.InnerText;
                    }
                    noticia.WidthClass = "w-" + listaValoresAtomarFinalWidth[indiceListaValores];
                    noticia.HeigthClass = "h-" + listaValoresAtomarFinalHeigth[indiceListaValores];
                    noticia.UrlImage = ExtractFirstHtmlImage(noticia.Descripcion);

                    pagina.Noticias.Add(noticia);
                }
                paginas.Add(pagina);
                quedanNoticias = quedanNoticias - cantidadEnEstaPagina;

            }
            return paginas;

        }

        private static List<int> GetValoresFinales(List<int> valoresPosible, Random ramdom, int cantidadEnEstaPagina, bool tenerEncuentaContadorPrevio)
        {
            List<int> listaValoresAtomarFinal = new List<int>();
            List<int> listaValoresAtomar = new List<int>();
            var acum = 0;


            for (int i = 0; i < cantidadEnEstaPagina; i++)
            {
                int minimoATomar = 1;
                if (cantidadEnEstaPagina > 4)
                {
                    minimoATomar = cantidadEnEstaPagina - i % 4 >= 1 ? 1 : 4;
                }



                var dividirAnchoOAltoEn = tenerEncuentaContadorPrevio ? i >= Contador ? cantidadEnEstaPagina - i : Contador : ramdom.Next(minimoATomar, cantidadEnEstaPagina - i > 4 ? 4 : cantidadEnEstaPagina - i);

                while (acum != 100)
                {
                    listaValoresAtomar = new List<int>();
                    acum = 0;
                    for (int i2 = 0; i2 < dividirAnchoOAltoEn; i2++)
                    {
                        var indiceValoresPosibleWidth = dividirAnchoOAltoEn == 1 ? 7 : ramdom.Next(0, valoresPosible.Count - 1);
                        listaValoresAtomar.Add(valoresPosible[indiceValoresPosibleWidth]);
                        acum = acum + valoresPosible[indiceValoresPosibleWidth];
                    }
                }
                if (!tenerEncuentaContadorPrevio)
                {
                    if (Contador != 4)
                        Contador++;
                    i = i + listaValoresAtomar.Count - 1;
                }
                else 
                {
                    var sumar = i >= Contador ? cantidadEnEstaPagina - i : Contador;
                    i = i + sumar - 1;

                }
                foreach (var val in listaValoresAtomar)
                {
                    listaValoresAtomarFinal.Add(val);
                }
                acum = 0;
            }
            return listaValoresAtomarFinal;
        }

        #endregion
    }


}
