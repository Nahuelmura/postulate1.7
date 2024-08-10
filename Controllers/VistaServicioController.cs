using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Postulate.Data;
using Postulate.Models;

namespace Postulate.Controllers
{
    public class VistaServicioController : Controller
    {
        private readonly ILogger<VistaServicioController> _logger;
        private readonly ApplicationDbContext _context;

        public VistaServicioController(ILogger<VistaServicioController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View("VistaServicio");
        }
 public JsonResult PerfilServicio(int id)
    {
        var perfilServicio = _context.Servicios
            .Include(s => s.Persona)
            .Include(s => s.Profesion)
            .Where(s => s.ServicioID == id)
            .Select(e => new VistaServicio
            {
                ServicioID = e.ServicioID,
                PersonaID = e.PersonaID,
                ProfesionID = e.ProfesionID,
                ImagenID = e.ImagenID,
                NombrePersona = e.Persona.Nombre,
                ApellidoPersona = e.Persona.Apellido,    
                TelefonoPersona = e.Persona.Telefono,
                EdadPersona = e.Persona.Edad,
                DocumentoPersona = e.Persona.Documento,
                



                NombreProfesion = e.Profesion.Nombre,
            
                Herramienta = e.Herramienta,
                Descripcion = e.Descripcion,
                Titulo = e.Titulo,
                Institucion = e.Institucion,
                EmailPersona =e.Persona.Email,
              
            })
            .FirstOrDefault();

        if (perfilServicio == null)
        {
            return Json(new { success = false, message = "ID no encontrado" });
        }

        return Json(new { success = true, data = perfilServicio });
    }
}

}

//   var serviciosPlanos = _context.Servicios(e => new VistaServicio
//     {
//         ServicioID = e.ServicioID,
//         PersonaID = e.PersonaID,
//         ProfesionID = e.ProfesionID,
//         ImagenID = e.ImagenID,
//         NombrePersona = e.Persona.Nombre,
//         NombreProfesion = e.Profesion.Nombre,
//         TelefonoPersona = e.Persona.Telefono,
//         Herramienta = e.Herramienta,
//         Descripcion = e.Descripcion,
//         Titulo = e.Titulo,
//         Institucion = e.Institucion
//     }).ToList();

//     if (id != null)
//     {
//         serviciosPlanos = serviciosPlanos.Where(t => t.ServicioID == id).ToList();
//     }


//   public JsonResult PerfilServicio(int id)
// {
//     var perfilServicio = _context.Servicios
//         .Include(s => s.Persona)
//         .Include(s => s.Profesion)
//         .Where(s => s.ServicioID == id)
//         .Select(s => new VistaServicio
//         {
//             ServicioID = s.ServicioID,
//             PersonaID = s.Persona.PersonaID,
//             NombrePersona = s.Persona.Nombre,
//             TelefonoPersona = s.Persona.Telefono,
//             ProfesionID = s.Profesion.ProfesionID,
//             NombreProfesion = s.Profesion.Nombre,
//             ImagenID = s.ImagenID,
//             Herramienta = s.Herramienta,
//             Descripcion = s.Descripcion,
//             Titulo = s.Titulo,
//             Institucion = s.Institucion
//         })
//         .ToList();

//     return Json(perfilServicio);
// }


// }
