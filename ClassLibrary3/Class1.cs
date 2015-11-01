using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebProg
{
    public class Determinant
    {
        public int[] b;
        public int[,] a;
        public int[][,] d;
        public int col;
        public double det;
        public int opr=0;
        public double sum = 0;
        public const int maxvalue = 1000;

        public bool input()
        {
            StreamReader f=null;
            try
            {
                f = new StreamReader("input.txt");
            }
            catch
            {
                return false;
            }
            string s = f.ReadLine();
            col = int.Parse(s);
            a = new int[col, col];
            b = new int[col];
            for (int i = 0; i < col; i++)
            {
                string[] buf = f.ReadLine().Split();
                for (int j = 0; j < col; ++j)
                    try
                    {
                        a[i, j] = int.Parse(buf[j]);
                        if(Math.Abs(a[i,j])>maxvalue)
                        {
                            return false;
                        }
                    }
                    catch
                    {
                        return false;
                    }
            }
            for (int i = 0; i < col; i++)
            {
                try
                {
                    string[] buf = f.ReadLine().Split();
                    b[i] = int.Parse(buf[0]);
                }
                catch
                {
                    return false;
                }
            }
            f.Close();
            return true;          
        }
        public int pro(int[,] c, int pr)
        {
            if (pr == 1)
            {
                return c[0, 0];
            }
            else
            {
                int ukaz = 0;
                pr--;
                int[,] e = c;
                c = new int[pr, pr];
                opr = 0;
                for (ukaz = 0; ukaz < pr + 1; ukaz++)
                {
                    for (int j = 0; j < ukaz; j++)
                    {
                        for (int i = 0; i < pr; i++)
                        {
                            c[i, j] = e[i +1, j];
                        }
                    }
                    for (int j = ukaz; j < pr; j++)
                    {
                        for (int i = 0; i < pr; i++)
                        {
                            c[i, j] = e[i + 1, j + 1];
                        }
                    }
                    opr += (ukaz % 2 == 0 ? 1 : -1) * e[0, ukaz] * pro(c, pr);                  
                }
            }
            return opr;
        }
        public int[][,] alter()
        {
            d = new int[col][,];
            for (int ukaz = 0; ukaz < col; ukaz++)
            {
                int[,] c = new int[col, col];
                for (int j = 0; j < col; j++)
                {
                    if (j == ukaz)
                    {
                        for (int i = 0; i < col; i++)
                        {
                            c[i, j] = b[i];
                        }
                    }
                    else
                    {
                        for (int i = 0; i < col; i++)
                        {
                            c[i, j] = a[i, j];
                        }
                    }
                }
                d[ukaz] = c;
            }   
            return d; 
        }
        public string analiz()
        {
            if (pro(a, col) != 0)
            {
                return korni();
            }
            bool asd=true;
            for (int i = 0; i < col; i++)
            {
                if (pro(d[i],col)==0)
                {
                    asd=false;
                }
            }
            if (asd == false)
            {
                return "Система недоопределена";
            }
            else { return "Система несовместна"; }
        }
        public string korni()
        {
            string arrx="Корни уравнения:"+Convert.ToChar(10);
            for (int i = 0; i < col; i++)
            {
                double x = Math.Round((pro(d[i], col) / det),3);
                arrx += "x" + (i + 1) + "= " + x + Convert.ToChar(10);
            }            
            return arrx;
        }        
    }
    public class Graff
    {
        public int col;
        public const int maxvalue = 100;
        public int[,] a;
        public int [,]w;
        public int[] def;
        public int count = 0;
        public bool input()
        {
            StreamReader f = null;
            try
            {
                f = new StreamReader("input.txt");
            }
            catch
            {
                return false;
            }
            string s = f.ReadLine();
            col = int.Parse(s);
            a = new int[col, col];            
            for (int i = 0; i < col; i++)
            {
                string[] buf = f.ReadLine().Split();
                for (int j = 0; j < col; ++j)
                    try
                    {
                        a[i, j] = int.Parse(buf[j]);
                        if (Math.Abs(a[i, j]) > maxvalue||a[i,j]<0)
                        {
                            return false;
                        }
                    }
                    catch
                    {
                        return false;
                    }
            }            
            f.Close();
            def = new int[col];
            for (int i = 0; i < col; i++)
            {
                def[i] = 0;
            }
            return true;
        }
        public void floid()
        {
            bool [,]nal = new bool[col,col];
            w = new int[col, col];
            for (int i = 0; i < col; i++)
                for (int j = 0; j < col; j++)
                {
                    w[i, j] = a[i, j];
                    nal[i, j] = false;
                } 
            for (int k = 0; k < col; k++)
            {
                for (int i = 0; i < col; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        if (w[i, k] != 0 && w[k, j] != 0) 
                        {
                            nal[i, j] = true;
                            if (w[i, j] != 0)
                            {
                                if (i != j)
                                {
                                    w[i, j] = Math.Min(w[i, j], w[i, k] + w[k, j]);                                    
                                }
                            }
                            else w[i, j] = w[i, k] + w[k, j];
                        }
                    }
                }
            }
            for (int i = 0; i < col; i++)
                    for (int j = 0; j < col; j++)
                        if (a[i, j] == 0 && a[j, i]==0 && nal[i,j] == false)
                        {
                            w[i, j] = 0;
                        }
        }
        public void dfs(int u,int con)
        {            
            count=con;
            for (int i = 0; i < col; i++)
            {
                if (i == u)
                {
                    def[i] = count;
                    continue;
                }
                if (w[u, i] != 0 && w[i, u] != 0 && def[i] == 0)
                {
                    def[i] = count;
                    dfs(i, count);
                }
            }
            for (int j = 0; j < col;j++ )
            {
                if (def[j] == 0)
                {                                      
                    dfs(j,count+1);
                }
            }            
        }
    }
	//public class Collect
	//{
	//	public enum PartType { Number, Identifier, Sign, Parenthesis }
	//	public struct Part
	//	{
	//		public string Value;
	//		public PartType Type;
	//		public int Position;
	//	}
	//	public static List<Part> Parts;
	//	public static string All;        
	//	public void Iniz(string q)
	//	{
	//		All = q;
	//	}
	//	public void Split()
	//	{
	//		if (All.Length > 100)
	//		{
	//			throw new Exception("Слишком динная строка.");
	//		}            
	//		Parts = new List<Part>();
	//		Part prt;
	//		int j = 0;
	//		while(j < All.Length)
	//		{
	//			switch (All[j])
	//			{
	//				case ' ':
	//					j++;
	//					break;
	//				case '+':
	//				case '-':
	//				case '*':
	//				case '/':
	//				case '^':
	//					prt.Value = All[j].ToString();
	//					prt.Type = PartType.Sign;
	//					prt.Position = j + 1;
	//					Parts.Add(prt);
	//					j++;
	//					break;
	//				case ')':
	//				case '(':
	//					prt.Value = All[j].ToString();
	//					prt.Type = PartType.Parenthesis;
	//					prt.Position = j + 1;
	//					Parts.Add(prt);
	//					j++;
	//					break;
	//				default:
	//					throw new Exception ("Недопустимый символ: позиция " + (j + 1).ToString());
	//			}
	//		}

	//	}


	//}
}
