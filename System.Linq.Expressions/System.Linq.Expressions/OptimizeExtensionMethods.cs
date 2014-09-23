using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq.Expressions
{
    public static class ExpressionExtensionsMethods
    {
        public static Delegate Optimize(this Expression expression)
        {
            var optimizer = new ExpressionOptimizer(expression);

            optimizer.Optimize();

            return optimizer.Result;
        }

        public static Object OptimiziedCalculation(this Expression expression, params Object[] parameters)
        {
            return expression.Optimize().DynamicInvoke(parameters);
        }
    }
}
