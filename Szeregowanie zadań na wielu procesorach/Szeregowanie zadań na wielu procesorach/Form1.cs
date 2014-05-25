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
        

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new Form();
            frm.Name = "Hello";
            var lb = new Label();
            lb.Text = "Hello World!!!";
            frm.Controls.Add(lb);
            frm.ShowDialog();


            Task task = new Task(5, 1);
            Processor proc = new Processor();
            c.addProcessor(proc);
            proc = new Processor();
            c.addProcessor(proc);

            c.addTask(task);
            task = new Task(4, 1);
            c.addTask(task);
            task = new Task(4, 1);
            c.addTask(task);
            task = new Task(2, 1);
            c.addTask(task);
            task = new Task(7, 1);
            c.addTask(task);
            task = new Task(1, 1);
            c.addTask(task);
            task = new Task(3, 1);
            c.addTask(task);
            c.chooseAlgoritm(1);
            c.symulate();
        }
    }
}
