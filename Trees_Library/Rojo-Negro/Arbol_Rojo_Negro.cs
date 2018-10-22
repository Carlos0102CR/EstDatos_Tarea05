using System;
using ArbolAVL;

namespace Trees_Library
{
    public class Arbol_Rojo_Negro
    {
        private Nodo_Rojo_Negro raiz;
        
        public Arbol_Rojo_Negro() { }
        
        private void rotacionIzq(Nodo_Rojo_Negro x)
        {
            Nodo_Rojo_Negro Y = x.derecho; // set Y
            x.derecho = Y.izquierdo;//cambia el subarbol izq de y en el subarbol derecho de x
            if (Y.izquierdo != null)
            {
                Y.izquierdo.padre = x;
            }
            if (Y != null)
            {
                Y.padre = x.padre;//linkea el padre x a y
            }
            if (x.padre == null)
            {
                raiz = Y;
            }else if (x == x.padre.izquierdo)
            {
                x.padre.izquierdo = Y;
            }
            else
            {
                x.padre.derecho = Y;
            }
            Y.izquierdo = x; //pone x en la izq de y
            if (x != null)
            {
                x.padre = Y;
            }
 
        }
        private void rotacionDer(Nodo_Rojo_Negro Y)
        {
            Nodo_Rojo_Negro X = Y.izquierdo;
            Y.izquierdo = X.derecho;
            if (X.derecho != null)
            {
                X.derecho.padre = Y;
            }
            if (X != null)
            {
                X.padre = Y.padre;
            }
            if (Y.padre == null)
            {
                raiz = X;
            }
            if (Y == Y.padre.derecho)
            {
                 Y.padre.derecho = X;
            }
            if(Y == Y.padre.izquierdo)
            {
                 Y.padre.izquierdo = X;
            }
 
            X.derecho = Y;
            if (Y != null)
            {
                Y.padre = X;
            }
        }
        
        public void imprimirArbol()
        {
            if (raiz == null)
            {
                Console.WriteLine("El arbol esta vacio!");
                return;
            }
            if (raiz != null)
            {
                ImprimirInOrden(raiz);
            }
        }
        
        public Nodo_Rojo_Negro buscar(int key)
        {
            bool isEncontrado = false;
            Nodo_Rojo_Negro temp = raiz;
            Nodo_Rojo_Negro item = null;
            while (!isEncontrado)
            {
                if (temp == null)
                {
                    break;
                }
                if (key < temp.dato)
                {
                    temp = temp.izquierdo;
                }
                if (key > temp.dato)
                {
                    temp = temp.derecho;
                }
                if (key == temp.dato)
                {
                    isEncontrado = true;
                    item = temp;
                }
            }
            if (isEncontrado)
            {
                Console.WriteLine("{0} was found", key);
                return temp;
            }
            else
            {
                Console.WriteLine("{0} not found", key);
                return null;
            }
        }
        
        public void insertar(int item)
        {
            Nodo_Rojo_Negro nuevoItem = new Nodo_Rojo_Negro(item);
            if (raiz == null)
            {
                raiz = nuevoItem;
                raiz.color = Color.Negro;
                return;
            }
            Nodo_Rojo_Negro Y = null;
            Nodo_Rojo_Negro X = raiz;
            while (X != null)
            {
                Y = X;
                if (nuevoItem.dato < X.dato)
                {
                    X = X.izquierdo;
                }
                else
                {
                    X = X.derecho;
                }
            }
            nuevoItem.padre = Y;
            if (Y == null)
            {
                raiz = nuevoItem;
            }
            else if (nuevoItem.dato < Y.dato)
            {
                Y.izquierdo = nuevoItem;
            }
            else
            {
                Y.derecho = nuevoItem;
            }
            nuevoItem.izquierdo = null;
            nuevoItem.derecho = null;
            nuevoItem.color = Color.Rojo;
            arreglarInsercion(nuevoItem);
        }
        private void ImprimirInOrden(Nodo_Rojo_Negro act)
        {
            if (act != null)
            {
                ImprimirInOrden(act.izquierdo);
                Console.Write("({0})", act.dato);
                ImprimirInOrden(act.derecho);
            }
        }
        private void arreglarInsercion(Nodo_Rojo_Negro item)
        {
            while (item != raiz && item.padre.color == Color.Rojo)
            {
                if (item.padre == item.padre.padre.izquierdo)
                {
                    Nodo_Rojo_Negro Y = item.padre.padre.derecho;
                    if (Y != null && Y.color == Color.Rojo)//Case 1: "tio" es rojo
                    {
                        item.padre.color = Color.Negro;
                        Y.color = Color.Negro;
                        item.padre.padre.color = Color.Rojo;
                        item = item.padre.padre;
                    }
                    else //Case 2: es negro
                    {
                        if (item == item.padre.derecho)
                        {
                            item = item.padre;
                            rotacionIzq(item);
                        }
                        //Case 3: cambiar color y rotar
                    item.padre.color = Color.Negro;
                    item.padre.padre.color = Color.Rojo;
                    rotacionDer(item.padre.padre);
                    }
 
                }
                else
                {
                    //lo mismo de arriba pero a la inversa
                    Nodo_Rojo_Negro X = null;
 
                    X = item.padre.padre.izquierdo;
                    if (X != null && X.color == Color.Negro)//Case 1
                    {
                         item.padre.color = Color.Rojo;
                         X.color = Color.Rojo;
                         item.padre.padre.color = Color.Negro;
                         item = item.padre.padre;
                    }
                    else //Case 2
                    {
                        if (item == item.padre.izquierdo)
                        {
                            item = item.padre;
                            rotacionDer(item);
                        }
                        //Case 3
                    item.padre.color = Color.Negro;
                    item.padre.padre.color = Color.Rojo;
                    rotacionIzq(item.padre.padre);
 
                    }
 
                }
                raiz.color = Color.Negro;//cambiar el color de la raiz a negro
            }
        }
        
        public void borrar(int llave)
        {
            
            Nodo_Rojo_Negro item = buscar(llave);
            Nodo_Rojo_Negro X = null;
            Nodo_Rojo_Negro Y = null;
 
            if (item == null)
            {
                Console.WriteLine("Nothing to delete!");
                return;
            }
            if (item.izquierdo == null || item.derecho == null)
            {
                Y = item;
            }
            else
            {
                Y = sucesorArbol(item);
            }
            if (Y.izquierdo != null)
            {
                X = Y.izquierdo;
            }
            else
            {
                X = Y.derecho;
            }
            if (X != null)
            {
                X.padre = Y;
            }
            if (Y.padre == null)
            {
                raiz = X;
            }
            else if (Y == Y.padre.izquierdo)
            {
                Y.padre.izquierdo = X;
            }
            else
            {
                Y.padre.izquierdo = X;
            }
            if (Y != item)
            {
                item.dato = Y.dato;
            }
            if (Y.color == Color.Negro)
            {
                arreglarBorrado(X);
            }
 
        }
        
        private void arreglarBorrado(Nodo_Rojo_Negro X)
        {
 
            while (X!= null && X != raiz && X.color == Color.Negro)
            {
                if (X == X.padre.izquierdo)
                {
                    Nodo_Rojo_Negro W = X.padre.derecho;
                    if (W.color == Color.Rojo)
                    {
                        W.color = Color.Negro; //case 1
                        X.padre.color = Color.Rojo; //case 1
                        rotacionIzq(X.padre); //case 1
                        W = X.padre.derecho; //case 1
                    }
                    if (W.izquierdo.color == Color.Negro && W.derecho.color == Color.Negro)
                    {
                        W.color = Color.Rojo; //case 2
                        X = X.padre; //case 2
                    }
                    else if (W.derecho.color == Color.Negro)
                    {
                        W.izquierdo.color = Color.Negro; //case 3
                        W.color = Color.Rojo; //case 3
                        rotacionDer(W); //case 3
                        W = X.padre.derecho; //case 3
                    }
                    W.color = X.padre.color; //case 4
                    X.padre.color = Color.Negro; //case 4
                    W.derecho.color = Color.Negro; //case 4
                    rotacionIzq(X.padre); //case 4
                    X = raiz; //case 4
                }
                else
                {
                    Nodo_Rojo_Negro W = X.padre.izquierdo;
                    if (W.color == Color.Rojo)
                    {
                        W.color = Color.Negro;
                        X.padre.color = Color.Rojo;
                        rotacionDer(X.padre);
                        W = X.padre.izquierdo;
                    }
                    if (W.derecho.color == Color.Negro && W.izquierdo.color == Color.Negro)
                    {
                        W.color = Color.Negro;
                        X = X.padre;
                    }
                    else if (W.izquierdo.color == Color.Negro)
                    {
                        W.derecho.color = Color.Negro;
                        W.color = Color.Rojo;
                        rotacionIzq(W);
                        W = X.padre.izquierdo;
                    }
                    W.color = X.padre.color;
                    X.padre.color = Color.Negro;
                    W.izquierdo.color = Color.Negro;
                    rotacionDer(X.padre);
                    X = raiz;
                }
            }
            if(X != null)
            X.color = Color.Negro;
        }
        private Nodo_Rojo_Negro minimo(Nodo_Rojo_Negro X)
        {
            while (X.izquierdo.izquierdo != null)
            {
                X = X.izquierdo;
            }
            if (X.izquierdo.derecho != null)
            {
                X = X.izquierdo.derecho;
            }
            return X;
        }
        private Nodo_Rojo_Negro sucesorArbol(Nodo_Rojo_Negro X)
        {
            if (X.izquierdo != null)
            {
                return minimo(X);
            }
            else
            {
                Nodo_Rojo_Negro Y = X.padre;
                while (Y != null && X == Y.derecho)
                {
                    X = Y;
                    Y = Y.padre;
                }
                return Y;
            }
        }
    }
}
