using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using neural_networks_kubsu.NeuralNetwork.ActivationFunction.IdentityActivationFunction;
using neural_networks_kubsu.NeuralNetwork.ActivationFunction.LinearActivationFunction;
using neural_networks_kubsu.NeuralNetwork.ActivationFunction.SigmoidActivationFunction;
using neural_networks_kubsu.NeuralNetwork.ActivationFunction.TanhActivationFunction;
using neural_networks_kubsu.NeuralNetwork.LossFunction.EuclideanDistanceLoss;
using neural_networks_kubsu.NeuralNetwork.WeightsInitializer.DefaultWeightsInitializer;

namespace neural_networks_kubsu
{
    public partial class FormMain : Form
    {
        private double[] _inputArray = new double[15];

        public static Label Label;
        public static Label LabelNeurons;
        private NeuralNetwork.NeuralNetwork _nn;

        private double[][] _inputData = new[]
        {
            new double[]
            {
                1.0, 1.0, 1.0,
                1.0, 0.0, 1.0,
                1.0, 0.0, 1.0,
                1.0, 0.0, 1.0,
                1.0, 1.0, 1.0
            },
            new double[]
            {
                0.0, 0.0, 1.0,
                0.0, 0.0, 1.0,
                0.0, 0.0, 1.0,
                0.0, 0.0, 1.0,
                0.0, 0.0, 1.0
            },
            new double[]
            {
                1.0, 1.0, 1.0,
                0.0, 0.0, 1.0,
                1.0, 1.0, 1.0,
                1.0, 0.0, 0.0,
                1.0, 1.0, 1.0
            },
            new double[]
            {
                1.0, 1.0, 1.0,
                0.0, 0.0, 1.0,
                1.0, 1.0, 1.0,
                0.0, 0.0, 1.0,
                1.0, 1.0, 1.0
            },
            new double[]
            {
                1.0, 0.0, 1.0,
                1.0, 0.0, 1.0,
                1.0, 1.0, 1.0,
                0.0, 0.0, 1.0,
                0.0, 0.0, 1.0
            },
            new double[]
            {
                1.0, 1.0, 1.0,
                1.0, 0.0, 0.0,
                1.0, 1.0, 1.0,
                0.0, 0.0, 1.0,
                1.0, 1.0, 1.0
            },
            new double[]
            {
                1.0, 1.0, 1.0,
                1.0, 0.0, 0.0,
                1.0, 1.0, 1.0,
                1.0, 0.0, 1.0,
                1.0, 1.0, 1.0
            },
            new double[]
            {
                1.0, 1.0, 1.0,
                0.0, 0.0, 1.0,
                0.0, 1.0, 1.0,
                0.0, 0.0, 1.0,
                0.0, 0.0, 1.0
            },
            new double[]
            {
                1.0, 1.0, 1.0,
                1.0, 0.0, 1.0,
                1.0, 1.0, 1.0,
                1.0, 0.0, 1.0,
                1.0, 1.0, 1.0
            },
            new double[]
            {
                1.0, 1.0, 1.0,
                1.0, 0.0, 1.0,
                1.0, 1.0, 1.0,
                0.0, 0.0, 1.0,
                1.0, 1.0, 1.0
            },
            new double[]
            {
                0.0, 0.0, 0.0,
                0.0, 0.0, 0.0,
                0.0, 0.0, 0.0,
                0.0, 0.0, 0.0,
                0.0, 0.0, 0.0
            }
        };

        private double[][] _outputData = new[]
        {
            new double[] {1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0},
            new double[] {0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0},
            new double[] {0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0},
            new double[] {0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0},
            new double[] {0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0},
            new double[] {0.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0},
            new double[] {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0},
            new double[] {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0},
            new double[] {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0},
            new double[] {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0},
            new double[] {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0},
        };


        public FormMain()
        {
            InitializeComponent();
            Label = labelStatus;
            LabelNeurons = labelNeuronValue;
            run();
        }

        private void run()
        {
            regen();
        }

        private void btn_click(Button btn)
        {
            var buttonId = btn.TabIndex;
            btn.BackColor = _inputArray[buttonId] == 0.0 ? Color.Black : Color.White;
            _inputArray[buttonId] = Math.Abs(1.0 - _inputArray[buttonId]);
            Predict();
        }

        public void Evaluate()
        {
            labelNeuronValue.Text = "" + _nn.Evaluate(_inputData, _outputData);
        }

        private void Predict()
        {
            var prediction = _nn.Predict(_inputArray);
            var s = "Prediction:\n";
            foreach (var result in prediction)
            {
                s += result + "\n";
            }

            labelStatus.Text = s;
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

        private void button15_Click(object sender, EventArgs e)
        {
            var thread2 = new Thread(Fit);
            thread2.Start();
        }

        private void Fit()
        {
            foreach (var VARIABLE in Enumerable.Range(0, 100000))
            {
                label1.Text = "" + VARIABLE;
                _nn.Fit(_inputData, _outputData, 100, 0.0, 0.01);
                Predict();
                // Evaluate();
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            regen();
        }

        private void regen()
        {
            _nn = NeuralNetwork.NeuralNetwork.Builder()
                .InputLayer(15)
                .HiddenLayer(2500, new SigmoidActivationFunction())
                .OutputLayer(10, new SigmoidActivationFunction())
                .LossFunction(new EuclideanDistanceLoss())
                .Build();
            _nn.Initialize(new DefaultWeightsInitializer());
            Predict();
            Evaluate();
        }
    }
}