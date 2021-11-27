using System;
using neural_networks_kubsu.NeuralNetwork.Evaluator;

namespace neural_networks_kubsu.NeuralNetwork.CallbackFunction.EvaluatorCallbackFunction
{
    public class EvaluatorCallbackFunction : ICallbackFunction
    {
        private readonly int _onEachEpoch;
        private readonly Action<double> _callback;

        public EvaluatorCallbackFunction(
            int onEachEpoch,
            IEvaluator evaluator,
            Action<double> callback)
        {
            _onEachEpoch = onEachEpoch;
            _callback = callback;
            _evaluator = evaluator;
        }

        private readonly IEvaluator _evaluator;

        public void Invoke(NeuralNetwork neuralNetwork)
        {
            if (neuralNetwork.Epoch % _onEachEpoch == 0)
            {
                _callback(_evaluator.Evaluate(neuralNetwork));
            }
        }
    }
}