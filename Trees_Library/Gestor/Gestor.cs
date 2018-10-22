using Trees_Library.AVL;

namespace Trees_Library.Gestor
{
    public class Gestor_AVL
    {
        private Arbol_AVL arbol;

        public Gestor_AVL()
        {
            arbol = new Arbol_AVL();
        }

        public string mostarArbolPreOrden()
        {
            return arbol.mostrarArbolPreorden();
        }

        public string mostarArbolInOrden()
        {
            return arbol.mostarArbolInOrden();
        }

        public string mostarArbolPostOrden()
        {
            return arbol.mostarArbolPostOrden();
        }

        public bool verificarArbolVacio()
        {
            return arbol.verificarArbolVacio();
        }

        public bool insertarArbol(int valor)
        {
            return arbol.insertarElemento(valor);
        }
    }
}