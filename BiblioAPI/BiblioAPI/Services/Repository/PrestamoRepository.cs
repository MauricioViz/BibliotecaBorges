using BiblioAPI.Models;
using BiblioAPI.Services.Interface;

namespace BiblioAPI.Services.Repository

{
    public class PrestamoRepository : IPrestamo
    {
        public void addConexionPrestamo(TbPrestamoLibro obj)
        {
            BiblioBD bd = new BiblioBD();
            try
            {
                bd.TbPrestamoLibros.Add(obj);
                bd.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void addPrestamo(TbPrestamo obj)
        {
            BiblioBD bd = new BiblioBD();

            try
            {
                bd.TbPrestamos.Add(obj);
                bd.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public IEnumerable<TbPrestamoLibro> GetAllPrestamoLibro()
        {
            BiblioBD bd = new BiblioBD();

            return bd.TbPrestamoLibros;
        }

        public IEnumerable<TbPrestamo> GetAllPrestamos()
        {
            BiblioBD bd = new BiblioBD();

            return bd.TbPrestamos;
        }

        public TbPrestamoLibro searchBookOnList(string idLibro, string idPrestamo)
        {
            BiblioBD bd = new BiblioBD();

            var obj = (from conexion in bd.TbPrestamoLibros
                    where conexion.IdLibro == idLibro && conexion.IdPrestamo == idPrestamo
                    select conexion).FirstOrDefault();
            return obj;
        }

        public string searchIDUserLista(String idUsuario)
        {
            BiblioBD bd = new BiblioBD();

            var obj = (from lalista in bd.TbPrestamos
                    where lalista.IdUsuario == idUsuario && lalista.EstPrestamo == "Listando"
                    select lalista.IdPrestamo).FirstOrDefault();
            return obj;
            
        }
        public IEnumerable<Carrito> getItemsOfIDPrestamo(String idPrestamo)
        {
            BiblioBD bd = new BiblioBD();

            var list = (from itemConexion in bd.TbPrestamoLibros
                        where itemConexion.IdPrestamo == idPrestamo
                        join itemLibro in bd.TbLibros on itemConexion.IdLibro equals itemLibro.IdLibro
                        select new Carrito
                        {
                            idDelLibro = itemLibro.IdLibro,
                            nomLibro = itemLibro.TituloLibro,
                            nomGenero = itemLibro.GeneroLibro,
                            cantidadSolicitada = itemConexion.Cantidad

                        }
                        );

            return list;
        }

        public IEnumerable<TbPrestamo> getPrestamosByUserID(string usuarioID)
        {
            BiblioBD bd = new BiblioBD();

            var lista = (from prestamo in bd.TbPrestamos
                         where prestamo.IdUsuario == usuarioID
                         select prestamo).ToList();
            return lista;
        }

        public void deleteConexionPrestamo(TbPrestamoLibro conexion)
        {
            BiblioBD bd = new BiblioBD();

            var obj = (from data in bd.TbPrestamoLibros
                       where data.IdLibro == conexion.IdLibro && data.IdPrestamo == conexion.IdPrestamo
                       select data).Single();
            bd.TbPrestamoLibros.Remove( obj );
            bd.SaveChanges();

        }

        public void deleteAllItemsPrestamo(string id)
        {
            BiblioBD bd = new BiblioBD();

            var obj = (from data in bd.TbPrestamoLibros
                       where data.IdPrestamo == id
                       select data).ToList();
            foreach(var item in obj )
            {
                bd.TbPrestamoLibros.Remove(item);
            }
            bd.SaveChanges();
        }
        //Usuario manda solicitud de prestamo
        public void sendRequest(string idPrestamo)
        {
            BiblioBD bd = new BiblioBD();
            DateTime fechaactual = DateTime.Now;
            String fecha = fechaactual.ToString("dd-MM-yyyy");

            var obj = (from data in bd.TbPrestamoLibros
                       where data.IdPrestamo == idPrestamo
                       select data).FirstOrDefault();
            if(obj != null)
            {
                var objPrestamo = (from data in bd.TbPrestamos
                                   where data.IdPrestamo == idPrestamo
                                   select data).Single();
                objPrestamo.EstPrestamo = "En espera";
                objPrestamo.FecPrestamo = fecha;
                bd.SaveChanges();
            }
            else
            {

            }
        }
        //Usuario cancela su solicitud de prestamo y se elimina de la base de datos todos los libros adjuntos
        public void userCancelRequest(string idPrestamo)
        {
            BiblioBD bd = new BiblioBD();

            var items = (from data in bd.TbPrestamoLibros
                         where data.IdPrestamo == idPrestamo
                         select data).ToList();
            foreach(var item in items)
            {
                bd.TbPrestamoLibros.Remove(item);
            }
            var prestamo = (from data in bd.TbPrestamos
                            where data.IdPrestamo == idPrestamo
                            select data).Single();
            bd.TbPrestamos.Remove(prestamo);
            bd.SaveChanges();
        }
        //Empleado acepta la solicitud de prestamo del usuario
        public void approveRequest(string idPrestamo)
        {
            BiblioBD bd = new BiblioBD();
            DateTime fechaactual = DateTime.Now;
            DateTime fechafutura = fechaactual.AddDays(14);

            String fechaInicial = fechaactual.ToString("dd-MM-yyyy");
            String fechaFinal = fechafutura.ToString("dd-MM-yyyy");

            var objPrestamo = (from data in bd.TbPrestamos
                               where data.IdPrestamo == idPrestamo
                               select data).Single();

            objPrestamo.EstPrestamo = "Aceptada";
            objPrestamo.FecPrestamo = fechaInicial;
            objPrestamo.FecDevolucion = fechaFinal;

            bd.SaveChanges();
        }
        //Empleado rechaza la solicitud de prestamo del usuario
        public void declineRequest(string idPrestamo)
        {
            BiblioBD bd = new BiblioBD();
            var objPrestamo = (from data in bd.TbPrestamos
                               where data.IdPrestamo == idPrestamo
                               select data).Single();
            objPrestamo.EstPrestamo = "Rechazada";
            bd.SaveChanges();
        }

        public void cancelDelivery(string idPrestamo)
        {
            BiblioBD bd = new BiblioBD();
            var objPrestamo = (from data in bd.TbPrestamos
                               where data.IdPrestamo == idPrestamo
                               select data).Single();
            objPrestamo.EstPrestamo = "Cancelado";
            objPrestamo.MultaPrestamo = objPrestamo.MultaPrestamo + 10;
            bd.SaveChanges();
        }

        public void confirmDelivery(string idPrestamo)
        {
            BiblioBD bd = new BiblioBD();
            var objPrestamo = (from data in bd.TbPrestamos
                               where data.IdPrestamo == idPrestamo
                               select data).Single();
            objPrestamo.EstPrestamo = "Prestado";
            bd.SaveChanges();
        }

        public void cancelarPrestamo(string idPrestamo)
        {
            BiblioBD bd = new BiblioBD();
            var objPrestamo = (from data in bd.TbPrestamos
                               where data.IdPrestamo == idPrestamo
                               select data).Single();
            objPrestamo.EstPrestamo = "Cancelado";
            objPrestamo.MultaPrestamo = objPrestamo.MultaPrestamo + 20;
            bd.SaveChanges();
        }

        public void cerrarPrestamo(string idPrestamo)
        {
            BiblioBD bd = new BiblioBD();
            var objPrestamo = (from data in bd.TbPrestamos
                               where data.IdPrestamo == idPrestamo
                               select data).Single();
            objPrestamo.EstPrestamo = "Finalizado";
            bd.SaveChanges();
        }
    }
}
