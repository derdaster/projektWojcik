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
            c.addProcessor(new Processor());

            c.addTask(new Task(5, Priority.Low ));
            c.addTask(new Task(4, Priority.Medium));
            c.addTask(new Task(4, Priority.VeryLow));
            c.addTask(new Task(2, Priority.Medium));
            c.addTask(new Task(7, Priority.VeryHeight));
            c.addTask(new Task(1, Priority.Low));
            c.addTask(new Task(3, Priority.VeryHeight));
            c.addTask(new Task(3, Priority.Medium));
            c.addTask(new Task(3, Priority.VeryLow));
            c.addTask(new Task(3, Priority.Height));
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
