using neural_networks_kubsu.NeuralNetwork.Neuron;

namespace neural_networks_kubsu.NeuralNetwork.ActivationFunction.IdentityActivationFunction
{
    public class IdentityActivationFunction : AbstractActivationFunction
    {
        public override void ActivateNeuron(INeuron neuron)
        {
            neuron.ActivationValue = neuron.NeuronValue;
            neuron.DerivativeValue = 1.0;
        }
    }
}