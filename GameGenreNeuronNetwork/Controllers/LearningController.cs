using GameGenreNeuronNetwork.Context;
using GameGenreNeuronNetwork.NeuronNetwork;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
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
            ClearMemory();

            Network net = new Network(_context);
            Network.Train(net);

            return RedirectToAction("Index", "Home");
        }

        private void ClearMemory()
        {
            StreamReader sr1 = new StreamReader("clear_layer_memory.xml", Encoding.Default);
            StreamWriter sw1 = new StreamWriter("hidden_layer_memory.xml", false);

            sw1.Write(sr1.ReadToEnd());
            sw1.Close();
            sr1.Close();

            StreamReader sr2 = new StreamReader("clear_layer_memory.xml", Encoding.Default);
            StreamWriter sw2 = new StreamWriter("output_layer_memory.xml", false);
            sw2.Write(sr2.ReadToEnd());
            sw2.Close();
            sr2.Close();
        }
    }
}
