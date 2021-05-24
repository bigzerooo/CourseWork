namespace GameGenreNeuronNetwork.NeuronNetwork
{
    public class HiddenLayer : Layer
    {
        public HiddenLayer(int non, int nopn, NeuronType nt, string type) : base(non, nopn, nt, type) { }
        public override void Recognize(Network net, Layer nextLayer)
        {
            double[] hidden_out = new double[Neurons.Length];
            for (int i = 0; i < Neurons.Length; ++i)
                hidden_out[i] = Neurons[i].Output;
            nextLayer.Data = hidden_out;
        }
        public override double[] BackwardPass(double[] gr_sums)
        {
            double[] gr_sum = null;
            for (int i = 0; i < numofneurons; ++i)
                for (int n = 0; n < numofprevneurons; ++n)
                    Neurons[i].Weights[n] += Constants.LearningSpeed * Neurons[i].Inputs[n] * Neurons[i].Gradientor(0, Neurons[i].Derivativator(Neurons[i].Output), gr_sums[i]);
            return gr_sum;
        }
    }
}
