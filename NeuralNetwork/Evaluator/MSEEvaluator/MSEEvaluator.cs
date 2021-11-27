using System;

namespace neural_networks_kubsu.NeuralNetwork.Evaluator.MSEEvaluator
{
    public class MSEEvaluator : IEvaluator
    {
        private readonly double[][] _inputData;
        private readonly double[][] _outputData;
        
        public MSEEvaluator(double[][] inputData, double[][] outputData)
        {
            _inputData = inputData;
            _outputData = outputData;
        }
        
        public double Evaluate(NeuralNetwork neuralNetwork)
        {
            var loss = 0.0;
            for (var i = 0; i < _inputData.Length; i++)
            {
                loss += Distance(_outputData[i], neuralNetwork.Predict(_inputData[i]));
            }
            return loss / _inputData.Length;
        }

        private double Distance(double[] expectedOutput, double[] currentOutput)
            {
                var s = 0.0;
                for (var i = 0; i < currentOutput.Length; i++)
                {
                    s += Math.Pow(expectedOutput[i] - currentOutput[i], 2.0);
                }
                return Math.Sqrt(s);
            }
    }
}