namespace neural_networks_kubsu.NeuralNetwork.ActivationFunction.LinearActivationFunction
{
    public class LinearActivationFunction : IActivationFunction
    {
        public double Activate(double value)
        {
            if (value > 1)
            {
                return 1 + (value - 1) * 0.01;
            }
            else if (value < -1)
            {
                return -1 + (value + 1) * 0.01;
            }
            else
            {
                return value;
            }
        }

        public double Derivative(double value)
        {
            if (value > 1)
            {
                return 0.01;
            }
            else if (value < -1)
            {
                return 0.01;
            }
            else
            {
                return 1;
            }
        }
    }
}