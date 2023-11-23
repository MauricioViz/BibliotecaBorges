using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoFinal2023.Models;
using ProyectoFinal2023.Services.Interface;

namespace ProyectoFinal2023.Controllers
{
    public class AutorController : Controller
    {
        private readonly IAutor _autor;
        private readonly ILibro _libro;
        public AutorController(IAutor _autor, ILibro _libro)
        {
            this._autor = _autor;
            this._libro = _libro;
        }
        public IActionResult AutorTabla()
        {
            var objSesionE = HttpContext.Session.GetString("sesionActivaEmpleado");
            if (objSesionE != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaEmpleado"));
                ViewBag.ElUsuario = ObjLog;
                ViewBag.Autores = _autor.getAllAutores();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }
        public IActionResult AutorAnadir(TbAutor autor)
        {
            if ((autor.NomAutor == null) || (autor.Nacionalidad == null) || (autor.FechaNacimiento == null))
            {
                return RedirectToAction("AutorTabla", "Autor");
            }
            else
            {
                var losautores = _autor.getAllAutores();
                int max = 0;
                foreach(var x in losautores)
                {
                    int num = Int32.Parse(x.IdAutor);
                    if(num > max)
                    {
                        max = num;
                    }
                }
                max = max + 1;
                Console.WriteLine(max);
                String nuevaId = max.ToString();
                autor.IdAutor = nuevaId;
                _autor.Add(autor);
                return RedirectToAction("AutorTabla", "Autor");
            }
            
        }
        public IActionResult AutorTablaLibro(String idAutor)
        {
            var objSesionE = HttpContext.Session.GetString("sesionActivaEmpleado");
            if (objSesionE != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaEmpleado"));
                var idLibros = _libro.getIdLibrosByIdAutor(idAutor);
                List<TbLibro> objLibros = new List<TbLibro>();
                foreach(String x in idLibros)
                {
                    var ellibro = _libro.GetProducto(x);
                    objLibros.Add(ellibro);
                }
                var autor = _autor.getAutorById(idAutor);
                ViewBag.ElAutor = autor;
                ViewBag.LosLibros = objLibros;                
                ViewBag.ElUsuario = ObjLog;
                
                return View();
            
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
    

        }
        public IActionResult removeAutorDeLibro(TbAutorLibro conexion)
        {
            
            _autor.RemoveAutorDeLibro(conexion);
            return RedirectToAction("LibroTablaAutores","Libro", new { idLibro = conexion.IdLibro });
        }
        public IActionResult removeLibroDeAutor(TbAutorLibro conexion)
        {
            _autor.RemoveAutorDeLibro(conexion);
            return RedirectToAction("AutorTablaLibro", "Autor", new { idAutor = conexion.IdAutor });
        }
        public IActionResult eliminarAutor(String idAutor)
        {
            List<String> idLibros = new List<String>();
            idLibros = _libro.getIdLibrosByIdAutor(idAutor);
            if (idLibros.Any())
            {
                TbAutorLibro conexion = new TbAutorLibro();
                conexion.IdAutor = idAutor;
                foreach (String x in idLibros){                  
                    conexion.IdLibro = x;                   
                    _autor.RemoveAutorDeLibro(conexion);
                }
                _autor.Delete(idAutor);
            }
            else
            {
                _autor.Delete(idAutor);
            }
            return RedirectToAction("AutorTabla", "Autor");
        }

    }
    
}
