using Trees_Library.AVL;
using Trees_Library.B;
using Trees_Library.B_;

public class Gestor
{
    private Arbol_AVL arbol;
    private ArbolB arbolB;
    private ArbolBM arbolBM;

    public Gestor()
    {
        arbol = new Arbol_AVL();
    }

    public Gestor(int gradoArbol)
    {
        arbolB = new ArbolB(gradoArbol);
        arbolBM = new ArbolBM(gradoArbol);
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

    public void InsertarEnArbolB(int dato)
    {
        arbolB.insertar(dato);
    }
    public void InsertarEnArbolBM(int dato)
    {
        arbolBM.insertar(dato);
    }

    public string ImprmirArboles(int tipo)
    {
        if (tipo != 1)
        {
            return arbolB.imprime();
        }
        else
        {
            return arbolBM.imprime();
        }
    }
}