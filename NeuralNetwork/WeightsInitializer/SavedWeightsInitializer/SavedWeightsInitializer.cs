using System.IO;
using System.Xml.Serialization;

namespace neural_networks_kubsu.NeuralNetwork.WeightsInitializer.SavedWeightsInitializer
{
    public class SavedWeightsInitializer : IWeightsInitializer
    {
        private readonly double[][][] _weights;
        
        public SavedWeightsInitializer(double[][][] weights)
        {
            _weights = weights;
        }
        
        public double[][] Initialize(int prevLayerUnits, int units, int layerIndex)
        {
            return _weights[layerIndex-1];
        }
    }
}