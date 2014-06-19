using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyChart
{

    public partial class Form1 : Form
    {
        private BackgroundWorker backgroundWorker1;
        private Thread oThread;
        private TextBox textBox1;
        delegate void SetTextCallback(string text);
        Szeregowanie_zadań_na_wielu_procesorach.Computer c = new Szeregowanie_zadań_na_wielu_procesorach.Computer();
        List<List<Szeregowanie_zadań_na_wielu_procesorach.Task>> lista = new List<List<Szeregowanie_zadań_na_wielu_procesorach.Task>>();
        List<Szeregowanie_zadań_na_wielu_procesorach.Task> MainList = new List<Szeregowanie_zadań_na_wielu_procesorach.Task>();
        List<Szeregowanie_zadań_na_wielu_procesorach.Task> EndList = new List<Szeregowanie_zadań_na_wielu_procesorach.Task>();
        List<Szeregowanie_zadań_na_wielu_procesorach.Task> Procesor1List = new List<Szeregowanie_zadań_na_wielu_procesorach.Task>();
        List<Szeregowanie_zadań_na_wielu_procesorach.Task> Procesor2List = new List<Szeregowanie_zadań_na_wielu_procesorach.Task>();
        int j = 1;
        Szeregowanie_zadań_na_wielu_procesorach.Task tempTask;
        public Form1()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(240, 20);
            this.textBox1.TabIndex = 0;
            
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(240, 20);
            this.textBox1.TabIndex = 0;

            c.wczytaj();
            c.chooseAlgoritm(1);
            c.symulate();
            //c.symulateInTime();
            
            List<Szeregowanie_zadań_na_wielu_procesorach.Task> newList = new List<Szeregowanie_zadań_na_wielu_procesorach.Task>();
            
           /* for (int i = 0; i < 1; i++)
            {
                Szeregowanie_zadań_na_wielu_procesorach.Task t = new Szeregowanie_zadań_na_wielu_procesorach.Task(j, Szeregowanie_zadań_na_wielu_procesorach.Priority.Medium);
                t.setTimeLeft(3);
                t.time = 5;
                newList.Add(t);
                t = new Szeregowanie_zadań_na_wielu_procesorach.Task(j, Szeregowanie_zadań_na_wielu_procesorach.Priority.Medium);
                t.setTimeLeft(5);
                t.time = 8;
                newList.Add(t);
                j++;
            }*/
            newList = c.getTaskList();
            this.MainList.AddRange(newList);

            this.panel1.SetColumnWith(40);
            this.panel1.Update(MainList);
            this.counterTask.Text = MainList.Count.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
           /*
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker1.RunWorkerAsync();
            while (MainList.Count > 0 || Procesor1List.Count > 0)
            {
                if (tempTask != null && tempTask.getTimeLeft() > 0)
                {
                    tempTask.setTimeLeft(tempTask.getTimeLeft() - 1);
                    Procesor1List.RemoveAt(0);
                    Procesor1List.Add(tempTask);
                }
                else
                    if (MainList.Count > 0)
                    {
                        Procesor1List.Add(this.MainList[0]);
                        tempTask = this.MainList[0];

                        tempTask.setTimeLeft(tempTask.time);

                        this.EndList.Add(tempTask);
                        this.MainList.RemoveAt(0);
                        System.Threading.Thread.Sleep(1000);
                    }
            }*/




            
            int maxEndTime = 0;
            int taskAmount = c.getTaskList().Count;
            //musi wrzucać taski przetwarzane do listy
            //i taski już przetworzone
            //dla każdego tasku po kolei na procesorze zmiejsza czas time left do 0

            //taskList-przechowuje zadania oczekujące
            //List<List<Task>> lista = new List<List<Task>>();//lista zadań aktualnie przetwarzanych na procesorze
            for (int n = 0; n < c.getProcList().Count; n++)
            {
                lista.Add(new List<Szeregowanie_zadań_na_wielu_procesorach.Task>());
            }
            foreach (var processor in c.getProcList())
            {
                if (processor.endTime > maxEndTime)
                    maxEndTime = processor.endTime;
            }
            int i = 0;

            //jak zabieramy task, to wyszukujemy go w tablicy i zabieramy
            //najlepiej posortować tablicę jak w algorytmie, albo żeby algorytm zostawiał ją posortowaną
            List<Szeregowanie_zadań_na_wielu_procesorach.Task> doneTasks = new List<Szeregowanie_zadań_na_wielu_procesorach.Task>();

            while (i < maxEndTime)
            {
                int tasda;
                if (i == 1)
                    tasda = 2;
                int tj = 0;
                foreach (var processor in c.getProcList())
                {

                    List<Szeregowanie_zadań_na_wielu_procesorach.Task> allTasksForSingleProcessor = processor.getTaskList();
                    foreach (var task in allTasksForSingleProcessor)
                    {
                        //sprawdzamy od początku czy zadanie już się skończyło, jeśli nie to czy się zaczyna w aktualnym czasie lub trwa
                        if ((task.getTimeLeft() > 0) && (task.getEndTime() - task.getTimeLeft()) == i)
                        {
                            if (lista[tj].Count > 0)//jeżeli zadanie już jest na liście
                            {
                                task.setTimeLeft(task.getTimeLeft() - 1);
                                if (lista[tj][0].GetId() != task.GetId())//jeżeli przetwarzane zadanie jest inne od aktualnego, zmieniamy listę
                                {
                                    if (lista[tj].Count > 0)
                                        lista[tj].RemoveAt(0);
                                    lista[tj].Add(task);
                                    int iterator = 0;
                                    foreach (var t in c.getTaskList())
                                    {
                                        if (t.GetId() == task.GetId())
                                        {
                                            c.getTaskList().RemoveAt(iterator);
                                            break;
                                        }

                                        iterator++;
                                    }

                                }
                                else
                                {
                                    if (lista[tj].Count > 0)
                                        lista[tj].RemoveAt(0);
                                    lista[tj].Add(task);
                                }
                                if (task.getTimeLeft() == 0)
                                {
                                    doneTasks.Add(task);
                                    allTasksForSingleProcessor.RemoveAt(0);
                                    break;
                                }

                                processor.setTaskList(allTasksForSingleProcessor);
                            }
                            else//kiedy na procku nic nie ma
                            {
                                task.setTimeLeft(task.getTimeLeft() - 1);
                                lista[tj].Add(task);
                                int iterator = 0;
                                foreach (var t in c.getTaskList())
                                {
                                    if (t.GetId() == task.GetId())
                                    {
                                        c.getTaskList().RemoveAt(iterator);
                                        break;
                                    }

                                    iterator++;
                                }
                            }
                            if (task.getTimeLeft() == 0)
                            {
                                doneTasks.Add(task);
                                allTasksForSingleProcessor.RemoveAt(0);
                                break;
                            }
                            else
                                break;
                        }
                        else
                            if (task.getTimeLeft() == 0)
                            {
                                doneTasks.Add(task);
                                allTasksForSingleProcessor.RemoveAt(0);
                                break;
                            }
                            else
                                break;

                    }
                    tj++;

                }
                //System.Threading.Thread.Sleep(2000);
                i++;

                List<Szeregowanie_zadań_na_wielu_procesorach.Processor> allProcessors = c.getProcList();

                string displayedText = "\nnumber of processors: " + allProcessors.Count + "\n";
                int j = 0;
                displayedText += "liczba tasków: " + c.getTaskList().Count.ToString() + "\n";
                c.writeToFile("\nnumber of processors: " + allProcessors.Count + "\n");
                c.writeToFile("liczba tasków: " + c.getTaskList().Count.ToString() + "\n");
                displayedText += "zakończone: " + doneTasks.Count.ToString() + "\n";
                c.writeToFile("zakończone: " + doneTasks.Count.ToString() + "\n");
                displayedText += "sekunda: " + i.ToString() + "\n";
                c.writeToFile("sekunda: " + i.ToString() + "\n");
                displayedText += "przetwarzane: " + (taskAmount - c.getTaskList().Count - doneTasks.Count).ToString() + "\n";
                c.writeToFile("przetwarzane: " + (taskAmount - c.getTaskList().Count - doneTasks.Count).ToString() + "\n");
                foreach (var processor in allProcessors)
                {
                    string tempTaskString = "";
                    displayedText += "processor " + j.ToString() + ": ";
                    tempTaskString += "processor " + j.ToString() + ": ";
                    //writeToFile("processor " + j.ToString() + ": ");

                    List<Szeregowanie_zadań_na_wielu_procesorach.Task> allTasksForSingleProcessor = processor.getTaskList();
                    foreach (var task in allTasksForSingleProcessor)
                    {
                        displayedText += " [" + task.getTime().ToString() + " l" + task.getTimeLeft().ToString() + " s" + task.getStartTime() + "] " + task.getEndTime() + " ";
                        tempTaskString += " [" + task.getTime().ToString() + " l" + task.getTimeLeft().ToString() + " s" + task.getStartTime() + "] " + task.getEndTime() + " ";
                        //writeToFile(" [" + task.getTime().ToString() + " l" + task.getTimeLeft().ToString() + " s" + task.getStartTime() + "] " + task.getEndTime() + " ");
                    }
                    c.writeToFile(tempTaskString);
                    displayedText += processor.endTime;

                    displayedText += " \n";
                    j++;
                }

                Console.Write(displayedText);
                Procesor1List = lista[0];
                Procesor2List = lista[1];
                EndList = doneTasks;
                //Procesor1List = lista[0];
                this.panel1.Update(MainList);
                this.panel2.Update(Procesor1List);
                this.panel3.Update(Procesor2List);
                this.processorChart2.Update(Procesor1List);
                this.processorChart1.Update(Procesor1List);
                if (EndList.Count > 0)
                    this.panel4.Update(EndList);
                this.counterTask.Text = MainList.Count.ToString();
                System.Threading.Thread.Sleep(1000);
            }
           // c.symulateInTime(lista);
            
                /*this.panel1.Update(MainList);
                this.panel2.Update(Procesor1List);
                this.panel3.Update(Procesor1List);
                this.processorChart2.Update(Procesor1List);
                this.processorChart1.Update(Procesor1List);
                if (EndList.Count > 0)
                    this.panel4.Update(EndList);
                this.counterTask.Text = MainList.Count.ToString();
                System.Threading.Thread.Sleep(1000);*/
            
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

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            while (MainList.Count > 0)
            {
                if (tempTask != null && tempTask.getTimeLeft() > 0)
                {
                    tempTask.setTimeLeft(tempTask.getTimeLeft() - 1);
                    Procesor1List.RemoveAt(0);
                    Procesor1List.Add(tempTask);
                }
                else
                    if (MainList.Count > 0)
                    {
                        Procesor1List.Add(this.MainList[0]);
                        tempTask = this.MainList[0];
                        this.MainList.RemoveAt(0);
                    }
                this.panel1.Update(MainList);
                this.panel2.Update(Procesor1List);
                this.panel3.Update(Procesor1List);
                this.panel4.Update(MainList);
                this.counterTask.Text = MainList.Count.ToString();

            }

        }

        private void processorChart2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void processorChart1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
