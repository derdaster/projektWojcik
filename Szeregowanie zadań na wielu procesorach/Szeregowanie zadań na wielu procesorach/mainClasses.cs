using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szeregowanie_zadań_na_wielu_procesorach
{
    public static class Randomizer
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

    class Computer : Subject
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
                case 1: algorithm = new algorithmFCFS(); break;
                case 2: algorithm = new algorithmSJF(); break;
                case 3: algorithm = new algorithmPriority(); break;
                case 4: algorithm = new Randomized(); break;
                default: algorithm = new algorithmFCFS(); break;
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

        public  List<Processor> getProcList()
        {
            return procList;
        }

        public List<Task> getTaskList()
        {
            return taskList;
        }
    }

    class Processor
    {
        List<Task> taskList = new List<Task>();
        public int endTime=0;
        public void addTask(Task t)
        {
            taskList.Add(t);
            endTime += t.getTime();
        }
        public int getCount()
        {
            return taskList.Count;
        }
        public void setEndTime(int x)
        {
            endTime += x;
        }
        public List<Task> getTaskList()
        {
            return taskList;
        }
    }
    class Task
    {
        public int time;
        int timeLeft;
        public int priority;
        int startTime;
        int endTime;
        Processor closedBy;
        public Task(int time, int priority)
        {
            this.time = time;
            this.priority = priority;
            this.timeLeft = time;

        }
        public void setStartTime(int x)
        {
            startTime=x;
        }

        public void setEndTime(int x)
        {
            endTime = x;
        }
        public void setTimeLeft(int x)
        {
            timeLeft = x;
        }
        public int getTime()
        {
            return time;
        }
    }

    interface schedulerAlgorithm
    {
        void proceed(List<Task> taskList, List<Processor> procList);
    }

    class algorithmFCFS : schedulerAlgorithm
    {
        void schedulerAlgorithm.proceed(List<Task> taskList, List<Processor> procList)
        {
            bool empty = false;
            
            foreach (var task in taskList)
            {
                empty = false;
                //sprawdzamy czy któryś procesor jest pusty
                foreach (var proc in procList)
                {
                    if (proc.getCount() == 0)
                    {
                        task.setStartTime(proc.endTime);
                        task.setEndTime(proc.endTime + task.getTime());
                        proc.addTask(task);
                        empty = true;
                        break;
                        
                    }
                }
                //sortujemy procesory, i przypisujemy do tego na którym najszybciej się skończy
                if (!empty)
                {

                    List<Processor> SortedList = procList.OrderBy(o => o.endTime).ToList();
                    procList = SortedList;
                    foreach (var proc in procList)
                    {
                        task.setStartTime(proc.endTime);
                        task.setEndTime(proc.endTime + task.getTime());
                        proc.addTask(task);
                        break;

                    }
                }
            }
            taskList.Clear();
        }

    }
    class algorithmSJF : schedulerAlgorithm
    {
        void schedulerAlgorithm.proceed(List<Task> taskList, List<Processor> procList)
        {
            bool empty = false;
            List<Task> SortedList1 = taskList.OrderBy(o => o.time).ToList();
            taskList = SortedList1;
            foreach (var task in taskList)
            {
                empty = false;
                //sprawdzamy czy któryś procesor jest pusty
                foreach (var proc in procList)
                {
                    if (proc.getCount() == 0)
                    {
                        task.setStartTime(proc.endTime);
                        task.setEndTime(proc.endTime + task.getTime());
                        proc.addTask(task);
                        empty = true;
                        break;

                    }
                }
                //sortujemy procesory, i przypisujemy do tego na którym najszybciej się skończy
                if (!empty)
                {

                    List<Processor> SortedList = procList.OrderBy(o => o.endTime).ToList();
                    procList = SortedList;
                    foreach (var proc in procList)
                    {
                        task.setStartTime(proc.endTime);
                        task.setEndTime(proc.endTime + task.getTime());
                        proc.addTask(task);
                        break;

                    }
                }
            }
            taskList.Clear();
        }
    }
    class algorithmPriority : schedulerAlgorithm
    {
        void schedulerAlgorithm.proceed(List<Task> taskList, List<Processor> procList)
        {
            bool empty = false;
            List<Task> SortedList1 = taskList.OrderBy(o => o.priority).ToList();
            taskList = SortedList1;
            foreach (var task in taskList)
            {
                empty = false;
                //sprawdzamy czy któryś procesor jest pusty
                foreach (var proc in procList)
                {
                    if (proc.getCount() == 0)
                    {
                        task.setStartTime(proc.endTime);
                        task.setEndTime(proc.endTime + task.getTime());
                        proc.addTask(task);
                        empty = true;
                        break;

                    }
                }
                //sortujemy procesory, i przypisujemy do tego na którym najszybciej się skończy
                if (!empty)
                {

                    List<Processor> SortedList = procList.OrderBy(o => o.endTime).ToList();
                    procList = SortedList;
                    foreach (var proc in procList)
                    {
                        task.setStartTime(proc.endTime);
                        task.setEndTime(proc.endTime + task.getTime());
                        proc.addTask(task);
                        break;

                    }
                }
            }
            taskList.Clear();
        }
    }

    class Randomized : schedulerAlgorithm
    {
        void schedulerAlgorithm.proceed(List<Task> taskList, List<Processor> procList)
        {
            Randomizer.Shuffle(taskList);
            bool empty = false;

            foreach (var task in taskList)
            {
                empty = false;
                //sprawdzamy czy któryś procesor jest pusty
                foreach (var proc in procList)
                {
                    if (proc.getCount() == 0)
                    {
                        task.setStartTime(proc.endTime);
                        task.setEndTime(proc.endTime + task.getTime());
                        proc.addTask(task);
                        empty = true;
                        break;

                    }
                }
                //sortujemy procesory, i przypisujemy do tego na którym najszybciej się skończy
                if (!empty)
                {

                    List<Processor> SortedList = procList.OrderBy(o => o.endTime).ToList();
                    procList = SortedList;
                    foreach (var proc in procList)
                    {
                        task.setStartTime(proc.endTime);
                        task.setEndTime(proc.endTime + task.getTime());
                        proc.addTask(task);
                        break;

                    }
                }
            }
            taskList.Clear();
        }
    }
    interface Observer
    {
        void actualise();
    }
    interface Subject
    {
        void addObserver();
        void removeObserver();
        void actualiseObserver();
    }
}
