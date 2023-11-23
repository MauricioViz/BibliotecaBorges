using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoFinal2023.Models;
using ProyectoFinal2023.Services.Interface;

namespace ProyectoFinal2023.Controllers
{
    public class PrestamoController : Controller
    {
        private readonly IPrestamo _prestamo;
        
        public PrestamoController(IPrestamo _prestamo)
        {
            this._prestamo = _prestamo;
            
        }

        public IActionResult IndexReserva()
        {
            var objSesion = HttpContext.Session.GetString("sesionActivaCliente");
            if (objSesion != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaCliente"));
                ViewBag.ElUsuario = ObjLog;
                
                var idlistaActualUsuario = _prestamo.searchIDUserLista(ObjLog.IdUsuario.ToString()); // Único prestamo que su estado esté en "Listando"
                if(idlistaActualUsuario == null) //En caso que no haya un prestamo en listando, se creará uno.
                {
                    Console.WriteLine("No tengo un prestamo listando, creando nuevo");
                    TbPrestamo nuevoPrestamo = new TbPrestamo();
                    var losPrestamos = _prestamo.GetAllPrestamos();
                    int max = 0;
                    foreach (var x in losPrestamos)
                    {
                        int num = Int32.Parse(x.IdPrestamo);
                        if (num > max)
                        {
                            max = num;
                        }
                    }
                    max = max + 1;
                    String nuevaId = max.ToString();
                    //Estableciendo datos del nuevo prestamo, su estado será "Listando" para identificarlo al añadir más libros.
                    nuevoPrestamo.IdPrestamo = nuevaId;
                    nuevoPrestamo.IdUsuario = ObjLog.IdUsuario;
                    nuevoPrestamo.FecPrestamo = "Sin definir";
                    nuevoPrestamo.FecDevolucion = "Sin definir";
                    nuevoPrestamo.EstPrestamo = "Listando";
                    nuevoPrestamo.MultaPrestamo = 0;
                    //Añadiendo prestamo a TbPrestamo
                    _prestamo.addPrestamo(nuevoPrestamo);
                    
                    return RedirectToAction("IndexReserva", "Prestamo");

                }
                else //De lo contrario, si ya existe uno.
                {
                    
                    List<Carrito> carro = new List<Carrito>();
                    var librosDelCarro = _prestamo.getItemsOfIDPrestamo(idlistaActualUsuario);
                    foreach(Carrito x in librosDelCarro)
                    {
                        
                        Carrito item = new Carrito();
                        item.idDelLibro = x.idDelLibro;
                        item.nomLibro = x.nomLibro;
                        item.nomGenero = x.nomGenero;
                        item.cantidadSolicitada = x.cantidadSolicitada;
                        carro.Add(item);

                    }
                    var prestamosDelUsuario = _prestamo.getPrestamosByUserID(ObjLog.IdUsuario.ToString()); //Todos los prestamos que tengan la ID del usuario actual
                    ViewBag.idPrestamoListando = idlistaActualUsuario.ToString();
                    ViewBag.allPrestamosUser = prestamosDelUsuario;
                    ViewBag.itemsCarrito = carro;
                    return View();
                }
                
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
        }

        public IActionResult Enlistar(TbPrestamoLibro obj)
        {
            var objSesion = HttpContext.Session.GetString("sesionActivaCliente");
            if (objSesion != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaCliente"));

                //Buscando si este usuario ya tiene un prestamo listando
                var codigoLista = _prestamo.searchIDUserLista(ObjLog.IdUsuario.ToString());
                if (codigoLista != null) //En caso de que este Usuario ya tenga un objeto en TbPrestamo con su ID de Usuario y en estado "Listando" +
                {                        //se usará la id Prestamo de este objeto y se usará para establecer la conexión.
                    //Primero se verifica que este libro no se encuentre ya en la lista del usuario
                    var objConexion = _prestamo.searchBookOnList(obj.IdLibro, codigoLista);
                    if (objConexion == null) //En caso que no se haya encontrado la conexión entre el libro a insertar y la lista prestamo en uso, se arma el objeto para insertarlo
                    {
                        var allConections = _prestamo.GetAllPrestamoLibro();
                        int max2 = 0;
                        foreach (var x in allConections)
                        {
                            int num = Int32.Parse(x.IdRel);
                            if (num > max2)
                            {
                                max2 = num;
                            }
                        }
                        max2 = max2 + 1;
                        String nuevaIdRel = max2.ToString();
                        obj.IdRel = nuevaIdRel;
                        obj.IdPrestamo = codigoLista;
                        _prestamo.addConexionPrestamo(obj);
                        return RedirectToAction("IndexReserva", "Prestamo");
                    }
                    else //En caso que la conexion ya exista, se enviara a la lista que mostrará el mensaje y se finalizara el proceso
                    {
                        return RedirectToAction("screenLibroEncontrado", "Prestamo");
                    }
                }
                else //En caso de que no se cumpla esta especificación, se creara un nuevo objeto Prestamo y se le añadira su respectiva ID prestamo e ID usuario.
                {
                    TbPrestamo nuevoPrestamo = new TbPrestamo();
                    var losPrestamos = _prestamo.GetAllPrestamos();
                    int max = 0;
                    foreach(var x in losPrestamos)
                    {
                        int num = Int32.Parse(x.IdPrestamo);
                        if(num > max) {
                            max = num;
                        }
                    }
                    max = max + 1;
                    String nuevaId = max.ToString();
                    //Estableciendo datos del nuevo prestamo, su estado será "Listando" para identificarlo al añadir más libros.
                    nuevoPrestamo.IdPrestamo = nuevaId;
                    nuevoPrestamo.IdUsuario = ObjLog.IdUsuario;
                    nuevoPrestamo.FecPrestamo = "Sin definir";
                    nuevoPrestamo.FecDevolucion = "Sin definir";
                    nuevoPrestamo.EstPrestamo = "Listando";
                    nuevoPrestamo.MultaPrestamo = 0;
                    //Añadiendo prestamo a TbPrestamo
                    _prestamo.addPrestamo(nuevoPrestamo);
                    //Tras la creación del nuevo objeto Prestamo "Listando", se procede a la conexión entre el libro dado, la cantidad y el prestamo.
                    //Primero se verifica que este libro no se encuentre ya en la lista del usuario
                    var objConexion = _prestamo.searchBookOnList(obj.IdLibro, nuevaId);
                    if(objConexion == null) //En caso que no se haya encontrado la conexión entre el libro a insertar y la lista prestamo en uso, se arma el objeto para insertarlo
                    {
                        var allConections = _prestamo.GetAllPrestamoLibro();
                        int max2 = 0;
                        foreach(var x in allConections)
                        {
                            int num = Int32.Parse(x.IdRel);
                            if(num > max2)
                            {
                                max2 = num;
                            }
                        }
                        max2 = max2 + 1;
                        String nuevaIdRel = max2.ToString();
                        obj.IdRel = nuevaIdRel;
                        obj.IdPrestamo = nuevaId;
                        _prestamo.addConexionPrestamo(obj);
                        return RedirectToAction("IndexReserva", "Prestamo");
                    }
                    else //En caso que si se haya encontrado la conexión, se notificara en la siguiente vista y finalizara el proceso
                    {
                        return RedirectToAction("screenLibroEncontrado", "Prestamo");
                    }

                }

            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
        }
        public IActionResult UsuarioLibrosDelPrestamo(String idPrestamo)
        {
            var objSesion = HttpContext.Session.GetString("sesionActivaCliente");
            if (objSesion != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>(HttpContext.Session.GetString("sesionActivaCliente"));
                List<Carrito> carro = new List<Carrito>();
                var librosDelPedido = _prestamo.getItemsOfIDPrestamo(idPrestamo);
                foreach(Carrito x in librosDelPedido)
                {
                    Carrito item = new Carrito();
                    item.idDelLibro = x.idDelLibro;
                    item.nomLibro = x.nomLibro;
                    item.nomGenero = x.nomGenero;
                    item.cantidadSolicitada = x.cantidadSolicitada;
                    carro.Add(item);
                }
                ViewBag.itemsCarrito = carro;
                ViewBag.ElUsuario = ObjLog;
                return View();
            }
            else
            {

                return RedirectToAction("Index", "Usuario");
            }
        }
        public IActionResult removerItemDeLista(TbPrestamoLibro conexion)
        {
            _prestamo.deleteConexionPrestamo(conexion);
            return RedirectToAction("IndexReserva", "Prestamo");
        }
        public IActionResult removeAllItemsLista(String idPrestamo)
        {
            _prestamo.deleteAllItemsPrestamo(idPrestamo);
            return RedirectToAction("IndexReserva", "Prestamo");
        }
        public IActionResult screenLibroEncontrado()
        {
            return View();
        }
        //Cambios solicitados por el usuario
        public IActionResult enviarSolicitud(String idPrestamo)
        {
            _prestamo.sendRequest(idPrestamo);
            return RedirectToAction("IndexReserva", "Prestamo");
        }
        public IActionResult cancelarSolicitud(String idPrestamo)
        {
            _prestamo.userCancelRequest(idPrestamo);
            return RedirectToAction("IndexReserva", "Prestamo");
        }

        //Cambios solicitados por el empleado
        public IActionResult listLibros(String idPrestamo)
        {
            var objSesion = HttpContext.Session.GetString("sesionActivaEmpleado");
            if (objSesion != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaEmpleado"));

                List<Carrito> carro = new List<Carrito>();
                var librosDelPrestamo = _prestamo.getItemsOfIDPrestamo(idPrestamo);
                foreach(Carrito x in librosDelPrestamo)
                {
                    Carrito item = new Carrito();
                    item.idDelLibro = x.idDelLibro;
                    item.nomLibro = x.nomLibro;
                    item.nomGenero = x.nomGenero;
                    item.cantidadSolicitada = x.cantidadSolicitada;
                    carro.Add(item);
                }
                ViewBag.itemsCarrito = carro;
                ViewBag.ElUsuario = ObjLog;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }
        public IActionResult aceptarSolicitud(String idPrestamo)
        {
            _prestamo.approveRequest(idPrestamo);
            return RedirectToAction("ControlSolicitudes", "Prestamo");
        }
        public IActionResult rechazarSolicitud(String idPrestamo)
        {
            _prestamo.declineRequest(idPrestamo);
            return RedirectToAction("ControlSolicitudes", "Prestamo");
        }
        public IActionResult cancelarEntrega(String idPrestamo)
        {
            _prestamo.cancelDelivery(idPrestamo);
            return RedirectToAction("ControlEntregas", "Prestamo");
        }
        public IActionResult confirmarEntrega(String idPrestamo)
        {
            _prestamo.confirmDelivery(idPrestamo);
            return RedirectToAction("ControlEntregas", "Prestamo");
        }
        public IActionResult cancelarPrestamo(String idPrestamo)
        {
            _prestamo.cancelarPrestamo(idPrestamo);
            return RedirectToAction("ControlActivos", "Prestamo");
        }
        public IActionResult cerrarPrestamo(String idPrestamo)
        {
            _prestamo.cerrarPrestamo(idPrestamo);
            return RedirectToAction("ControlActivos", "Prestamo");
        }
        public IActionResult ControlSolicitudes()
        {
            var objSesion = HttpContext.Session.GetString("sesionActivaEmpleado");
            if (objSesion != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaEmpleado"));

                
                ViewBag.AllPrestamos = _prestamo.GetAllPrestamos();
                ViewBag.ElUsuario = ObjLog;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }
        public IActionResult ControlEntregas()
        {
            var objSesion = HttpContext.Session.GetString("sesionActivaEmpleado");
            if (objSesion != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaEmpleado"));


                ViewBag.AllPrestamos = _prestamo.GetAllPrestamos();
                ViewBag.ElUsuario = ObjLog;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }
        public IActionResult ControlActivos()
        {
            var objSesion = HttpContext.Session.GetString("sesionActivaEmpleado");
            if (objSesion != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaEmpleado"));


                ViewBag.AllPrestamos = _prestamo.GetAllPrestamos();
                ViewBag.ElUsuario = ObjLog;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }
        public IActionResult ControlPrestamos()
        {
            var objSesion = HttpContext.Session.GetString("sesionActivaEmpleado");
            if (objSesion != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaEmpleado"));


                ViewBag.AllPrestamos = _prestamo.GetAllPrestamos();
                ViewBag.ElUsuario = ObjLog;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }
        public IActionResult tablaPrestamosUsuario(String idUsuario)
        {
            var objSesion = HttpContext.Session.GetString("sesionActivaEmpleado");
            if (objSesion != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaEmpleado"));


                ViewBag.AllPrestamos = _prestamo.getPrestamosByUserID(idUsuario);
                
                ViewBag.ElUsuario = ObjLog;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }
    }
}
