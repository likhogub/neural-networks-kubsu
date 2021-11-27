using System;

namespace neural_networks_kubsu.NeuralNetwork.CallbackFunction.EpochCallbackFunction
{
    public class EpochCallbackFunction : ICallbackFunction
    {
        private readonly Action<double> _callback;

        public EpochCallbackFunction(Action<double> callback)
        {
            _callback = callback;
        }

        public void Invoke(NeuralNetwork neuralNetwork)
        {
            _callback(neuralNetwork.Epoch);
        }
    }
}