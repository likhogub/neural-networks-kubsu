using neural_networks_kubsu.NeuralNetwork.Neuron;

namespace neural_networks_kubsu.NeuralNetwork.Layer
{
    public abstract class AbstractLayer : ILayer
    {
        public INeuron[] Neurons { get; }

        protected AbstractLayer(int units)
        {
            Neurons = new INeuron[units];
        }
    }
}