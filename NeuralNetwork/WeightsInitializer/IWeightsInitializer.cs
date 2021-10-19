namespace neural_networks_kubsu.NeuralNetwork.WeightsInitializer
{
    public interface IWeightsInitializer
    {
        public double[][] Initialize(int prevLayerUnits, int units, int layerIndex);
    }
}