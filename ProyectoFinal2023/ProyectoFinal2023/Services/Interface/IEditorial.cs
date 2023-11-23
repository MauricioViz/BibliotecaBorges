using ProyectoFinal2023.Models;

namespace ProyectoFinal2023.Services.Interface
{
    public interface IEditorial
    {
        TbEditorial getEditorialById(String id);
        List<String> getIdEditorialByIdLibro(String id);
        IEnumerable<TbEditorial> getAllEditoriales();
        void Add(TbEditorial editorial);
        void Update(TbEditorial editorial);
        void Delete(string id);
        void RemoveEditorialDeLibro(TbEditorialLibro conexion);
    }
}
