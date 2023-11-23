using Microsoft.AspNetCore.Mvc;
using BiblioAPI.Models;
using BiblioAPI.Services.Interface;

namespace BiblioAPI.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class ApiController : Controller
    {
        private readonly ILibro _libro;
        private readonly IAutor _autor;
        private readonly IEditorial _editorial;
        private readonly IPrestamo _prestamo;
        public ApiController(ILibro _libro, IAutor _autor, IEditorial _editorial, IPrestamo _prestamo)
        {
            this._libro = _libro;
            this._autor = _autor;
            this._editorial = _editorial;
            this._prestamo = _prestamo;
        }
        //Metodos libros----------------------------------------------------------------------------

        [HttpGet("listarLibros")]
        public IActionResult GetLibros()
        {
            return Ok(_libro.GetAllLibros());
        }

        [HttpGet("buscarLibro/{id}")]
        public IActionResult getLibro(String id)
        {
            var obj = _libro.GetProducto(id);
            if (obj == null)
            {
                return NotFound("El producto(" + id.ToString() + ") no existe");
            }
            else
            {
                return Ok(obj);
            }
        }

        [HttpPost("agregarLibro")]
        public IActionResult addLibro(TbLibro libro)
        {
            var loslibros = _libro.GetAllLibros();
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
            _libro.Add(libro);
            return CreatedAtAction(nameof(addLibro), libro);
        }

        [HttpDelete("eliminarLibro/{id}")]
        public IActionResult removeLibro(String id)
        {
            _libro.Delete(id);
            return NoContent();
        }

        

        //Metodos autores-----------------------------------------------------------

        [HttpGet("listarAutores")]
        public IActionResult GetAutores()
        {
            return Ok(_autor.getAllAutores());
        }

        [HttpGet("buscarAutor/{id}")]
        public IActionResult getAutor(String id)
        {
            var obj = _autor.getAutorById(id);
            if (obj == null)
            {
                return NotFound("El autor(" + id.ToString() + ") no existe");
            }
            else
            {
                return Ok(obj);
            }
        }

        [HttpPost("agregarAutor")]
        public IActionResult addAutor(TbAutor autor)
        {
            var losautores = _autor.getAllAutores();
            int max = 0;
            foreach (var x in losautores)
            {
                int num = Int32.Parse(x.IdAutor);
                if (num > max)
                {
                    max = num;
                }
            }
            max = max + 1;
            String nuevaId = max.ToString();
            autor.IdAutor = nuevaId;
            _autor.Add(autor);
            return CreatedAtAction(nameof(addAutor), autor);
        }

        [HttpDelete("eliminarAutor/{id}")]
        public IActionResult deleteAutor(String id)
        {
            List<String> idLibros = new List<String>(); 
            idLibros = _libro.getIdLibrosByIdAutor(id); //Se hace una lista que busca las conexiones de dicho autor con libros
            if (idLibros.Any()) //Condicional, si la lista idLibros tiene almenos un valor, entra
            {
                TbAutorLibro conexion = new TbAutorLibro();
                conexion.IdAutor = id; //Se establece de permanente que idAutor del obj conexion será la id enviada
                foreach(String x in idLibros)
                {
                    conexion.IdLibro = x; //Se cambia constantemente el valor del idLibro de la conexión para eliminarla directamente
                    _autor.RemoveAutorDeLibro(conexion);
                }
                _autor.Delete(id); //Por ultimo, se borra el autor
            }
            else //En caso que no haya ninguna conexion, se borra el autor directamente
            {
                _autor.Delete(id);
            }
            
            return NoContent();
        }


        //Metodos editoriales-----------------------------------------------------
        [HttpGet("listarEditoriales")]
        public IActionResult GetEditoriales()
        {
            return Ok(_editorial.getAllEditoriales());
        }

        [HttpGet("buscarEditorial/{id}")]
        public IActionResult getEditorial(String id)
        {
            var obj = _editorial.getEditorialById(id);
            if(obj == null)
            {
                return NotFound("El editorial(" + id.ToString() + ") no existe");
            }
            else
            {
                return Ok(obj);
            }
        }

        [HttpPost("agregarEditorial")]
        public IActionResult addEditorial(TbEditorial editorial)
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
            String nuevaId = max.ToString();
            editorial.IdEditorial = nuevaId;
            _editorial.Add(editorial);
            return CreatedAtAction(nameof(addEditorial), editorial); 
        }

        [HttpDelete("eliminarEditorial/{id}")]
        public IActionResult deleteEditorial(String id)
        {
            List<String> idLibros = new List<String>();
            idLibros = _libro.getIdLibrosByIdEditorial(id);
            if (idLibros.Any())
            {
                TbEditorialLibro conexion = new TbEditorialLibro();
                conexion.IdEditorial = id;
                foreach(String x in idLibros)
                {
                    conexion.IdLibro = x;
                    _editorial.RemoveEditorialDeLibro(conexion);
                }
                _editorial.Delete(id);
            }
            else
            {
                _editorial.Delete(id);
            }
            
            return NoContent();
        }
        //Metodos prestamos----------------------------------------------------------------------------
        [HttpGet("listarPrestamos")]
        public IActionResult getPrestamos()
        {
            return Ok(_prestamo.GetAllPrestamos());
        }
    }
}
