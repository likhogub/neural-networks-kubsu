using System;
using System.Linq;

namespace neural_networks_kubsu.NeuralNetwork.Layer.OutputLayer
{
    public class OutputLayer : HiddenLayer.HiddenLayer, IOutputLayer
    {
        public double[] Result
        {
            get
            {
                var result = new double[Neurons.Length];
                foreach (var i in Enumerable.Range(0, Neurons.Length))
                {
                    result[i] = Neurons[i].ActivationValue;
                }
                return result;
            }
        }

        public OutputLayer(int units) : base(units)
        {
        }

        public void ComputeDelta(double[] expectedData)
        {
            foreach (var i in Enumerable.Range(0, Neurons.Length))
            {
                var errorValue = Math.Pow(expectedData[i] - Neurons[i].ActivationValue, 2.0);
                Neurons[i].Delta = ActivationFunction.Derivative(Neurons[i].NeuronValue) * errorValue;
            }
        }
    }
}