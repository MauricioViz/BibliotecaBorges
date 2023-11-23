using ProyectoFinal2023.Models;
using ProyectoFinal2023.Services.Interface;

namespace ProyectoFinal2023.Services.Repository
{
    public class ClienteRepository : ICliente
    {
        

        public void Add(TbUsuario cliente)
        {
            BiblioBD bd = new BiblioBD();

            try
            {
 

                bd.TbUsuarios.Add(cliente);
                bd.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Delete(string idUsuario)
        {
            BiblioBD bd = new BiblioBD();
            //Primero, ubicamos todos los prestamos que tengan el id del usuario en cuestión
            var objPrestamos = (from data in bd.TbPrestamos
                                where data.IdUsuario == idUsuario
                                select data).ToList();
            //Una vez obtenidos todos los prestamos del usuario y habiendolos listado, se recorrerá la lista para extraer cada id prestamo y luego buscar las conexiones de este prestamo.
            foreach (var item in objPrestamos)
            {
                String idPrestamo = item.IdPrestamo;
                var objConexionesDelPrestamo = (from data in bd.TbPrestamoLibros
                                                where data.IdPrestamo == idPrestamo
                                                select data).ToList();
                //Una vez listadas las conexiones que esta id prestamo tiene, se procede a recorrer dicha lista y eliminar cada conexión.
                foreach (var conexion in objConexionesDelPrestamo)
                {
                    bd.TbPrestamoLibros.Remove(conexion);
                }
                //Por ultimo, se elimina el objeto prestamo en cuestión, y se repite hasta terminar la lista de prestamos por eliminar.
                bd.TbPrestamos.Remove(item);
            }
            //Tras todo el proceso, se procede a buscar al usuario por su ID y luego se elimina.
            var objUsuario = (from data in bd.TbUsuarios
                              where data.IdUsuario == idUsuario
                              select data).Single();
            bd.TbUsuarios.Remove(objUsuario);
            bd.SaveChanges(); //Se guarda todo lo realizado.

        }

        public IEnumerable<TbUsuario> GetAllClientes()
        {
            BiblioBD bd = new BiblioBD();
            var users = (from data in bd.TbUsuarios
                         where data.TipoUsuario == "Cliente"
                         select data).ToList();
            return users;
        }

        public TbUsuario GetCliente(string id)
        {
            BiblioBD bd = new BiblioBD();

            var obj = (from elUsuario in bd.TbUsuarios
                       where elUsuario.IdUsuario == id
                       select elUsuario).Single();
            return obj;
        }
       

        public void Update(TbUsuario clienteDataActual, TbUsuario clienteDataNueva)
        {
            BiblioBD bd = new BiblioBD();
            Console.WriteLine(clienteDataNueva.PassUsuario);

            var ClienteAGuardar = (from usuario in bd.TbUsuarios
                                   where usuario.IdUsuario == clienteDataActual.IdUsuario
                                   select usuario).FirstOrDefault();

            if(ClienteAGuardar != null)
            {
                ClienteAGuardar.NomUsuario = clienteDataNueva.NomUsuario;
                ClienteAGuardar.TlfUsuario = clienteDataNueva.TlfUsuario;
                ClienteAGuardar.CorreoUsuario = clienteDataNueva.CorreoUsuario;
                ClienteAGuardar.PassUsuario = clienteDataNueva.PassUsuario;

                bd.SaveChanges();
            }
            
        }
    }
}
