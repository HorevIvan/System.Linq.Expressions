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

        public abstract Expression[] GetExpressions();

        public ExpressionNode GetNode(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Invoke:
                    {
                        return Constructor<InvocationNode>(expression);
                    }
                default:
                    { 
                        throw (new NotSupportedException("Node type {0} is not supported".Set(Root.NodeType)));
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

    public abstract class TypedExpressionNode<ExpressionType> : ExpressionNode
        //
        where ExpressionType: Expression
    {
        public virtual ExpressionType Target
        {
            get { return Root.To<ExpressionType>(); }
        }
    }

    public class InvocationNode : TypedExpressionNode<InvocationExpression>
    {
        public override Expression[] GetExpressions()
        {
            return (new[] { Target.Expression });
        }
    }

    public class LambdaNode : TypedExpressionNode<LambdaExpression>
    {
        public override Expression[] GetExpressions()
        {
            return (new[] { Target.Body });
        }
    }
}
