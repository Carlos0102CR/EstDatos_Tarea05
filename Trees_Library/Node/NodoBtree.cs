using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArbolAVL;

namespace Trees_Library.Node
{
  public  class NodoBtree<T>
    {

        private T info;

        /**
         * Siguiente NodoBtree 
         */
        private NodoBtree<T> sig;
        private NodoBtree<T> anterio;




        public NodoBtree()
        {
            this.info = default(T);
            this.sig = null;
            this.anterio = null;
        }


        public NodoBtree(T info, NodoBtree<T> sig)
        {
            this.info = info;
            this.sig = sig;
        }

        public NodoBtree(T info, NodoBtree<T> sig, NodoBtree<T> ant)
        {
            this.info = info;
            this.sig = sig;
            this.anterio = ant;
        }

        public T getInfo()
        {
            return this.info;
        }


        public NodoBtree<T> getSig()
        {
            return this.sig;
        }


        public void setInfo(T nuevo)
        {
            this.info = nuevo;
        }

        public void setSig(NodoBtree<T> nuevo)
        {
            this.sig = nuevo;
        }


        public void setAnt(NodoBtree<T> cabeza)
        {
            anterio = cabeza;
        }

        public NodoBtree<T> getAnt()
        {
            return anterio;
        }
    }
}
