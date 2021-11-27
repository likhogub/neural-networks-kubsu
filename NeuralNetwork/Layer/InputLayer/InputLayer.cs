namespace neural_networks_kubsu.NeuralNetwork.Layer.InputLayer
{
    public class InputLayer : AbstractLayer, IInputLayer
    {
        public InputLayer(int units) : base(units)
        {
            for (var i = 0; i < units; i++)
            {
                Neurons[i] = new Neuron.Neuron(null);
            }
        }

        public void Feed(double[] inputData)
        {
            for (var i = 0; i < inputData.Length; i++)
            {
                
                Neurons[i].ActivationValue = inputData[i];
            }
        }
    }
}