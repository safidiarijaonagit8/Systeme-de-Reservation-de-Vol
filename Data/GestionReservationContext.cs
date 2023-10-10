using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GestionReservation.Models;

namespace GestionReservation.Data
{
    public class GestionReservationContext : DbContext
    {
        public GestionReservationContext (DbContextOptions<GestionReservationContext> options)
            : base(options)
        {
        }

        public DbSet<GestionReservation.Models.Avion> Avion { get; set; }

        public DbSet<GestionReservation.Models.Vol> Vol { get; set; }

        public DbSet<GestionReservation.Models.Administrateur> Administrateur { get; set; }

        public DbSet<GestionReservation.Models.Client> Client { get; set; }

        public DbSet<GestionReservation.Models.DetailsVols> DetailsVols { get; set; }

        public DbSet<GestionReservation.Models.DetailsPlaceVol> DetailsPlaceVol { get; set; }

        public DbSet<GestionReservation.Models.Reservation> Reservation { get; set; }

        public DbSet<GestionReservation.Models.DetailsReservations> DetailsReservations { get; set; }





    }
}
