using neural_networks_kubsu.NeuralNetwork.Layer;
using neural_networks_kubsu.NeuralNetwork.Neuron;

namespace neural_networks_kubsu.NeuralNetwork.ActivationFunction
{
    public abstract class AbstractActivationFunction : IActivationFunction
    {
        public abstract void ActivateNeuron(INeuron neuron);

        public virtual void ActivateLayer(ILayer layer)
        {
            foreach (var neuron in layer.Neurons)
            {
                ActivateNeuron(neuron);
            }
        }
    }
}