using System;
using System.Linq;

namespace neural_networks_kubsu.NeuralNetwork.WeightsInitializer.DefaultWeightsInitializer
{
    public class DefaultWeightsInitializer : IWeightsInitializer
    {
        /* Initializes weights array. Array[0] is bias. Array[1..size+1] is weight value. */
        public double[][] Initialize(int prevLayerUnits, int units, int layerIndex)
        {
            var rnd = new Random();
            var weights = new double[units][];
            foreach (var i in Enumerable.Range(0, units))
            {
                weights[i] = new double[prevLayerUnits + 1];
            }
            
            foreach (var neuronIndex in Enumerable.Range(0, units))
            {
                var s = Enumerable.Range(0, prevLayerUnits + 1)
                    .Sum(i => (weights[neuronIndex][i] = rnd.NextDouble() * 2 - 1));

                var mean = s / (prevLayerUnits);

                foreach (var i in Enumerable.Range(0, prevLayerUnits))
                {
                    weights[neuronIndex][i] -= mean;
                }
            }
            return weights;
        }
    }
}