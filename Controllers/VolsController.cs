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
    public class VolsController : Controller
    {
        private readonly GestionReservationContext _context;

        public VolsController(GestionReservationContext context)
        {
            _context = context;
        }

        // GET: Vols
        public async Task<IActionResult> Index(string searchString, string currentFilter, int? pageNumber)
        {
            if (HttpContext.Session.GetInt32("IdAdminSession") != null)
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
                // return View(await vols.AsNoTracking().ToListAsync());
                // return View(await _context.Vol.ToListAsync());
            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Administrateur/Index.cshtml");
            }
        }

        // GET: Vols/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetInt32("IdAdminSession") != null)
            {
                if (id == null)
            {
                return NotFound();
            }

            var vol = await _context.Vol
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vol == null)
            {
                return NotFound();
            }

            return View(vol);
            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Administrateur/Index.cshtml");
            }
        }

        // GET: Vols/Create
        public async Task<IActionResult> Create()
        {
            if (HttpContext.Session.GetInt32("IdAdminSession") != null)
            {
                ViewData["listeAvions"] = await _context.Avion.ToListAsync();
            return View();
            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Administrateur/Index.cshtml");
            }
        }

        // POST: Vols/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateHeureVol,DureeVol,LieuDepart,LieuArrivee,IdAvion")] Vol vol)
        {
            if (HttpContext.Session.GetInt32("IdAdminSession") != null)
            {
                if (ModelState.IsValid)
            {
                _context.Add(vol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vol);
            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Administrateur/Index.cshtml");
            }
        }

        // GET: Vols/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetInt32("IdAdminSession") != null)
            {
                if (id == null)
            {
                return NotFound();
            }

            var vol = await _context.Vol.FindAsync(id);
            if (vol == null)
            {
                return NotFound();
            }
            ViewData["listeAvions"] = await _context.Avion.ToListAsync();
            return View(vol);
            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Administrateur/Index.cshtml");
            }
        }

        // POST: Vols/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateHeureVol,DureeVol,LieuDepart,LieuArrivee,IdAvion")] Vol vol)
        {
            if (HttpContext.Session.GetInt32("IdAdminSession") != null)
            {
                if (id != vol.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolExists(vol.Id))
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
            return View(vol);
            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Administrateur/Index.cshtml");
            }
        }

        // GET: Vols/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetInt32("IdAdminSession") != null)
            {
                if (id == null)
            {
                return NotFound();
            }

            var vol = await _context.Vol
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vol == null)
            {
                return NotFound();
            }

            return View(vol);
            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Administrateur/Index.cshtml");
            }
        }

        // POST: Vols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetInt32("IdAdminSession") != null)
            {
                var vol = await _context.Vol.FindAsync(id);
            _context.Vol.Remove(vol);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["nonconnecter"] = "1";
                return View("~/Views/Administrateur/Index.cshtml");
            }
        }

        private bool VolExists(int id)
        {
            return _context.Vol.Any(e => e.Id == id);
        }
    }
}
