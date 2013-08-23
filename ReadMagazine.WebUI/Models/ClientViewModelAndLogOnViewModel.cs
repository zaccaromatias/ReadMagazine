using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadMagazine.WebUI.Models
{
    public class ClientViewModelAndLogOnViewModel
    {
        public ClientExtendedViewModel ClientExtendedView{ get; set; }
        public LogOnViewModel Login { get; set; }
    }
}