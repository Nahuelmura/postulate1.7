using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Postulate.Data;
using Postulate.Models;

namespace Postulate.Controllers;

public class PersonaController : Controller
{
    private readonly ILogger<PersonaController> _logger;
    
        private readonly ApplicationDbContext _context;

    public PersonaController(ILogger<PersonaController> logger,ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }


 
    public IActionResult Index()
    {  

        // Cargar provincias de la base de datos
        var provincias = _context.Provincias.ToList();
        var provinciasBuscar = provincias.ToList();

        // Agregar opciones de selección predeterminadas
        provincias.Add(new Provincia { ProvinciaID = 0, Nombre = "[SELECCIONE...]" });
        provinciasBuscar.Add(new Provincia { ProvinciaID = 0, Nombre = "[TODAS LAS PROVINCIAS]" });

        // Asignar las listas de selección al ViewBag con las claves correctas
        ViewBag.ProvinciaID = new SelectList(provincias.OrderBy(c => c.Nombre), "ProvinciaID", "Nombre");
        ViewBag.ProvinciaBuscarID = new SelectList(provinciasBuscar.OrderBy(c => c.Nombre), "ProvinciaID", "Nombre");

        // Cargar localidades de la base de datos
        var localidades = _context.Localidades.ToList();
        var localidadesBuscar = localidades.ToList();

        // Agregar opciones de selección predeterminadas
        localidades.Add(new Localidad { LocalidadID = 0, Nombre = "[SELECCIONE...]" });
        localidadesBuscar.Add(new Localidad { LocalidadID = 0, Nombre = "[TODAS LAS LOCALIDADES]" });

        // Asignar las listas de selección al ViewBag con las claves correctas
        ViewBag.LocalidadID = new SelectList(localidades.OrderBy(c => c.Nombre), "LocalidadID", "Nombre");
        ViewBag.LocalidadBuscarID = new SelectList(localidadesBuscar.OrderBy(c => c.Nombre), "LocalidadID", "Nombre");

        // Retornar la vista "Persona" con los datos cargados en ViewBag
        return View("Persona");
      
    }

    public ActionResult TraerDatosPersonal( int  Id, string? nombre, string? apellido, int edad, int telefono,int correo)

    {  
       
        List <VistaTraerDatosPersonal> TraerDatosPersonal = new List<VistaTraerDatosPersonal>();
        var personas = _context.Personas.ToList();

        if (Id != null)
        {
            personas = personas.Where(a => a.PersonaID == Id).ToList();  
        }

        foreach (var persona in personas)
    {
        var datosPersona = new VistaTraerDatosPersonal
        {
            PersonaID = persona.PersonaID,
              LocalidadID = persona.LocalidadID,
            Nombre = persona.Nombre,
            Apellido = persona.Apellido,
            Edad = persona.Edad,
            Telefono = persona.Telefono,
            Email = persona.Email
        };
        
        TraerDatosPersonal.Add(datosPersona);
    }

    return Json(TraerDatosPersonal);
       
    }

[HttpPost]
public ActionResult Guardar(int localidadId, int? personaID, string nombre, string apellido, int edad, int telefono, int documento, string? correo)
{
    string resultado = "Error al guardar el formulario";

    if (personaID == 0 || !personaID.HasValue)
    {
        var existeFormulario = _context.Personas.Any(p => p.PersonaID == personaID);
        if (!existeFormulario)
        {
            var Nuevousuario = new Persona
            {
                LocalidadID = localidadId,
                Nombre = nombre,
                Apellido = apellido,
                Edad = edad,
                Telefono = telefono,
                Documento = documento,
                Email = correo
            };
            _context.Personas.Add(Nuevousuario);
            _context.SaveChanges();
            resultado = "Usuario guardado correctamente";
        }
        else
        {
            resultado = "El formulario ya existe";
        }
    }
    else
    {
        var personaEditar = _context.Personas.Where(e => e.PersonaID == personaID).SingleOrDefault();
        if (personaEditar != null)
        {
            var existePersona = _context.Personas.Where(p => p.Nombre == nombre && p.PersonaID != personaID).Count();
            if ( existePersona == 0)
            {
                personaEditar.LocalidadID = localidadId;
                personaEditar. Nombre = nombre;
                personaEditar .Apellido = apellido;
                personaEditar.Edad = edad;
                personaEditar.Telefono = telefono;
                personaEditar.Documento = documento;
                personaEditar.Email = correo;
                _context.SaveChanges();


            }
            else{
                resultado ="ya existe";
            }
        }
        
    }

     return Json(resultado);
}

}
