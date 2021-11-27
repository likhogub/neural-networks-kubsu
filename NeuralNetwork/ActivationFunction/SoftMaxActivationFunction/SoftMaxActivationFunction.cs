using System;
using neural_networks_kubsu.NeuralNetwork.Layer;
using neural_networks_kubsu.NeuralNetwork.Neuron;

namespace neural_networks_kubsu.NeuralNetwork.ActivationFunction.SoftMaxActivationFunction
{
    public class SoftMaxActivationFunction : AbstractActivationFunction
    {
        private double _layerSum;
        
        public override void ActivateNeuron(INeuron neuron)
        {
            neuron.ActivationValue /= _layerSum;
            neuron.DerivativeValue = neuron.ActivationValue * (1 - neuron.ActivationValue);
        }

        public override void ActivateLayer(ILayer layer)
        {
            var s = 0.0;
            foreach (var neuron in layer.Neurons)
            {
                s += neuron.ActivationValue = Math.Exp(neuron.NeuronValue);
            }
            _layerSum = s;
            foreach (var neuron in layer.Neurons)
            {
                ActivateNeuron(neuron);
            }
        }
    }
}