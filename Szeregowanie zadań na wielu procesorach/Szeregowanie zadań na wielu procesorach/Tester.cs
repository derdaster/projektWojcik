using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szeregowanie_zadań_na_wielu_procesorach
{
    public static class Tester
    {
        public static void performSimpleAlghoritmTest(Computer c, int alghoritm)
        {
            c.addProcessor(new Processor());
            c.addProcessor(new Processor());
            c.addProcessor(new Processor());
            //c.addProcessor(new Processor());

            c.addTask(new Task(5, 1));
            c.addTask(new Task(4, 2));
            c.addTask(new Task(4, 1));
            c.addTask(new Task(2, 3));
            c.addTask(new Task(7, 1));
            c.addTask(new Task(1, 5));
            c.addTask(new Task(3, 1));
            //c.addTask(new Task(3, 2));
            //c.addTask(new Task(3, 1));
            //c.addTask(new Task(3, 9));
            c.chooseAlgoritm(alghoritm);
            c.symulate();
        }

        public static string getPerformedTestData(Computer c, string name)
        {
            List<Processor> allProcessors = c.getProcList();

            string displayedText = name + "\nnumber of processors: " + allProcessors.Count + "\n";
            int i = 0;

            foreach (var processor in allProcessors)
            {
                displayedText += "processor " + i.ToString() + ": ";

                List<Task> allTasksForSingleProcessor = processor.getTaskList();
                foreach (var task in allTasksForSingleProcessor)
                {
                    displayedText += " [" + task.getTime().ToString() + " p" + task.priority.ToString() + "] ";
                }
                displayedText += "\n";
                i++;
            }
            return displayedText + "\n";
        }
    }
}
