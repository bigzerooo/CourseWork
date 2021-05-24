using GameGenreNeuronNetwork.Context;
using GameGenreNeuronNetwork.Models;
using GameGenreNeuronNetwork.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameGenreNeuronNetwork.Controllers
{
    public class TrainingSetsController : Controller
    {
        private readonly DatabaseContext _context;

        public TrainingSetsController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //optimize using joins
            var trainingSets = _context.TrainingSets.ToList();
            List<TrainingSetExtended> trainingSetExtended = new List<TrainingSetExtended>();
            foreach(var item in trainingSets)
            {
                var gameGenreName = _context
                    .GameGenres
                    .First(g => g.Id == item.GameGenreId)
                    .Name;

                var gameAspects = (from gr in _context.GameAspectGroups
                                    join asp in _context.GameAspects on gr.GameAspectId equals asp.Id
                                    where gr.GroupId == item.GameAspectGroupId
                                    select new GameAspectWithValue
                                    {
                                        Name = asp.Name,
                                        Value = gr.Value
                                    }).ToList();

                trainingSetExtended.Add(new TrainingSetExtended { Name = gameGenreName, GameAspects = gameAspects });
            }

            return View(trainingSetExtended);
        }

        public IActionResult Create()
        {
            var genres = _context.GameGenres.ToArray();
            var aspects = _context.GameAspects.Select(a => new GameAspectWithValue { Id = a.Id, Name = a.Name, Value = 0 }).ToArray();

            var model = new TrainingSetCreateViewModel { Genres = genres, Aspects = aspects };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrainingSetCreateViewModel model)
        {
            int groupId;
            try
            {
                groupId = _context.GameAspectGroups.OrderBy(a=>a.GroupId).Last().GroupId + 1;
            }
            catch
            {
                groupId = 1;
            }
            var aspects = _context.GameAspects.ToList();
            foreach (var i in aspects)
            {
                _context.GameAspectGroups.Add(new GameAspectGroup { GroupId = groupId, GameAspectId = i.Id, Value = model.SelectedIds != null && model.SelectedIds.Count() > 0 && model.SelectedIds.Contains(i.Id) ? 1 : 0 });
            }
            _context.Add(new TrainingSet { GameGenreId = model.SelectedGenreId, GameAspectGroupId = groupId });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
