using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionReservation.Models
{
    public class Intermediaire
    {
        public List<Vol> resultatVolDateTime { get; set; }
        public List<Vol> resultatVolDate { get; set; }

        public Vol volrechercher { get; set; }
        public Intermediaire()
        {
            this.resultatVolDateTime = new List<Vol>();
            this.resultatVolDate = new List<Vol>();
            this.volrechercher = new Vol();
        }
    }
}
