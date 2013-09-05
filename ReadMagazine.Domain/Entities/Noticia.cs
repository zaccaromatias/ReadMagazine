using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ReadMagazine.Domain.Entities
{
    public class Noticia
    {

        [HiddenInput(DisplayValue = false)]
        public int Id_Noticia { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Required(ErrorMessage = "Please enter a Channel")]
        public int Id_Channel { get; set; }

        [Required(ErrorMessage = "Please enter a Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter a Link")]
        public string Link { get; set; }

        [Required(ErrorMessage = "Please enter a Descripcion")]
        public string Descripcion { get; set; }

        public string WidthClass { get; set; }
        public string HeigthClass { get; set; }
        public string Contenido { get; set; }

        
        public string UrlImage { get; set; }
    }
}
