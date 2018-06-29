namespace DinersClubOnline.Model
{
    public class Privilegio
    {
        //Datos
        public string Nombre { get; set; }
        public int NumeroCuotasSinIntereses { get; set; }

        //Ubicación
        public Coordenada Coordenada { get; set; }
    }
}