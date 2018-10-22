using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trees_Library.Node;

namespace Trees_Library.B
{
   public class Cola<T>
    {
        private NodoBtree<T> inicio;

        /**
         * Tamaño de la cola
         */
        private int tamanio;

        public Cola()
        {
            this.inicio = new NodoBtree<T>();
            this.inicio.setSig(inicio);
            inicio.setAnt(inicio);
            this.tamanio = 0;
        }


        public void enColar(T info)
        {
            NodoBtree<T> x = new NodoBtree<T>(info, inicio, inicio.getAnt());
            inicio.getAnt().setSig(x);
            inicio.setAnt(x);
            this.aumentarTamanio();
        }


        public T deColar()
        {
            if (this.esVacia())
                return (default(T));
            NodoBtree<T> x = this.inicio.getSig();
            this.inicio.setSig(x.getSig());
            x.getSig().setAnt(inicio);
            x.setSig(null);
            x.setAnt(null);
            this.tamanio--;
            return (x.getInfo());
        }


        public void vaciar()
        {
            this.inicio.setSig(this.inicio);
            this.inicio.setAnt(this.inicio);
            this.tamanio = 0;
        }

        protected NodoBtree<T> getInicio()
        {
            return this.inicio;
        }



        public T getInfoInicio()
        {
            return this.inicio.getSig().getInfo();
        }



        protected void aumentarTamanio()
        {
            this.tamanio++;
        }


        protected void setInicio(NodoBtree<T> ini)
        {
            this.inicio = ini;
        }


        public int getTamanio()
        {
            return (this.tamanio);
        }


        public bool esVacia()
        {
            return (this.getTamanio() == 0);
        }
        public String toString()
        {
            String msj = "";
            NodoBtree<T> c = this.inicio.getSig();
            while (c != inicio)
            {
                msj += c.getInfo().ToString() + "->";
                c = c.getSig();
            }
            return msj;
        }
    }
}
