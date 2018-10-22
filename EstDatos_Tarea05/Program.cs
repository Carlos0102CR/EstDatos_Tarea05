using System;
using Trees_Library.Gestor;

namespace ArbolAVL
{
    public class Program
    {
        private static Gestor_AVL gestorAVL;
        private static Gestor_Rojo_Negro gestorRojoNegro;
        private static Gestor_B gestorB;

        public static void Main(string[] args)
        {
            gestorAVL = new Gestor_AVL();
            gestorRojoNegro = new Gestor_Rojo_Negro();

            int[] datos = {1, 3, 5, 7, 9, 10, 13, 15, 17, 18};

            foreach (var data in datos)
            {
                gestorAVL.insertarArbol(data);
                gestorRojoNegro.insertarArbol(data);
            }
            

            bool salir = false;
            do
            {
                Console.WriteLine("\nArbol AVL y Rojo-Negro\n");
                Console.WriteLine("\n1.Insertar" +
                "\n2.Mostrar Arbol" +
                "\n3.Insertar en Arbol B" +
                "\n4.Insertar en Arbol B+" +
                "\n5.Mostrar ArbolB o ArbolB+" +
                "\n6.Salir");

                int opcionSeleccionada = seleccionarOpcion();
                salir = ejecutarSeleccion(opcionSeleccionada);
            } while (!salir);
        }

        public static int seleccionarOpcion()
        {
            Console.Write("\nSeleccione su opción: ");
            int.TryParse(Console.ReadLine(), out int opcion);
            Console.WriteLine("\n");

            return opcion;
        }

        public static bool ejecutarSeleccion(int opcion)
        {
            bool salir = false;

            switch (opcion)
            {
                case 1:
                    insertarElemento();
                    break;

                case 2:
                    Console.WriteLine("Arbol AVL:");
                    Console.WriteLine(gestorAVL.mostarArbolInOrden());
                    Console.WriteLine("");
                    Console.WriteLine("Arbol Rojo-Negro:");
                    gestorRojoNegro.mostarArbolInOrden();
                    break;
                case 3:
                    InsertarEnArbolB();

                    break;
                case 4:
                    InsertarEnArbolBM();

                    break;
                case 5:
                    ImprimirArbolesB();

                    break;
             

                case 6:
                    salir = true;
                    break;

                default:
                    Console.WriteLine("Opcion invalida");
                    break;
            }

            return salir;
        }

        private static void ImprimirArbolesB()
        {
            Console.WriteLine("Selccione una opcion valida");
            Console.WriteLine("\n1.Arbol B" +
                              "\n2.Arbol B+");
            int.TryParse(Console.ReadLine(), out int valor);
            switch (valor)
            {
                case 1:
                    Console.WriteLine(gestorB.ImprmirArboles(valor));
                    break;
                case 2:
                    Console.WriteLine(gestorB.ImprmirArboles(valor));
                    break;

                default:
                    Console.WriteLine("Opcion invalida");
                    break;
            }

        }

        private static void InsertarEnArbolBM()
        {
            Console.Write("Digite el valor: ");
            int.TryParse(Console.ReadLine(), out int valor);
            gestorB.InsertarEnArbolBM(valor);
        }

        private static void InsertarEnArbolB()
        {

            Console.Write("Digite el valor: ");
            int.TryParse(Console.ReadLine(), out int valor);
            gestorB.InsertarEnArbolB(valor);

        }

        public static void insertarElemento()
        {
            Console.Write("Digite el valor: ");
            int.TryParse(Console.ReadLine(), out int valor);
            gestorRojoNegro.insertarArbol(valor);

            if (gestorAVL.insertarArbol(valor))
            {
                Console.WriteLine("Nodo insertado");
            }
            else
            {
                Console.WriteLine("Elemento no insertado, el valor ya existe en el arbol");
            }
        }
    }
}
