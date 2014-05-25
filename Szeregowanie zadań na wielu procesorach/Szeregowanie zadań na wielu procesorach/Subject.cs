using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szeregowanie_zadań_na_wielu_procesorach
{
    public interface Subject
    {
        void addObserver();
        void removeObserver();
        void actualiseObserver();
    }
}
