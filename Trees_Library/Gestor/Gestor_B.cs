using Trees_Library.AVL;
using Trees_Library.B;
using Trees_Library.B_;

namespace Trees_Library.Gestor
{
    public class Gestor_B
    {
        private ArbolB arbolB;
        private ArbolBM arbolBM;

        public Gestor_B()
        {
            arbolB = new ArbolB();
            arbolBM = new ArbolBM();
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
            string resultado = "";
            if (tipo == 1)
            {
                if (arbolB.getRaiz() != null)
                {
                    resultado = arbolB.imprime();
                }
                else
                {
                    resultado = "Arbol esta vacio";
                }


            }
            else
            {
                if (arbolBM.getRaiz() != null)
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
}