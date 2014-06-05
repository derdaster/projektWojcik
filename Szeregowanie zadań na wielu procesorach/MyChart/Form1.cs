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
        List<Szeregowanie_zadań_na_wielu_procesorach.Task> MainList = new List<Szeregowanie_zadań_na_wielu_procesorach.Task>();
        List<Szeregowanie_zadań_na_wielu_procesorach.Task> Procesor1List = new List<Szeregowanie_zadań_na_wielu_procesorach.Task>();
        int j = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Szeregowanie_zadań_na_wielu_procesorach.Task> newList = new List<Szeregowanie_zadań_na_wielu_procesorach.Task>();
            for (int i = 0; i < 3; i++)
            {
                newList.Add(new Szeregowanie_zadań_na_wielu_procesorach.Task(j,Szeregowanie_zadań_na_wielu_procesorach.Priority.Medium));
                
                j++;
            }

            this.MainList.AddRange(newList);

            this.panel1.SetColumnWith(40);
            this.panel1.Update(MainList);
            this.counterTask.Text = MainList.Count.ToString();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            if (MainList.Count > 0)
            {
                Procesor1List.Add(this.MainList[0]);
                this.MainList.RemoveAt(0);
            }
            this.panel1.Update(MainList);
            this.panel2.Update(Procesor1List);
            this.counterTask.Text = MainList.Count.ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tasksChart1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

    }
}
