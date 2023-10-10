using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionReservation.Models
{
    public class DetailsReservations
    {
        [Display(Name = "Id Reservation")]
        public int Id { get; set; }

     
        public int IdClient { get; set; }

       
        public String Identifiant { get; set; }

        [Display(Name = "Date et heure du vol")]
        public DateTime DateHeureVol { get; set; }

        [Display(Name = "Durée du vol")]
        public int DureeVol { get; set; }

        [Display(Name = "Lieu de départ")]
        public string LieuDepart { get; set; }

        [Display(Name = "Lieu d' arrivée")]
        public string LieuArrivee { get; set; }
        public string Modele { get; set; }

        [Display(Name = "Nbr Siege Classe Affaire réservé")]
        public int NbrPlaceAffaireResa { get; set; }

        [Display(Name = "Nbr Siege Classe Economique réservé")]
        public int NbrPlaceEcoResa { get; set; }


   


    }
}
