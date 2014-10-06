using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace v3
{
    class Program
    {
        static void Main()
        {
            Func<Int32, Int32> f1 = (x) => 2 * x;
            Func<Int32, Int32> f2 = (x) => x + 1;

            Expression<Func<Int32, Int32, Int32>> ef = (x, y) => (f1(x) + f2(x)) * (f1(x) - f2(x));

            Console.WriteLine(ef);

            var tree = new ExpressionNode(ef);

            Console.WriteLine();
            Console.WriteLine("AllNodes");
            foreach (var node in tree.GetAllNodes())
            {
                Console.WriteLine(node);
            }

            Console.WriteLine();
            Console.WriteLine("DublicateNodes");
            foreach (var expression in ExpressionOptimizer.GetDublicate(tree))
            {
                Console.WriteLine(expression);
            }

            var optimizerResult = (new ExpressionOptimizer(ef)).Optimize();

            Console.WriteLine();
            Console.WriteLine("DublicateInvoks");
            foreach(var expression in optimizerResult.Duplications)
            {
                Console.WriteLine(expression);
            }

            Console.WriteLine();
            Console.WriteLine("Rebuild");
            tree.Rebuild();

            Console.ReadLine();
        }
    }
}
