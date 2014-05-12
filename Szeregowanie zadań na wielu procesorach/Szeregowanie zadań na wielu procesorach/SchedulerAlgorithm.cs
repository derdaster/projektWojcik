using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szeregowanie_zadań_na_wielu_procesorach
{
    interface SchedulerAlgorithm
    {
        public void algorithm();
    }

    class Algorithm1 : SchedulerAlgorithm
    {

    }

    class Core
    {
        public void chooseAlgorithm();
        public void process();
        public void addObserver();
        public void removeObserver();
        public void actualiseObservers();
    }
    interface SchedulerAlgorithm
    {
        public void addObserver();
        public void removeObserver();
        public void actualiseObservers();
    }
}
