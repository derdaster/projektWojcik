using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szeregowanie_zadań_na_wielu_procesorach
{
    public class Task
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
            startTime = x;
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
}
