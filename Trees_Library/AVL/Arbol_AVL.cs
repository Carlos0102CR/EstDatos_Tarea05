using ArbolAVL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees_Library.AVL
{
    public class Arbol_AVL
    {
         private Nodo raiz;
        private string concatenacion;

        public Arbol_AVL()
        {
            raiz = null;
            concatenacion = "";
        }

        public bool insertarElemento(int dato)
        {
            Nodo nodo = new Nodo { elemento = dato };

            if (!encontrarValor(dato))
            {
                raiz = insertarRecursivo(raiz, nodo);
                return true;
            }
            else
            {
                return false;
            }
        }

        private Nodo insertarRecursivo(Nodo nodoRaiz, Nodo nuevo)
        {
            if (nodoRaiz == null)
            {
                nodoRaiz = nuevo;
                return nodoRaiz;
            }
            else if (nuevo.elemento < nodoRaiz.elemento)
            {
                nodoRaiz.nodoIzquierdo = insertarRecursivo(nodoRaiz.nodoIzquierdo, nuevo);
                nodoRaiz = balancearArbol(nodoRaiz);
            }
            else
            {
                nodoRaiz.nodoDerecho = insertarRecursivo(nodoRaiz.nodoDerecho, nuevo);
                nodoRaiz = balancearArbol(nodoRaiz);
            }

            return nodoRaiz;
        }

        private Nodo balancearArbol(Nodo nodoRaiz)
        {
            int factorEquilibrio = obtenerFactorEquilibrioNodo(nodoRaiz);

            if (factorEquilibrio > 1)
            {
                if (obtenerFactorEquilibrioNodo(nodoRaiz.nodoIzquierdo) > 0)
                {
                    nodoRaiz = rotarDobleIzquierdo(nodoRaiz);
                }
                else
                {
                    nodoRaiz = rotarSimpleDerecho(nodoRaiz);
                }
            }
            else if (factorEquilibrio < -1)
            {
                if (obtenerFactorEquilibrioNodo(nodoRaiz.nodoDerecho) > 0)
                {
                    nodoRaiz = rotarSimpleIzquierdo(nodoRaiz);
                }
                else
                {
                    nodoRaiz = rotarDobleDerecho(nodoRaiz);
                }
            }

            return nodoRaiz;
        }

        public bool encontrarValor(int valor)
        {
            if (encontrarValor(valor, raiz) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Nodo encontrarValor(int valor, Nodo nodoRaiz)
        {
            if (nodoRaiz != null)
            {
                if (valor == nodoRaiz.elemento)
                {
                    return nodoRaiz;
                }
                else if (valor < nodoRaiz.elemento)
                {
                    return encontrarValor(valor, nodoRaiz.nodoIzquierdo);
                }
                else
                {
                    return encontrarValor(valor, nodoRaiz.nodoDerecho);
                }
            }
            else
            {
                return null;
            }
        }

        private void arbolPreOrden(Nodo raiz)
        {
            concatenacion = concatenacion + raiz.elemento + "\n";

            if (raiz.nodoIzquierdo != null)
            {
                arbolPreOrden(raiz.nodoIzquierdo);
            }

            if (raiz.nodoDerecho != null)
            {
                arbolPreOrden(raiz.nodoDerecho);
            }
        }

        private void arbolInOrden(Nodo raiz)
        {
            if (raiz.nodoIzquierdo != null)
            {
                arbolInOrden(raiz.nodoIzquierdo);
            }

            concatenacion = concatenacion + raiz.elemento + "\n";

            if (raiz.nodoDerecho != null)
            {
                arbolInOrden(raiz.nodoDerecho);
            }
        }

        private void arbolPostOrden(Nodo raiz)
        {
            if (raiz.nodoIzquierdo != null)
            {
                arbolPostOrden(raiz.nodoIzquierdo);
            }

            if (raiz.nodoDerecho != null)
            {
                arbolPostOrden(raiz.nodoDerecho);
            }

            concatenacion = concatenacion + raiz.elemento + "\n";
        }

        public string mostrarArbolPreorden()
        {
            concatenacion = "";
            arbolPreOrden(raiz);
            return concatenacion;
        }

        public string mostarArbolInOrden()
        {
            concatenacion = "";
            arbolInOrden(raiz);
            return concatenacion;
        }

        public string mostarArbolPostOrden()
        {
            concatenacion = "";
            arbolPostOrden(raiz);
            return concatenacion;
        }

        private int nodoMayorValor(int nodoIzquierdo, int nodoDerecho)
        {
            return nodoIzquierdo > nodoDerecho ? nodoIzquierdo : nodoDerecho;
        }

        private int obtenerAlturaNodoRaiz(Nodo nodoRaiz)
        {
            int altura = 0;

            if (nodoRaiz != null)
            {
                int izquierdo = obtenerAlturaNodoRaiz(nodoRaiz.nodoIzquierdo);
                int derecho = obtenerAlturaNodoRaiz(nodoRaiz.nodoDerecho);
                int maximo = nodoMayorValor(izquierdo, derecho);

                altura = maximo + 1;
            }

            return altura;
        }

        private int obtenerFactorEquilibrioNodo(Nodo nodoRaiz)
        {
            int izquierdo = obtenerAlturaNodoRaiz(nodoRaiz.nodoIzquierdo);
            int derecho = obtenerAlturaNodoRaiz(nodoRaiz.nodoDerecho);

            return izquierdo - derecho;
        }

        private Nodo rotarDobleIzquierdo(Nodo nodoRaiz)
        {
            Nodo nodo = nodoRaiz.nodoIzquierdo;
            nodoRaiz.nodoIzquierdo = nodo.nodoDerecho;
            nodo.nodoDerecho = nodoRaiz;

            return nodo;
        }

        private Nodo rotarDobleDerecho(Nodo nodoRaiz)
        {
            Nodo nodo = nodoRaiz.nodoDerecho;
            nodoRaiz.nodoDerecho = nodo.nodoIzquierdo;
            nodo.nodoIzquierdo = nodoRaiz;

            return nodo;
        }

        private Nodo rotarSimpleDerecho(Nodo nodoRaiz)
        {
            Nodo nodo = nodoRaiz.nodoIzquierdo;
            nodoRaiz.nodoIzquierdo = rotarDobleDerecho(nodo);

            return rotarDobleIzquierdo(nodoRaiz);
        }

        private Nodo rotarSimpleIzquierdo(Nodo nodoRaiz)
        {
            Nodo nodo = nodoRaiz.nodoDerecho;
            nodoRaiz.nodoDerecho = rotarDobleIzquierdo(nodo);

            return rotarDobleDerecho(nodoRaiz);
        }

        public bool verificarArbolVacio()
        {
            return raiz != null ? false : true;
        }
    }
}