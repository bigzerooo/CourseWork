using GameGenreNeuronNetwork.Context;
using GameGenreNeuronNetwork.Models.ViewModels;
using GameGenreNeuronNetwork.NeuronNetwork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameGenreNeuronNetwork.Controllers
{
    public class GuessingController : Controller
    {
        private readonly DatabaseContext _context;

        public GuessingController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var aspects = _context.GameAspects.Select(a => new GameAspectWithValue { Id = a.Id, Name = a.Name, Value = 0 }).ToArray();

            var model = new GuessingViewModel { GameAspects = aspects };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Guess(GuessingViewModel model)
        {
            var aspects = _context.GameAspects.ToList();
            List<double> selectedIds = new List<double>();

            foreach (var i in aspects)
            {
                selectedIds.Add(model.SelectedIds != null && model.SelectedIds.Count() > 0 && model.SelectedIds.Contains(i.Id) ? 1 : 0);
            }

            Network net = new Network(_context);
            var result = net.Guess(net, selectedIds.ToArray());

            return RedirectToAction("Result", "Guessing", routeValues: new { result = result });
        }

        public IActionResult Result(string[] result)
        {
            return View(model: result);
        }
    }
}
