using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReadMagazine.Domain.Concrete.ORM;
using entite = ReadMagazine.Domain.Entities;

namespace ReadMagazine.Domain.Abstract
{
    public interface IChannelRepository
    {
        
       entite.ChannelRss GetNoticias(ChannelDB channel);
    }
}
