namespace neural_networks_kubsu.NeuralNetwork.ActivationFunction.RawActivationFunction
{
    public class RawActivationFunction : IActivationFunction
    {
        public double Activate(double value)
        {
            return value;
        }

        public double Derivative(double value)
        {
            return 1;
        }
    }
}