using ProyectoFinal2023.Models;

namespace ProyectoFinal2023.Services.Interface
{
    public interface IUsuario
    {
        IEnumerable<TbUsuario> getAllUsuarios();
        TbUsuario getValidarUsuario(TbUsuario usuario);
        TbUsuario getValidarRegistro(TbUsuario usuario);
    }

}
