using BiblioAPI.Models;
using BiblioAPI.Services.Interface;

namespace BiblioAPI.Services.Repository
{
    public class LibroRepository : ILibro
    {
        public void Add(TbLibro libro)
        {
            BiblioBD bd = new BiblioBD();
            try
            {               

                bd.TbLibros.Add(libro);
                bd.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Delete(string idLibro)
        {
            BiblioBD bd = new BiblioBD();
            // Eliminando las conexiones con los autores.
            var objAutor = (from data in bd.TbAutorLibros
                            where data.IdLibro == idLibro
                            select data).ToList();
            foreach(var item in objAutor)
            {
                bd.TbAutorLibros.Remove(item);
            }
            //Eliminando las conexiones con las editoriales.
            var objEditorial = (from data in bd.TbEditorialLibros
                                where data.IdLibro == idLibro
                                select data).ToList();
            foreach(var item in objEditorial)
            {
                bd.TbEditorialLibros.Remove(item);
            }
            //Eliminando las conexiones con los prestamos.
            var objPrestamos = (from data in bd.TbPrestamoLibros
                                where data.IdLibro == idLibro
                                select data).ToList();
            foreach(var item in objPrestamos)
            {
                bd.TbPrestamoLibros.Remove(item);
            }
            //Finalmente, una vez todas las conexiones se hayan eliminado, eliminar el objeto Libro.
            var objLibro = (from data in bd.TbLibros
                            where data.IdLibro == idLibro
                            select data).Single();
            bd.TbLibros.Remove(objLibro);
            bd.SaveChanges();
        }

        public IEnumerable<TbLibro> GetAllLibros()
        {
            BiblioBD bd = new BiblioBD();

            return bd.TbLibros;
        }
        public IEnumerable<TbAutorLibro> GetAllAutorLibro()
        {
            BiblioBD bd = new BiblioBD();
            return bd.TbAutorLibros;
        }
        public IEnumerable<TbEditorialLibro> GetAllEditorialLibro()
        {
            BiblioBD bd = new BiblioBD();
            return bd.TbEditorialLibros;
        }

        public TbLibro GetProducto(string id)
        {
            BiblioBD bd = new BiblioBD();

            return (from ellibro in bd.TbLibros 
                    where ellibro.IdLibro == id 
                    select ellibro).Single();
        }    
        public void Update(TbLibro libro)
        {
            throw new NotImplementedException();
        }
        public TbAutorLibro getRelationAutorLibro(TbAutorLibro conexion)
        {
            BiblioBD bd = new BiblioBD();

            var obj = (from data in bd.TbAutorLibros
                        where data.IdAutor == conexion.IdAutor &&
                            data.IdLibro ==  conexion.IdLibro
                    select data).FirstOrDefault();
            return obj;
        }
        public TbEditorialLibro getRelationEditorialLibro(TbEditorialLibro conexion)
        {
            BiblioBD bd = new BiblioBD();

            var obj = (from data in bd.TbEditorialLibros
                       where data.IdEditorial == conexion.IdEditorial &&
                            data.IdLibro == conexion.IdLibro
                            select data).FirstOrDefault();
            return obj;
        }

        public void AddAutorLibro(TbAutorLibro conexion)
        {
            BiblioBD bd = new BiblioBD();

            try
            {
                
                bd.TbAutorLibros.Add(conexion);
                bd.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AddEditorialLibro(TbEditorialLibro conexion)
        {
            BiblioBD bd = new BiblioBD();
            try
            {
                bd.TbEditorialLibros.Add(conexion);
                bd.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteAutorLibro(string id)
        {
            throw new NotImplementedException();
        }

        public void DeleteEditorialLibro(string id)
        {
            throw new NotImplementedException();
        }

        public List<string> getIdLibrosByIdAutor(string idAutor)
        {
            BiblioBD bd = new BiblioBD();

            return (from data in bd.TbAutorLibros
                    where data.IdAutor == idAutor
                    select data.IdLibro).ToList();
        }

        public List<string> getIdLibrosByIdEditorial(string idEditorial)
        {
            BiblioBD bd = new BiblioBD();

            return (from data in bd.TbEditorialLibros
                    where data.IdEditorial == idEditorial
                    select data.IdLibro).ToList();
        }
    }
}
