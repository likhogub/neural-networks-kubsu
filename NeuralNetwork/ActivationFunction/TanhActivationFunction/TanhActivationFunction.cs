using System;
using neural_networks_kubsu.NeuralNetwork.Neuron;

namespace neural_networks_kubsu.NeuralNetwork.ActivationFunction.TanhActivationFunction
{
    public class TanhActivationFunction : AbstractActivationFunction
    {
        public override void ActivateNeuron(INeuron neuron)
        {
            neuron.ActivationValue = Math.Tanh(neuron.NeuronValue);
            neuron.DerivativeValue = 1.0 - neuron.ActivationValue * neuron.ActivationValue;
        }
    }
}