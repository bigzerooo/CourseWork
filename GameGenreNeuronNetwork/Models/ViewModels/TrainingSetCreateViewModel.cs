namespace GameGenreNeuronNetwork.Models.ViewModels
{
    public class TrainingSetCreateViewModel
    {
        public GameGenre[] Genres { get; set; }
        public GameAspectWithValue[] Aspects { get; set; }
        public int[] SelectedIds { get; set; }
        public int SelectedGenreId { get; set; }
    }
}
