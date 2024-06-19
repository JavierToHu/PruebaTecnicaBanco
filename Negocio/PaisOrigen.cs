using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PaisOrigen
    {
        public int IdPaisOrigen { get; set; }

        public string NombrePaisOrigen { get; set; }

        public List<PaisOrigen> Paises { get; set; }
    }
}