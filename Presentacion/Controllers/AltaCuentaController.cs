using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class AltaCuentaController : Controller
    {
        // GET: AltaCuenta
        public ActionResult GetViewPersonaAltaCuenta()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllCuentaByIdPersona(int IdPersona)
        {
            var result = AltaCuenta.GetAllCuentaByIdPersona(IdPersona);

            if (result != null)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetTiposBancos()
        {
            var result = TipoBanco.GetListBanco();

            if (result != null)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult DeleteCuenta(int IdAltaCuenta)
        {
            var result = AltaCuenta.DeleteCuentaByIdAltaCuenta(IdAltaCuenta);

            if (result == true)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult AddCuenta(AltaCuenta altaCuenta)
        {
            var result = AltaCuenta.AddCuentaByIdAltaCuenta(altaCuenta);

            if (result == true)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPut]
        public JsonResult UpdateCuenta(AltaCuenta altaCuenta)
        {
            var result = AltaCuenta.UpdateAltaCuentaByIdAltaCuenta(altaCuenta);

            if (result == true)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}