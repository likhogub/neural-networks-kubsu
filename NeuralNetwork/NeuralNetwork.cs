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
        
        public NeuralNetwork()
        {
        }

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
            foreach (var layerIndex in Enumerable.Range(1, _layers.Count - 1))
            {
                _layers[layerIndex].Weights = weightsInitializer.Initialize(
                    _layers[layerIndex - 1].Neurons.Length,
                    _layers[layerIndex].Neurons.Length,
                    layerIndex + 1
                );
            }
            foreach (var layerIndex in Enumerable.Range(0, _layers.Count - 1))
            {
                _layers[layerIndex].NextLayer = _layers[layerIndex + 1];
            }
            foreach (var layerIndex in Enumerable.Range(1, _layers.Count - 1))
            {
                _layers[layerIndex].PreviousLayer = _layers[layerIndex - 1];
            }
            foreach (var layerIndex in Enumerable.Range(0, _layers.Count))
            {
                _layers[layerIndex].Initialize();
            }
        }

        public void CorrectWeights()
        {
            foreach (var layer in _layers)
            {
                layer.CorrectWeights();                
            }
        }
        
        public void ComputeDelta(double[] data, double learningRate)
        {
            OutputLayer.ComputeDelta(data);
            for (int i = _layers.Count - 2; i >= 0; i--)
            {
                _layers[i].ComputeDelta(learningRate);
            }

            // var s = "";
            // foreach (var layer in _layers)
            // {
            //     foreach (var neuron in layer.Neurons)
            //     {
            //         s += neuron.Delta + "\n";
            //     }
            //
            //     s += "\n";
            // }
            //
            // FormMain.LabelNeurons.Text = s;
        }
        
        public void Fit(double[][] inputBatch, double[][] outputBatch, int epochs, double learningRate)
        {
            foreach (var epoch in Enumerable.Range(0, epochs))
            {
                foreach (var i in Enumerable.Range(0, inputBatch.Length))
                {
                    ComputeDelta(inputBatch[i], learningRate);
                    CorrectWeights();
                }

                if (epoch % 1 == 0)
                {
                    FormMain.LabelNeurons.Text = "" + epoch + " " + Evaluate(inputBatch, outputBatch);
                }
            }
        }

        public double Evaluate(double[][] inputBatch, double[][] outputBatch)
        {
            var s = 0.0;
            foreach (var i in Enumerable.Range(0, inputBatch.Length))
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
                _instance._layers.Add(new HiddenLayer(units){ActivationFunction = activationFunction});
                return this;
            }

            public NeuralNetworkBuilder OutputLayer(int units, IActivationFunction activationFunction)
            {
                _instance._layers.Add(new OutputLayer(units){ActivationFunction = activationFunction});
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