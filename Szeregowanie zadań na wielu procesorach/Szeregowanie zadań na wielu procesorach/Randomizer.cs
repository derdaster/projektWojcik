using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szeregowanie_zadań_na_wielu_procesorach
{
    // Randomizuje Liste wejściową 
    public static class Randomizer
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int numberOfElements = list.Count;
            while (numberOfElements > 1)
            {
                numberOfElements--;
                int random = rng.Next(numberOfElements + 1);
                T value = list[random];
                list[random] = list[numberOfElements];
                list[numberOfElements] = value;
            }
        }
    }
}
