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
    public class GameAspectsController : Controller
    {
        private readonly DatabaseContext _context;

        public GameAspectsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: GameAspects
        public async Task<IActionResult> Index()
        {
            return View(await _context.GameAspects.ToListAsync());
        }

        // GET: GameAspects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameAspect = await _context.GameAspects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameAspect == null)
            {
                return NotFound();
            }

            return View(gameAspect);
        }

        // GET: GameAspects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GameAspects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] GameAspect gameAspect)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameAspect);
                await _context.SaveChangesAsync();

                var groupIds = _context.GameAspectGroups
                    .Select(g => g.GroupId)
                    .Distinct();

                var gameAspectGroups = new List<GameAspectGroup>();

                foreach(var i in groupIds)
                {
                    gameAspectGroups.Add(new GameAspectGroup { GroupId = i, GameAspectId = gameAspect.Id, Value = 0 });
                }

                await _context.AddRangeAsync(gameAspectGroups);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameAspect);
        }

        // GET: GameAspects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameAspect = await _context.GameAspects.FindAsync(id);
            if (gameAspect == null)
            {
                return NotFound();
            }
            return View(gameAspect);
        }

        // POST: GameAspects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] GameAspect gameAspect)
        {
            if (id != gameAspect.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameAspect);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameAspectExists(gameAspect.Id))
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
            return View(gameAspect);
        }

        // GET: GameAspects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameAspect = await _context.GameAspects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameAspect == null)
            {
                return NotFound();
            }

            return View(gameAspect);
        }

        // POST: GameAspects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameAspect = await _context.GameAspects.FindAsync(id);
            _context.GameAspects.Remove(gameAspect);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameAspectExists(int id)
        {
            return _context.GameAspects.Any(e => e.Id == id);
        }
    }
}
