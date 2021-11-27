using neural_networks_kubsu.NeuralNetwork.Layer;
using neural_networks_kubsu.NeuralNetwork.Neuron;

namespace neural_networks_kubsu.NeuralNetwork.ActivationFunction
{
    public interface IActivationFunction
    {
        void ActivateNeuron(INeuron neuron);
        void ActivateLayer(ILayer layer);
    }
}