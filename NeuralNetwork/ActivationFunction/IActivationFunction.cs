namespace neural_networks_kubsu.NeuralNetwork.ActivationFunction
{
    public interface IActivationFunction
    {
        double Activate(double value);
        double Derivative(double value);
    }
}