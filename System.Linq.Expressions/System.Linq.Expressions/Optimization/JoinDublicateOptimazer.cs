using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq.Expressions.Optimization
{
    public class JoinDublicateOptimazer : ExpressionOptimizer
    {
        public override void Optimize()
        {
            
        }

        public static IEnumerable<ExpressionTree> GetDublicateNodes(ExpressionTree tree)
        {
            return tree.GetAllNodes()
                .GroupBy(node => node.Root.ToString())
                .Where(group => group.Skip(1).Any())
                .SelectMany(node => node);
        }

        public static IEnumerable<ExpressionTree> GetNodesForReduce(ExpressionTree tree)
        {
            return GetDublicateNodes(tree)
                .GroupBy(node => node.Root.ToString())
                .Select(group => group.First());
        }
    }
}
