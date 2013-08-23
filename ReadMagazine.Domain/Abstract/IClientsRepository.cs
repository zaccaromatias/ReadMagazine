using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReadMagazine.Domain.Concrete.ORM;

namespace ReadMagazine.Domain.Abstract
{
    public interface IClientRepository
    {
         IQueryable<Client> Clients {get;}
         void SaveClient(Client client);
         Client GetUser(Client client);
    }

}
