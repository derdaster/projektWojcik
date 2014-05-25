using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szeregowanie_zadań_na_wielu_procesorach
{
    class Computer : Subject
    {
        List<Task> taskList = new List<Task>();
        schedulerAlgorithm algorithm;
        public void chooseAlgoritm(int i)
        {
            switch(i){
                case 1: algorithm = new algorithm1(); break;
                case 2: algorithm = new algorithm2(); break;
                case 3: algorithm = new algorithm3(); break;
                case 4: algorithm = new algorithm4(); break;
                default: algorithm = new algorithm1(); break;
            }
        }
        public void symulation()
        {
            algorithm.proceed(taskList);
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
    }
    class Processor
    {
        List<Task> taskList = new List<Task>();


    }
    class Task
    {
        int time;
        int timeLeft;
        int priority;
        int startTime;
        int endTime;
        Processor closedBy;
        public Task()
        {

        }
    }

    interface schedulerAlgorithm
    {
        public void proceed(List<Task> taskList);
    }

    class algorithm1 : schedulerAlgorithm
    {
        public void schedulerAlgorithm.proceed(List<Task> taskList)
        {
            //do something
        }

    }
    class algorithm2 : schedulerAlgorithm
    {
        public void schedulerAlgorithm.proceed(List<Task> taskList)
        {
            //do something
        }
    }
    class algorithm3 : schedulerAlgorithm
    {
        public void schedulerAlgorithm.proceed(List<Task> taskList)
        {
            //do something
        }
    }
    class algorithm4 : schedulerAlgorithm
    {
        public void schedulerAlgorithm.proceed(List<Task> taskList)
        {
            //do something
        }
    }
    interface Observer
    {
        public void actualise();
    }
    interface Subject
    {
        List<Observer> observers = new List<Observer>();
        public void addObserver();
        public void removeObserver();
        public void actualiseObserver();
    }
}
