using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szeregowanie_zadań_na_wielu_procesorach
{
    public class algorithmSJF : schedulerAlgorithm
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
                        task.setEndTime(proc.endTime + task.getTime() + task.getStartTime());
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
                        if (proc.endTime >= task.getStartTime())
                        {
                            task.setEndTime(proc.endTime + task.getTime());
                        }
                        else
                        {
                            task.setEndTime(task.getStartTime() + task.getTime());
                        }
                        proc.addTask(task);
                        break;
                    }
                }
            }
           // taskList.Clear();
        }
    }
}
