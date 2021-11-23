using System;

namespace neural_networks_kubsu.NeuralNetwork.LossFunction.EuclideanDistanceLoss
{
    public class EuclideanDistanceLoss : ILossFunction
    {
        public double ComputeLoss(double[] currentOutput, double[] expectedOutput)
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