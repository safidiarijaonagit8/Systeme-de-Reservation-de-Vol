using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionReservation.Models
{
    public class Avion
    {
        [Display(Name = "Id Avion")]
        public int Id { get; set; }

        [Display(Name = "Modèle")]
        public string Modele { get; set; }

        [Display(Name = "Nombre siège Affaire")]
        public int NbrSiegeAffaire { get; set; }

        [Display(Name = "Nombre siège Economique")]
        public int NbrSiegeEconomique { get; set; }
    }
}
