using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GestionReservation.Models
{
    public class Vol
    {
        public int Id { get; set; }

        [Display(Name = "Date et heure du vol")]
        public DateTime DateHeureVol { get; set; }

        [Display(Name = "Durée du vol")]
        public int DureeVol { get; set; }

        [Display(Name = "Lieu de départ")]
        public string LieuDepart { get; set; }

        [Display(Name = "Lieu d' arrivée")]
        public string LieuArrivee { get; set; }

        [Display(Name = "Avion Id")]
        public int IdAvion { get; set; }
    }
}
