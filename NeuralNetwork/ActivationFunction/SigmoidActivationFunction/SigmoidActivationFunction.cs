using System;
using neural_networks_kubsu.NeuralNetwork.Layer;
using neural_networks_kubsu.NeuralNetwork.Neuron;

namespace neural_networks_kubsu.NeuralNetwork.ActivationFunction.SigmoidActivationFunction
{
    public class SigmoidActivationFunction : AbstractActivationFunction
    {
        public override void ActivateNeuron(INeuron neuron)
        {
            neuron.ActivationValue = 1.0 / (1.0 + Math.Exp(-neuron.NeuronValue));
            neuron.DerivativeValue = neuron.ActivationValue * (1 - neuron.ActivationValue);
        }
    }
}