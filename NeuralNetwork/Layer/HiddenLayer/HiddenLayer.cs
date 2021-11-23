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
                neuron.Correct(learningRate, PreviousLayer);
            }
        }

        public void Initialize()
        {
            for (var i = 0; i < Weights.Length; i++)
            {
                Neurons[i] = new Neuron.Neuron(Weights[i], ActivationFunction);
            }
        }

        public void ComputeDelta()
        {
            for (var i = 0; i < Neurons.Length; i++)
            {
                var computedDelta = 0.0;
                for (var j = 0; j < NextLayer.Neurons.Length; j++)
                {
                    computedDelta += NextLayer.Neurons[j].Delta * NextLayer.Neurons[j].Weights[i];
                }
                Neurons[i].Delta = computedDelta * ActivationFunction.Derivative(Neurons[i].NeuronValue);
            }
        }
    }
}