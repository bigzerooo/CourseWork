using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameGenreNeuronNetwork.Models
{
    public class TrainingSet
    {
        public int Id { get; set; }
        public int GameGenreId { get; set; }
        public int GameAspectGroupId { get; set; }

        public GameGenre GameGenre { get; set; }
        
    }
}
