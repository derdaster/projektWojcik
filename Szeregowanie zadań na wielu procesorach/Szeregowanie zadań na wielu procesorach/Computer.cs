using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
