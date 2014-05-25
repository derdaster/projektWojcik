using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szeregowanie_zadań_na_wielu_procesorach
{
    public class algorithmPriority : schedulerAlgorithm
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
}
