using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TipoPersonaFiscal
    {
        public int IdTipoPersonaFisica { get; set; }

        public string NombreTipoPersonaFisica { get; set; }

        public List<TipoPersonaFiscal> TiposPersonasFiscal { get; set; }
    }
}