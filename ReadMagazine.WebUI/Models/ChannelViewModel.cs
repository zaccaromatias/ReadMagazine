using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReadMagazine.Domain.Entities;

namespace ReadMagazine.WebUI.Models
{
    public class ChannelViewModel : ChannelRss
    {
        public IEnumerable<Channel> Channels { get; set; }
        public int Numero { get; set; }
        
    }

   
}