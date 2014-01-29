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
using ReadMagazine.Domain.Helpers;

namespace ReadMagazine.Domain.Concrete
{
    public class EFChannelRepository : IChannelRepository
    {
        private ReadMagazineEntities context = new ReadMagazineEntities();
        private XmlNamespaceManager NamespaceManager { get; set; }
        private static int Contador { get; set; }
        private static List<int> DivicionesList { get; set; }

        #region IChannelRepository Members

        public IQueryable<ORM.Noticia> Noticias
        {
            get { return context.Noticias; }
        }


        public entitie.ChannelRss GetNoticias(ORM.ChannelDB channelView)
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
                if (quedanNoticias != 1)
                {
                    while ((quedanNoticias % minimoNoticiasPorPagina <= 1 && quedanNoticias % minimoNoticiasPorPagina != 0))
                    {
                        minimoNoticiasPorPagina++;
                    }
                }
                else minimoNoticiasPorPagina = 1;
                var maximoNoticiasPorPagina = quedanNoticias >= 8 ? 8 : quedanNoticias <= minimoNoticiasPorPagina ? minimoNoticiasPorPagina : quedanNoticias;
                Pagina pagina = new Pagina() { Noticias = new List<entitie.Noticia>() };
                var cantidadEnEstaPagina = ramdom.Next(minimoNoticiasPorPagina, maximoNoticiasPorPagina);
                List<int> listaValoresAtomarFinalWidth;
                List<int> listaValoresAtomarFinalHeigth;
                Contador = 0;
                var primeroWidth = ramdom.Next(0, 1);
                if (primeroWidth == 0)
                {
                    //Cargar primero los anchos
                    DivicionesList = new List<int>();
                    listaValoresAtomarFinalWidth = GetValoresFinales(valoresPosible, ramdom, cantidadEnEstaPagina, false);
                    listaValoresAtomarFinalHeigth = GetValoresFinales(valoresPosible, ramdom, cantidadEnEstaPagina, true);
                }
                else
                {
                    //Cargar Primero las alturas
                    DivicionesList = new List<int>();
                    listaValoresAtomarFinalHeigth = GetValoresFinales(valoresPosible, ramdom, cantidadEnEstaPagina, false);
                    listaValoresAtomarFinalWidth = GetValoresFinales(valoresPosible, ramdom, cantidadEnEstaPagina, true);
                }
                var indiceListaValoresW = 0;
                var indiceListaValoresH = 0;
                int acum = 0;
                for (int i = totalDeNoticias - quedanNoticias; i < (totalDeNoticias - quedanNoticias) + cantidadEnEstaPagina; i++)
                {
                    entitie.Noticia noticia = new entitie.Noticia();
                    var titulo = items[i].SelectSingleNode("title");
                    var desc = items[i].SelectSingleNode("description");
                    var link = items[i].SelectSingleNode("link");
                    var contenido = items[i].SelectSingleNode("content:encoded", this.NamespaceManager);

                    noticia.Title = titulo.InnerText;
                    noticia.Link = link.InnerText;
                    noticia.Descripcion =  desc.InnerText;
                    if (contenido != null)
                    {
                        noticia.Contenido = contenido.InnerText;
                    }
                    else
                        noticia.Contenido = string.Empty;
                    noticia.UrlImage = ExtractImages.ExtractFirstHtmlImage(desc.InnerText + noticia.Contenido);
                    //var pepe = ExtractImages.CambiarTamanoImagen(noticia.UrlImage,683, 600);


                    if (primeroWidth == 0)
                    {
                        if (acum >= DivicionesList[indiceListaValoresH])
                        {
                            indiceListaValoresH++;
                            acum = -1;
                        }
                    }
                    else
                    {
                        if (acum >= DivicionesList[indiceListaValoresW])
                        {
                            indiceListaValoresW++;
                            acum = -1;
                        }
                    }
                    noticia.WidthClass = "w-" + listaValoresAtomarFinalWidth[indiceListaValoresW];


                    noticia.HeigthClass = "h-" + listaValoresAtomarFinalHeigth[indiceListaValoresH];



                    pagina.Noticias.Add(noticia);

                    if (primeroWidth == 0)
                        indiceListaValoresW++;
                    else
                        indiceListaValoresH++;
                    acum++;
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
                    DivicionesList.Add(dividirAnchoOAltoEn);
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
