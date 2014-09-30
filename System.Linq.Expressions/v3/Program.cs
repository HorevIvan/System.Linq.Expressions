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

            Expression<Func<Int32, Int32, Int32>> ef = (x, y) => (f1(x) + f2(x)) * f1(x + y) * f2(x - y);

            Console.WriteLine(ef);

            var n = ExpressionNode.Constructor(ef);

            Console.ReadLine();
        }
    }
}
