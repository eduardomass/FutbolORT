using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Futbol.BaseDatos;
using Futbol.Models;
using Microsoft.AspNetCore.Authorization;

namespace Futbol.Controllers
{
    [Authorize(Roles = "Administrador, Jugador")]
    public class UsuariosController : Controller
    {
        private readonly FutbolDbContext _context;

        public UsuariosController(FutbolDbContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Jugadores.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Jugadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Jugador usuario)
        {
            if (ModelState.IsValid)
            {
                //1. Obtener todos los usuarios
                
                //3. Si no se repite agrega, sino pum!
                if (this.ValidarRepeticion(usuario))
                {
                    _context.Add(usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Email Repetido";
                    return View(usuario);
                }
            }
            return View(usuario);
        }

        private bool ValidarRepeticion(Usuario usuario)
        {
            List<Usuario> listaUsuarios = _context.Jugadores.ToList<Usuario>();
            listaUsuarios = _context.Administradores.ToList<Usuario>();
            //2. Por cada usuario, ver si el email se repite contra el email recibido(usuario)
            var noSeRepite = !listaUsuarios
                .Where(a => a.Email != null)
                .Any(usu => usu.Email.Equals(usuario.Email, StringComparison.OrdinalIgnoreCase) &&
                usu.Id != usuario.Id);

            return noSeRepite;
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Jugadores.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Password,Email")] Usuario usuario)
        {
            
            if (ModelState.IsValid)
            {
                if (this.ValidarRepeticion(usuario))
                {

                    try
                    {
                        //var usuarioBD = _context.Usuarios.FirstOrDefault(o=>o.Id == usuario.Id);
                        //usuarioBD.Email = usuario.Email;
                        _context.Update(usuario).State = EntityState.Detached;
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UsuarioExists(usuario.Id))
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
                else
                {
                    ViewBag.Error = "Email Repetido";
                    return View(usuario);
                }
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Jugadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Jugadores.FindAsync(id);
            _context.Jugadores.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Jugadores.Any(e => e.Id == id);
        }
    }
}
