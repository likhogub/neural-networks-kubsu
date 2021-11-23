using neural_networks_kubsu.NeuralNetwork.ActivationFunction;
using neural_networks_kubsu.NeuralNetwork.Layer;

namespace neural_networks_kubsu.NeuralNetwork.Neuron
{
    public class Neuron : INeuron
    {
        public double ActivationValue { get; private set; }
        public double NeuronValue { get; private set; }
        public double[] Weights { get; }
        public double Delta { get; set; }
        private readonly IActivationFunction _activationFunction;

        public Neuron(double[] weights, IActivationFunction activationFunction)
        {
            Weights = weights;
            _activationFunction = activationFunction;
        }

        public void Compute(ILayer prevLayer)
        {
            var neurons = prevLayer.Neurons;
            var sum = Weights[0];
            for (var i = 0; i < neurons.Length; i++)
            {
                sum += neurons[i].ActivationValue * Weights[i + 1];
            }

            NeuronValue = sum;
            ActivationValue = _activationFunction.Activate(sum);
        }

        public void Correct(double learningRate, ILayer prevLayer)
        {
            Weights[0] += learningRate * Delta;
            for (var i = 1; i <= prevLayer.Neurons.Length; i++)
            {
                Weights[i] += learningRate * Delta * prevLayer.Neurons[i - 1].ActivationValue;
            }
        }
    }
}