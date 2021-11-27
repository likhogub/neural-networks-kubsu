using System.Windows.Forms.DataVisualization.Charting;

namespace neural_networks_kubsu.NeuralNetwork.CallbackFunction.ChartCallbackFunction
{
    public class ChartCallbackFunction : ICallbackFunction
    {
        private readonly Series _series = new(); 
        private readonly int _onEachEpoch; 
        
        public ChartCallbackFunction(int onEachEpoch, Chart chart)
        {
            _onEachEpoch = onEachEpoch;
            chart.Series.Add(_series);
            _series.ChartType = SeriesChartType.Line;
            _series.IsVisibleInLegend = false;
        }
        public void Invoke(NeuralNetwork neuralNetwork)
        {
            if (neuralNetwork.Epoch == 0)
            {
                _series.Points.Clear();
            }

            if (neuralNetwork.Epoch % _onEachEpoch == 0)
            {
                _series.Points.AddXY(neuralNetwork.Epoch, neuralNetwork.Evaluate());
            }
        }
    }
}