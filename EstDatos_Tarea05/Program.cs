using System;

namespace ArbolAVL
{
    public class Program
    {
        private static Gestor gestor;

        public static void Main(string[] args)
        {
            gestor = new Gestor();

            bool salir = false;
            do
            {
                Console.WriteLine("\n1.Insertar" +
                "\n2.Mostrar Arbol" +
                "\n3.Salir");

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
                    Console.WriteLine("\n1.Pre-Orden" +
                        "\n2.In-Orden"
                        + "\n3.Post-Orden");

                    int seleccion = seleccionarOpcion();
                    mostrarArbol(seleccion);
                    break;

                case 3:
                    salir = true;
                    break;

                default:
                    Console.WriteLine("Opcion invalida");
                    break;
            }

            return salir;
        }

        public static void insertarElemento()
        {
            Console.Write("Digite el valor: ");
            int.TryParse(Console.ReadLine(), out int valor);

            if (gestor.insertarArbol(valor))
            {
                Console.WriteLine("Nodo insertado");
            }
            else
            {
                Console.WriteLine("Elemento no insertado, el valor ya existe en el arbol");
            }
        }

        public static void mostrarArbol(int opcion)
        {
            if (!gestor.verificarArbolVacio())
            {
                mostrarArbolBinario(opcion);
            }
            else
            {
                Console.WriteLine("El arbol esta vacio");
            }
        }

        public static void mostrarArbolBinario(int opcion)
        {
            switch (opcion)
            {
                case 1:
                    Console.WriteLine(gestor.mostarArbolPreOrden());
                    break;
                case 2:
                    Console.WriteLine(gestor.mostarArbolInOrden());
                    break;
                case 3:
                    Console.WriteLine(gestor.mostarArbolPostOrden());
                    break;
            }
        }
    }
}
