using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trees_Library.Node;

namespace Trees_Library.B
{
   public class ListaCD<T>
    {
        private NodoBtree<T> cabeza;

        /**
         * Representa el tamaño de la Lista
         */
        private int tamanio = 0;

        public ListaCD()
        {
            this.cabeza = new NodoBtree<T>();
            this.cabeza.setSig(cabeza);
            cabeza.setAnt(cabeza);
        }


        public void insertarAlInicio(T dato)
        {
            NodoBtree<T> x = new NodoBtree<T>(dato, cabeza.getSig(), cabeza);
            cabeza.setSig(x);
            x.getSig().setAnt(x);
            this.tamanio++;
        }


        public void insertarAlFinal(T dato)
        {
            NodoBtree<T> x = new NodoBtree<T>(dato, cabeza, cabeza.getAnt());
            cabeza.getAnt().setSig(x);
            cabeza.setAnt(x);
            this.tamanio++;
        }


        public void insertarOrdenado(T info)
        {
            if (this.esVacia())
                this.insertarAlInicio(info);
            else
            {
                NodoBtree<T> x = this.cabeza;
                NodoBtree<T> y = x;
                x = x.getSig();
                while (x != this.cabeza)
                {

                    int rta = Comparable.Comparador(info, x.getInfo());

                    if (rta < 0)
                        break;
                    y = x;
                    x = x.getSig();
                }
                if (x == cabeza.getSig())
                    this.insertarAlInicio(info);
                else
                {
                    y.setSig(new NodoBtree<T>(info, x, y));
                    x.setAnt(y.getSig());
                    this.tamanio++;
                }
            }
        }


        public T eliminar(int i)
        {
            try
            {
                NodoBtree<T> x;
                if (i == 0)
                {
                    x = this.cabeza.getSig();
                    this.cabeza.setSig(x.getSig());
                    this.cabeza.getSig().setAnt(this.cabeza);
                    x.setSig(null);
                    x.setAnt(null);
                    this.tamanio--;
                    return (x.getInfo());
                }
                x = this.getPos(i - 1);
                if (x == null)
                    return (default(T));
                NodoBtree<T> y = x.getSig();
                x.setSig(y.getSig());
                y.getSig().setAnt(x);
                y.setSig(null);
                y.setAnt(null);
                this.tamanio--;
                return (y.getInfo());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return (default(T));
        }


        public void vaciar()
        {
            this.cabeza = new NodoBtree<T>();
            this.cabeza.setSig(cabeza);
            cabeza.setAnt(cabeza);
            this.tamanio = 0;
        }


        public T get(int i)
        {
            try
            {
                NodoBtree<T> x = this.getPos(i);
                if (x == null)
                    return (default(T));
                return (x.getInfo());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return (default(T));
        }


        public void set(int i, T dato)
        {
            try
            {
                NodoBtree<T> t = this.getPos(i);
                if (t != null)
                    t.setInfo(dato);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public int getTamanio()
        {
            return (this.tamanio);
        }


        public bool esVacia()
        {
            return (cabeza == cabeza.getSig() || this.getTamanio() == 0);
        }


        public bool esta(int info)
        {
            return (this.getIndice(info) != -1);
        }


        public String toString()
        {
            if (this.esVacia())
                return ("Lista Vacia");
            String r = "";
            for (NodoBtree<T> x = this.cabeza.getSig(); x.getInfo() != null; x = x.getSig())
                r += x.getInfo().ToString() + "<->";
            return (r);
        }



        private NodoBtree<T> getPos(int i)
        {
            if (i < 0 || i >= this.tamanio)
            {
                Console.WriteLine("Error indice no valido en una Lista Circular Doblemente Enlazada");
                return (null);
            }
            NodoBtree<T> x = cabeza.getSig();
            for (; i-- > 0; x = x.getSig()) ;
            return x;
        }


        public int getIndice(int dato)
        {
            int i = 0;
            for (NodoBtree<T> x = this.cabeza.getSig(); x != this.cabeza; x = x.getSig())
            {
                if (x.getInfo().Equals(dato))
                    return (i);
                i++;
            }
            return (-1);
        }
    }
}
