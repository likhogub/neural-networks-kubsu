using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using neural_networks_kubsu.NeuralNetwork.ActivationFunction.SigmoidActivationFunction;
using neural_networks_kubsu.NeuralNetwork.ActivationFunction.TanhActivationFunction;
using neural_networks_kubsu.NeuralNetwork.LossFunction.EuclideanDistanceLoss;
using neural_networks_kubsu.NeuralNetwork.WeightsInitializer.DefaultWeightsInitializer;
using neural_networks_kubsu.NeuralNetwork.WeightsInitializer.SavedWeightsInitializer;

namespace neural_networks_kubsu
{
    public partial class FormMain : Form
    {
        private readonly double[] _inputArray = new double[15];

        public static Label LabelEpochs;
        public static Label LabelNeurons;
        private NeuralNetwork.NeuralNetwork _nn;

        private readonly double[][] _inputData =
        {
            new[]
            {
                1.0, 1.0, 1.0,
                1.0, 0.0, 1.0,
                1.0, 0.0, 1.0,
                1.0, 0.0, 1.0,
                1.0, 1.0, 1.0
            },
            new[]
            {
                0.0, 0.0, 1.0,
                0.0, 0.0, 1.0,
                0.0, 0.0, 1.0,
                0.0, 0.0, 1.0,
                0.0, 0.0, 1.0
            },
            new[]
            {
                1.0, 1.0, 1.0,
                0.0, 0.0, 1.0,
                1.0, 1.0, 1.0,
                1.0, 0.0, 0.0,
                1.0, 1.0, 1.0
            },
            new[]
            {
                1.0, 1.0, 1.0,
                0.0, 0.0, 1.0,
                1.0, 1.0, 1.0,
                0.0, 0.0, 1.0,
                1.0, 1.0, 1.0
            },
            new[]
            {
                1.0, 0.0, 1.0,
                1.0, 0.0, 1.0,
                1.0, 1.0, 1.0,
                0.0, 0.0, 1.0,
                0.0, 0.0, 1.0
            },
            new[]
            {
                1.0, 1.0, 1.0,
                1.0, 0.0, 0.0,
                1.0, 1.0, 1.0,
                0.0, 0.0, 1.0,
                1.0, 1.0, 1.0
            },
            new[]
            {
                1.0, 1.0, 1.0,
                1.0, 0.0, 0.0,
                1.0, 1.0, 1.0,
                1.0, 0.0, 1.0,
                1.0, 1.0, 1.0
            },
            new[]
            {
                1.0, 1.0, 1.0,
                0.0, 0.0, 1.0,
                0.0, 1.0, 1.0,
                0.0, 0.0, 1.0,
                0.0, 0.0, 1.0
            },
            new[]
            {
                1.0, 1.0, 1.0,
                1.0, 0.0, 1.0,
                1.0, 1.0, 1.0,
                1.0, 0.0, 1.0,
                1.0, 1.0, 1.0
            },
            new[]
            {
                1.0, 1.0, 1.0,
                1.0, 0.0, 1.0,
                1.0, 1.0, 1.0,
                0.0, 0.0, 1.0,
                1.0, 1.0, 1.0
            },
            new[]
            {
                0.0, 0.0, 0.0,
                0.0, 0.0, 0.0,
                0.0, 0.0, 0.0,
                0.0, 0.0, 0.0,
                0.0, 0.0, 0.0
            }
        };

        private readonly double[][] _outputData =
        {
            new[] {1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0},
            new[] {0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0},
            new[] {0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0},
            new[] {0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0},
            new[] {0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0},
            new[] {0.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0},
            new[] {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0},
            new[] {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0},
            new[] {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0},
            new[] {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0},
            new[] {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0},
        };


        public FormMain()
        {
            InitializeComponent();
            LabelNeurons = labelEvaluationValue;
            LabelEpochs = epochLabel;
            CreateNeuralNetwork();
            Randomize();
        }

        private void Fit()
        {
            var epochs = decimal.ToInt32(epochsUpDown.Value);
            _nn.Fit(_inputData, _outputData, epochs, 0.01);
            Evaluate();
            Predict();
        }

        private void CreateNeuralNetwork()
        {
            _nn = NeuralNetwork.NeuralNetwork.Builder()
                .InputLayer(15)
                .HiddenLayer(76, new SigmoidActivationFunction())
                .HiddenLayer(36, new SigmoidActivationFunction())
                .OutputLayer(10, new SigmoidActivationFunction())
                .LossFunction(new EuclideanDistanceLoss())
                .Build();
        }

        private void Evaluate()
        {
            labelEvaluationValue.Text = "Loss: " + _nn.Evaluate(_inputData, _outputData);
        }

        private void Predict()
        {
            var prediction = _nn.Predict(_inputArray);
            var s = "Prediction:\n";
            for (var i = 0; i < 10; i++)
            {
                s += i + ": " + Math.Round(prediction[i], 4) + "\n";
            }
            labelStatus.Text = s;
        }
        
        private void Export()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            var filename = saveFileDialog1.FileName;
            var serializer = new XmlSerializer(typeof(double[][][]));
            using var fs = new FileStream(filename, FileMode.Create);
            serializer.Serialize(fs, _nn.ExportWeights());
            fs.Close();
        }

        private void Import()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            var filename = openFileDialog1.FileName;
            _nn.Initialize(new SavedWeightsInitializer(filename));
            Predict();
            Evaluate();
        }

        private void Randomize()
        {
            _nn.Initialize(new DefaultWeightsInitializer());
            Predict();
            Evaluate();
        }
        
        private void btn_click(Button btn)
        {
            var buttonId = btn.TabIndex;
            btn.BackColor = _inputArray[buttonId] == 0.0 ? Color.Black : Color.White;
            _inputArray[buttonId] = Math.Abs(1.0 - _inputArray[buttonId]);
            Predict();
        }

        private void button0_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            Export();
        }

        private void importBtn_Click(object sender, EventArgs e)
        {
            Import();
        }

        private void randomizeBtn_Click(object sender, EventArgs e)
        {
            Randomize();
        }

        private void fitBtn_Click(object sender, EventArgs e)
        {
            var thread2 = new Thread(Fit);
            thread2.Start();
        }
    }
}