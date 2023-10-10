using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GestionReservation.Models
{
    public class DetailsVols   //Vue
    {

        [Display(Name = "Id Vol")]
        public int Id { get; set; }

        [Display(Name = "Date et heure du vol")]
        public DateTime DateHeureVol { get; set; }

        [Display(Name = "Durée du vol")]
        public int DureeVol { get; set; }

        [Display(Name = "Lieu de départ")]
        public string LieuDepart { get; set; }

        [Display(Name = "Lieu d' arrivée")]
        public string LieuArrivee { get; set; }

        [Display(Name = "Modèle")]
        public string Modele { get; set; }

        [Display(Name = "Nombre Siège Classe Affaire")]
        public int NbrSiegeAffaire { get; set; }

        [Display(Name = "Nombre Siège Classe Economique")]
        public int NbrSiegeEconomique { get; set; }


        [Display(Name = "Nombre Place Affaire Réservée")]
        public Nullable<int> nbrPlaceAffaireReserver { get; set; }

        [Display(Name = "Nombre Place Economique Réservée")]   
        public Nullable<int> nbrPlaceEconomieReserver { get; set; }




    }
}
