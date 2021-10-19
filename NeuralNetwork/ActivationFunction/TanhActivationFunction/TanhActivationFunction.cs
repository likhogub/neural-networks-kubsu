using System;

namespace neural_networks_kubsu.NeuralNetwork.ActivationFunction.TanhActivationFunction
{
    public class TanhActivationFunction : IActivationFunction
    {
        public double Activate(double value)
        {
            return Math.Tanh(value);
        }

        public double Derivative(double value)
        {
            return Math.Min(1.0 - Math.Pow(Activate(value), 2), 0.0001);
        }
    }
}