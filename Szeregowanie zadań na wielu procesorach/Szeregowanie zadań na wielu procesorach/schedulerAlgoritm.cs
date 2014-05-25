using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szeregowanie_zadań_na_wielu_procesorach
{
    interface schedulerAlgorithm
    {
        void proceed(List<Task> taskList, List<Processor> procList);
    }
}
