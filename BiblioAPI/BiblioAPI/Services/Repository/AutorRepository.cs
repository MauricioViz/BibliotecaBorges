using BiblioAPI.Models;
using BiblioAPI.Services.Interface;

namespace BiblioAPI.Services.Repository
{
    public class AutorRepository : IAutor
    {
        

        public void Add(TbAutor autor)
        {
            BiblioBD bd = new BiblioBD();
            try
            {
                bd.TbAutors.Add(autor);
                bd.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Delete(string id)
        {
            BiblioBD bd = new BiblioBD();

            var obj = (from elautor in bd.TbAutors
                       where elautor.IdAutor == id
                       select elautor).Single();
            bd.TbAutors.Remove(obj);
            bd.SaveChanges();
        }
        public void Update(TbAutor autor)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TbAutor> getAllAutores()
        {
            BiblioBD bd = new BiblioBD();

            return bd.TbAutors;
        }

        public TbAutor getAutorById(string id)
        {
            BiblioBD bd = new BiblioBD();

            return (from elautor in bd.TbAutors
                    where elautor.IdAutor == id
                    select elautor).Single();
        }

        public List<String> getIdAutorByIdLibro(string id)
        {
            BiblioBD bd = new BiblioBD();

            return (from data in bd.TbAutorLibros
                    where data.IdLibro == id
                    select data.IdAutor).ToList();
        }

        public void RemoveAutorDeLibro(TbAutorLibro conexion)
        {
            BiblioBD bd = new BiblioBD();

            var obj = (from data in bd.TbAutorLibros
                       where data.IdLibro == conexion.IdLibro && data.IdAutor == conexion.IdAutor
                       select data).Single();
            bd.TbAutorLibros.Remove(obj);
            bd.SaveChanges();
        }
    }
}
