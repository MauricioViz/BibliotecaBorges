using BiblioAPI.Models;

namespace BiblioAPI.Services.Interface
{
    public interface ILibro
    {
        IEnumerable<TbLibro> GetAllLibros();
        IEnumerable<TbAutorLibro> GetAllAutorLibro();
        IEnumerable<TbEditorialLibro> GetAllEditorialLibro();
        TbLibro GetProducto(String id);
        void Add(TbLibro libro);
        void Update(TbLibro libro);
        void Delete(string idLibro);

        List<String> getIdLibrosByIdAutor(String idAutor);
        List<String> getIdLibrosByIdEditorial(String idEditorial);

        TbAutorLibro getRelationAutorLibro(TbAutorLibro conexion);
        TbEditorialLibro getRelationEditorialLibro(TbEditorialLibro conexion);

        void AddAutorLibro(TbAutorLibro obj);
        void DeleteAutorLibro(String id);

        void AddEditorialLibro(TbEditorialLibro obj);
        void DeleteEditorialLibro(String id);
        
    }
}
