using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szeregowanie_zadań_na_wielu_procesorach
{
    public enum Priority { VeryLow, Low, Medium, Height, VeryHeight };

    public class Task : IEquatable<Task>, IComparable<Task>
    {
        private static Int64 counter = 0;
        private Int64 Id;
        public int time;
        public int timeLeft { get; set; }
        public Priority priority;
        int startTime;
        int endTime;
        Processor closedBy;

        public Task(int time, Priority priority)
        {
            this.time = time;
            this.priority = priority;
            this.timeLeft = time;
            counter++;
            Id = counter;

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

        Int64 GetId()
        {
            return Id;
        }

        #region IEquatable<Task> Members

        bool IEquatable<Task>.Equals(Task other)
        {
            if (other == null) return false;
            return (this.Id.Equals(other.Id));
        }

        #endregion

        #region IComparable<Task> Members

        int IComparable<Task>.CompareTo(Task other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                // when other come later than this
                if (other.startTime > this.startTime)
                {
                    return 1;
                }
                else
                {
                    if (other.startTime == this.startTime)
                    {
                        // when both have this same start time, but other have bigger priority
                        if (other.priority > this.priority)
                        {
                            return -1;
                        }
                        else
                        {
                            if (other.priority == this.priority)
                            {
                                return 0;
                            }
                            else // if this have higher priority 
                            {
                                return 1;
                            }
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        #endregion
    }
}
