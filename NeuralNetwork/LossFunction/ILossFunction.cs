namespace neural_networks_kubsu.NeuralNetwork.LossFunction
{
    public interface ILossFunction
    {
        public double ComputeLoss(double[] currentOutput, double[] expectedOutput);
    }
}