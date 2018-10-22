using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees_Library.B
{
     public class Componente
    {
        //Pagina
        private Pagina p;

        private int v;




        public Componente()
        {
        }


        public Componente(Pagina p, int v)
        {
            this.p = p;
            this.v = v;
        }

        public Pagina getP()
        {
            return p;
        }


        public int getV()
        {
            return v;
        }


        public void setP(Pagina p)
        {
            this.p = p;
        }

        public void setV(int v)
        {
            this.v = v;
        }
    }
}
