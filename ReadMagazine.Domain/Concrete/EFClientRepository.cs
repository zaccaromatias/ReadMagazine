using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using ReadMagazine.Domain.Abstract;
using ReadMagazine.Domain.Concrete.ORM;




namespace ReadMagazine.Domain.Concrete
{
    public class EFClientRepository : IClientRepository
    {
        private ReadMagazineEntities context = new ReadMagazineEntities();
        public IQueryable<Client> Clients
        {
            get { return context.Clients; }
        }
        public void SaveClient(Client client)
        {
            try
            {
                if (client.ClientId == 0)
                {
                    context.Clients.Add(client);
                }
                else
                {
                    context.Entry(client).State = System.Data.EntityState.Modified;
                }
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            
        }

        public Client GetUser(Client client) 
        {
            if (client != null)
            {
                var user = context.Clients.FirstOrDefault(cl => cl.UserName == client.UserName && cl.Password == client.Password);
                return user;
                    
            }
            else
                return null;
        }
    }
}
