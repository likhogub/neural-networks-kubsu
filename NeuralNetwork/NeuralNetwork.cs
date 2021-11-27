using System;
using System.Collections.Generic;
using System.Linq;
using neural_networks_kubsu.NeuralNetwork.ActivationFunction;
using neural_networks_kubsu.NeuralNetwork.CallbackFunction;
using neural_networks_kubsu.NeuralNetwork.Evaluator;
using neural_networks_kubsu.NeuralNetwork.Layer.HiddenLayer;
using neural_networks_kubsu.NeuralNetwork.Layer.InputLayer;
using neural_networks_kubsu.NeuralNetwork.Layer.OutputLayer;
using neural_networks_kubsu.NeuralNetwork.WeightsInitializer;

namespace neural_networks_kubsu.NeuralNetwork
{
    public class NeuralNetwork
    {
        private IInputLayer _inputLayer;
        private readonly List<IHiddenLayer> _layers = new();
        private IOutputLayer OutputLayer => (IOutputLayer) _layers.Last();
        private IEvaluator _evaluator;
        private readonly LinkedList<ICallbackFunction> _callbackFunctions = new();
        public int Epoch { get; private set; } = 0;
        
        
        public double[] Predict(double[] inputData)
        {
            _inputLayer.Feed(inputData);
            foreach (var layer in _layers)
            {
                layer.ComputeNeurons();
            }
            return OutputLayer.Result;
        }

        private void ConnectLayers()
        {
            _layers[0].PreviousLayer = _inputLayer;
            
            for (var layerIndex = 1; layerIndex <= _layers.Count - 1; layerIndex++)
            {
                _layers[layerIndex].PreviousLayer = _layers[layerIndex - 1];
            }
            
            for (var layerIndex = 0; layerIndex < _layers.Count - 1; layerIndex++)
            {
                _layers[layerIndex].NextLayer = _layers[layerIndex + 1];
            }
        }
        
        public void InitializeWeights(IWeightsInitializer weightsInitializer)
        {
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

            foreach (var layer in _layers)
            {
                layer.InitializeWeights();
            }

            Epoch = 0;
        }

        private void CorrectWeights(double learningRate)
        {
            foreach (var layer in _layers)
            {
                layer.CorrectWeights(learningRate);
            }
        }

        private void ComputeDelta(double inertia, double[] data)
        {
            OutputLayer.ComputeDelta(data);
            for (var i = _layers.Count - 2; i >= 0; i--)
            {
                _layers[i].ComputeDelta(inertia);
            }
        }

        public double Evaluate() => _evaluator.Evaluate(this);

        public void Fit(double[][] inputBatch, double[][] outputBatch, int epochs, double learningRate, double inertia)
        {
            var endEpoch = Epoch + epochs;
            for (; Epoch < endEpoch; Epoch++)
            {
                for (var i = 0; i < inputBatch.Length; i++)
                {
                    Predict(inputBatch[i]);
                    ComputeDelta(inertia, outputBatch[i]);
                    CorrectWeights(learningRate);
                }

                foreach (var callbackFunction in _callbackFunctions)
                {
                    callbackFunction.Invoke(this);
                }
            }
        }

        public double[][][] ExportWeights()
        {
            var array = new double[_layers.Count][][];
            for (var i = 0; i < _layers.Count; i++)
            {
                array[i] = _layers[i].Weights;
            }
            return array;
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

            public NeuralNetworkBuilder Evaluator(IEvaluator evaluator)
            {
                _instance._evaluator = evaluator;
                return this;
            }

            public NeuralNetworkBuilder CallbackFunction(ICallbackFunction callbackFunction)
            {
                _instance._callbackFunctions.AddLast(callbackFunction);
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

                if (_instance._evaluator == null)
                {
                    throw new Exception("Evaluator not provided");
                }

                _instance.ConnectLayers();
                
                return _instance;
            }
        }
    }
}