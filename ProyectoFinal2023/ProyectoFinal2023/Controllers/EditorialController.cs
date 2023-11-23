using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoFinal2023.Models;
using ProyectoFinal2023.Services.Interface;

namespace ProyectoFinal2023.Controllers
{
    public class EditorialController : Controller
    {
        private readonly IEditorial _editorial;
        private readonly ILibro _libro;
        public EditorialController(IEditorial _editorial, ILibro _libro)
        {
            this._editorial = _editorial;
            this._libro = _libro;
        }
        public IActionResult EditorialTabla()
        {
            var objSesionE = HttpContext.Session.GetString("sesionActivaEmpleado");
            if (objSesionE != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaEmpleado"));
                ViewBag.ElUsuario = ObjLog;
                ViewBag.Editoriales = _editorial.getAllEditoriales();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }
        public IActionResult EditorialAnadir(TbEditorial editorial)
        {
            if ((editorial.NomEditorial == null) || (editorial.DirEditorial == null) || (editorial.TlfEditorial == null))
            {
                return RedirectToAction("EditorialTabla", "Editorial");
            }
            else
            {
                var laseditoriales = _editorial.getAllEditoriales();
                int max = 0;
                foreach (var x in laseditoriales)
                {
                    int num = Int32.Parse(x.IdEditorial);
                    if (num > max)
                    {
                        max = num;
                    }
                }
                max = max + 1;
                Console.WriteLine(max);
                String nuevaId = max.ToString();
                editorial.IdEditorial = nuevaId;
                _editorial.Add(editorial);
                return RedirectToAction("EditorialTabla", "Editorial");
            }
        }
        public IActionResult EditorialTablaLibro(String idEditorial)
        {
            var objSesionE = HttpContext.Session.GetString("sesionActivaEmpleado");
            if (objSesionE != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaEmpleado"));
                var idLibros = _libro.getIdLibrosByIdEditorial(idEditorial);
                List<TbLibro> objLibros = new List<TbLibro>();
                foreach (String x in idLibros)
                {
                    var ellibro = _libro.GetProducto(x);
                    objLibros.Add(ellibro);
                }
                var editorial = _editorial.getEditorialById(idEditorial);
                ViewBag.LaEditorial = editorial;
                ViewBag.LosLibros = objLibros;
                ViewBag.ElUsuario = ObjLog;

                return View();

            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }
        public IActionResult removeEditorialDeLibro(TbEditorialLibro conexion)
        {
            _editorial.RemoveEditorialDeLibro (conexion);
            return RedirectToAction("LibroTablaEditoriales", "Libro", new { idLibro = conexion.IdLibro });
        }
        public IActionResult removeLibroDeEditorial(TbEditorialLibro conexion)
        {
            _editorial.RemoveEditorialDeLibro(conexion);
            return RedirectToAction("EditorialTablaLibro", "Editorial", new { idEditorial = conexion.IdEditorial });
        }
        public IActionResult eliminarEditorial(String idEditorial)
        {
            List<String> idLibros = new List<String>();
            idLibros = _libro.getIdLibrosByIdEditorial(idEditorial);
            if (idLibros.Any())
            {
                TbEditorialLibro conexion = new TbEditorialLibro();
                conexion.IdEditorial = idEditorial;
                foreach(String x in idLibros)
                {
                    conexion.IdLibro = x;
                    _editorial.RemoveEditorialDeLibro(conexion);
                }
                _editorial.Delete(idEditorial);
            }
            else
            {
                _editorial.Delete(idEditorial);
            }
            return RedirectToAction("EditorialTabla", "Editorial");
        }
    }
}
