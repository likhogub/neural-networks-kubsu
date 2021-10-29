namespace neural_networks_kubsu.NeuralNetwork.ActivationFunction.IdentityActivationFunction
{
    public class IdentityActivationFunction : IActivationFunction
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