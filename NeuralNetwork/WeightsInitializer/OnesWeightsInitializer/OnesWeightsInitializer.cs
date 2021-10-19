using System.Linq;

namespace neural_networks_kubsu.NeuralNetwork.WeightsInitializer.OnesWeightsInitializer
{
    public class OnesWeightsInitializer : IWeightsInitializer
    {
        public double[][] Initialize(int prevLayerUnits, int units, int layerIndex)
        {
            var weights = new double[units][];
            foreach (var i in Enumerable.Range(0, units))
            {
                weights[i] = new double[prevLayerUnits + 1];
                foreach (var j in Enumerable.Range(0, prevLayerUnits + 1))
                {
                    weights[i][j] = 1;
                }
            }
            return weights;
        }
    }
}