using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionReservation.Data;
using GestionReservation.Models;
using Microsoft.AspNetCore.Http;

namespace GestionReservation.Controllers
{
    public class AvionsController : Controller
    {
        private readonly GestionReservationContext _context;

        public AvionsController(GestionReservationContext context)
        {
            _context = context;
        }

        // GET: Avions
       /* public async Task<IActionResult> Index()
        {
            return View(await _context.Avion.ToListAsync());
        }*/
        public async Task<IActionResult> Index(string searchString, string currentFilter, int? pageNumber)
        {
            if (HttpContext.Session.GetInt32("IdAdminSession") != null)
            {
                ViewData["CurrentFilter"] = searchString;
            var avions = from a in _context.Avion
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
                avions = avions.Where(s => s.Modele.Contains(searchString));
            }

            int pageSize = 3;
            return View(await PaginatedList<Avion>.CreateAsync(avions.AsNoTracking(), pageNumber ?? 1, pageSize));

            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Administrateur/Index.cshtml");
            }
        }


        // GET: Avions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetInt32("IdAdminSession") != null)
            {
                if (id == null)
            {
                return NotFound();
            }

            var avion = await _context.Avion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avion == null)
            {
                return NotFound();
            }

            return View(avion);
            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Administrateur/Index.cshtml");
            }
        }

        // GET: Avions/Create
        public IActionResult Create()
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

        // POST: Avions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Modele,NbrSiegeAffaire,NbrSiegeEconomique")] Avion avion)
        {
            if (HttpContext.Session.GetInt32("IdAdminSession") != null)
            {
                if (ModelState.IsValid)
            {
                _context.Add(avion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(avion);
            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Administrateur/Index.cshtml");
            }
        }

        // GET: Avions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetInt32("IdAdminSession") != null)
            {
                if (id == null)
            {
                return NotFound();
            }

            var avion = await _context.Avion.FindAsync(id);
            if (avion == null)
            {
                return NotFound();
            }
            return View(avion);
            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Administrateur/Index.cshtml");
            }
        }

        // POST: Avions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Modele,NbrSiegeAffaire,NbrSiegeEconomique")] Avion avion)
        {
            if (HttpContext.Session.GetInt32("IdAdminSession") != null)
            {
                if (id != avion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvionExists(avion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(avion);
            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Administrateur/Index.cshtml");
            }
        }

        // GET: Avions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetInt32("IdAdminSession") != null)
            {
                if (id == null)
            {
                return NotFound();
            }

            var avion = await _context.Avion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avion == null)
            {
                return NotFound();
            }

            return View(avion);
            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Administrateur/Index.cshtml");
            }
        }

        // POST: Avions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetInt32("IdAdminSession") != null)
            {
                var avion = await _context.Avion.FindAsync(id);
            _context.Avion.Remove(avion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Administrateur/Index.cshtml");
            }
        }

        private bool AvionExists(int id)
        {
            return _context.Avion.Any(e => e.Id == id);
        }
    }
}
