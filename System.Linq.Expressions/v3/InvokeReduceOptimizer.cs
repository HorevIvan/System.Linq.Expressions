using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace v3
{
    public class InvokeReduceOptimizer
    {
        public Expression Source { private set; get; }

        public ExpressionNode Tree { private set; get; }

        public IEnumerable<Expression> DublicateInvoks { private set; get; }

        public InvokeReduceOptimizer(Expression source)
        {
            Source = source;

            Tree = (new ExpressionNode(source));
        }

        public void Optimize()
        {
            DublicateInvoks = GetDublicateInvoks(Tree);
        }

        public static IEnumerable<Expression> GetDublicateInvoks(ExpressionNode tree)
        {
            return
                GetDublicate(tree)
                    .GroupBy(expression => expression.ToString())                           // grouping expressions by it string implementation 
                    .Select(group => group.First())                                         // getting first element from each group
                    .Where(expression => expression.NodeType == ExpressionType.Invoke);     // selecting invocable expressions
        }

        public static IEnumerable<Expression> GetDublicate(ExpressionNode rootNode)
        {
            return
                rootNode.GetAllNodes()                              // create expression tree and get all it subexpressions
                    .Select(expressionTree => expressionTree.Root)  // getting free expressions from tree nodes
                    .GroupBy(expression => expression.ToString())   // grouping by string implementation
                    .Where(group => group.Skip(1).Any())            // selecting groups where amount of element more 1
                    .SelectMany(group => group);                    // getting free expressions from selected groups
        }
    }
}
