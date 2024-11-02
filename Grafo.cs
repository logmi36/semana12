using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s12p30
{
    public class Grafo
    {

        int[,] mAdyacencia;
        int[] indegree;
        int nodos;

        public Grafo(int nodos)
        {
            this.nodos = nodos;
            mAdyacencia = new int[nodos, nodos];
            indegree = new int[nodos];
        }

        public void AdicionarArista(int nodoInicio, int nodoFinal)
        {
            mAdyacencia[nodoInicio, nodoFinal] = 1;
        }

        public void MostrarAdyacencia()
        {
            int n = 0;
            int m = 0;

            Console.ForegroundColor = ConsoleColor.Yellow;

            for (n = 0; n < nodos; n++)
            {
                Console.Write("\t{0}", n);
            }

            Console.WriteLine();

            for (n = 0; n < nodos; n++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(n);

                for (m = 0; m < nodos; m++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\t{0}", mAdyacencia[n, m]);
                }
                Console.WriteLine();
            }

        }

        public void CalcularIndegree()
        {
            int n = 0;
            int m = 0;

            for (n = 0; n < nodos; n++)
            {
                for (m = 0; m < nodos; m++)
                {
                    if (mAdyacencia[m, n] == 1)
                        indegree[n]++;
                }
            }
        }

        public void MostrarInDregree()
        {
            int n = 0;
            Console.ForegroundColor = ConsoleColor.White;
            for (n = 0; n < nodos; n++)
            {
                Console.WriteLine("Nodo: {0},{1}", n, indegree[n]);
            }

        }


        public int EncuentraI0()
        {
            bool encontrado = false;
            int n = 0;

            for (n = 0; n < nodos; n++)
            {
                if (indegree[n] == 0)
                {
                    encontrado = true;
                    break;
                }
            }

            if (encontrado)
                return n;
            else
                return -1;
        }

        public void DrecremenarInDigree(int nodo)
        {
            indegree[nodo] = -1;
            int n = 0;

            for (n = 0; n < nodos; n++)
            {
                if (mAdyacencia[nodo, n] == 1)
                {
                    indegree[n]--;
                }
            }
        }


    }

}
