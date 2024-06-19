using Negocio;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Ast;

namespace Presentacion.Controllers
{
    public class PersonaController : Controller
    {
        [HttpGet]
        public ActionResult GetAllPersonaView()
        {
            Negocio.Persona persona = new Negocio.Persona();

            var resutl = Negocio.Persona.GetAllPersona();

            if (resutl != null)
            {
                persona.Personas = resutl;
                return View(persona);
            }
            else
            {
                return View(persona);
            }
        }

        [HttpGet]
        public ActionResult FormsPersona(int? IdPersona)
        {
            Persona persona = new Persona();

            List<TipoPersonaFiscal> ListaTiposPersonaFiscal = new List<TipoPersonaFiscal>
            {
                new TipoPersonaFiscal
                {
                    IdTipoPersonaFisica = 1,
                    NombreTipoPersonaFisica = "Fisica"
                },
                new TipoPersonaFiscal
                {
                    IdTipoPersonaFisica = 2,
                    NombreTipoPersonaFisica = "Moral"
                },
                new TipoPersonaFiscal
                {
                    IdTipoPersonaFisica = 3,
                    NombreTipoPersonaFisica = "Fisica con Actividad Empresarial"
                }
            };

            List<PaisOrigen> ListaPaisOrigen = new List<PaisOrigen>
            {
                new PaisOrigen
                {
                    IdPaisOrigen = 1,
                    NombrePaisOrigen = "Mexico"
                },
                new PaisOrigen
                {
                    IdPaisOrigen = 2,
                    NombrePaisOrigen = "Estados Unidos"
                },
                new PaisOrigen
                {
                    IdPaisOrigen = 3,
                    NombrePaisOrigen = "Argentina"
                }
            };

            List<Sexo> ListaSexo = new List<Sexo>
            {
                new Sexo
                {
                    IdSexo = 1,
                    NombreSexo = "Masculino"
                },
                new Sexo
                {
                    IdSexo = 2,
                    NombreSexo = "Femenino"
                },
                new Sexo
                {
                    IdSexo = 3,
                    NombreSexo = "Sin definir"
                }
            };

            if (IdPersona != null) // Update
            {
                var result = Persona.GetByIdPersona(IdPersona.Value);

                //DateTime fechaNacimientoAuxiliar = DateTime.ParseExact(result.FechaNacimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime fechaNacimientoAuxiliar = DateTime.Parse(result.FechaNacimiento);

                result.FechaNacimiento = fechaNacimientoAuxiliar.ToString("MM / dd / yyyy");

                persona = result;

                ViewBag.ListaTiposPersonaFiscal = new SelectList(ListaTiposPersonaFiscal, "IdTipoPersonaFisica", "NombreTipoPersonaFisica", persona.tipoPersonaFiscal.IdTipoPersonaFisica);
                ViewBag.ListaPaisOrigen = new SelectList(ListaPaisOrigen, "IdPaisOrigen", "NombrePaisOrigen", persona.paisOrigen.IdPaisOrigen);
                ViewBag.ListaSexo = new SelectList(ListaSexo, "IdSexo", "NombreSexo", persona.sexo.IdSexo);
 
                return View(persona);
            }
            else // Add
            {
                ViewBag.ListaTiposPersonaFiscal = new SelectList(ListaTiposPersonaFiscal, "IdTipoPersonaFisica", "NombreTipoPersonaFisica");
                ViewBag.ListaPaisOrigen = new SelectList(ListaPaisOrigen, "IdPaisOrigen", "NombrePaisOrigen");
                ViewBag.ListaSexo = new SelectList(ListaSexo, "IdSexo", "NombreSexo");

                return View(persona);
            }
        }

        [HttpPost]
        public ActionResult FormsPersona(Persona persona)
        {
            if (persona.IdPersona != 0) //Update
            {
                var result = Persona.UpdatePersona(persona);

                if (result == true)
                {
                    TempData["SuccessMessage"] = "La persona se actualizo exitosamente.";
                    return RedirectToAction("GetAllPersonaView");
                }
                else
                {
                    TempData["SuccessMessage"] = "Error";
                    return RedirectToAction("GetAllPersonaView");
                }
            }
            else
            {
                var result = Persona.AddPersona(persona);

                if (result == true)
                {
                    TempData["SuccessMessage"] = "La persona se agrego exitosamente.";
                    return RedirectToAction("GetAllPersonaView");
                }
                else
                {
                    TempData["SuccessMessage"] = "Error";
                    return RedirectToAction("GetAllPersonaView");
                }
            }
        }

        [HttpGet]
        public ActionResult DeletePersona(int IdPersona)
        {
            var result = Persona.DeletePersona(IdPersona);

            if (result == true)
            {
                TempData["SuccessMessage"] = "La persona se eliminó exitosamente.";
                return RedirectToAction("GetAllPersonaView");
            }
            else
            {
                TempData["SuccessMessage"] = "Error";
                return RedirectToAction("GetAllPersonaView");
            }
        }
    }
}