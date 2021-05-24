using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameGenreNeuronNetwork.Models
{
    public class GameAspect
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<GameAspectGroup> GameAspectGroups { get; set; }
    }
}
