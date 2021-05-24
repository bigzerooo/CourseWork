using GameGenreNeuronNetwork.Context;
using System.Collections.Generic;
using System.Linq;

namespace GameGenreNeuronNetwork.NeuronNetwork
{
    public class InputLayer
    {
        private readonly (double[], double[])[] _trainset;

        public InputLayer(DatabaseContext context)
        {
            var sets = context.TrainingSets.ToList();
            var genreIds = context.GameGenres.Select(g => g.Id).ToList();

            var tupleList = new List<(double[] aspects, double[] result)>();

            foreach (var item in sets)
            {
                var aspects = context
                    .GameAspectGroups
                    .Where(a => a.GroupId == item.GameAspectGroupId)
                    .Select(a => a.Value)
                    .ToArray();

                var result = new double[genreIds.Count];
                result[genreIds.IndexOf(item.GameGenreId)] = 1;

                tupleList.Add((aspects, result));
            }

            _trainset = tupleList.ToArray();
        }

        public (double[], double[])[] Trainset { get => _trainset; }
    }
}
