using System.IO;
using System.Xml.Serialization;

namespace neural_networks_kubsu.NeuralNetwork.WeightsInitializer.SavedWeightsInitializer
{
    public class SavedWeightsInitializer : IWeightsInitializer
    {
        private readonly double[][][] _weights;
        
        public SavedWeightsInitializer(string filename)
        {
            var serializer = new XmlSerializer(typeof(double[][][]));
            using var fs = new FileStream(filename, FileMode.OpenOrCreate);
            _weights = (double[][][])serializer.Deserialize(fs);
            fs.Close();
        }
        
        public double[][] Initialize(int prevLayerUnits, int units, int layerIndex)
        {
            return _weights[layerIndex-1];
        }
    }
}