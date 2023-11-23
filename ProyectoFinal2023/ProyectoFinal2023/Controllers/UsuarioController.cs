using Microsoft.AspNetCore.Mvc;
using ProyectoFinal2023.Models;
using ProyectoFinal2023.Services.Interface;
using Newtonsoft.Json;
using System.Numerics;



namespace ProyectoFinal2023.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuario _usuario;
        private readonly ICliente _cliente;
        private readonly ILibro _libro;

        public UsuarioController(IUsuario usuario, ICliente _cliente, ILibro _libro)
        {
            _usuario = usuario;
            this._cliente = _cliente;
            this._libro = _libro;
        }
        public IActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }

        public IActionResult Register()
        {
            HttpContext.Session.Clear();
            return View();
        }

        public IActionResult Validar(TbUsuario usuario)
        {
            Console.WriteLine(usuario.CorreoUsuario); Console.WriteLine(usuario.PassUsuario);

            var objUsuario = _usuario.getValidarUsuario(usuario);
            if(objUsuario != null){
                Console.WriteLine(objUsuario.CorreoUsuario); Console.WriteLine(objUsuario.PassUsuario);
                if (objUsuario.TipoUsuario.Equals("Cliente")){
                    HttpContext.Session.SetString("sesionActivaCliente", JsonConvert.SerializeObject(objUsuario));
                    
                    return RedirectToAction("IndexLogCliente","Usuario");
                }
                else{
                    
                    HttpContext.Session.SetString("sesionActivaEmpleado", JsonConvert.SerializeObject(objUsuario));
                    return RedirectToAction("IndexLogEmpleado", "Usuario"); //Esta linea está como prueba, aún falta el Index para Empleado
                }            
            }
            else{
                return RedirectToAction("Login", "Usuario");
            }
            
        }
        public IActionResult ValidarRegistro(TbUsuario usuario)
        {
            if((usuario.CorreoUsuario == null)||(usuario.NomUsuario == null)||(usuario.TlfUsuario == null)||(usuario.PassUsuario == null))
            {
                return RedirectToAction("Register", "Usuario");
            }
            Console.WriteLine(usuario.NomUsuario);
            Console.WriteLine("Verificando");
            
                Console.WriteLine("Pasando a registrar");
                var objUsuario = _usuario.getValidarRegistro(usuario);
                if (objUsuario == null)
                {
                    var losusuarios = _usuario.getAllUsuarios();
                    int max = 0;
                
                    foreach (var x in losusuarios)
                    {
                    int num = Int32.Parse(x.IdUsuario);
                        if(num > max)
                        {
                            max = num;
                        }
                    }
                    max = max + 1;
                    Console.WriteLine("La id a colocar es " + max);
                    String nuevaId = max.ToString();
                usuario.IdUsuario = nuevaId;
                usuario.TipoUsuario = "Cliente";
                _cliente.Add(usuario);
                    return RedirectToAction("ConfirmarRegistro", "Usuario");

                }
                else
                {
                    return RedirectToAction("Register", "Usuario");
                }
                       
        }
        public IActionResult ConfirmarRegistro()
        {
            return View();
        }

        public IActionResult Index()
        {
            var objSesionC = HttpContext.Session.GetString("sesionActivaCliente");
            var objSesionE = HttpContext.Session.GetString("sesionActivaEmpleado");
            if (objSesionC != null)
            {
                // var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                //                (HttpContext.Session.GetString("sesionActivaCliente"));
                return RedirectToAction("IndexLogCliente", "Usuario");
            }
            else if (objSesionE != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaEmpleado"));
                return RedirectToAction("IndexLogEmpleado", "Usuario");
            }
            else
            {
                //Proceso para listar los 3 libros más recientes añadidos
                var listaLibros = _libro.GetAllLibros();

                List<TbLibro> librosRecientes = new List<TbLibro> ();
                TbLibro libro1 = null;
                TbLibro libro2 = null;
                TbLibro libro3 = null;
                TbLibro libro4 = null;
                int max = 0;
                foreach(var libro in  listaLibros)
                {
                    var numero = Int32.Parse(libro.IdLibro);
                    if(numero > max)
                    {
                        max = numero;
                        libro1 = libro;
                    }
                }
                librosRecientes.Add(libro1);
                int limite = max;
                max = 0;
                foreach(var libro in listaLibros)
                {
                    var numero = Int32.Parse(libro.IdLibro);
                    if((numero > max) && (numero < limite)){
                        max = numero;
                        libro2 = libro;
                    }
                }
                librosRecientes.Add(libro2);
                int limite2 = max;               
                max = 0;
                foreach(var libro in listaLibros)
                {
                    var numero = Int32.Parse(libro.IdLibro);
                    if((numero > max) && (numero < limite2))
                    {
                        max = numero;
                        libro3 = libro;
                    }
                }
                librosRecientes.Add(libro3);
                int limite3 = max;
                max = 0;
                foreach(var libro in listaLibros)
                {
                    var numero = Int32.Parse(libro.IdLibro);
                    if ((numero > max) && (numero < limite3))
                    {
                        max = numero;
                        libro4 = libro;
                    }
                }
                librosRecientes.Add(libro4);
                ViewBag.libros = librosRecientes;
                
                return View();
            }
            
        }

        
        public IActionResult IndexLogCliente()
        {
            var objSesion = HttpContext.Session.GetString("sesionActivaCliente");
            if (objSesion != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario> (HttpContext.Session.GetString("sesionActivaCliente"));
                
                ViewBag.ElUsuario = ObjLog;

                //Proceso para listar los 3 libros más recientes añadidos
                var listaLibros = _libro.GetAllLibros();

                List<TbLibro> librosRecientes = new List<TbLibro>();
                TbLibro libro1 = null;
                TbLibro libro2 = null;
                TbLibro libro3 = null;
                TbLibro libro4 = null;
                int max = 0;
                foreach (var libro in listaLibros)
                {
                    var numero = Int32.Parse(libro.IdLibro);
                    if (numero > max)
                    {
                        max = numero;
                        libro1 = libro;
                    }
                }
                librosRecientes.Add(libro1);
                int limite = max;
                max = 0;
                foreach (var libro in listaLibros)
                {
                    var numero = Int32.Parse(libro.IdLibro);
                    if ((numero > max) && (numero < limite))
                    {
                        max = numero;
                        libro2 = libro;
                    }
                }
                librosRecientes.Add(libro2);
                int limite2 = max;
                max = 0;
                foreach (var libro in listaLibros)
                {
                    var numero = Int32.Parse(libro.IdLibro);
                    if ((numero > max) && (numero < limite2))
                    {
                        max = numero;
                        libro3 = libro;
                    }
                }
                librosRecientes.Add(libro3);
                int limite3 = max;
                max = 0;
                foreach (var libro in listaLibros)
                {
                    var numero = Int32.Parse(libro.IdLibro);
                    if ((numero > max) && (numero < limite3))
                    {
                        max = numero;
                        libro4 = libro;
                    }
                }
                librosRecientes.Add(libro4);
                ViewBag.libros = librosRecientes;

                return View();
            }
            else
            {

                return RedirectToAction("Index", "Usuario");
            }
            
        }
        public IActionResult IndexLogEmpleado()
        {
            var objSesion = HttpContext.Session.GetString("sesionActivaEmpleado");
            if (objSesion != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaEmpleado"));
                ViewBag.ElUsuario = ObjLog;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Usuario");
        }

        public IActionResult CuentaUsuario()
        {
            var objSesion = HttpContext.Session.GetString("sesionActivaCliente");
            if (objSesion != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario> (HttpContext.Session.GetString("sesionActivaCliente"));
                Console.WriteLine(ObjLog.IdUsuario);
                ViewBag.ElUsuario = ObjLog;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }
        public IActionResult VerificarContraseña(TbUsuario usuario)
        {
            var objSesion = HttpContext.Session.GetString("sesionActivaCliente");
            if (objSesion != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaCliente"));

                if(ObjLog.PassUsuario == usuario.PassUsuario)
                {
                    ViewBag.ElUsuario = ObjLog;
                    return RedirectToAction("EditarUsuario", "Usuario", new { cod = ObjLog.IdUsuario });
                }
                else
                {
                    ViewBag.ElUsuario = ObjLog;
                    return RedirectToAction("CuentaUsuario", "Usuario");
                }               
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }
        public IActionResult EditarUsuario(String cod)
        {
            var objSesion = HttpContext.Session.GetString("sesionActivaCliente");
            if (objSesion != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaCliente"));
                ViewBag.ElUsuario = ObjLog;
                return View(_cliente.GetCliente(cod));
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }
        public IActionResult EditarFinalizado(TbUsuario usuarioNuevo)
        {
            
            var objSesion = HttpContext.Session.GetString("sesionActivaCliente");
            if (objSesion != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaCliente"));
                HttpContext.Session.Clear();
                Console.WriteLine(ObjLog.PassUsuario);
                Console.WriteLine("Cuenta editada.");
                _cliente.Update(ObjLog, usuarioNuevo);
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }
        public IActionResult eliminarCuenta(TbUsuario usuario)
        {
            var objSesion = HttpContext.Session.GetString("sesionActivaCliente");
            if (objSesion != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaCliente"));

                if (ObjLog.PassUsuario == usuario.PassUsuario)
                {
                    _cliente.Delete(ObjLog.IdUsuario); //Se pasa a eliminar todas las conexiones del usuario.
                    HttpContext.Session.Clear();
                    return RedirectToAction("Login", "Usuario");
                }
                else
                {
                    return RedirectToAction("CuentaUsuario", "Usuario");
                }
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }

        }
        // [Route("Usuario/Delete/{idUsuario}")]
        public IActionResult eliminarCuentaFromPanel(String idUsuario)
        {

            _cliente.Delete(idUsuario);
            
            return RedirectToAction("ControlClientes", "Cliente");


        }
        

        


    }
}
