using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoFinal2023.Models;
using ProyectoFinal2023.Services.Interface;

namespace ProyectoFinal2023.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ICliente obj;
        
        public ClienteController(ICliente clienteObj)
        {
            obj = clienteObj;
        }

        public IActionResult ControlClientes()
        {
            var objSesion = HttpContext.Session.GetString("sesionActivaEmpleado");
            if (objSesion != null)
            {
                var ObjLog = JsonConvert.DeserializeObject<TbUsuario>
                                (HttpContext.Session.GetString("sesionActivaEmpleado"));


                ViewBag.AllClientes = obj.GetAllClientes();
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
