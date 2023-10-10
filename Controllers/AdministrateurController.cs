using GestionReservation.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionReservation.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using BC = BCrypt.Net.BCrypt;

namespace GestionReservation.Controllers
{
    public class AdministrateurController : Controller
    {
        private readonly GestionReservationContext _context;

        public AdministrateurController(GestionReservationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
      /*  public async Task<IActionResult> InsertAdmin()
        {
            Administrateur a = new Administrateur();
            a.Identifiant = "adminTana";
            a.Mdp = BC.HashPassword("adminmdp");
            _context.Add(a);
            await _context.SaveChangesAsync();
            return View();
        }*/

        [HttpPost]
        public async Task<IActionResult> Login(Administrateur model)
        {
            if (ModelState.IsValid)
            {
                var User = from m in _context.Administrateur select m;
                User = User.Where(s => s.Identifiant.Contains(model.Identifiant));
                if (User.Count() != 0)
                {
                    // if (User.First().Mdp == model.Mdp)
                    if (BC.Verify(model.Mdp, User.First().Mdp))
                    {
                        
                        HttpContext.Session.SetInt32("IdAdminSession", User.First().Id);
                        return RedirectToAction("Success");
                        
                    }
                }
            }
            ViewData["erreur"] = "erreur";
            return View("~/Views/Administrateur/Index.cshtml");
        }
        public IActionResult Success()
        {
            if (HttpContext.Session.GetInt32("IdAdminSession") != null)
            {

                return View();

            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Administrateur/Index.cshtml");
            }
            
        }
        public async Task<IActionResult> ListeReservation(string searchString, string currentFilter, int? pageNumber)
        {
            if (HttpContext.Session.GetInt32("IdAdminSession") != null)
            {
                ViewData["CurrentFilter"] = searchString;
            var reservations = from a in _context.DetailsReservations
                         select a;
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
                reservations = reservations.Where(s => s.Identifiant.Contains(searchString));
            }
            int pageSize = 3;
            return View(await PaginatedList<DetailsReservations>.CreateAsync(reservations.AsNoTracking(), pageNumber ?? 1, pageSize));

            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Administrateur/Index.cshtml");
            }

        }
        public IActionResult Logout()
            //session destroy
        {
            HttpContext.Session.Remove("IdAdminSession");
            return View("~/Views/Home/Index.cshtml");
        }
        public IActionResult Fail()
        {
            return View();
        }
    }
}
