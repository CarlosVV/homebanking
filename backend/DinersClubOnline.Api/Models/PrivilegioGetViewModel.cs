using System.Collections.Generic;

namespace DinersClubOnline.Api.Models
{
    public class PrivilegiosGetViewModel
    {
        public int TotalPrivilegios { get; set; }
        public List<PrivilegioViewModel> Resultados { get; set; }
        public string Url { get; set; }
    }
}