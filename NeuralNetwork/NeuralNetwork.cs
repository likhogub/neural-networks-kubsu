using System;
using System.Collections.Generic;
using System.Linq;
using neural_networks_kubsu.NeuralNetwork.ActivationFunction;
using neural_networks_kubsu.NeuralNetwork.Layer.HiddenLayer;
using neural_networks_kubsu.NeuralNetwork.Layer.InputLayer;
using neural_networks_kubsu.NeuralNetwork.Layer.OutputLayer;
using neural_networks_kubsu.NeuralNetwork.LossFunction;
using neural_networks_kubsu.NeuralNetwork.WeightsInitializer;

namespace neural_networks_kubsu.NeuralNetwork
{
    public class NeuralNetwork
    {
        private IInputLayer _inputLayer;
        private IOutputLayer OutputLayer => (IOutputLayer) _layers.Last();
        private readonly List<IHiddenLayer> _layers = new();
        private ILossFunction _lossFunction;

        public double[] Predict(double[] inputData)
        {
            _inputLayer.Feed(inputData);
            foreach (var layer in _layers)
            {
                layer.ComputeNeurons();
            }

            return OutputLayer.Result;
        }

        public void Initialize(IWeightsInitializer weightsInitializer)
        {
            _layers[0].PreviousLayer = _inputLayer;
            _layers[0].Weights = weightsInitializer.Initialize(
                _inputLayer.Neurons.Length,
                _layers[0].Neurons.Length,
                1
            );
            
            for (var layerIndex = 1; layerIndex < _layers.Count; layerIndex++)
            {
                _layers[layerIndex].Weights = weightsInitializer.Initialize(
                    _layers[layerIndex - 1].Neurons.Length,
                    _layers[layerIndex].Neurons.Length,
                    layerIndex + 1
                );
            }

            for (var layerIndex = 0; layerIndex < _layers.Count - 1; layerIndex++)
            {
                _layers[layerIndex].NextLayer = _layers[layerIndex + 1];
            }

            for (var layerIndex = 1; layerIndex <= _layers.Count - 1; layerIndex++)
            {
                _layers[layerIndex].PreviousLayer = _layers[layerIndex - 1];
            }

            foreach (var layer in _layers)
            {
                layer.Initialize();
            }
        }

        private void CorrectWeights(double learningRate)
        {
            foreach (var layer in _layers)
            {
                layer.CorrectWeights(learningRate);
            }
        }

        private void ComputeDelta(double[] data)
        {
            OutputLayer.ComputeDelta(data);
            for (var i = _layers.Count - 2; i >= 0; i--)
            {
                _layers[i].ComputeDelta();
            }
        }

        public void Fit(double[][] inputBatch, double[][] outputBatch, int epochs, double learningRate)
        {
            for (var epoch = 0; epoch < epochs; epoch++)
            {
                for (var i = 0; i < inputBatch.Length; i++)
                {
                    Predict(inputBatch[i]);
                    ComputeDelta(outputBatch[i]);
                    CorrectWeights(learningRate);
                }

                if (epoch % 10 == 0)
                {
                    FormMain.LabelNeurons.Text = "Loss: " + Evaluate(inputBatch, outputBatch);
                }
            }
        }

        public double Evaluate(double[][] inputBatch, double[][] outputBatch)
        {
            var s = 0.0;
            for (var i = 0; i < inputBatch.Length; i++)
            {
                s += Math.Pow(_lossFunction.ComputeLoss(Predict(inputBatch[i]), outputBatch[i]), 2.0);
            }
            return Math.Sqrt(s);
        }

        public static NeuralNetworkBuilder Builder()
        {
            return new NeuralNetworkBuilder();
        }

        public class NeuralNetworkBuilder
        {
            private readonly NeuralNetwork _instance;

            internal NeuralNetworkBuilder()
            {
                _instance = new NeuralNetwork();
            }

            public NeuralNetworkBuilder InputLayer(int units)
            {
                _instance._inputLayer = new InputLayer(units);
                return this;
            }

            public NeuralNetworkBuilder HiddenLayer(int units, IActivationFunction activationFunction)
            {
                _instance._layers.Add(new HiddenLayer(units) {ActivationFunction = activationFunction});
                return this;
            }

            public NeuralNetworkBuilder OutputLayer(int units, IActivationFunction activationFunction)
            {
                _instance._layers.Add(new OutputLayer(units) {ActivationFunction = activationFunction});
                return this;
            }

            public NeuralNetworkBuilder LossFunction(ILossFunction lossFunction)
            {
                _instance._lossFunction = lossFunction;
                return this;
            }

            public NeuralNetwork Build()
            {
                if (_instance._inputLayer == null)
                {
                    throw new Exception("InputLayer not provided");
                }

                if (_instance.OutputLayer == null)
                {
                    throw new Exception("OutputLayer not provided");
                }

                if (_instance._lossFunction == null)
                {
                    throw new Exception("LossFunction not provided");
                }

                return _instance;
            }
        }
    }
}