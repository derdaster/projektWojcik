using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Szeregowanie_zadań_na_wielu_procesorach
{
    public partial class Form1 : Form
    {
        Computer c = new Computer();
        Computer c2 = new Computer();
        Computer c3 = new Computer();
        Computer c4 = new Computer();
        Computer c5 = new Computer();

        public Form1()
        {
            InitializeComponent();
        }

        private void testAlgorithms_Click(object sender, EventArgs e)
        {
            testLabel.Size = new Size(200, 200);
            Tester.performSimpleAlghoritmTest(c, 1);
            Tester.performSimpleAlghoritmTest(c2, 2);
            Tester.performSimpleAlghoritmTest(c3, 3);
            Tester.performSimpleAlghoritmTest(c4, 4);
            Tester.performSimpleAlghoritmTest(c5, 5);

            string displayedText = Tester.getPerformedTestData(c, "FCFS");
            displayedText += Tester.getPerformedTestData(c2, "SJF");
            displayedText += Tester.getPerformedTestData(c3, "Priority");
            displayedText += Tester.getPerformedTestData(c4, "Randomized");
            displayedText += Tester.getPerformedTestData(c5, "BruteForcePowa");

            Console.Write(displayedText);
            testLabel.Text = displayedText;
        }
    }
}
