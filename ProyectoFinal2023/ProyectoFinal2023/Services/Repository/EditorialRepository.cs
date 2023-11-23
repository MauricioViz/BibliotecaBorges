using ProyectoFinal2023.Models;
using ProyectoFinal2023.Services.Interface;

namespace ProyectoFinal2023.Services.Repository
{
    public class EditorialRepository : IEditorial
    {
        public void Add(TbEditorial editorial)
        {
            BiblioBD bd = new BiblioBD();
            try
            {
                bd.TbEditorials.Add(editorial);
                bd.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Delete(string id)
        {
            BiblioBD bd = new BiblioBD();

            var obj = (from laeditorial in bd.TbEditorials
                       where laeditorial.IdEditorial == id
                       select laeditorial).Single();

            bd.TbEditorials.Remove(obj);
            bd.SaveChanges();
        }
        public void Update(TbEditorial editorial)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TbEditorial> getAllEditoriales()
        {
            BiblioBD bd = new BiblioBD();

            return bd.TbEditorials;
        }

        public TbEditorial getEditorialById(string id)
        {
            BiblioBD bd = new BiblioBD();

            return (from data in bd.TbEditorials
                    where data.IdEditorial == id
                    select data).Single();
        }

        public List<String> getIdEditorialByIdLibro(string id)
        {
            BiblioBD bd = new BiblioBD();

            return (from data in bd.TbEditorialLibros
                    where data.IdLibro == id
                    select data.IdEditorial).ToList();
        }

        public void RemoveEditorialDeLibro(TbEditorialLibro conexion)
        {
            BiblioBD bd = new BiblioBD();

            var obj = (from data in bd.TbEditorialLibros
                       where data.IdLibro == conexion.IdLibro && data.IdEditorial == conexion.IdEditorial
                       select data).Single();
            bd.TbEditorialLibros.Remove(obj);
            bd.SaveChanges();
        }
    }
}
