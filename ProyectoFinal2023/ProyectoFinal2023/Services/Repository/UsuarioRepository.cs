using ProyectoFinal2023.Models;
using ProyectoFinal2023.Services.Interface;

namespace ProyectoFinal2023.Services.Repository
{
    public class UsuarioRepository : IUsuario
    {
        public IEnumerable<TbUsuario> getAllUsuarios()
        {
            BiblioBD bd = new BiblioBD();

            return bd.TbUsuarios;
        }

        public TbUsuario getValidarRegistro(TbUsuario usuario)
        {
            BiblioBD bd = new BiblioBD();

            var obj = (from tusuario in bd.TbUsuarios
                       where tusuario.CorreoUsuario == usuario.CorreoUsuario ||
                              tusuario.TlfUsuario == usuario.TlfUsuario
                       select tusuario).FirstOrDefault();
            return obj;
        }

        public TbUsuario getValidarUsuario(TbUsuario usuario)
        {
            BiblioBD bd = new BiblioBD();

            var obj = (from tusuario in bd.TbUsuarios
                       where tusuario.CorreoUsuario == usuario.CorreoUsuario &&
                              tusuario.PassUsuario == usuario.PassUsuario
                       select tusuario).FirstOrDefault();
            return obj;
        }
        
    }
}
