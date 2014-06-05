using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyChart
{
    public enum Priority {VeryLow, Low , Medium , Height ,VeryHeight};

    public class ProcessorTask : IEquatable<ProcessorTask>, IComparable<ProcessorTask>
    {
        private static Int64 counter = 0;
        public int timeLeft { set; get; }
        public int time { set; get; }
        public Priority priority { set; get; }
        public int startTime { set; get; }
        public int endTime { set; get; }
        private Int64 Id;

        public ProcessorTask()
        {
            timeLeft = 0;
            endTime = 0;
            counter++;
            Id = counter;
        }

        Int64 GetId()
        {
            return Id;
        }

        #region IEquatable<ProcessorTask> Members

        bool IEquatable<ProcessorTask>.Equals(ProcessorTask other)
        {
            if (other == null) return false;
            return (this.Id.Equals(other.Id));
        }

        #endregion

        #region IComparable<ProcessorTask> Members

        public int CompareTo(ProcessorTask other)
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
