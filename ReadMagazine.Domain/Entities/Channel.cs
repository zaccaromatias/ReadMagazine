using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ReadMagazine.Domain.Concrete.ORM;

namespace ReadMagazine.Domain.Entities
{
    public class Channel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        
        [HiddenInput(DisplayValue = false)]
        [Required(ErrorMessage = "Please enter a Client")]
        public int ID_Client { get; set; }

        [Required(ErrorMessage = "Please enter a Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a UrlXml")]
        public string UrlXml { get; set; }

        //[Required(ErrorMessage = "Please enter a Client")]
        //public virtual Client Client { get; set; }

        //[Required(ErrorMessage = "Please enter a Client")]
        //public virtual ICollection<Noticia> Noticias { get; set; }
    }
}
