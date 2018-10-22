namespace ArbolAVL
{
    public class Nodo
    {
        private int dato;
        private Nodo izquierdo;
        private Nodo derecho;

        public Nodo()
        {
            dato = 0;
            izquierdo = null;
            derecho = null;
        }

        public int elemento
        {
            get { return dato; }
            set { dato = value; }
        }

        public Nodo nodoIzquierdo
        {
            get { return izquierdo; }
            set { izquierdo = value; }
        }

        public Nodo nodoDerecho
        {
            get { return derecho; }
            set { derecho = value; }
        }
    }
}