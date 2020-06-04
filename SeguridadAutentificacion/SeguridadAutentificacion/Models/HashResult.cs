using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeguridadAutentificacion.Models
{
    public class HashResult
    {
        public string Has { get; set; }
        public byte[] Salt { get; set; }
    }
}
