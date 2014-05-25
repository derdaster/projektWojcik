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
        
        public Form1()
        {
            InitializeComponent();
        }

        private void performSimpleAlghoritmTest (Computer c, int alghoritm)
        {
            c.addProcessor(new Processor());
            c.addProcessor(new Processor());
            c.addProcessor(new Processor());

            c.addTask(new Task(5, 1));
            c.addTask(new Task(4, 2));
            c.addTask(new Task(4, 1));
            c.addTask(new Task(2, 3));
            c.addTask(new Task(7, 1));
            c.addTask(new Task(1, 5));
            c.addTask(new Task(3, 1));
            c.addTask(new Task(3, 2));
            c.addTask(new Task(3, 1));
            c.addTask(new Task(3, 9));
            c.chooseAlgoritm(alghoritm);
            c.symulate();
        }

        private string getPerformedTestData(Computer c, string name)
        {
            List<Processor> allProcessors = c.getProcList();

            string displayedText = name + "\nnumber of processors: " + allProcessors.Count + "\n";
            int i = 0;

            foreach (var processor in allProcessors)
            {
                displayedText += "processor " + i.ToString() + ": ";

                List<Task> allTasksForSingleProcessor = processor.getTaskList();
                foreach (var task in allTasksForSingleProcessor)
                {
                    displayedText += " [" + task.getTime().ToString() + " p" + task.priority.ToString() + "] ";
                }
                displayedText += "\n";
                i++;
            }
            return displayedText + "\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            testLabel.Size = new Size(200, 200);
            performSimpleAlghoritmTest(c, 1);
            performSimpleAlghoritmTest(c2, 2);
            performSimpleAlghoritmTest(c3, 3);
            performSimpleAlghoritmTest(c4, 4);

            string displayedText = getPerformedTestData(c,"FCFS");
            displayedText += getPerformedTestData(c2 , "SJF");
            displayedText += getPerformedTestData(c3, "Priority");
            displayedText += getPerformedTestData(c4, "Randomized");

            Console.Write(displayedText);
            testLabel.Text = displayedText;
        }
    }
}
