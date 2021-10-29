using System;
using System.Linq;
using neural_networks_kubsu.NeuralNetwork.ActivationFunction;

namespace neural_networks_kubsu.NeuralNetwork.Layer.HiddenLayer
{
    public class HiddenLayer : AbstractLayer, IHiddenLayer
    {
        public IActivationFunction ActivationFunction { get; set; }
        public ILayer PreviousLayer { get; set; }
        public ILayer NextLayer { get; set; }
        public double[][] Weights { get; set; }

        public HiddenLayer(int units) : base(units)
        {
        }

        public void ComputeNeurons()
        {
            foreach (var neuron in Neurons)
            {
                neuron.Compute(PreviousLayer);
            }
        }

        public void CorrectWeights(double learningRate)
        {
            foreach (var neuron in Neurons)
            {
                neuron.Correct(learningRate);
            }
        }

        public void Initialize()
        {
            foreach (var i in Enumerable.Range(0, Weights.Length))
            {
                Neurons[i] = new Neuron.Neuron(Weights[i], ActivationFunction);
            }
        }

        public void ComputeDelta(double inertiaCoef)
        {
            foreach (var i in Enumerable.Range(0, Neurons.Length))
            {
                var computedDelta = 0.0;
                var g = ActivationFunction.Derivative(Neurons[i].NeuronValue);
                foreach (var j in Enumerable.Range(0, NextLayer.Neurons.Length))
                {
                    computedDelta += NextLayer.Neurons[j].Weights[i] * NextLayer.Neurons[j].Delta;
                }
                Neurons[i].Delta = (inertiaCoef * Neurons[i].Delta + (1 - inertiaCoef) * g * computedDelta);
            }
        }
    }
}