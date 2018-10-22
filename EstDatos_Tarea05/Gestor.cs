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
        arbolB = new ArbolB();
        arbolBM = new ArbolBM();
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
        arbolBM.insertarBMas(dato);
    }

    public string ImprmirArboles(int tipo)
    {
        string  resultado= "";
        if (tipo == 1)
        {
            if (arbolB.getRaiz()!=null)
            {
             resultado= arbolB.imprime();
            }
            else
            {
                resultado = "Arbol esta vacio";
            }
            
           
        }
        else
        {
            if (arbolBM.getRaiz()!=null)
            {
                resultado = arbolBM.imprime();
            }
            else
            {
                resultado = "Arbol B+ esta vacio";
            }
            
        }

        return resultado;
    }
}