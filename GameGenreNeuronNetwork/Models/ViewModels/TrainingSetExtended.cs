using System.Collections.Generic;

namespace GameGenreNeuronNetwork.Models.ViewModels
{
    public class TrainingSetExtended
    {
        public string Name { get; set; }
        public List<GameAspectWithValue> GameAspects { get; set; }
    }
}
