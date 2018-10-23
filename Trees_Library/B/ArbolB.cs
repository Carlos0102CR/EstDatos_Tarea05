using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees_Library.B
{
  public  class ArbolB
    {
        private int n;//esto es orden
    
        /**
    * numero maximo de apuntadores
    */
        private int m;

        private int m1;
        private Pagina raiz;

        public ArbolB()
        {
            this.raiz = null;
            this.n = 2;
            this.m = n * 2;
            this.m1 = (this.m) + 1;
        }


        public ArbolB(int n)
        {
            if (n <= 0)
            {
                Console.WriteLine("Tamano del orden del arbol no es válido");
                return;
            }
            this.raiz = null;
            this.n = n;
            this.m = n * 2;
            this.m1 = (this.m) + 1;
        }

        public Pagina getRaiz()
        {
            return raiz;
        }


        protected void setRaiz(Pagina raiz)
        {
            this.raiz = raiz;
        }

        public int getN()
        {
            return n;
        }


        public int getM()
        {
            return m;
        }

        public int getM1()
        {
            return m1;
        }


        public void setN(int n)
        {
            this.n = n;
        }


        public bool insertar(int x)
        {

            Pila<Pagina> pila = new Pila<Pagina>();

            int[] subir = new int[1];
            int[] subir1 = new int[1];

            int posicion = 0, i = 0, terminar, separar;
            Pagina p = null, nuevo = null, nuevo1;
            if (this.raiz == null)
            {
                this.raiz = this.crearPagina(x);
            }
            else
            {
                posicion = buscar(this.raiz, x, pila);
                if (posicion == -1)
                    return (false);
                else
                {
                    terminar = separar = 0;
                    while ((!pila.esVacia()) && (terminar == 0))
                    {
                        p = pila.desapilar();
                        if (p.getCont() == this.m)
                        {
                            if (separar == 0)
                            {
                                nuevo = romper(p, null, x, subir);
                                separar = 1;
                            }
                            else
                            {
                                nuevo1 = romper(p, nuevo, subir[0], subir1);
                                subir[0] = subir1[0];
                                nuevo = nuevo1;
                            }
                        }
                        else
                        {
                            if (separar == 1)
                            {
                                separar = 0;
                                i = donde(p, subir[0]);
                                i = insertar(p, subir[0], i);
                                cderechaApunt(p, i + 1);
                                p.getApuntadores()[i + 1] = nuevo;

                            }
                            else
                            {
                                posicion = insertar(p, x, posicion);
                            }
                            terminar = 1;
                        }
                    }
                    if ((separar == 1) && (terminar == 0))
                    {
                        this.setRaiz(this.crearPagina(subir[0]));
                        this.raiz.getApuntadores()[0] = p;
                        this.raiz.getApuntadores()[1] = nuevo;
                    }
                }
            }
            return (true);
        }

        private int insertar(Pagina p, int x, int i)
        {
            int j;
            if (p.getCont() != 0)
            {
                int compara = Comparable.Comparador(p.getInfo()[i], x);

                if (compara < 0)
                    i++;
                else
                {
                    j = p.getCont() - 1;
                    while (j >= i)
                    {
                        p.getInfo()[j + 1] = p.getInfo()[j];
                        j = j - 1;
                    }
                }
            }
            p.setCont(p.getCont() + 1);
            p.getInfo()[i] = x;
            return (i);
        }


        public bool eliminar(int x)
        {
            int posicion, i, k;
            Pagina p, q = null, r, t;
            Pila<Componente> pila = new Pila<Componente>();
            Componente objeto = new Componente();
            posicion = esta(this.raiz, x, pila);
            if (posicion == -1)
                return (false);
            else
            {
                objeto = pila.desapilar();
                p = objeto.getP();
                i = objeto.getV();
                if (!this.esHoja(p))
                {
                    t = p;
                    k = i;
                    pila.apilar(new Componente(p, i + 1));
                    p = p.getApuntadores()[i + 1];
                    while (p != null)
                    {
                        pila.apilar(new Componente(p, 0));
                        p = p.getApuntadores()[0];
                    }
                    objeto = pila.desapilar();
                    p = objeto.getP();
                    i = objeto.getV();
                    t.getInfo()[k] = p.getInfo()[0];
                    x = (int)p.getInfo()[0];
                    posicion = 0;
                }
                if (p.getCont() > this.n)
                    this.retirar(p, posicion);
                else
                {
                    if (!pila.esVacia())
                    {
                        objeto = pila.desapilar();
                        q = objeto.getP();
                        i = objeto.getV();
                        if (i < q.getCont())
                        {
                            r = q.getApuntadores()[i + 1];
                            if (r.getCont() > this.n)
                            {
                                this.retirar(p, posicion);
                                this.cambio(p, q, r, i, x);
                            }
                            else
                            {
                                if (i != 0)
                                {
                                    r = q.getApuntadores()[i - 1];
                                    if (r.getCont() > this.n)
                                    {
                                        this.retirar(p, posicion);
                                        this.cambio(p, q, r, (i - 1), x);
                                    }
                                    else
                                    {
                                        this.unir(q, r, p, (i - 1), pila, x, posicion);
                                    }
                                }
                                else
                                {
                                    this.unir(q, r, p, i, pila, x, posicion);
                                }
                            }
                        }
                        else
                        {
                            r = q.getApuntadores()[i - 1];
                            if (r.getCont() > this.n)
                            {
                                this.retirar(p, posicion);
                                this.cambio(p, q, r, (i - 1), x);
                            }
                            else
                                this.unir(q, r, p, (i - 1), pila, x, posicion);
                        }
                    }
                    else
                    {
                        this.retirar(p, posicion);
                        if (p.getCont() == 0)
                        {
                            this.setRaiz(null);
                        }
                    }
                }
            }
            return true;
        }


        private void retirar(Pagina p, int i)
        {
            while (i < p.getCont() - 1)
            {
                p.getInfo()[i] = p.getInfo()[i + 1];
                i++;
            }
            p.setCont(p.getCont() - 1);
        }


        private Pagina crearPagina(int x)
        {
            Pagina p = new Pagina(n);
            inicializar(p);
            p.setCont(1);
            p.getInfo()[0] = x;
            return (p);
        }


        private void inicializar(Pagina p)
        {
            int i = 0;
            p.setCont(0);
            while (i < this.m1)
                p.getApuntadores()[i++] = null;
        }


        public bool esta(int dato)
        {
            Pila<Componente> pi = new Pila<Componente>();
            return (this.esta(this.raiz, dato, pi) != (-1));
        }

        private int esta(Pagina p, int x, Pila<Componente> pila)
        {
            int i = 0;
            bool encontro = false;
            int posicion = -1;
            while ((p != null) && !encontro)
            {
                i = 0;
                int compara = Comparable.Comparador(p.getInfo()[i], x);

                while ((compara < 0) && (i < (p.getCont() - 1)))
                {
                    i++;
                    compara = Comparable.Comparador(p.getInfo()[i], x);
                }
                if ((compara > 0))
                {
                    pila.apilar(new Componente(p, i));
                    p = p.getApuntadores()[i];
                }
                else
                    if ((compara < 0))
                {
                    pila.apilar(new Componente(p, i + 1));
                    p = p.getApuntadores()[i + 1];
                }
                else
                {
                    pila.apilar(new Componente(p, i));
                    encontro = true;
                }
            }
            if (encontro)
            {
                posicion = i;
            }
            return (posicion);
        }


        private int buscar(Pagina p, int x, Pila<Pagina> pila)
        {
            int i = -1, posicion;
            bool encontro = false;
            posicion = -1;
            while ((p != null) && !(encontro))
            {
                pila.apilar(p);
                i = 0;
                int compara = Comparable.Comparador(p.getInfo()[i], x);

                while ((compara < 0) && (i < (p.getCont() - 1)))
                {
                    i++;
                    compara = Comparable.Comparador(p.getInfo()[i], x);

                }
                if ((compara > 0))
                    p = p.getApuntadores()[i];
                else
                {
                    if (compara < 0)
                        p = p.getApuntadores()[i + 1];
                    else
                        encontro = true;
                }
            }
            if (!encontro)
                posicion = i;
            return (posicion);
        }


        protected int donde(Pagina p, int x)
        {
            int i;
            i = 0;
            int compara = Comparable.Comparador(p.getInfo()[i], x);

            while ((compara < 0) && (i < (p.getCont() - 1)))
            {
                i++;
                compara = Comparable.Comparador(p.getInfo()[i], x);

            }
            return i;
        }


        private Pagina romper(Pagina p, Pagina t, int x, int[] subir)
        {
            int[] a = new int[m1];
            int i = 0;
            bool s = false;
            Pagina[] b = new Pagina[this.m1 + 1];
            while (i < this.m && !s)
            {
                int compara = Comparable.Comparador(p.getInfo()[i], x);

                if (compara < 0)
                { //<-- X es mayor que el dato del arbol
                    a[i] = (int)p.getInfo()[i];
                    b[i] = p.getApuntadores()[i];
                    p.getApuntadores()[i++] = null;
                }
                else
                    s = true;
            }
            a[i] = x;
            b[i] = p.getApuntadores()[i];
            p.getApuntadores()[i] = null;
            b[++i] = t;
            while ((i <= this.m))
            {
                a[i] = (int)p.getInfo()[i - 1];
                b[i + 1] = p.getApuntadores()[i];
                p.getApuntadores()[i++] = null;
            }
            Pagina q = new Pagina(this.n);
            inicializar(q);
            p.setCont(n); q.setCont(n);
            i = 0;
            while (i < this.n)
            {
                p.getInfo()[i] = a[i];
                p.getApuntadores()[i] = b[i];
                q.getInfo()[i] = a[i + n + 1];
                q.getApuntadores()[i] = b[i + n + 1];
                i++;
            }
            p.getApuntadores()[n] = b[n];
            q.getApuntadores()[n] = b[m1];
            subir[0] = a[n];
            return (q);
        }


        protected void cizquierda_apunt(Pagina p, int i, int j)
        {
            while (i < j)
            {
                p.getApuntadores()[i] = p.getApuntadores()[i + 1];
                i++;
            }
            p.getApuntadores()[i] = null;
        }


        protected void cderechaApunt(Pagina p, int i)
        {
            int j;
            j = p.getCont();
            while (j > i)
            {
                p.getApuntadores()[j] = p.getApuntadores()[j - 1];
                j--;
            }
        }


        protected void cambio(Pagina p, Pagina q, Pagina r, int i, int x)
        {
            int k;
            int t;
            int compara = Comparable.Comparador(r.getInfo()[r.getCont() - 1], x);

            if (compara < 0)
            {
                t = (int)q.getInfo()[i];
                retirar(q, i);
                k = 0;
                k = insertar(p, t, k);
                t = (int)r.getInfo()[r.getCont() - 1];
                retirar(r, r.getCont() - 1);
                k = i;
                if (k == -1)
                    k = 0;
                k = insertar(q, t, k);
            }
            else
            {
                t = (int)q.getInfo()[i];
                retirar(q, i);
                k = p.getCont() - 1;
                if (k == -1)
                    k = 0;
                k = insertar(p, t, k);
                t = (int)r.getInfo()[0];
                retirar(r, 0);
                k = i;
                if (q.getCont() != 0)
                    if (k > q.getCont() - 1)
                        k = q.getCont() - 1;
                k = insertar(q, t, k);
            }
        }


        private void unir(Pagina q, Pagina r, Pagina p, int i, Pila<Componente> pila, int x, int posicion)
        {
            int terminar = 0, j = 0, k;
            Pagina t = null;
            Componente objeto = new Componente();
            retirar(p, posicion);
            int compara = Comparable.Comparador(r.getInfo()[0], x);

            if (compara > 0)
            {
                t = p;
                p = r;
                r = t;
            }
            while (terminar == 0)
            {
                /*1*/
                if ((r.getCont() < this.n) && (p.getCont() > this.n))
                {
                    cambio(r, q, p, i, x);
                    r.getApuntadores()[r.getCont()] = p.getApuntadores()[0];
                    this.cizquierda_apunt(p, 0, p.getCont() + 1);
                    terminar = 1;
                }
                /*2*/
                else
                        if ((p.getCont() < this.n) && (r.getCont() > this.n))
                {
                    cambio(p, q, r, i, x);
                    this.cderechaApunt(p, 0);
                    p.getApuntadores()[0] = r.getApuntadores()[r.getCont() + 1];
                    r.getApuntadores()[r.getCont() + 1] = null;
                    terminar = 1;
                }
                else
                {
                    j = r.getCont();
                    r.getInfo()[j++] = q.getInfo()[i];
                    k = 0;
                    while (k <= p.getCont() - 1)
                    {
                        r.getInfo()[j++] = (int)p.getInfo()[k++];
                    }
                    r.setCont(j);
                    this.retirar(q, i);
                    k = 0;
                    j = this.m - p.getCont();
                    while (p.getApuntadores()[k] != null)
                    {
                        r.getApuntadores()[j++] = p.getApuntadores()[k++];
                    }
                    p = null;
                    /*3*/
                    if (q.getCont() == 0)
                    {
                        q.getApuntadores()[i + 1] = null;
                        /*4*/
                        if (pila.esVacia())
                        {
                            q = null;
                        }
                    }
                    else
                        this.cizquierda_apunt(q, i + 1, q.getCont() + 1);
                    /*5*/
                    if (q != null)
                    {
                        /*6*/
                        if (q.getCont() >= this.n)
                        {
                            terminar = 1;
                        }
                        else
                        {
                            t = q;
                            /*7*/
                            if (!pila.esVacia())
                            {
                                objeto = pila.desapilar();
                                q = objeto.getP();
                                i = objeto.getV();
                                compara = Comparable.Comparador(q.getInfo()[0], x);

                                if (compara <= 0)
                                {
                                    p = t;
                                    r = q.getApuntadores()[i - 1];
                                    i = i - 1;
                                }
                                else
                                {
                                    r = t;
                                    p = q.getApuntadores()[i + 1];
                                }
                            }
                            else
                                terminar = 1;
                        }
                    }
                    else
                    {
                        terminar = 1;
                        this.setRaiz(r);
                    }
                }
            }
        }

        public ListaCD<int> getHojas()
        {
            ListaCD<int> l = new ListaCD<int>();
            getHojas(this.raiz, l);
            return (l);
        }

        /**
         * Metodo de tipo privado que retorna un iterador con las hojas del Arbol B. <br>
         * <b>post: </b> Se retorno un iterador con las hojas del Arbol B.<br>
         * @param r representa la raiz del arbol, o raiz de subarbol. <br>
         * @param l Lista para el almacenamiento de los datos del arbol. <br>
         */
        private void getHojas(Pagina r, ListaCD<int> l)
        {
            if (r == null)
                return;
            if (this.esHoja(r))
            {
                for (int j = 0; j < r.getCont(); j++)
                    l.insertarAlFinal(r.getInfo()[j]);
            }
            for (int i = 0; i < r.getCont() + 1; i++)
            {
                getHojas(r.getApuntadores()[i], l);
            }
        }


        public int contarHojas()
        {
            return contarHojas(raiz);
        }


        private int contarHojas(Pagina r)
        {
            if (r == null)
                return 0;
            int cont = 0;
            if (this.esHoja(r))
                cont++;
            for (int i = 0; i < r.getCont() + 1; i++)
            {
                cont += contarHojas(r.getApuntadores()[i]);
            }
            return (cont);
        }



        protected bool esHoja(Pagina p)
        {
            int j = 0;
            while ((p.getApuntadores()[j] == null) && (j < (p.getCont() - 1)))
                j++;
            return (p.getApuntadores()[j] == null);
        }


        public bool esVacio()
        {
            return (this.raiz == null);
        }



        public int getPeso()
        {
            if (this.esVacio())
                return (0);
            return (getPeso(this.getRaiz()));
        }


        private int getPeso(Pagina r)
        {
            if (r == null)
                return 0;
            int cont = 0;
            cont += r.getCont();
            for (int i = 0; i < r.getCont() + 1; i++)
            {
                cont += getPeso(r.getApuntadores()[i]);
            }
            return (cont);
        }

        public int getAltura()
        {
            return (getAltura(this.getRaiz()));
        }


        private int getAltura(Pagina r)
        {
            int alt = 0;
            while (r != null)
            {
                alt++;
                r = r.getApuntadores()[0];
            }
            return (alt);
        }


        public String imprime()
        {
            String msg = "";
            return (this.imprime(this.raiz, msg));
        }


        private String imprime(Pagina r, String msg)
        {
            int i = 0;
            
            while (i <= r.getCont())
            {
                if (r.getApuntadores()[i]==null)
                {

                    msg += r.ToString() +"" + "\n";
                    if (i==0)
                    {
                        break;
                    }
                   
                }
                else
                {

                    msg += r.ToString() + "  pagina = " + i + "   ES =" + r.getApuntadores()[i].ToString() + "\n";
                    if (!this.esHoja(r.getApuntadores()[i]))
                        msg += this.imprime(r.getApuntadores()[i], msg);
                }
               
                

                i++;
            }
            return msg;
        }


        public ArbolB clonar()
        {
            ArbolB clon = new ArbolB(this.getN());
            if (raiz == null)
                return (clon);
            clon.setRaiz(clonar(this.raiz));
            return (clon);
        }

        private Pagina clonar(Pagina r)
        {
            if (r == null)
                return (null);
            else
            {
                int[] info = new int[r.getM()];
                for (int i = 0; i < r.getCont(); i++)
                {
                    info[i] = (int)r.getInfo()[i];
                }
                Pagina aux = new Pagina(r.getN());
                aux.setInfo(info);
                aux.setCont(r.getCont());
                for (int i = 0; i < aux.getCont() + 1; i++)
                {
                    aux.getApuntadores()[i] = clonar(r.getApuntadores()[i]);
                }
                return (aux);
            }
        }

        public ListaCD<int> preOrden()
        {
            ListaCD<int> l = new ListaCD<int>();
            preOrden(this.raiz, l);
            return (l);
        }

        private void preOrden(Pagina r, ListaCD<int> l)
        {
            if (r == null)
                return;
            for (int j = 0; j < r.getCont(); j++)
                l.insertarAlFinal(r.getInfo()[j]);
            for (int i = 0; i < r.getCont() + 1; i++)
            {
                preOrden(r.getApuntadores()[i], l);
            }
        }


        public ListaCD<int> inOrden()
        {
            ListaCD<int> l = new ListaCD<int>();
            inOrden(this.raiz, l);
            return (l);
        }

        private void inOrden(Pagina r, ListaCD<int> l)
        {
            if (r == null)
                return;
            inOrden(r.getApuntadores()[0], l);
            for (int j = 0; j < r.getCont(); j++)
                l.insertarAlFinal(r.getInfo()[j]);
            for (int i = 1; i < r.getCont() + 1; i++)
            {
                inOrden(r.getApuntadores()[i], l);
            }
        }


        public ListaCD<int> postOrden()
        {
            ListaCD<int> l = new ListaCD<int>();
            postOrden(this.raiz, l);
            return (l);
        }

        private void postOrden(Pagina r, ListaCD<int> l)
        {
            if (r == null)
                return;
            for (int i = 0; i < r.getCont() + 1; i++)
            {
                postOrden(r.getApuntadores()[i], l);
            }
            for (int j = 0; j < r.getCont(); j++)
                l.insertarAlFinal(r.getInfo()[j]);
        }


        public ListaCD<int> impNiveles()
        {
            ListaCD<int> l = new ListaCD<int>();
            if (!this.esVacio())
            {
                Cola<Pagina> c = new Cola<Pagina>();
                c.enColar(this.getRaiz());
                Pagina x;
                while (!c.esVacia())
                {
                    x = c.deColar();
                    if (x != null)
                    {
                        for (int i = 0; i < x.getCont(); i++)
                            l.insertarAlFinal(x.getInfo()[i]);
                        for (int j = 0; j < x.getCont() + 1; j++)
                            c.enColar(x.getApuntadores()[j]);
                    }
                }
            }
            return (l);
        }

        public void podar()
        {
            if (this.esHoja(raiz))
                this.setRaiz(null);
            podar(this.raiz);
        }

        private void podar(Pagina r)
        {
            if (r == null)
                return;
            for (int i = 0; i < r.getCont() + 1; i++)
            {
                Pagina apunt = r.getApuntadores()[i];
                if (this.esHoja(apunt))
                    r.getApuntadores()[i] = null;
            }
            for (int j = 0; j < r.getCont() + 1; j++)
            {
                if (r.getApuntadores()[j] != null)
                {
                    podar(r.getApuntadores()[j]);
                }
            }

        }

    }
}
