using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Futbol.BaseDatos;
using Futbol.Models;
using Futbol.Reglas;
using System.Security.Claims;
using System.Threading;

namespace Futbol.Controllers
{
    public class PartidosController : Controller
    {
        private readonly FutbolDbContext _context;

        public PartidosController(FutbolDbContext context)
        {
            _context = context;
        }

        // GET: Partidos
        public async Task<IActionResult> Index()
        {
            var futbolDbContext = _context.Partidos.Include(p => p.EquipoLocal).Include(p => p.EquipoVisitante);
            return View(await futbolDbContext.ToListAsync());
        }

        // GET: Partidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partido = await _context.Partidos
                .Include(p => p.EquipoLocal)
                .Include(p => p.EquipoVisitante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partido == null)
            {
                return NotFound();
            }

            return View(partido);
        }

        // GET: Partidos/Create
        public IActionResult Create()
        {
            //ViewData["EquipoLocalId"] = new SelectList(_context.Equipos, "Id", "Id");
            //ViewData["EquipoVisitianteId"] = new SelectList(_context.Equipos, "Id", "Id");
            return View();
        }

        // POST: Partidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Fecha")] Partido partido)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(partido);
                RNPartido.AgregarPartido(_context, partido.Fecha);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["EquipoLocalId"] = new SelectList(_context.Equipos, "Id", "Id", partido.EquipoLocalId);
            //ViewData["EquipoVisitianteId"] = new SelectList(_context.Equipos, "Id", "Id", partido.EquipoVisitianteId);
            return View(partido);
        }

        // GET: Partidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partido = await _context.Partidos.FindAsync(id);
            if (partido == null)
            {
                return NotFound();
            }
            ViewData["EquipoLocalId"] = new SelectList(_context.Equipos, "Id", "Id", partido.EquipoLocalId);
            ViewData["EquipoVisitianteId"] = new SelectList(_context.Equipos, "Id", "Id", partido.EquipoVisitianteId);
            return View(partido);
        }

        // POST: Partidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EquipoLocalId,EquipoVisitianteId,Fecha")] Partido partido)
        {
            if (id != partido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartidoExists(partido.Id))
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
            ViewData["EquipoLocalId"] = new SelectList(_context.Equipos, "Id", "Id", partido.EquipoLocalId);
            ViewData["EquipoVisitianteId"] = new SelectList(_context.Equipos, "Id", "Id", partido.EquipoVisitianteId);
            return View(partido);
        }

        // GET: Partidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partido = await _context.Partidos
                .Include(p => p.EquipoLocal)
                .Include(p => p.EquipoVisitante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partido == null)
            {
                return NotFound();
            }

            return View(partido);
        }

        // POST: Partidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            throw new Exception("Falta borrar los equipos y jugadores asociados en todo caso!");

            var partido = await _context.Partidos.FindAsync(id);
            _context.Partidos.Remove(partido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartidoExists(int id)
        {
            return _context.Partidos.Any(e => e.Id == id);
        }


        public ActionResult Confirmacion()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            int idJugador = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            var jugador = _context.Jugadores.Where(o => o.Id == idJugador).FirstOrDefault();
            var ultimoPartido = _context.Partidos
              .OrderByDescending(x => x.Fecha)
              .FirstOrDefault();
            List<int> idsEquiposPosibles = new List<int>();
            idsEquiposPosibles.Add(ultimoPartido.EquipoLocalId);
            idsEquiposPosibles.Add(ultimoPartido.EquipoVisitianteId);

            var listaJugadoresPorEquipos = _context.JugadoresPorEquipos.Where(o => idsEquiposPosibles.Contains(o.EquipoId)).ToList();

            ViewModel.VMConfirmacion modelo = new ViewModel.VMConfirmacion();
            modelo.Jugador = jugador;
            modelo.Confirmado = listaJugadoresPorEquipos.Any(o => o.JugadorId == idJugador);
            modelo.FechaPartido = ultimoPartido.Fecha;
            modelo.CantidadLugaresDisponibles = 10 - listaJugadoresPorEquipos.Where(o => o.JugadorId != null).Count();
            return View(modelo);

        }

    }
}
