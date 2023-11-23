using BiblioAPI.Models;

namespace BiblioAPI.Services.Interface
{
    public interface IAutor
    {
        TbAutor getAutorById(String id);
        List<String> getIdAutorByIdLibro(String id);

        IEnumerable<TbAutor> getAllAutores();
        void Add(TbAutor autor);
        void Update(TbAutor autor);
        void Delete(string id);

        void RemoveAutorDeLibro(TbAutorLibro conexion);
    }
}
