using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReadMagazine.Domain.Entities
{
    public class ChannelRss
    {
        public string TituloChannel { get; set; }
        public string LinkChannel { get; set; }
        public string UrlImage { get; set; }
        public string DescriptionChannel { get; set; }

        public List<Pagina> Paginas { get; set; }
        //public List<Noticia> Items { get; set; }

        public Noticia NoticiaTapa
        {
            get 
            {
                //int indicePagina = -1;
                //int indiceNoticia = -1;
                //int maxHeight = 0;
                //int posMaxNoticia = 0;
                //int posMaxPagina = 0;
                //foreach (Pagina pagina in this.Paginas)
                //{
                //    indiceNoticia = -1;
                //    indicePagina++;
                //    foreach (Noticia noticia in pagina.Noticias)
                //    {
                //        indiceNoticia++;
                //        if (noticia.HeigthMaxImages > maxHeight)
                //        {
                //            posMaxNoticia = indiceNoticia;
                //            posMaxPagina = indicePagina;
                //            maxHeight = noticia.HeigthMaxImages;
                //        }
                //    }
                //}
                //if (indicePagina == -1)
                //    return null;
                //else
                //    return this.Paginas[posMaxPagina].Noticias[posMaxNoticia];

                return this.Paginas[0].Noticias[3];

            }
        }
    }
}
