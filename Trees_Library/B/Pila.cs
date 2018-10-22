using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trees_Library.Node;

namespace Trees_Library.B
{
   public class Pila<T>
    {
        private NodoBtree<T> tope;

        /**
         * Tamaño de la Pila
         */
        private int tamanio;



        public Pila()
        {
            this.tope = null;
            this.tamanio = 0;
        }

        public void apilar(T info)
        {
            if (this.esVacia())
                this.tope = new NodoBtree<T>(info, null);
            else
                this.tope = new NodoBtree<T>(info, this.tope);
            this.tamanio++;
        }


        public T desapilar()
        {
            if (this.esVacia())
                return (default(T));
            NodoBtree<T> x = this.tope;
            this.tope = tope.getSig();
            this.tamanio--;
            if (tamanio == 0)
                this.tope = null;
            return (x.getInfo());
        }


        public void vaciar()
        {
            this.tope = null;
            this.tamanio = 0;
        }


        public T getTope()
        {
            return (this.tope.getInfo());
        }

        public int getTamanio()
        {
            return (this.tamanio);
        }


        public bool esVacia()
        {
            return (this.tope == null || this.tamanio == 0);
        }



        public String toString()
        {
            String msj = "";
            NodoBtree<T> p = tope;
            while (p != null)
            {
                msj += p.getInfo().ToString() + "->";
                p = p.getSig();
            }
            return msj;
        }
    }
}
