using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TipoBanco
    {
        public int IdTipoBanco { get; set; }
        public string NombreTipoBanco { get; set; }

        public List<TipoBanco> TiposBancos { get; set; }

        public static List<TipoBanco> GetListBanco()
        {
            List<TipoBanco> ListaTiposBanco = new List<TipoBanco>
            {
                  new TipoBanco
                  {
                      IdTipoBanco = 1,
                      NombreTipoBanco = "BBVA"
                  },
                  new TipoBanco
                  {
                      IdTipoBanco= 2,
                      NombreTipoBanco = "Santander"
                  },
                  new TipoBanco
                  {
                      IdTipoBanco = 3,
                      NombreTipoBanco = "ScotiaBank"
                  }
            };

            return ListaTiposBanco;
        }
    }
}