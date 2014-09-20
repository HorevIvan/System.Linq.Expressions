using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq.Expressions
{
    public class ExpressionTree
    {
        public Expression Root { private set; get; }

        public ExpressionTree(Expression root)
        {
            Root = root;
        }

        #region Nodes

        private IEnumerable<ExpressionTree> _Nodes;

        public IEnumerable<ExpressionTree> Nodes
        {
            get { return (_Nodes ?? (_Nodes = GetNodes())); }
        }

        private IEnumerable<ExpressionTree> GetNodes()
        {
            foreach(var expression in GetSubExpressions(Root))
            {
                yield return (new ExpressionTree(expression));
            }
        }

        #endregion

        public static IEnumerable<Expression> GetSubExpressions(Expression root)
        {
            switch(root.NodeType)
            {
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.ArrayLength:
                case ExpressionType.Quote:
                case ExpressionType.TypeAs:
                    {
                        return GetNodesFromUnaryExpression(root.To<UnaryExpression>());
                    }
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
                        return GetNodesFromBinaryExpression(root.To<BinaryExpression>());
                    }
                case ExpressionType.TypeIs:
                    {
                        return GetNodesFromTypeBinaryExpression(root.To<TypeBinaryExpression>());
                    }
                case ExpressionType.Invoke:
                    {
                        return GetNodesFromInvocationExpression(root.To<InvocationExpression>());
                    }
                default:
                    {
                        throw (new NotSupportedException("Node type {0} is not supported".Set(root.NodeType)));
                    }
            }
        }

        public static IEnumerable<Expression> GetNodesFromUnaryExpression(UnaryExpression expression)
        {
            yield return expression.Operand;
        }

        private static IEnumerable<Expression> GetNodesFromBinaryExpression(BinaryExpression expression)
        {
            yield return expression.Left;

            yield return expression.Right;
        }

        public static IEnumerable<Expression> GetNodesFromTypeBinaryExpression(TypeBinaryExpression expression)
        {
            yield return expression.Expression;
        }

        public static IEnumerable<Expression> GetNodesFromInvocationExpression(InvocationExpression expression)
        {
            yield return expression.Expression;
        }
    }
}
