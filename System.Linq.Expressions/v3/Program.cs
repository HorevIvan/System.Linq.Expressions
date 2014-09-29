using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace v3
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class ExpressionNode
    {
        public Expression Root { private set; get; }

        #region Nodes

        private ExpressionNode[] _Nodes;

        public ExpressionNode[] Nodes
        {
            get { return _Nodes ?? (_Nodes = GetNodes()); }
        }

        private ExpressionNode[] GetNodes()
        {
            return
                GetSubNodes(Root)
                    .Select(subNode => (new ExpressionNode { Root = subNode }))
                        .ToArray();
        }

        public IEnumerable<ExpressionNode> GetAllNodes()
        {
            var nodes = Nodes.AsEnumerable();

            foreach (var node in Nodes)
            {
                nodes = nodes.Concat(node.GetAllNodes());
            }

            return nodes;
        }

        public static Expression[] GetSubNodes(Expression root)
        {
            switch (root.NodeType)
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
                case ExpressionType.Lambda:
                    {
                        return GetNodesFromLambdaExpression(root.To<LambdaExpression>());
                    }
                case ExpressionType.Conditional:
                    {
                        return GetNodesFromConditionalExpression(root.To<ConditionalExpression>());
                    }
                case ExpressionType.MemberAccess:
                    {
                        return GetNodesFromMemberExpression(root.To<MemberExpression>());
                    }
                case ExpressionType.Constant:
                    {
                        return GetNodesFromConstantExpression(root.To<ConstantExpression>());
                    }
                default:
                    {
                        throw (new NotSupportedException("Node type {0} is not supported".Set(root.NodeType)));
                    }
            }
        }

        private static Expression[] GetNodesFromLambdaExpression(LambdaExpression lambdaExpression)
        {
            return (new Expression[] { lambdaExpression.Body });
        }

        public static Expression[] GetNodesFromUnaryExpression(UnaryExpression expression)
        {
            return (new Expression[] { expression.Operand });
        }

        private static Expression[] GetNodesFromBinaryExpression(BinaryExpression expression)
        {
            return (new Expression[] { expression.Left, expression.Right });
        }

        public static Expression[] GetNodesFromTypeBinaryExpression(TypeBinaryExpression expression)
        {
            return (new Expression[] { expression.Expression });
        }

        public static Expression[] GetNodesFromInvocationExpression(InvocationExpression expression)
        {
            return (new Expression[] { expression.Expression });
        }

        public static Expression[] GetNodesFromConditionalExpression(ConditionalExpression expression)
        {
            return (new Expression[] { expression.Test, expression.IfFalse, expression.IfTrue });
        }

        public static Expression[] GetNodesFromMemberExpression(MemberExpression expression)
        {
            return (new Expression[] { expression.Expression });
        }

        public static Expression[] GetNodesFromConstantExpression(ConstantExpression expression)
        {
            return (new Expression[] { });
        }

        #endregion
    }
}
