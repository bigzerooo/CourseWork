namespace GameGenreNeuronNetwork.Models
{
    public class GameAspectGroup
    {
        public int GroupId { get; set; }
        public int GameAspectId { get; set; }
        public double Value { get; set; }

        public GameAspect GameAspect { get; set; }
    }
}
