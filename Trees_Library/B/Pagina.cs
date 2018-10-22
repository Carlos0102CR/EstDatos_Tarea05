using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees_Library.B
{
   public class Pagina
    {
        private int ordenArbol;
        private int MaximoLlaves;
        private int MaximoApuntadores;

        /**
         * numero de llaves de pagina
         */
        private int cont;

        /**
         * llaves clasificadas ascendentemente
         */
        private int[] info;

        /**
         * direcciones de los hijos de la pagina 
         */
        private Pagina[] apuntadores;



        public Pagina(int n)
        {
            this.ordenArbol = n;
            this.MaximoLlaves = n * 2;
            this.MaximoApuntadores = this.MaximoLlaves + 1;
            this.info = new int[MaximoLlaves];
            for (int i = 0; i < info.Length; i++)
                info[i] = default(int);
            this.apuntadores = new Pagina[this.MaximoApuntadores];
            for (int i = 0; i < apuntadores.Length; i++)
                apuntadores[i] = null;
        }




        public int getN()
        {
            return ordenArbol;
        }


        public int getCont()
        {
            return cont;
        }


        public int[] getInfo()
        {
            return info;
        }


        public int getM()
        {
            return MaximoLlaves;
        }


        public int getM1()
        {
            return MaximoApuntadores;
        }


        public Pagina[] getApuntadores()
        {
            return apuntadores;
        }
        public void setN(int n)
        {
            this.ordenArbol = n;
        }
        public void setCont(int cont)
        {
            this.cont = cont;
        }
        public void setApuntadores(Pagina[] apuntadores)
        {
            this.apuntadores = apuntadores;
        }
        public void setInfo(int[] info)
        {
            this.info = info;
        }


        public override String ToString()
        {
            String msg = "  Informacion de la pagina";
            int i = 0;
            while (i < this.getCont())
            {
                msg += " --> " + this.info[i++].ToString();
            }
            return msg;
        }
    }
}
