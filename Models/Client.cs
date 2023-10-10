using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace GestionReservation.Models
{
    public class Client
    {
        public int Id { get; set; }

       // [MaxLength(50)]
        public string Identifiant { get; set; }

        public string Mdp { get; set; }

      
    }
}
