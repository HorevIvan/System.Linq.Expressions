using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace v3
{

    public abstract class ExpressionNode
    {
        public Expression Root { protected set; get; }

        public abstract IEnumerable<Expression> GetExpressions();

        public IEnumerable<ExpressionNode> Nodes
        {
            get { return GetExpressions().Select(Constructor); }
        }

        public static ExpressionNode Constructor(Expression root)
        {
            switch (root.NodeType)
            {
                case ExpressionType.Lambda:
                    {
                        return Constructor<LambdaNode>(root);
                    }
                case ExpressionType.Invoke:
                    {
                        return Constructor<InvocationNode>(root);
                    }
                default:
                    {
                        throw (new NotSupportedException("Node type {0} is not supported".Set(root.NodeType)));
                    }
            }
        }

        public static NodeType Constructor<NodeType>(Expression root)
            //
            where NodeType: ExpressionNode, new()
        {
            return (new NodeType { Root = root });
        }
    }

    public abstract class TypedExpressionNode<T> : ExpressionNode
        //
        where T: Expression
    {
        public T Target
        {
            get { return Root.To<T>(); }
        }
    }

    public class InvocationNode : TypedExpressionNode<InvocationExpression>
    {
        public override IEnumerable<Expression> GetExpressions()
        {
            yield return Target.Expression;
        }
    }

    public class LambdaNode : TypedExpressionNode<LambdaExpression>
    {
        public override IEnumerable<Expression> GetExpressions()
        {
            yield return Target.Body;
        }
    }
}
