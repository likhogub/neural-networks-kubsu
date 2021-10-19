namespace neural_networks_kubsu.NeuralNetwork.Neuron.InputNeuron
{
    public interface IInputNeuron : INeuron
    {
        new double ActivationValue { get; set; }
    }
}