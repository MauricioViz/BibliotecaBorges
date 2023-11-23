using BiblioAPI.Models;

namespace BiblioAPI.Services.Interface
{
    public interface IPrestamo
    {
        IEnumerable<TbPrestamo> GetAllPrestamos();
        IEnumerable<TbPrestamoLibro> GetAllPrestamoLibro();
        String searchIDUserLista(String idUsuario);
        void addPrestamo(TbPrestamo obj);
        TbPrestamoLibro searchBookOnList(String idLibro, String idPrestamo);
        void addConexionPrestamo(TbPrestamoLibro obj);
        void deleteConexionPrestamo(TbPrestamoLibro conexion);
        void deleteAllItemsPrestamo(String id);

        IEnumerable<Carrito> getItemsOfIDPrestamo(String id);
        IEnumerable<TbPrestamo> getPrestamosByUserID(String usuarioID);
        //Para clientes
        void sendRequest(String idPrestamo);
        void userCancelRequest(String idPrestamo);
        //Para empleados
        void approveRequest(String idPrestamo);
        void declineRequest(String idPrestamo);
        void cancelDelivery(String idPrestamo);
        void confirmDelivery(String idPrestamo);
        void cancelarPrestamo(String idPrestamo);
        void cerrarPrestamo(String idPrestamo);
    }
}
