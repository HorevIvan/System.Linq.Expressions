using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq.Expressions
{
    public class ExpressionOptimizer
    {
        public ExpressionTree Tree { private set; get; }

        public ExpressionOptimizer(Expression expression)
        {
            Tree = (new ExpressionTree(expression));
        }

        public IEnumerable<ExpressionTree> GetDublicateNodes()
        {
            return Tree.GetAllNodes()
                .GroupBy(node => node)
                .Where(node => node.Skip(1).Any())
                .SelectMany(node => node);
        }
    }
}
