using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Sexo
    {
        public int IdSexo { get; set; }

        public string NombreSexo { get; set; }

        public List<Sexo> Sexos { get; set; }
    }
}