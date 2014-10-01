using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace v3
{
    public class ExpressionNode
    {
        public Expression Root { private set; get; }

        public ExpressionNode(Expression root)
        {
            Root = root;
        }

        public IEnumerable<ExpressionNode> Nodes
        {
            get { return Expressions.Select(expression => new ExpressionNode(expression)); }
        }

        public IEnumerable<Expression> Expressions
        {
            get { return GetExpressions(Root); }
        }

        public static Expression[] GetExpressions(Expression root)
        {
            switch (root.NodeType)
            {
                case ExpressionType.Lambda:
                    {
                        return new[] { root.To<LambdaExpression>().Body };
                    }
                case ExpressionType.Invoke:
                    {
                        return new[] { root.To<InvocationExpression>().Expression };
                    }
                default:
                    {
                        throw (new NotSupportedException("Node type {0} is not supported".Set(root.NodeType)));
                    }
            }
        }
    }
}
