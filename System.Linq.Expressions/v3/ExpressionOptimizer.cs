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
        public static IEnumerable<Expression> GetDublicate(Expression root)
        {
            return
                (new ExpressionNode(root)).GetAllNodes()            // create expression tree and get all it subexpressions
                    .Select(expressionTree => expressionTree.Root)  // getting free expressions from tree nodes
                    .GroupBy(expression => expression.ToString())   // grouping by string implementation
                    .Where(group => group.Skip(1).Any())            // selecting groups where amount of element more 1
                    .SelectMany(group => group);                    // getting free expressions from selected groups
        }

        public static IEnumerable<Expression> GetDublicate(Expression root, params ExpressionType[] types)
        {
            return
                 GetDublicate(root)
                    .Where(expression => types.Contains(expression.NodeType))       // selecting only function invokes
                    .Select(expression => expression.To<InvocationExpression>());   // cast to invocation expressions
        }
    }
}
