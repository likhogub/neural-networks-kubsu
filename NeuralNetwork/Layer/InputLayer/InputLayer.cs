using neural_networks_kubsu.NeuralNetwork.Neuron.InputNeuron;

namespace neural_networks_kubsu.NeuralNetwork.Layer.InputLayer
{
    public class InputLayer : AbstractLayer, IInputLayer
    {
        public InputLayer(int units) : base(units)
        {
            for (var i = 0; i < units; i++)
            {
                Neurons[i] = new InputNeuron();
            }
        }

        public void Feed(double[] inputData)
        {
            for (var i = 0; i < inputData.Length; i++)
            {
                ((InputNeuron) Neurons[i]).ActivationValue = inputData[i];
            }
        }
    }
}