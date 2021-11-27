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
            ActivationFunction.ActivateLayer(this);
        }

        public void CorrectWeights(double learningRate)
        {
            foreach (var neuron in Neurons)
            {
                neuron.Correct(learningRate, PreviousLayer);
            }
        }

        public void InitializeWeights()
        {
            for (var i = 0; i < Weights.Length; i++)
            {
                Neurons[i] = new Neuron.Neuron(Weights[i]);
            }
        }

        public void ComputeDelta(double inertia)
        {
            for (var i = 0; i < Neurons.Length; i++)
            {
                var computedDelta = 0.0;
                foreach (var nextLayerNeurons in NextLayer.Neurons)
                {
                    computedDelta += nextLayerNeurons.Delta * nextLayerNeurons.Weights[i];
                }

                Neurons[i].Delta = inertia * Neurons[i].Delta + (1 - inertia) * computedDelta * Neurons[i].DerivativeValue;
            }
        }
    }
}