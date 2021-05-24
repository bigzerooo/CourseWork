using GameGenreNeuronNetwork.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameGenreNeuronNetwork.NeuronNetwork
{
    public class Network
    {
        private readonly DatabaseContext _context;

        public Network(DatabaseContext context)
        {
            input_layer = new InputLayer(context);

            int numberOfInputs = context.GameAspects.Count();
            int numberOfOutputs = context.GameGenres.Count();

            hidden_layer = new HiddenLayer(Constants.NumberOfHiddenNeurons,
            numberOfInputs,
            NeuronType.Hidden,
            nameof(hidden_layer));

            output_layer = new OutputLayer(numberOfOutputs,
            Constants.NumberOfHiddenNeurons,
            NeuronType.Output,
            nameof(output_layer));

            fact = new double[numberOfOutputs];

            _context = context;
        }
        InputLayer input_layer;
        public HiddenLayer hidden_layer;
        public OutputLayer output_layer;

        public double[] fact;
        double GetMSE(double[] errors)
        {
            double sum = 0;
            for (int i = 0; i < errors.Length; ++i)
                sum += Math.Pow(errors[i], 2);
            return 0.5d * sum;
        }
        double GetCost(double[] mses)
        {
            double sum = 0;
            for (int i = 0; i < mses.Length; ++i)
                sum += mses[i];
            return (sum / mses.Length);
        }
        public static void Train(Network net)
        {
            const double threshold = Constants.Accuracy;
            double[] temp_mses = new double[10];
            double temp_cost;
            do
            {
                for (int i = 0; i < net.input_layer.Trainset.Length; ++i)
                {
                    net.hidden_layer.Data = net.input_layer.Trainset[i].Item1;
                    net.hidden_layer.Recognize(null, net.output_layer);
                    net.output_layer.Recognize(net, null);
                    double[] errors = new double[net.input_layer.Trainset[i].Item2.Length];
                    for (int x = 0; x < errors.Length; ++x)
                        errors[x] = net.input_layer.Trainset[i].Item2[x] - net.fact[x];
                    temp_mses[i] = net.GetMSE(errors);
                    double[] temp_gsums = net.output_layer.BackwardPass(errors);
                    net.hidden_layer.BackwardPass(temp_gsums);
                }
                temp_cost = net.GetCost(temp_mses);
                Console.WriteLine($"{temp_cost}");
            } while (temp_cost > threshold);
            net.hidden_layer.WeightInitialize(MemoryMode.SET, nameof(hidden_layer));
            net.output_layer.WeightInitialize(MemoryMode.SET, nameof(output_layer));
        }

        public string[] Guess(Network net, double[] aspects)
        {
            net.hidden_layer.Data = aspects;
            net.hidden_layer.Recognize(null, net.output_layer);
            net.output_layer.Recognize(net, null);

            var results = new List<string>();
            var genres = _context.GameGenres.ToArray();
            for (int j = 0; j < net.fact.Length; ++j)
                results.Add($"{Math.Round(net.fact[j] * 100, 2)}% - {genres[j].Name}");
            return results.ToArray();
        }
    }
}
