using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szeregowanie_zadań_na_wielu_procesorach
{
    public class Processor
    {
        List<Task> taskList = new List<Task>();
        public int endTime=0;

        public void addTask(Task t)
        {
            taskList.Add(t);
            endTime = t.getEndTime();
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

        public void setTaskList(List<Task> allTasksForSingleProcessor)
        {
            taskList = allTasksForSingleProcessor;
        }
    }
}
