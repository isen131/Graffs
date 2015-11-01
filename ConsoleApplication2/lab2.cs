using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProg;
namespace ConsoleApplication2

{
    class lab2
    {
        static void Main(string[] args)
        {
            Graff gr = new Graff();
            if (gr.input() == false)
            {
                Console.WriteLine("Файл не найден или не соответствует формату.");
                Console.ReadKey();
                return;
            }
            int count=0;
            bool check=true;
            gr.floid();
            Console.WriteLine("Матрица смежности:");
            Console.WriteLine();
            Console.Write("    ");
            for (int i = 0; i < gr.col; i++)
            {
                Console.Write("{0,4:d}",(char)(65 + i));
            }
            Console.WriteLine();
            for (int i = 0; i < gr.col; i++)
            {
                Console.Write("{0,4:d}",(char)(65 + i));
                if (gr.a[i, i] != 0)
                {
                    count++;
                }
                for (int j = 0; j < gr.col; j++)
                {
                    if (gr.a[i, j] != gr.a[j, i])
                    {
                        check = false;
                    }
                    if (gr.a[i, j] == 0)
                    {
                        Console.Write("   -");
                    }
                    else
                    {
                        Console.Write("{0,4:d}",gr.w[i, j]);
                    }
                }
                Console.WriteLine();
            }


            Console.WriteLine();
            Console.WriteLine();
            if (count == 0)
                Console.WriteLine("Петли отсутствуют");
            else
                Console.WriteLine("В графе имеются петли:" + count);
            if (check == true)
            {
                Console.WriteLine("Граф неориентированный");
            }
            else
            {
                Console.WriteLine("Граф ориентированный");
            }
            Console.WriteLine();
            Console.WriteLine();            
            Console.WriteLine("Матрица расстояний:");
            Console.Write("    ");
            for (int i = 0; i < gr.col; i++)
            {
                Console.Write("{0,4:d}", (char)(65 + i));
            }
            Console.WriteLine();
            for (int i = 0; i < gr.col; i++)
            {
                Console.Write("{0,4:d}", (char)(65 + i));
                for (int j = 0; j < gr.col; j++)
                {
                    if (i == j)
                    {
                        Console.Write("   0");
                    }
                    else
                    {
                        if (gr.w[i, j] == 0)
                        {
                            Console.Write("   -");
                        }
                        else
                        {
                            Console.Write("{0,4:d}", gr.w[i, j]);
                        }
                    }
                }
                Console.WriteLine();
            }
            gr.dfs(0,1);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Принаджежность к компонентам связности:");
            for (int i = 0; i < gr.col; i++)
            {
                Console.Write((char)(65 + i) + " ");
            }
            Console.WriteLine();
            for (int i = 0; i < gr.col; i++)
            {
                Console.Write(gr.def[i]+" ");
            }
                       
            
            //System.Threading.Thread.Sleep(10000);
            //return;
            while (true) ;
        }
    }
}
