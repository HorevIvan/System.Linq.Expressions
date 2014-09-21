﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FDD = System.Func<System.Double, System.Double>;
using EFDD = System.Linq.Expressions.Expression<System.Func<System.Double, System.Double, System.Double>>;

namespace System.Linq.Expressions.Samples
{
    class Program
    {
        static void Main(String[] args)
        {
            FDD f1 = (x) => Math.Sin(x);
            FDD f2 = (x) => (x * x);
            FDD f3 = (x) => (x > 0 ? Math.Abs(x) : Math.Sqrt(x));

            EFDD ef = (x, y) =>
                f1(x) > f1(y)
                    ? f2(f3(x) + f3(y)) / (f1(x) == 0 ? 1 : f2(f3(x) + f3(y)))
                    : f3(f2(x + y)) / (f1(y) == 0 ? 1 : f2(f3(x) + f3(y)));

            var et = new ExpressionTree(ef);

            Print(et);

            Console.ReadLine();
        }

        private static void Print(ExpressionTree et, Int32 level = 0)
        {
            var l = 70;

            var body = (new String('-', level)) + " " + et.Root.ToString();

            if(body.Length > l) body = body.Substring(0, l);

            Console.WriteLine(body);

            foreach(var node in et.Nodes)
            {
                Print(node, ++level);
            }
        }
    }
}
