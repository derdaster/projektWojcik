using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szeregowanie_zadañ_na_wielu_procesorach
{
    public class algorithmBruteForce : schedulerAlgorithm
    {
        List<int> localTaskMatchingList = new List<int>();
        List<int> optimalSolution = new List<int>();
        List<Task> localTaskList;
        int numberOfTasks, numberOfProcessors, totalTimeForAllTasks;
        int solutionOptimalityRate = Int32.MaxValue;

        void schedulerAlgorithm.proceed(List<Task> taskList, List<Processor> procList)
        {
            numberOfTasks = taskList.Count;
            numberOfProcessors = procList.Count;
            localTaskList = taskList;

            for (int i = 0; i < numberOfTasks; i++)
                localTaskMatchingList.Add(-1);

            bruteDFS(0);

            for (int index=0 ; index < optimalSolution.Count; index++)
                procList[optimalSolution[index]].addTask(taskList[index]);

            taskList.Clear();
        }

        bool bruteDFS( int index )
        {
            for (int processor = 0; processor < numberOfProcessors; processor++)
            {
                localTaskMatchingList[index] = processor;

                if (index < numberOfTasks - 1)
                    bruteDFS(index + 1);
                else if (checkOptimality())
                    storeSolution();
            }
            return true;
        }

		bool checkOptimality()
        {
            List<int> times = new List<int>();
            int minValue = Int32.MaxValue;
            int maxValue = 0;

            for (int i = 0; i < numberOfProcessors; i++)
                times.Add(0);

            for (int i = 0; i < localTaskMatchingList.Count; ++i)
                times[localTaskMatchingList[i]] += localTaskList[i].getTime();

            foreach(var value in times)
            {
                if (value < minValue)
                    minValue = value;

                if (value > maxValue)
                    maxValue = value;
            }

            int currentRate = maxValue - minValue;

            if (currentRate < solutionOptimalityRate)
            {
                solutionOptimalityRate = currentRate;
                return true;
            }
            return false;
        }

		void storeSolution()
        {
            optimalSolution.Clear();
            optimalSolution.AddRange(localTaskMatchingList);
        }

        int abstractValue(int value)
        {
            int temp = value >> 31;
            return (value ^ temp) - temp;
        }

        int calculateOptimalTimePerProcessor(int numerOfProcessors)
        {
            totalTimeForAllTasks = 0;
            foreach (var task in localTaskList)
                totalTimeForAllTasks += task.getTime();

            if (numerOfProcessors <= 0 || totalTimeForAllTasks <= 0)
                return -1;

            return totalTimeForAllTasks / numerOfProcessors;
        }

    }
}
