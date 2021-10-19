using neural_networks_kubsu.NeuralNetwork.Neuron;

namespace neural_networks_kubsu.NeuralNetwork.Layer
{
    public interface ILayer
    {
        INeuron[] Neurons { get; }
    }
}