using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq.Expressions.Optimization
{
    public abstract class ExpressionOptimizer
    {
        public ExpressionTree Tree { private set; get; }

        public abstract void Optimize();

        //public ExpressionOptimizer(Expression expression)
        //{
        //    Tree = (new ExpressionTree(expression));
        //}

        public static OptimizerType Create<OptimizerType>(ExpressionTree tree)
            //
            where OptimizerType: ExpressionOptimizer, new()
        {
            return (new OptimizerType { Tree = tree });
        }
    }
}
