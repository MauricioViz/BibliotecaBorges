using Microsoft.AspNetCore.Mvc;
using ProyectoFinal2023.Services.Interface;

namespace ProyectoFinal2023.Controllers
{
    public class EmpleadoController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }


    }
}
