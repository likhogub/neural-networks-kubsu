using System;
using System.Linq;

namespace neural_networks_kubsu.NeuralNetwork.LossFunction.EuclideanDistanceLoss
{
    public class EuclideanDistanceLoss : ILossFunction
    {
        public double ComputeLoss(double[] currentOutput, double[] expectedOutput)
        {
            var s = 0.0;
            foreach (var i in Enumerable.Range(0, currentOutput.Length))
            {
                s += Math.Pow(expectedOutput[i] - currentOutput[i], 2.0);
            }
            return Math.Sqrt(s);
        }
    }
}