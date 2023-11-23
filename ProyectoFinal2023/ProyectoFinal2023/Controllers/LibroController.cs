using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoFinal2023.Models;
using ProyectoFinal2023.Services.Interface;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal2023.Controllers
{
    public class LibroController : Controller
    {
        private readonly ILibro objLibro;
        private readonly IAutor _autor;
        private readonly IEditorial _editorial;

        public LibroController(ILibro objLibro, IAutor _autor, IEditorial _editorial)
        {
            this.objLibro = objLibro;
            this._autor = _autor;
            this._editorial = _editorial;
        }
        public IActionResult InfoLibro(String idLibro)
        {
            var objSesion = HttpContext.Session.GetString("sesionActivaCliente");
            var objSesionE = HttpContext.Session.GetString("sesionActivaEmpleado");
            if (objSesion != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaCliente"));
                return RedirectToAction("InfoLibroLogCliente", "Libro", new { idLibro = idLibro });
            }
            else if (objSesionE != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaEmpleado"));
                return RedirectToAction("IndexLogEmpleado", "Usuario");
            }
            else
            {
                //Haciendo lista autores en base a la id del libro
                var idAutores = _autor.getIdAutorByIdLibro(idLibro);
                List<TbAutor> objAutores = new List<TbAutor>();
                foreach (String x in idAutores)
                {
                    var elautor = _autor.getAutorById(x);
                    objAutores.Add(elautor);
                }
                //Haciendo lista editoriales en base a la id del libro
                var idEditoriales = _editorial.getIdEditorialByIdLibro(idLibro);
                List<TbEditorial> objEditoriales = new List<TbEditorial>();
                foreach (String x in idEditoriales)
                {
                    var laeditorial = _editorial.getEditorialById(x);
                    objEditoriales.Add(laeditorial);
                }
                List<int> cantidades = new List<int>();
                cantidades.Add(1); cantidades.Add(2); cantidades.Add(3); cantidades.Add(4); cantidades.Add(5);

                ViewBag.Cantidades = cantidades;
                ViewBag.ElLibro = objLibro.GetProducto(idLibro);
                ViewBag.LosAutores = objAutores;
                ViewBag.LasEditoriales = objEditoriales;
                return View();
            }
        }
        public IActionResult InfoLibroLogCliente(String idLibro)
        {
            var objSesion = HttpContext.Session.GetString("sesionActivaCliente");

            if (objSesion != null)
            {
                Console.WriteLine(idLibro);
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaCliente"));
                //Haciendo lista autores en base a la id del libro
                var idAutores = _autor.getIdAutorByIdLibro(idLibro);
                List<TbAutor> objAutores = new List<TbAutor>();
                foreach (String x in idAutores)
                {
                    var elautor = _autor.getAutorById(x);
                    objAutores.Add(elautor);
                }
                //Haciendo lista editoriales en base a la id del libro
                var idEditoriales = _editorial.getIdEditorialByIdLibro(idLibro);
                List<TbEditorial> objEditoriales = new List<TbEditorial>();
                foreach (String x in idEditoriales)
                {
                    var laeditorial = _editorial.getEditorialById(x);
                    objEditoriales.Add(laeditorial);
                }
                List<int> cantidades = new List<int>();
                cantidades.Add(1); cantidades.Add(2); cantidades.Add(3); cantidades.Add(4); cantidades.Add(5);

                ViewBag.Cantidades = cantidades;
                ViewBag.ElLibro = objLibro.GetProducto(idLibro);
                ViewBag.LosAutores = objAutores;
                ViewBag.LasEditoriales = objEditoriales;
                ViewBag.ElUsuario = ObjLog;
                return View();
            }

            else
            {
                return RedirectToAction("InfoLibro", "Libro", new { idLibro = idLibro });
            }

        }

        public IActionResult Catalogo()
        {
            var objSesionC = HttpContext.Session.GetString("sesionActivaCliente");
            var objSesionE = HttpContext.Session.GetString("sesionActivaEmpleado");
            if (objSesionC != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaCliente"));
                return RedirectToAction("CatalogoLogCliente", "Libro");
            }
            else if (objSesionE != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaEmpleado"));
                return RedirectToAction("CatalogoLogEmpleado", "Libro");
            }
            else
            {
                return View(objLibro.GetAllLibros());
            }
        }
        public IActionResult CatalogoLogCliente()
        {
            var objSesion = HttpContext.Session.GetString("sesionActivaCliente");
            if (objSesion != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaCliente"));
                ViewBag.ElUsuario = ObjLog;
                return View(objLibro.GetAllLibros());
            }
            else
            {
                return RedirectToAction("Catalogo", "Libro");
            }
        }
        public IActionResult LibroTabla()
        {
            var objSesionE = HttpContext.Session.GetString("sesionActivaEmpleado");
            if (objSesionE != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaEmpleado"));
                ViewBag.ElUsuario = ObjLog;
                ViewBag.Libros = objLibro.GetAllLibros();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }
        public IActionResult LibroTablaAutores(String idLibro)
        {
            var objSesionE = HttpContext.Session.GetString("sesionActivaEmpleado");
            if (objSesionE != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaEmpleado"));

                var idAutores = _autor.getIdAutorByIdLibro(idLibro);
                List<TbAutor> objAutores = new List<TbAutor>();
                foreach (String x in idAutores)
                {
                    var elautor = _autor.getAutorById(x);
                    objAutores.Add(elautor);
                }
                var libro = objLibro.GetProducto(idLibro);
                ViewBag.ElLibro = libro;
                ViewBag.LosAutores = objAutores;
                ViewBag.ListaAutores = _autor.getAllAutores();

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }
        public IActionResult AgregarAutorLibro(TbAutorLibro obj)
        {

            var autorlibro = objLibro.getRelationAutorLibro(obj);
            if (autorlibro == null)
            {
                Console.WriteLine(obj.IdAutor);
                Console.WriteLine("Entre");
                var conexiones = objLibro.GetAllAutorLibro();
                int max = 0;
                foreach (var x in conexiones)
                {
                    var numero = Int32.Parse(x.IdRel);
                    Console.WriteLine(numero);
                    if (numero > max)
                    {
                        max = numero;
                    }
                }
                max = max + 1;
                String nuevaID = max.ToString();
                obj.IdRel = nuevaID;
                objLibro.AddAutorLibro(obj);
                return RedirectToAction("LibroTablaAutores", "Libro", new { idLibro = obj.IdLibro });
            }
            else
            {
                Console.WriteLine("No entre");
                return RedirectToAction("LibroTablaAutores", "Libro", new { idLibro = obj.IdLibro });
            }

        }

        public IActionResult LibroTablaEditoriales(String idLibro)
        {
            var objSesionE = HttpContext.Session.GetString("sesionActivaEmpleado");
            if (objSesionE != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaEmpleado"));

                var idEditoriales = _editorial.getIdEditorialByIdLibro(idLibro);
                List<TbEditorial> objEditoriales = new List<TbEditorial>();
                foreach (String x in idEditoriales)
                {
                    var laeditorial = _editorial.getEditorialById(x);
                    objEditoriales.Add(laeditorial);
                }
                var libro = objLibro.GetProducto(idLibro);
                ViewBag.ElLibro = libro;
                ViewBag.LasEditoriales = objEditoriales;
                ViewBag.ListaEditoriales = _editorial.getAllEditoriales();


                return View();
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }

        public IActionResult AgregarEditorialLibro(TbEditorialLibro obj)
        {
            var editoriallibro = objLibro.getRelationEditorialLibro(obj);
            if (editoriallibro == null)
            {
                Console.WriteLine(obj.IdEditorial);
                Console.WriteLine("Entre");
                var conexiones = objLibro.GetAllEditorialLibro();
                int max = 0;

                foreach (var x in conexiones)
                {
                    var numero = Int32.Parse(x.IdRel);
                    Console.WriteLine(numero);
                    if (numero > max)
                    {
                        max = numero;
                    }
                }
                max = max + 1;
                String nuevaID = max.ToString();
                obj.IdRel = nuevaID;

                objLibro.AddEditorialLibro(obj);
                return RedirectToAction("LibroTablaEditoriales", "Libro", new { idLibro = obj.IdLibro });
            }
            else
            {
                Console.WriteLine("No entre");
                return RedirectToAction("LibroTablaEditoriales", "Libro", new { idLibro = obj.IdLibro });
            }

        }



        public IActionResult LibroAnadir(TbLibro libro)
        {
            Console.WriteLine(libro.TituloLibro); Console.WriteLine(libro.GeneroLibro); Console.WriteLine(libro.FecPublic);
            if ((libro.TituloLibro == null) || (libro.GeneroLibro == null) || (libro.FecPublic == null))
            {
                return RedirectToAction("LibroTabla", "Libro");
            }
            else
            {
                var loslibros = objLibro.GetAllLibros();
                int max = 0;
                foreach (var x in loslibros)
                {
                    int num = Int32.Parse(x.IdLibro);
                    if (num > max)
                    {
                        max = num;
                    }
                }
                max = max + 1;
                String nuevaId = max.ToString();
                libro.IdLibro = nuevaId;
                libro.EstLibro = "Disponible";
                objLibro.Add(libro);
                return RedirectToAction("LibroTabla", "Libro");

            }
        }



        public IActionResult LibroBorrar(String idLibro)
        {
            objLibro.Delete(idLibro);
            return RedirectToAction("LibroTabla", "Libro");
        }
        public IActionResult LibroEditar()
        {
            return View();
        }
        public IActionResult ControlSolicitudes()
        {
            return View();
        }
        public IActionResult ControlMultas()
        {
            return View();
        }
        public IActionResult ControlReservasActivas()
        {
            return View();
        }
        // ---------------------------------------

       
    }
}
