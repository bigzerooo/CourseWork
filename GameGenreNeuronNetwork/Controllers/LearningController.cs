using GameGenreNeuronNetwork.Context;
using GameGenreNeuronNetwork.NeuronNetwork;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameGenreNeuronNetwork.Controllers
{
    public class LearningController : Controller
    {
        private readonly DatabaseContext _context;

        public LearningController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Learn()
        {
            Network net = new Network(_context);
            Network.Train(net);

            return RedirectToAction("Index", "Home");
        }
    }
}
