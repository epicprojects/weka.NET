using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestWeka
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        const int percentSplit = 66;
        public static string classifyTest()
        {
            try
            {


                String result = "";

                weka.core.Instances insts = new weka.core.Instances(new java.io.FileReader("C:\\Program Files\\Weka-3-7\\data\\iris.arff"));
                insts.setClassIndex(insts.numAttributes() - 1);

                weka.classifiers.Classifier cl = new weka.classifiers.trees.J48();
              //  Console.WriteLine("Performing " + percentSplit + "% split evaluation.");
                result += "Performing " + percentSplit + "% split evaluation.\n";
                //randomize the order of the instances in the dataset.
                weka.filters.Filter myRandom = new weka.filters.unsupervised.instance.Randomize();
                myRandom.setInputFormat(insts);
                insts = weka.filters.Filter.useFilter(insts, myRandom);

                int trainSize = insts.numInstances() * percentSplit / 100;
                int testSize = insts.numInstances() - trainSize;
                weka.core.Instances train = new weka.core.Instances(insts, 0, trainSize);

                cl.buildClassifier(train);
                int numCorrect = 0;
                for (int i = trainSize; i < insts.numInstances(); i++)
                {
                    weka.core.Instance currentInst = insts.instance(i);
                    double predictedClass = cl.classifyInstance(currentInst);
                    if (predictedClass == insts.instance(i).classValue())
                        numCorrect++;
                }
                //Console.WriteLine(numCorrect + " out of " + testSize + " correct (" + (double)((double)numCorrect / (double)testSize * 100.0) + "%)");
                result += (numCorrect + " out of " + testSize + " correct (" + (double)((double)numCorrect / (double)testSize * 100.0) + "%)");
                
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ex.Message;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
           MessageBox.Show(classifyTest());
        }
        

    }
}
