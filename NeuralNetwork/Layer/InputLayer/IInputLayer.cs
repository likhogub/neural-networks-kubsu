namespace neural_networks_kubsu.NeuralNetwork.Layer.InputLayer
{
    public interface IInputLayer : ILayer
    {
        void Feed(double[] inputData);
    }
}