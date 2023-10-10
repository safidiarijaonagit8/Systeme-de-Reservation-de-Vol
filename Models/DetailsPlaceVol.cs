using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionReservation.Models
{
    public class DetailsPlaceVol
    {
        public int Id { get; set; }

        public int IdVol { get; set; }

        public int nbrPlaceAffaireReserver { get; set; }

        public int nbrPlaceEconomieReserver { get; set; }


    }
}
