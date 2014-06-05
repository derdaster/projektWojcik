using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyChart
{
    public partial class Form1 : Form
    {
        List<ProcessorTask> MainList = new List<ProcessorTask>();
        int j = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<ProcessorTask> newList = new List<ProcessorTask>();
            for (int i = 0; i < 3; i++)
            {
                newList.Add(new ProcessorTask()
                {
                    endTime = 0,
                    priority = Priority.Medium,
                    startTime = i,
                    time = j,
                    timeLeft = 0
                }
                );
                j++;
            }

            this.MainList.AddRange(newList);

            this.myChart1.SetColumnWith(40);
            this.myChart1.Update(MainList);
            this.counterTask.Text = MainList.Count.ToString();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MainList.Count > 0)
            {
                this.MainList.RemoveAt(0);
            }
            this.myChart1.Update(MainList);
            this.counterTask.Text = MainList.Count.ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

    }
}
