using GestionReservation.Data;
using GestionReservation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BC = BCrypt.Net.BCrypt;


namespace GestionReservation.Controllers
{
    public class ClientController : Controller
    {
        private readonly GestionReservationContext _context;

        public ClientController(GestionReservationContext context)
        {
            _context = context;
        }
       
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Client model)
        {
            if (ModelState.IsValid)
            {
                var User = from m in _context.Client select m;
                User = User.Where(s => s.Identifiant.Contains(model.Identifiant));
                if (User.Count() != 0)
                {
                   // if (User.First().Mdp == model.Mdp) 
                   if(BC.Verify(model.Mdp, User.First().Mdp))
                    {
                      
                        HttpContext.Session.SetInt32("IdClientSession", User.First().Id);
                        return RedirectToAction("Success");

                    }
                }
            }
            ViewData["erreur"] = "erreur";
            return View("~/Views/Client/Index.cshtml");
            //return RedirectToAction("Index");
        }
        public IActionResult Success()
        {
            if (HttpContext.Session.GetInt32("IdClientSession") != null)
            {
                return View();
            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Client/Index.cshtml");
            }
        }
        public IActionResult Signup()
        {
            return View();
        }
        public IActionResult Logout()
        //session destroy all
        {
            //session rehetra
            HttpContext.Session.Remove("IdClientSession");

            return View("~/Views/Home/Index.cshtml");
        }
        public IActionResult Fail()
        {
            return View();
        }
        public async Task<IActionResult> ListeVol(string searchString, string currentFilter, int? pageNumber)
        {
            if (HttpContext.Session.GetInt32("IdClientSession") != null)
            {
                ViewData["CurrentFilter"] = searchString;
                var vols = from v in _context.Vol
                                   select v;

                if (searchString != null)
                {
                    pageNumber = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                ViewData["CurrentFilter"] = searchString;
                if (!String.IsNullOrEmpty(searchString))
                {
                    vols = vols.Where(s => s.LieuDepart.Contains(searchString)
                                           || s.LieuArrivee.Contains(searchString));
                }

                int pageSize = 3;
                return View(await PaginatedList<Vol>.CreateAsync(vols.AsNoTracking(), pageNumber ?? 1, pageSize));

                //  ViewData["listeVol"] = await _context.Vol.ToListAsync();
                //  return View();
            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Client/Index.cshtml");
            }
        }
        public IActionResult FormRechercheVol()
        {
            if (HttpContext.Session.GetInt32("IdClientSession") != null)
            {


                return View();
            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Client/Index.cshtml");
            }
        }
        public IActionResult ResultatRechercheVol(Vol model)
        {
            if (HttpContext.Session.GetInt32("IdClientSession") != null)
            {
                var entity = _context.Vol
                      .Where(
                    x => x.DateHeureVol.Year == model.DateHeureVol.Year
                    && x.DateHeureVol.Month == model.DateHeureVol.Month
                    && x.DateHeureVol.Day == model.DateHeureVol.Day
                    && x.DateHeureVol.Hour == model.DateHeureVol.Hour
                    && x.DateHeureVol.Minute == model.DateHeureVol.Minute
                    && x.LieuDepart == model.LieuDepart
                    && x.LieuArrivee == model.LieuArrivee
                ).ToList();

            var entity1 = _context.Vol
                      .Where(
                    x => x.DateHeureVol.Year == model.DateHeureVol.Year
                    && x.DateHeureVol.Month == model.DateHeureVol.Month
                    && x.DateHeureVol.Day == model.DateHeureVol.Day
                    && x.LieuDepart == model.LieuDepart
                    && x.LieuArrivee == model.LieuArrivee
                ).ToList();


            //mampiasa ViewModel
            Intermediaire intermediaire = new Intermediaire();
            intermediaire.resultatVolDateTime = entity;
            intermediaire.resultatVolDate = entity1;
            intermediaire.volrechercher = model;
            //ViewData["VolDateTime"] = entity;
           // ViewData["listeVol"] = await _context.Vol.ToListAsync();
            return View(intermediaire);
            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Client/Index.cshtml");
            }
        }
        public async Task<IActionResult> DetailsVol(int id)
        {
            if (HttpContext.Session.GetInt32("IdClientSession") != null)
            {


                var detailvol = await _context.DetailsVols
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detailvol == null)
            {
                return NotFound();
            }

            return View(detailvol);

            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Client/Index.cshtml");
            }
        }
        public async Task<IActionResult> FormReservation(int id)
        {
            if (HttpContext.Session.GetInt32("IdClientSession") != null)
            {
                ViewData["idVol"] = id;
            return View();
            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Client/Index.cshtml");
            }
        }
        public async Task<IActionResult> VerificationReservation(int idVol,int resanbrplaceaffaire,int resanbrplaceeco)
        {
            if (HttpContext.Session.GetInt32("IdClientSession") != null)
            {
                DetailsVols detailvol = await _context.DetailsVols
               .FirstOrDefaultAsync(m => m.Id == idVol);

            if(detailvol.nbrPlaceAffaireReserver == null)
            {
                detailvol.nbrPlaceAffaireReserver = 0;
            }
            if (detailvol.nbrPlaceEconomieReserver == null)
            {
                detailvol.nbrPlaceEconomieReserver = 0;
            }
            int? placeAffairedispo = detailvol.NbrSiegeAffaire - detailvol.nbrPlaceAffaireReserver;
            int? placeEconomiedispo = detailvol.NbrSiegeEconomique - detailvol.nbrPlaceEconomieReserver;

            if(placeAffairedispo < resanbrplaceaffaire)
            {
                ViewData["idVol"] = idVol;       
                ViewData["manqueplaceaffaire"] = "erreur";
                return View("~/Views/Client/VerificationReservation.cshtml");
            }
            if(placeEconomiedispo < resanbrplaceeco)
            {
                ViewData["idVol"] = idVol;       
                ViewData["manqueplaceeco"] = "erreur";
                return View("~/Views/Client/VerificationReservation.cshtml");
            }
            if(placeAffairedispo >= resanbrplaceaffaire && placeEconomiedispo >= resanbrplaceeco)
            {
                // ViewData["idVol"] = idVol;

                ViewData["idVol"] = idVol;
                ViewData["resanbrplaceaffaire"] = resanbrplaceaffaire;
                ViewData["resanbrplaceeco"] = resanbrplaceeco;
                ViewData["placesok"] = "1";
                return View("~/Views/Client/VerificationReservation.cshtml");
            }

            return View();
            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Client/Index.cshtml");
            }
        }
        public async Task<IActionResult> MesReservations()
        {
            if (HttpContext.Session.GetInt32("IdClientSession") != null)
            {
             

                var mesreservations = _context.DetailsReservations
                         .Where(e => e.IdClient == HttpContext.Session.GetInt32("IdClientSession"))
                         .ToList();
                ViewData["mesreservations"] = mesreservations;
                return View(); 
            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Client/Index.cshtml");
            }
        }
        public async Task<IActionResult> PropositionVol(int idVol)
        {
            if (HttpContext.Session.GetInt32("IdClientSession") != null)
            {


                Vol vol = _context.Vol
                     .Where(v => v.Id == idVol).First();
                                       

                List <Vol> entity = new List<Vol>();
                if (vol != null)
                { 
                    entity = _context.Vol
                     .Where(x => x.DateHeureVol > vol.DateHeureVol
                   && x.LieuDepart == vol.LieuDepart
                  && x.LieuArrivee == vol.LieuArrivee
                 ).ToList();
                }

                
                ViewData["listeVol"] = entity;
                return View();
            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Client/Index.cshtml");
            }
        }
        public async Task<IActionResult> SaveReservation(int idVol, int resanbrplaceaffaire, int resanbrplaceeco)
        {
            //
            if (HttpContext.Session.GetInt32("IdClientSession") != null)
            {
                int? idClient = HttpContext.Session.GetInt32("IdClientSession");
            Reservation reservation = new Reservation();
            reservation.IdClient = (int)idClient;
            reservation.IdVol = idVol;
            reservation.nbrPlaceAffaireResa = resanbrplaceaffaire;
            reservation.nbrPlaceEcoResa = resanbrplaceeco;
            _context.Add(reservation);
            await _context.SaveChangesAsync();
            var detailplacevol = await _context.DetailsPlaceVol
               .FirstOrDefaultAsync(m => m.IdVol == idVol);
            if(detailplacevol == null)
            {
                // insert
                DetailsPlaceVol insertplacevol = new DetailsPlaceVol();
                insertplacevol.IdVol = idVol;
                insertplacevol.nbrPlaceAffaireReserver = resanbrplaceaffaire;
                insertplacevol.nbrPlaceEconomieReserver = resanbrplaceeco;
                _context.Add(insertplacevol);
                await _context.SaveChangesAsync();


            }
            else
            {
                //update
                detailplacevol.nbrPlaceAffaireReserver = detailplacevol.nbrPlaceAffaireReserver + resanbrplaceaffaire;
                detailplacevol.nbrPlaceEconomieReserver = detailplacevol.nbrPlaceEconomieReserver + resanbrplaceeco;
                _context.SaveChanges();


            }

            return RedirectToAction("MesReservations");
            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Client/Index.cshtml");
            }

        }

            [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ValidationSignup(Client client, String mdp1)
        {
            if (ModelState.IsValid)
            {
                if (client.Mdp.Equals(mdp1))
                {
                    client.Mdp = BC.HashPassword(client.Mdp);
                    _context.Add(client);
                    await _context.SaveChangesAsync();
                    ViewData["signinsuccess"] = "1";
                    return View("~/Views/Client/Index.cshtml");
                    //  return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewData["mdpdiff"] = "erreur";

                    return View("~/Views/Client/Signup.cshtml");
                }
            }
            return View("~/Views/Client/Signup.cshtml");
        }

    }

    
}
