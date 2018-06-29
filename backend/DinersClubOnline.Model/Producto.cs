namespace DinersClubOnline.Model
{
    /// <summary>Producto es un tipo de tarjeta que posee el cliente titular</summary>
    public class Producto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public bool AcumulaMillas { get; set; }
    }
}