using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using ReadMagazine.Domain.Entities;


namespace ReadMagazine.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<ClientEntitie> Clients { get; set; }
        
    }
}
