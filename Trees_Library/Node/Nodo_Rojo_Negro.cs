namespace ArbolAVL
{
    public class Nodo_Rojo_Negro
    {
        public Color color;
        public Nodo_Rojo_Negro izquierdo;
        public Nodo_Rojo_Negro derecho;
        public Nodo_Rojo_Negro padre;
        public int dato;

        public Nodo_Rojo_Negro(int dato) { this.dato = dato; }
        public Nodo_Rojo_Negro(Color color) { this.color = color; }
        public Nodo_Rojo_Negro(int dato, Color color) { this.dato = dato; this.color = color; }
    }
}