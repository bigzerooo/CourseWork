using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GameGenreNeuronNetwork.NeuronNetwork
{
    public abstract class Layer
    {
        protected Layer(int non, int nopn, NeuronType nt, string type)
        {
            numofneurons = non;
            numofprevneurons = nopn;
            Neurons = new Neuron[non];
            double[,] Weights = WeightInitialize(MemoryMode.GET, type);
            for (int i = 0; i < non; ++i)
            {
                double[] temp_weights = new double[nopn];
                for (int j = 0; j < nopn; ++j)
                    temp_weights[j] = Weights[i, j];
                Neurons[i] = new Neuron(null, temp_weights, nt);
            }
        }
        protected int numofneurons;
        protected int numofprevneurons;
        Neuron[] _neurons;
        public Neuron[] Neurons { get => _neurons; set => _neurons = value; }
        public double[] Data
        {
            set
            {
                for (int i = 0; i < Neurons.Length; ++i)
                    Neurons[i].Inputs = value;
            }
        }
        public double[,] WeightInitialize(MemoryMode mm, string type)
        {
            double[,] _weights = new double[numofneurons, numofprevneurons];
            Console.WriteLine($"{type} weights are being initialized...");
            XmlDocument memory_doc = new XmlDocument();
            memory_doc.Load($"{type}_memory.xml");
            XmlElement memory_el = memory_doc.DocumentElement;
            switch (mm)
            {
                case MemoryMode.GET:
                    for (int l = 0; l < _weights.GetLength(0); ++l)
                        for (int k = 0; k < _weights.GetLength(1); ++k)
                            _weights[l, k] = double.Parse(memory_el.ChildNodes.Item(k + _weights.GetLength(1) * l).InnerText.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                    break;
                case MemoryMode.SET:
                    for (int l = 0; l < Neurons.Length; ++l)
                        for (int k = 0; k < numofprevneurons; ++k)
                            memory_el.ChildNodes.Item(k + numofprevneurons * l).InnerText = Neurons[l].Weights[k].ToString();
                    break;
            }
            memory_doc.Save($"{type}_memory.xml");
            Console.WriteLine($"{type} weights have been initialized...");
            return _weights;
        }
        abstract public void Recognize(Network net, Layer nextLayer);
        abstract public double[] BackwardPass(double[] stuff);
    }
}
