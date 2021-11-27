using neural_networks_kubsu.NeuralNetwork.Neuron;

namespace neural_networks_kubsu.NeuralNetwork.ActivationFunction.LinearActivationFunction
{
    public class LinearActivationFunction : AbstractActivationFunction
    {
        public override void ActivateNeuron(INeuron neuron)
        {
            switch (neuron.NeuronValue)
            {
                case > 1:
                    neuron.ActivationValue = 1 + (neuron.NeuronValue - 1) * 0.01;
                    neuron.DerivativeValue = 0.01;
                    break;
                case < -1:
                    neuron.ActivationValue = -1 + (neuron.NeuronValue + 1) * 0.01;
                    neuron.DerivativeValue = 0.01;
                    break;
                default:
                    neuron.ActivationValue = neuron.NeuronValue;
                    neuron.DerivativeValue = 1;
                    break;
            }
        }
    }
}