using System;
using System.Collections.Generic;
using System.Linq;

namespace Grammarpda
{

    class Program
    {
        class tra
        {
            public string shooroo;
            public string payan;
        }
        static string Filter(string str, List<char> charsToRemove)
        {
            foreach (char c in charsToRemove)
            {
                str = str.Replace(c.ToString(), String.Empty);
            }

            return str;
        }
        static void hazflanda(List<tra> transitions)
        {
            //remove landa
            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].payan == "#")
                {
                    string a = transitions[i].shooroo;
                    for (int j = 0; j < transitions.Count; j++)
                    {
                        if (transitions[j].payan.Contains(a))
                        {
                            string[] b = transitions[j].payan.Split('<');
                            string[] c = b[1].Split('>');
                            tra d = new tra();
                            d.shooroo = transitions[j].shooroo;
                            d.payan = b[0] + c[1];
                            if (d.payan.Length > 0)
                            {
                                transitions.Add(d);
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].payan == "#")
                {
                    transitions.RemoveAt(i);
                    i--;
                }
            }
        }

        static void hazfunit(List<tra> transitions)
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].payan.Length == 3 && transitions[i].payan.Contains(">"))
                {
                    List<char> charsToRemove = new List<char>() { '>', '<' };
                    string a = Filter(transitions[i].payan, charsToRemove);
                    if (char.IsUpper(char.Parse(a)))
                    {
                        string b = transitions[i].payan;
                        for (int j = 0; j < transitions.Count; j++)
                        {
                            if (transitions[j].shooroo == b)
                            {
                                tra d = new tra();
                                d.shooroo = transitions[i].shooroo;
                                d.payan = transitions[j].payan;
                                transitions.Add(d);
                            }
                        }
                        transitions.Remove(transitions[i]);
                        i--;
                    }
                }
            }


        }

        static void checkreachable(string var1, List<tra> transitions, List<Reach> reachable)
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].shooroo == var1)
                {
                    if (checkcaptal(transitions[i].payan))
                    {
                        for (int j = 0; j < reachable.Count; j++)
                        {
                            if (reachable[j].variable == transitions[i].shooroo)
                            {
                                reachable[j].condition = "reachable";
                                break;
                            }
                        }
                    }
                }
            }
        }

        static bool checkcaptal(string var1)
        {
            for (int i = 0; i < var1.Length; i++)
            {
                if (char.IsUpper(var1[i]))
                {
                    return false;
                }
            }
            return true;
        }

        static void checkrechablefromvar(string var1, List<Reach> reachable, List<tra> transitions)
        {
            for (int i = 0; i < reachable.Count; i++)
            {
                if (reachable[i].condition == "reachable")
                {
                    for (int j = 0; j < transitions.Count; j++)
                    {
                        if (transitions[j].shooroo == var1 && transitions[j].payan.Contains(reachable[i].variable))
                        {
                            for (int k = 0; k < reachable.Count; k++)
                            {
                                if (reachable[k].variable == var1)
                                {
                                    reachable[k].condition = "reachable";
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }



        static void checkrechablefromvar1(string var1, List<Reach> reachable, List<tra> transitions)
        {
            for (int i = 0; i < reachable.Count; i++)
            {
                if (reachable[i].condition == "reachable")
                {
                    for (int j = 0; j < transitions.Count; j++)
                    {
                        if (transitions[j].shooroo == reachable[i].variable && transitions[j].payan.Contains(var1))
                        {
                            for (int k = 0; k < reachable.Count; k++)
                            {
                                if (reachable[k].variable == var1)
                                {
                                    reachable[k].condition = "reachable";
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }


        static void removetra(string va1, List<tra> transitions)
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].shooroo == va1 || transitions[i].payan.Contains(va1))
                {
                    transitions.RemoveAt(i);
                    i--;
                }
            }
        }

        class Reach
        {
            public string variable;
            public string condition;
        }

        static string RandomStr()
        {
            string x = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var rand = new Random();
            int index = rand.Next(x.Length);
            return x[index].ToString();
        }

        static void hazftekrary(List<tra> transitions)
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                for (int j = i + 1; j < transitions.Count; j++)
                {
                    if (transitions[i].shooroo == transitions[j].shooroo)
                    {
                        if (transitions[i].payan == transitions[j].payan)
                        {
                            transitions.RemoveAt(j);
                            j--;
                        }
                    }
                }
            }

            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].shooroo == transitions[i].payan)
                {
                    transitions.RemoveAt(i);
                    i--;
                }
            }
        }

        static void tabdilchomsky(List<tra> transitions, List<string> var)
        {
            List<string> zakhire = new List<string>();
            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].payan.Length > 6)
                {
                    for (int j = 0; j < transitions[i].payan.Length / 6; j++)
                    {
                        string h = RandomStr();
                        h = "<" + h + ">";
                        while (var.Contains(h))
                        {
                            h = RandomStr();
                            h = "<" + h + ">";
                        }
                        int u = 6 * j;
                        tra d = new tra();
                        d.shooroo = h;
                        d.payan = transitions[i].payan.Substring(u, u + 6);
                        transitions.Add(d);
                        zakhire.Add(h);
                    }
                }


                for (int j = 0; j < zakhire.Count; j++)
                {
                    int u = 6 * j;
                    transitions[i].payan = transitions[i].payan.Replace(transitions[i].payan.Substring(u, u + 6), zakhire[j]);
                }
                for (int y = 0; y < zakhire.Count; y++)
                {
                    zakhire.RemoveAt(y);
                }
            }
        }



        //jiiiiiiiiiiiiiio







        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<string> var = new List<string>();
            List<tra> transitions = new List<tra>();
            List<Reach> reachable = new List<Reach>();
            List<Reach> reachable2 = new List<Reach>();
            List<Reach> reachable3 = new List<Reach>();
            for (int i = 0; i < n; i++)
            {
                string a = Console.ReadLine();
                a = a.Replace(" ", "");
                // Console.WriteLine(a);
                string[] b = a.Split(new string[] { "->" }, StringSplitOptions.None);
                var.Add(b[0]);
                string[] c = b[1].Split('|');
                for (int j = 0; j < c.Length; j++)
                {
                    tra d = new tra();
                    d.shooroo = b[0];
                    d.payan = c[j];
                    transitions.Add(d);
                }
            }

            string startvariable = transitions[0].shooroo;

            string inputtest = Console.ReadLine();

            //hazf tekraty
            hazftekrary(transitions);
            //hazf landa
            hazflanda(transitions);
            //hazf unit
            hazfunit(transitions);



            //hazf useless
            //1 be terminal nemirese
            for (int i = 0; i < var.Count; i++)
            {
                Reach a = new Reach();
                a.variable = var[i];
                a.condition = "notreachable";
                reachable.Add(a);
            }

            for (int i = 0; i < var.Count; i++)
            {

                checkreachable(var[i], transitions, reachable);
            }


            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < reachable.Count; j++)
                {
                    if (reachable[j].condition == "notreachable")
                    {
                        checkrechablefromvar(reachable[j].variable, reachable, transitions);
                    }
                }
            }

            //2 hazf useless az tarigh S
            Reach a1 = new Reach();
            a1.variable = startvariable;
            a1.condition = "reachable";
            reachable2.Add(a1);
            for (int i = 1; i < var.Count; i++)
            {
                Reach a = new Reach();
                a.variable = var[i];
                a.condition = "notreachable";
                reachable2.Add(a);
            }

            for (int i = 0; i < var.Count; i++)
            {
                for (int j = 0; j < transitions.Count; j++)
                {
                    if (transitions[j].shooroo == startvariable && transitions[j].payan.Contains(var[i]))
                    {
                        for (int k = 0; k < reachable2.Count; k++)
                        {
                            if (reachable2[k].variable == var[i])
                            {
                                reachable2[k].condition = "reachable";
                                break;
                            }
                        }
                    }
                }
            }


            //for (int i = 0; i < reachable2.Count; i++)
            //{
            //    Console.WriteLine(reachable2[i].variable + " " + reachable2[i].condition);
            //}

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < reachable2.Count; j++)
                {
                    if (reachable2[j].condition == "notreachable")
                    {
                        checkrechablefromvar1(reachable2[j].variable, reachable2, transitions);
                    }
                }
            }


            for (int i = 0; i < var.Count; i++)
            {
                Reach a = new Reach();
                a.variable = var[i];
                a.condition = "reachable";
                reachable3.Add(a);
            }

            for (int i = 1; i < reachable.Count; i++)
            {
                if (reachable[i].condition == "notreachable" || reachable2[i].condition == "notreachable")
                {
                    reachable3[i].condition = "notreachable";
                }
            }

            for (int i = 0; i < reachable3.Count; i++)
            {
                if (reachable3[i].condition == "notreachable")
                {
                    removetra(reachable3[i].variable, transitions);
                }
            }




            //chomsky tabdil
            //find symbol
            List<string> symbol = new List<string>();
            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].payan.Contains("a"))
                {
                    symbol.Add("a");
                }

                if (transitions[i].payan.Contains("b"))
                {
                    symbol.Add("b");
                }

                if (transitions[i].payan.Contains("c"))
                {
                    symbol.Add("c");
                }

                if (transitions[i].payan.Contains("d"))
                {
                    symbol.Add("d");
                }

                if (transitions[i].payan.Contains("e"))
                {
                    symbol.Add("e");
                }

                if (transitions[i].payan.Contains("f"))
                {
                    symbol.Add("f");
                }

                if (transitions[i].payan.Contains("g"))
                {
                    symbol.Add("g");
                }

                if (transitions[i].payan.Contains("h"))
                {
                    symbol.Add("h");
                }

                if (transitions[i].payan.Contains("i"))
                {
                    symbol.Add("i");
                }

                if (transitions[i].payan.Contains("j"))
                {
                    symbol.Add("j");
                }

                if (transitions[i].payan.Contains("k"))
                {
                    symbol.Add("k");
                }

                if (transitions[i].payan.Contains("l"))
                {
                    symbol.Add("l");
                }

                if (transitions[i].payan.Contains("m"))
                {
                    symbol.Add("m");
                }

                if (transitions[i].payan.Contains("n"))
                {
                    symbol.Add("n");
                }

                if (transitions[i].payan.Contains("o"))
                {
                    symbol.Add("o");
                }

                if (transitions[i].payan.Contains("p"))
                {
                    symbol.Add("p");
                }

                if (transitions[i].payan.Contains("q"))
                {
                    symbol.Add("q");
                }

                if (transitions[i].payan.Contains("r"))
                {
                    symbol.Add("r");
                }

                if (transitions[i].payan.Contains("s"))
                {
                    symbol.Add("s");
                }

                if (transitions[i].payan.Contains("t"))
                {
                    symbol.Add("t");
                }

                if (transitions[i].payan.Contains("u"))
                {
                    symbol.Add("u");
                }

                if (transitions[i].payan.Contains("v"))
                {
                    symbol.Add("v");
                }

                if (transitions[i].payan.Contains("w"))
                {
                    symbol.Add("w");
                }

                if (transitions[i].payan.Contains("x"))
                {
                    symbol.Add("x");
                }

                if (transitions[i].payan.Contains("y"))
                {
                    symbol.Add("y");
                }

                if (transitions[i].payan.Contains("z"))
                {
                    symbol.Add("z");
                }

            }

            symbol = symbol.Distinct().ToList();

            for (int i = 0; i < symbol.Count; i++)
            {
                string rastr = RandomStr();
                rastr = "<" + rastr + ">";
                while (var.Contains(rastr))
                {
                    rastr = RandomStr();
                    rastr = "<" + rastr + ">";
                }

                for (int z = 0; z < transitions.Count; z++)
                {
                    if (transitions[z].payan.Length > 1 && transitions[z].payan.Contains(symbol[i]))
                    {
                        string x = transitions[z].payan.Replace(symbol[i], rastr);
                        transitions[z].payan = x;
                    }
                }
                tra s = new tra();
                s.shooroo = rastr;
                s.payan = symbol[i];
                transitions.Add(s);
            }
            tabdilchomsky(transitions, var);

            //for (int i = 0; i < transitions.Count; i++)
            //{
            //    Console.WriteLine(transitions[i].shooroo + " " + transitions[i].payan);
            //}
            //cyk

            List<string>[,] jadval = new List<string>[inputtest.Length, inputtest.Length];
            for (int i = 0; i < inputtest.Length; i++)
            {
                List<string> f = new List<string>();
                for (int j = 0; j < transitions.Count; j++)
                {
                    if (transitions[j].payan == inputtest[i].ToString())
                    {
                        f.Add(transitions[j].shooroo);
                    }
                }
                jadval[i, i] = f;
            }


            //for(int i=0;i< inputtest.Length; i++)
            //{
            //    for (int j = 0; j < jadval[i, i].Count; j++)
            //    {
            //        Console.WriteLine(i+" "+jadval[i, i][j]);
            //    }
            //}


            for (int u = 1; u < inputtest.Length; u++)
            {
                for (int i = 0; i < inputtest.Length - 1; i++)
                {
                    List<string> f = new List<string>();
                    if ((i + u) < inputtest.Length)
                    {
                        bedastlist(f, jadval, transitions, i, i + u);
                        //Console.WriteLine(i + " " + (i + u));
                    }
                }
            }
            bool t = false;
            for (int i = 0; i < jadval[0, inputtest.Length - 1].Count; i++)
            {
                if (jadval[0, inputtest.Length - 1][i] == startvariable)
                {
                    t = true;
                    Console.WriteLine("Accepted");
                    break;
                }
            }

            if (t == false)
            {
                Console.WriteLine("Rejected");
            }

        }

        static void bedastlist(List<string> f, List<string>[,] jadval, List<tra> transitions, int i, int j)
        {

            List<string> e = new List<string>();
            for (int k = 0; k < jadval[i, i].Count; k++)
            {
                for (int z = 0; z < jadval[i + 1, j].Count; z++)
                {
                    //Console.WriteLine(jadval[i, i][k] + jadval[i + 1, j][z] + "______");
                    e.Add((jadval[i, i][k] + jadval[i + 1, j][z]).ToString());
                }
            }

            for (int k = 0; k < jadval[i, j - 1].Count; k++)
            {
                for (int z = 0; z < jadval[j, j].Count; z++)
                {
                    e.Add((jadval[i, j - 1][k] + jadval[j, j][z]).ToString());
                }
            }

            for (int k = 0; k < transitions.Count; k++)
            {
                for (int z = 0; z < e.Count; z++)
                {
                    if (transitions[k].payan == e[z])
                    {
                        f.Add(transitions[k].shooroo);
                    }
                }
            }
            jadval[i, j] = f;
        }
    }

}
