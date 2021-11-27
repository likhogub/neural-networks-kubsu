using neural_networks_kubsu.NeuralNetwork.Layer;

namespace neural_networks_kubsu.NeuralNetwork.Neuron
{
    public interface INeuron
    {
        double ActivationValue { get; set; }
        double DerivativeValue { get; set; }
        double NeuronValue { get; }
        double[] Weights { get; }
        double Delta { get; set; }
        void Compute(ILayer prevLayer);
        void Correct(double learningRate, ILayer prevLayer);
    }
}