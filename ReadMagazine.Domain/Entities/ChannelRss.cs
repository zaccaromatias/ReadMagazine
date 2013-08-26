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
        
        public List<Noticia> Items { get; set; }
    }
}
