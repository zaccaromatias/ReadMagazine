//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReadMagazine.Domain.Concrete.ORM
{
    using System;
    using System.Collections.Generic;
    
    public partial class Channel
    {
        public Channel()
        {
            this.Noticias = new HashSet<Noticia>();
        }
    
        public int Id { get; set; }
        public int ID_Client { get; set; }
        public string Name { get; set; }
        public string UrlXml { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual ICollection<Noticia> Noticias { get; set; }
    }
}
