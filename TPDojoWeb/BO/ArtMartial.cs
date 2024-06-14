using Microsoft.VisualStudio.TextTemplating;
using TPDojoWeb.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPDojoWeb.BO
{
    public class ArtMartial :Entity
    {
        public List<Samourai>? Samourais { get; set; }
    }
}



