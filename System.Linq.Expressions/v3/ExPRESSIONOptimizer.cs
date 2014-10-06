using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace v3
{
    public class ExpressionOptimizer
    {
        public ExpressionNode Tree { private set; get; }    

        public ExpressionOptimizer(Expression source)
        {
            Tree = (new ExpressionNode(source));
        }

        public OptimizerResult Optimize()
        {
            var result = new OptimizerResult();

            result.Duplications = GetDublicateInvoks(Tree);

            //result.CalculatedDuplications //TODO

            //result.ChangedExpression //TODO

            return result;
        }

        private IEnumerable<InvocationExpression> GetDublicateInvoks(ExpressionNode tree)
        {
            return
                GetDublicate(tree)
                    .GroupBy(expression => expression.ToString())                           // grouping expressions by it string implementation 
                    .Select(group => group.First())                                         // getting first element from each group
                    .Where(expression => expression.NodeType == ExpressionType.Invoke)      // selecting invocable expressions
                    .Select(expression => expression.To<InvocationExpression>());           // cast expressions to invoks
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

    public class OptimizerResult
    {
        public IEnumerable<InvocationExpression> Duplications { set; get; }

        public Dictionary<InvocationExpression, ConstantExpression> CalculatedDuplications { set; get; }

        public Expression ChangedExpression { set; get; }
    }
}
