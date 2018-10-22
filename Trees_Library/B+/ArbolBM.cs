using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trees_Library.B;

namespace Trees_Library.B_
{
 public class ArbolBM: ArbolB
    {
        private Pagina vsam;
        public ArbolBM()
        {

            this.vsam = base.getRaiz();
        }
        public ArbolBM(int n)
        {
            base.setN(n);
            this.vsam = base.getRaiz();
        }


        public Pagina getVsam()
        {
            return vsam;
        }


        public bool insertarBMas(int x)
        {

            Pila<Pagina> pila = new Pila<Pagina>();

            int[] subir = new int[1];
            int[] subir1 = new int[1];

            int posicion = 0, i = 0, terminar, separar;
            Pagina p = null, nuevo = null, nuevo1 = null;


            if (base.getRaiz() == null)
            {
                base.setRaiz(this.crearPagina(base.getN(), x));
                vsam = base.getRaiz();
                return (true);
            }
            else
            {
                posicion = buscar(base.getRaiz(), x, pila);
                if (posicion == -1)
                    return (false);
                else
                {
                    terminar = separar = 0;
                    while ((!pila.esVacia()) && (terminar == 0))
                    {
                        p = (Pagina)pila.desapilar();
                        if (p.getCont() == base.getM())
                        {
                            if (separar == 0)
                            {
                                nuevo = romper(p, null, x, subir, separar);
                                separar = 1;
                            }
                            else
                            {
                                nuevo1 = romper(p, nuevo, subir[0], subir1, separar);
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
                                base.cderechaApunt(p, i + 1);
                                p.getApuntadores()[i + 1] = nuevo;

                            }
                            else
                                posicion = insertar(p, x, posicion);
                            terminar = 1;
                        }
                    }
                    if ((separar == 1) && (terminar == 0))
                    {
                        this.setRaiz(this.crearPagina(base.getN(), subir[0]));
                        base.getRaiz().getApuntadores()[0] = p;
                        base.getRaiz().getApuntadores()[1] = nuevo;
                    }
                }
            }
            return true;
        }

        private int insertar(Pagina p, int x, int i)
        {
            int j;
            if (p.getCont() != 0)
            {
                int compara = Comparable.Comparador(p.getInfo()[i], x);

                if (compara < 0)
                {
                    i++;
                }
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
            p.getInfo()[i] = x;
            p.setCont(p.getCont() + 1);
            return i;
        }


        public bool eliminarBMas(int x)
        {
            int posicion, i, k;
            int temp = default(int);
            Pagina p = null, q = null, r = null;
            Pila<Componente> pila = new Pila<Componente>();
            Componente objeto = null;
            posicion = estaBMas(base.getRaiz(), x, pila);
            if (posicion == -1)
                return false;//la llave no existe en el arbol

            objeto = pila.desapilar();
            p = objeto.getP();
            i = objeto.getV();
            if (p.getCont() > base.getN())
            {
                retirar(p, posicion);
                return true;
            }

            if (pila.esVacia())
            {
                retirar(p, posicion);
                if (p.getCont() == 0)
                {
                    base.setRaiz(null);
                    vsam = base.getRaiz();
                }
                return true;
            }
            objeto = (Componente)pila.desapilar();
            q = objeto.getP();
            i = objeto.getV();
            if (i < q.getCont())
            {
                r = q.getApuntadores()[i + 1];
                if (r.getCont() > base.getN())
                {
                    retirar(p, posicion);
                    temp = (int)r.getInfo()[0];
                    retirar(r, 0);
                    retirar(q, i);
                    k = donde(p, temp);
                    insertar(p, temp, k);
                    k = donde(q, (int)r.getInfo()[0]);
                    insertar(q, (int)r.getInfo()[0], k);
                    return true;
                }
            }
            if (i > 0)
            {
                r = q.getApuntadores()[i - 1];
                if (r.getCont() > base.getN())
                {
                    retirar(p, posicion);
                    temp = (int)r.getInfo()[r.getCont() - 1];
                    retirar(r, r.getCont() - 1);
                    retirar(q, i - 1);
                    k = this.donde(p, temp);
                    insertar(p, temp, k);
                    k = this.donde(q, temp);
                    insertar(q, temp, k);
                    return true;
                }
            }
            if (i > 0)
                i--;
            unirBMas(q, r, p, i, pila, x, posicion);
            return true;
        }

        /**
         * Metodo que permite retirar un dato del arbol indicada. <br>
         * <b>post: </b> Se elimino el dato del arbol B. <br>
         * @param p pagina de la que se desea retirar el dato.  <br>
         * @param i posicion del dato en el arbol. <br>
         */
        private void retirar(Pagina p, int i)
        {
            while (i < p.getCont() - 1)
            {
                p.getInfo()[i] = p.getInfo()[i + 1];
                i++;
            }
            p.setCont(p.getCont() - 1);
        }


        private Pagina crearPagina(int n, int x)
        {
            Pagina p = new Pagina(n);
            inicializar(p);
            p.setCont(1);
            p.getInfo()[0] = (x);
            return p;
        }

        private void inicializar(Pagina p)
        {
            int i = 0;
            p.setCont(0);
            while (i < base.getM1())
                p.getApuntadores()[i++] = null;
        }


        public bool estaBMas(int dato)
        {
            Pila<Componente> pi = new Pila<Componente>();
            return (this.estaBMas(base.getRaiz(), dato, pi) != (-1));
        }

        private int estaBMas(Pagina p, int x, Pila<Componente> pi)
        {
            int i = 0;
            bool encontro = false;
            int posicion = -1;
            while ((p != null) && (!encontro))
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
                    pi.apilar(new Componente(p, i));
                    p = p.getApuntadores()[i];
                }
                else
                    if ((compara < 0))
                {
                    pi.apilar(new Componente(p, i + 1));
                    if (p.getApuntadores()[0] != null)
                        p = p.getApuntadores()[i + 1];
                    else
                        p = null;
                }
                else
                {
                    if (p.getApuntadores()[0] != null)
                    {
                        pi.apilar(new Componente(p, i + 1));
                        p = p.getApuntadores()[i + 1];
                    }
                    else
                    {
                        pi.apilar(new Componente(p, i));
                        encontro = true;
                    }
                }
            }
            if (encontro)
            {
                posicion = i;
            }
            return posicion;
        }


        private int buscar(Pagina p, int x, Pila<Pagina> pila)
        {
            int i = 0;
            bool encontro = false;
            int posicion = -1;
            while ((p != null) && (!encontro))
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
                   if (compara < 0)
                    if (p.getApuntadores()[0] != null)
                        p = p.getApuntadores()[i + 1];
                    else
                        p = null;
                else if (p.getApuntadores()[0] != null)
                    p = p.getApuntadores()[i + 1];
                else
                    encontro = true;
            }
            if (!encontro)
                posicion = i;
            return posicion;
        }


        private Pagina romper(Pagina p, Pagina t, int x, int[] subir, int separar)
        {
            int[] a = new int[base.getM1()];
            int i = 0;
            bool s = false;
            Pagina[] b = new Pagina[base.getM1() + 1];
            Pagina temp = null;

            if (separar == 0)
            {
                temp = p.getApuntadores()[base.getM()];
                p.getApuntadores()[base.getM()] = null;
            }

            while ((i < base.getM()) && (!s))
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

            while ((i <= base.getM()))
            {
                a[i] = (int)(p.getInfo()[i - 1]);
                b[i + 1] = p.getApuntadores()[i];
                p.getApuntadores()[i++] = null;
            }

            Pagina q = new Pagina(base.getN());
            inicializar(q);
            i = 0;
            if (separar == 0)
            {
                p.setCont(base.getN());
                q.setCont(base.getN() + 1);
                q.getInfo()[0] = a[base.getN()];

                while (i < base.getN())
                {
                    p.getInfo()[i] = a[i];
                    p.getApuntadores()[i] = b[i];
                    q.getInfo()[i + 1] = a[i + base.getN() + 1];
                    q.getApuntadores()[i] = b[i + base.getN() + 1];
                    i++;
                }
                q.getApuntadores()[base.getM()] = temp;
                p.getApuntadores()[base.getM()] = q;
            }
            else
            {
                p.setCont(base.getN()); q.setCont(base.getN());

                while (i < base.getN())
                {
                    p.getInfo()[i] = a[i];
                    p.getApuntadores()[i] = b[i];
                    q.getInfo()[i] = a[i + base.getN() + 1];
                    q.getApuntadores()[i] = b[i + base.getN() + 1];
                    i++;
                }
            }
            p.getApuntadores()[base.getN()] = b[base.getN()];
            q.getApuntadores()[base.getN()] = b[base.getM1()];
            subir[0] = a[base.getN()];
            return q;
        }

        private void unirBMas(Pagina q, Pagina r, Pagina p, int i, Pila<Componente> pi, int x, int posicion)
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
                if ((r.getCont() < base.getN()) && (p.getCont() > base.getN()))
                {
                    base.cambio(r, q, p, i, x);
                    r.getApuntadores()[r.getCont()] = p.getApuntadores()[0];
                    this.cizquierda_apunt(p, 0, p.getCont() + 1);
                    terminar = 1;
                }
                /*2*/
                else
                        if ((p.getCont() < base.getN()) && (r.getCont() > base.getN()))
                {
                    base.cambio(p, q, r, i, x);
                    this.cderechaApunt(p, 0);
                    p.getApuntadores()[0] = r.getApuntadores()[r.getCont() + 1];
                    r.getApuntadores()[r.getCont() + 1] = null;
                    terminar = 1;
                }
                else
                {
                    j = r.getCont();
                    if (r.getApuntadores()[0] == null)
                        r.getApuntadores()[base.getM()] = p.getApuntadores()[base.getM()];
                    else
                        r.getInfo()[j++] = q.getInfo()[i];
                    k = 0;
                    while (k <= p.getCont() - 1)
                        r.getInfo()[j++] = (int)p.getInfo()[k++];

                    r.setCont(j);
                    retirar(q, i);
                    k = 0;
                    j = base.getM() - p.getCont();

                    while (p.getApuntadores()[k] != null)
                        r.getApuntadores()[j++] = p.getApuntadores()[k++];
                    p = null;
                    /*3*/
                    if (q.getCont() == 0)
                    {
                        q.getApuntadores()[i + 1] = null;
                        /*4*/
                        if (pi.esVacia())
                        {
                            q = null;
                        }
                    }
                    else
                        this.cizquierda_apunt(q, i + 1, q.getCont() + 1);
                    /*5*/
                    if (q != null)
                        /*6*/
                        if (q.getCont() >= base.getN())
                            terminar = 1;

                        else
                        {
                            t = q;
                            /*7*/
                            if (!pi.esVacia())
                            {
                                objeto = (Componente)pi.desapilar();
                                q = objeto.getP();
                                i = objeto.getV();
                                compara = Comparable.Comparador(q.getInfo()[0], x);

                                if (compara <= 0)
                                {
                                    p = t;
                                    r = q.getApuntadores()[i - 1];
                                    i--;
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

                    else
                    {
                        terminar = 1;
                        base.setRaiz(r);
                    }
                }
            }
        }



        new public ListaCD<int> getHojas()
        {
            return (base.getHojas());
        }


        new public bool esVacio()
        {
            return (base.esVacio());
        }



        new public ListaCD<int> inOrden()
        {
            return (base.inOrden());
        }


        public int getPesoBMas()
        {
            return (getPesoBMas(base.getRaiz()));
        }

        private int getPesoBMas(Pagina r)
        {
            int cant = 0;
            while (r.getApuntadores()[0] != null)
            {
                r = r.getApuntadores()[0];
            }
            while (r != null)
            {
                cant += r.getCont();
                r = r.getApuntadores()[base.getM()];
            }
            return (cant);
        }


        new public int getAltura()
        {
            return (base.getAltura());
        }


        public String listar_vsam()
        {
            return (listar_vsam(this.vsam));
        }


        public String listar_vsam(Pagina vsam)
        {
            String msg = "";
            int i;
            while (vsam != null)
            {
                i = 0;
                while (i < vsam.getCont())
                    msg += vsam.getInfo()[i++].ToString() + "-->";
                vsam = vsam.getApuntadores()[base.getM()];
            }
            return msg;
        }


        new public String imprime()
        {
            return (base.imprime());
        }



        new public ArbolBM clonar()
        {
            ArbolBM clon = new ArbolBM(this.getN());
            if (base.getRaiz() == null)
                return (clon);
            clon.setRaiz(clonar(base.getRaiz()));
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


        public void limpiarBMas()
        {
            base.setRaiz(null);
            this.vsam = base.getRaiz();
        }
    }
}
