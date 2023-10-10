using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionReservation.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public int IdClient { get; set; }

        public int IdVol { get; set; }

        public int nbrPlaceAffaireResa { get; set; }


        public int nbrPlaceEcoResa { get; set; }
    }
}
