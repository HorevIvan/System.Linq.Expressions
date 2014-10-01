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

        public IEnumerable<ExpressionNode> GetAllNodes()
        {
            var nodes = Nodes;

            foreach (var node in Nodes)
            {
                nodes = nodes.Concat(node.GetAllNodes());
            }

            return nodes;
        }

        public IEnumerable<Expression> Expressions
        {
            get { return GetExpressions(Root); }
        }

        public static Expression[] GetExpressions(Expression root)
        {
            switch (root.NodeType)
            {
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.Coalesce:
                case ExpressionType.ArrayIndex:
                case ExpressionType.RightShift:
                case ExpressionType.LeftShift:
                case ExpressionType.ExclusiveOr:
                    {
                        var binaryExpression = root.To<BinaryExpression>();

                        return new[] { binaryExpression.Left, binaryExpression.Right };
                    }
                case ExpressionType.Lambda:
                    {
                        return new[] { root.To<LambdaExpression>().Body };
                    }
                case ExpressionType.Invoke:
                    {
                        return new[] { root.To<InvocationExpression>().Expression };
                    }
                case ExpressionType.MemberAccess:
                    {
                        return new[] { root.To<MemberExpression>().Expression };
                    }
                case ExpressionType.Constant:
                    {
                        return new Expression[] { };
                    }
                default:
                    {
                        throw (new NotSupportedException("Node type {0} is not supported".Set(root.NodeType)));
                    }
            }
        }

        public override String ToString()
        {
            return Root.ToString();
        }
    }
}
