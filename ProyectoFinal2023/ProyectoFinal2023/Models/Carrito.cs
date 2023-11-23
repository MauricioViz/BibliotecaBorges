namespace ProyectoFinal2023.Models
{
    public class Carrito
    {
        public string idDelLibro { get; set; } = null!;
        public string nomLibro { get; set; } = null!;

        public string nomGenero { get; set; } = null!;

        public int cantidadSolicitada { get; set; } 
    }
}
