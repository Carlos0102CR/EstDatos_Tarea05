using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees_Library.B
{
  public  class Comparable
    {
        public static int Comparador(object a, object b)
        {
            int valor1, valor2;
            valor1 = (int)a;
            valor2 = (int)b;

            if (valor1 > valor2)
            {
                return 1;
            }

            if (valor1 < valor2)
            {
                return -1;
            }

            return 0;
        }
    }
}
