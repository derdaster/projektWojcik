using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Szeregowanie_zadań_na_wielu_procesorach
{
    public class Computer : Subject
    {
        List<Task> taskList = new List<Task>();
        List<Observer> observers = new List<Observer>();
        List<Processor> procList = new List<Processor>();
        schedulerAlgorithm algorithm;

        public void addTask(Task task)
        {
            taskList.Add(task);
        }

        public void addProcessor(Processor processor)
        {
            procList.Add(processor);
        }

        public void chooseAlgoritm(int algoritm)
        {
            switch (algoritm)
            {
                case 1:
                    algorithm = new algorithmFCFS();
                    break;
                case 2:
                    algorithm = new algorithmSJF();
                    break;
                case 3:
                    algorithm = new algorithmPriority();
                    break;
                case 4:
                    algorithm = new Randomized();
                    break;
                case 5:
                    algorithm = new algorithmBruteForce();
                    break;
                default:
                    algorithm = new algorithmFCFS();
                    break;
            }
        }
        public void symulateInTime()
        {
            
            clearFile();
            int maxEndTime = 0;
            int taskAmount = taskList.Count;
            //musi wrzucać taski przetwarzane do listy
            //i taski już przetworzone
            //dla każdego tasku po kolei na procesorze zmiejsza czas time left do 0

            //taskList-przechowuje zadania oczekujące
            List<List<Task>> lista = new List<List<Task>>();//lista zadań aktualnie przetwarzanych na procesorze
            for (int n = 0; n < procList.Count; n++)
            {
                lista.Add(new List<Task>());
            }
            foreach (var processor in procList)
            {
                if (processor.endTime > maxEndTime)
                    maxEndTime = processor.endTime;
            }
            int i = 0;

            //jak zabieramy task, to wyszukujemy go w tablicy i zabieramy
            //najlepiej posortować tablicę jak w algorytmie, albo żeby algorytm zostawiał ją posortowaną
            List<Task> doneTasks = new List<Task>();

            while (i < maxEndTime)
            {
                int tasda;
                if (i == 1)
                    tasda = 2;
                int tj = 0;
                foreach (var processor in procList)
                {

                    List<Task> allTasksForSingleProcessor = processor.getTaskList();
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
                                    foreach (var t in taskList)
                                    {
                                        if (t.GetId() == task.GetId())
                                        {
                                            taskList.RemoveAt(iterator);
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
                                foreach (var t in taskList)
                                {
                                    if (t.GetId() == task.GetId())
                                    {
                                        taskList.RemoveAt(iterator);
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

                List<Processor> allProcessors = procList;

                string displayedText = "\nnumber of processors: " + allProcessors.Count + "\n";
                int j = 0;
                displayedText += "liczba tasków: " + taskList.Count.ToString() + "\n";
                writeToFile("\nnumber of processors: " + allProcessors.Count + "\n");
                writeToFile("liczba tasków: " + taskList.Count.ToString() + "\n");
                displayedText += "zakończone: " + doneTasks.Count.ToString() + "\n";
                writeToFile("zakończone: " + doneTasks.Count.ToString() + "\n");
                displayedText += "sekunda: " + i.ToString() + "\n";
                writeToFile("sekunda: " + i.ToString() + "\n");
                displayedText += "przetwarzane: " + (taskAmount - taskList.Count - doneTasks.Count).ToString() + "\n";
                writeToFile("przetwarzane: " + (taskAmount - taskList.Count - doneTasks.Count).ToString() + "\n");
                foreach (var processor in allProcessors)
                {
                    string tempTaskString = "";
                    displayedText += "processor " + j.ToString() + ": ";
                    tempTaskString += "processor " + j.ToString() + ": ";
                    //writeToFile("processor " + j.ToString() + ": ");

                    List<Task> allTasksForSingleProcessor = processor.getTaskList();
                    foreach (var task in allTasksForSingleProcessor)
                    {
                        displayedText += " [" + task.getTime().ToString() + " l" + task.getTimeLeft().ToString() + " s" + task.getStartTime() + "] " + task.getEndTime() + " ";
                        tempTaskString += " [" + task.getTime().ToString() + " l" + task.getTimeLeft().ToString() + " s" + task.getStartTime() + "] " + task.getEndTime() + " ";
                        //writeToFile(" [" + task.getTime().ToString() + " l" + task.getTimeLeft().ToString() + " s" + task.getStartTime() + "] " + task.getEndTime() + " ");
                    }
                    writeToFile(tempTaskString);
                    displayedText += processor.endTime;
                    
                    displayedText += " \n";
                    j++;
                }

                Console.Write(displayedText);
            }
        }
        public void writeToFile(string text)
        {
            string path = @"file"+algorithm.ToString()+".txt";
            if (!File.Exists(path))
            {
                using (var stream = File.Create(path)) { }
                TextWriter tw = new StreamWriter(path);
                tw.WriteLine(text);
                tw.Close();
            }
            else if (File.Exists(path))
            {
                TextWriter tw = new StreamWriter(path, true);
                tw.WriteLine(text);
                tw.Close();
            }
        }
        public void clearFile()
        {
            string path = @"file" + algorithm.ToString() + ".txt";

            using (var stream = File.Create(path)) { }
        }

        public void symulate()
        {
            algorithm.proceed(taskList, procList);
        }

        public void timer()
        {
            //co ma dokładnie robić?
        }

        public void addObserver()
        {
        }

        public void removeObserver()
        {
        }

        public void actualiseObserver()
        {
        }

        public List<Processor> getProcList()
        {
            return procList;
        }

        public List<Task> getTaskList()
        {
            return taskList;
        }

        public void setTaskList(List<Task> lista)
        {
            taskList = lista;
        }
    }
}
