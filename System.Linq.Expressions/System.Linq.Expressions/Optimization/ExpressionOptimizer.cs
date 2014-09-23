using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq.Expressions.Optimization
{
    public abstract class ExpressionOptimizer
    {
        public Expression Expression { private set; get; }

        public abstract void Optimize();

        public static OptimizerType Constructor<OptimizerType>(Expression expression)
            //
            where OptimizerType: ExpressionOptimizer, new()
        {
            return (new OptimizerType { Expression = expression });
        }
    }
}
