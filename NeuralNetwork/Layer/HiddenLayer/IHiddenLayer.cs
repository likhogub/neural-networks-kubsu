﻿namespace neural_networks_kubsu.NeuralNetwork.Layer.HiddenLayer
{
    public interface IHiddenLayer : ILayer
    {
        ILayer PreviousLayer { get; set; }
        ILayer NextLayer { get; set; }
        double[][] Weights { get; set; }
        void ComputeNeurons();
        void CorrectWeights();
        void Initialize();
        public void ComputeDelta(double learningRate);
    }
}