using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq.Expressions
{
    public class InvocableReduceOptimizer : ExpressionOptimizer
    {
        public IEnumerable<InvocationExpression> ForwardExpressions { private set; get; }        

        public InvocableReduceOptimizer(Expression expression)
            //
            : base(expression)
        {
        }

        public override void Optimize()
        {
            ForwardExpressions =
                GetNodesForReduce(Expression)
                    .Where(expression => expression.NodeType == ExpressionType.Invoke)  // selecting only function invokes
                    .Select(expression => expression.To<InvocationExpression>());       // cast to invocation expressions

            //TODO
        }

        public static IEnumerable<Expression> GetDublicateNodes(Expression root)
        {
            return
                (new ExpressionTree(root)).GetAllNodes()            // create expression tree and get all it subexpressions
                    .Select(expressionTree => expressionTree.Root)  // getting free expressions from tree nodes
                    .GroupBy(expression => expression.ToString())   // grouping by string implementation
                    .Where(group => group.Skip(1).Any())            // selecting groups where amount of element more 1
                    .SelectMany(group => group);                    // getting free expressions from selected groups
        }

        public static IEnumerable<Expression> GetNodesForReduce(Expression root)
        {
            return
                GetDublicateNodes(root)
                    .GroupBy(expression => expression.ToString())   // grouping expressions by it string implementation 
                    .Select(group => group.First());                // getting first element from each group
        }
    }
}
