using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameGenreNeuronNetwork.Context;
using GameGenreNeuronNetwork.Models;

namespace GameGenreNeuronNetwork.Controllers
{
    public class GameGenresController : Controller
    {
        private readonly DatabaseContext _context;

        public GameGenresController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: GameGenres
        public async Task<IActionResult> Index()
        {
            return View(await _context.GameGenres.ToListAsync());
        }

        // GET: GameGenres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameGenre = await _context.GameGenres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameGenre == null)
            {
                return NotFound();
            }

            return View(gameGenre);
        }

        // GET: GameGenres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GameGenres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] GameGenre gameGenre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameGenre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameGenre);
        }

        // GET: GameGenres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameGenre = await _context.GameGenres.FindAsync(id);
            if (gameGenre == null)
            {
                return NotFound();
            }
            return View(gameGenre);
        }

        // POST: GameGenres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] GameGenre gameGenre)
        {
            if (id != gameGenre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameGenre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameGenreExists(gameGenre.Id))
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
            return View(gameGenre);
        }

        // GET: GameGenres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameGenre = await _context.GameGenres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameGenre == null)
            {
                return NotFound();
            }

            return View(gameGenre);
        }

        // POST: GameGenres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameGenre = await _context.GameGenres.FindAsync(id);
            _context.GameGenres.Remove(gameGenre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameGenreExists(int id)
        {
            return _context.GameGenres.Any(e => e.Id == id);
        }
    }
}
