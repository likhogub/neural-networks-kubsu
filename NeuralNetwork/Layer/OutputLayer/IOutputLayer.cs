using neural_networks_kubsu.NeuralNetwork.Layer.HiddenLayer;

namespace neural_networks_kubsu.NeuralNetwork.Layer.OutputLayer
{
    public interface IOutputLayer : IHiddenLayer
    {
        double[] Result { get; }
        void ComputeDelta(double[] expectedData);
    }
}