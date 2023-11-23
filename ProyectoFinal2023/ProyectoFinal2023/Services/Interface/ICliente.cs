using ProyectoFinal2023.Models;

namespace ProyectoFinal2023.Services.Interface
{
    public interface ICliente
    {
        IEnumerable<TbUsuario> GetAllClientes();
        void Add(TbUsuario cliente);
        void Update(TbUsuario clienteDataActual, TbUsuario clienteDataNueva);
        void Delete(String idUsuario);
        TbUsuario GetCliente(String id);
    }
}
