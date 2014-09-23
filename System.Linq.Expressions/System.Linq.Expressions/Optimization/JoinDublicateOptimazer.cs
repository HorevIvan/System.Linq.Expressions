using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq.Expressions.Optimization
{
    public class JoinDublicateOptimazer : ExpressionOptimizer
    {
        public IEnumerable<Expression> ReduceNodes { private set; get; }

        public override void Optimize()
        {
            ReduceNodes = 
                GetNodesForReduce(Expression)
                    .Where(expression => expression.NodeType == ExpressionType.Invoke);
        }

        public static IEnumerable<Expression> GetDublicateNodes(Expression expression)
        {
            return (new ExpressionTree(expression)).GetAllNodes()
                .Select(node=>node.Root)
                .GroupBy(node => node.ToString())
                .Where(group => group.Skip(1).Any())
                .SelectMany(node => node);
        }

        public static IEnumerable<Expression> GetNodesForReduce(Expression expression)
        {
            return GetDublicateNodes(expression)
                .GroupBy(node => node.ToString())
                .Select(group => group.First());
        }
    }
}
