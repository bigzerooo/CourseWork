using System.Collections.Generic;

namespace GameGenreNeuronNetwork.Models
{
    public class GameGenre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public GameAspectGroup[] GameAspectGroups { get; set; }
        public List<TrainingSet> TrainingSets { get; set; }
    }
}
