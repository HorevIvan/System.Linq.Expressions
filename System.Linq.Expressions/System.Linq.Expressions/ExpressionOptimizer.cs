
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq.Expressions
{
    public abstract class ExpressionOptimizer
    {
        public Expression Expression { private set; get; }

        public Delegate Result { protected set; get; }

        public abstract void Optimize();

        public ExpressionOptimizer(Expression expression)
        {
            Expression = expression;
        }
    }
}
