using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadMagazine.Domain.Entities
{
    public class ClientEntitie
    {
        [HiddenInput(DisplayValue = false)]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Please enter a User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter a Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter a Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public virtual ICollection<Channel> Channels { get; set; }
    }
}
