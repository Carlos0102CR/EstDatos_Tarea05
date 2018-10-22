namespace Trees_Library.Gestor
{
    public class Gestor_Rojo_Negro:IGestor
    {
        private Arbol_Rojo_Negro arbol;

        public Gestor_Rojo_Negro()
        {
            arbol = new Arbol_Rojo_Negro();
        }

        public string mostarArbolInOrden()
        {
            arbol.ImprimirArbol();
            return "fin";
        }

        public bool insertarArbol(int valor)
        {
            arbol.Insertar(valor);
            return true;
        }
    }
}