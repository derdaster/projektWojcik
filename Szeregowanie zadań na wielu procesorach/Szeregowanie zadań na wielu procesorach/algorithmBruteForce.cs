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
        List<int> optimalSolution;
        List<Task> localTaskList;
        int numberOfTasks, numberOfProcessors, totalTimeForAllTasks, optimalTime;

        void schedulerAlgorithm.proceed(List<Task> taskList, List<Processor> procList)
        {
            numberOfTasks = taskList.Count;
            numberOfProcessors = procList.Count;
            localTaskList = taskList;
            optimalTime = calculateOptimalTimePerProcessor(numberOfProcessors);

            for (int i = 0; i < numberOfTasks; i++)
                localTaskMatchingList.Add(-1);

            bruteDFS(0);
            taskList.Clear();
        }

        bool bruteDFS( int index )
        {
            for (int i = 0; i < numberOfProcessors; i++)
            {
                localTaskMatchingList[index] = i;

                if (index < numberOfTasks - 1)
                {
                    bruteDFS(index + 1);
                }
                else if (checkOptimality())
                {
                    storeSolution();
                    foreach (var element in localTaskMatchingList)
                        Console.Write(element.ToString());
                    Console.Write("\n");
                }
            }
            return true;
        }

		bool checkOptimality()
        {
			//Tutaj trzeba sprawdziæ czy rozwiazanie wygenerowane jest optymalne
			//obecnie nie mam pomys³u jak ;d ale z czasem coœ wymyœlê 
            return true;
        }

		void storeSolution()
        {
            optimalSolution = localTaskMatchingList;
        }

		int calculateOptimalTimePerProcessor( int numerOfProcessors )
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
