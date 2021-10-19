using System.Linq;
using neural_networks_kubsu.NeuralNetwork.Neuron.InputNeuron;

namespace neural_networks_kubsu.NeuralNetwork.Layer.InputLayer
{
    public class InputLayer : AbstractLayer, IInputLayer
    {
        public InputLayer(int units) : base(units)
        {
            foreach (var i in Enumerable.Range(0, units))
            {
                Neurons[i] = new InputNeuron();
            }
        }

        public void Feed(double[] inputData)
        {
            foreach (var i in Enumerable.Range(0, inputData.Length))
            {
                ((InputNeuron) Neurons[i]).ActivationValue = inputData[i];
            }
        }
    }
}