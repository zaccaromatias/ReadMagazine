using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ReadMagazine.Domain.Entities;

namespace ReadMagazine.WebUI.Models
{
    public class ClientExtendedViewModel : ClientEntitie
    {
        [Required(ErrorMessage = "Please enter a ConfirmPasword")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}