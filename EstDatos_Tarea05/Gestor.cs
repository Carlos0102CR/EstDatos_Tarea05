using Trees_Library.AVL;

public class Gestor
{
    private Arbol_AVL arbol;

    public Gestor()
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