using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq.Expressions
{
    public class ReplaceVisitor : ExpressionVisitor
    {
        public Expression Replaceable { private set; get; }

        public Expression Replacer { private set; get; }

        public ReplaceVisitor(Expression replaceable, Expression replacer)
        {
            Replaceable = replaceable;

            Replacer = replacer;
        }

        public Expression Modify(Expression expression)
        {
            if(expression.Is<InvocationExpression>())
            {
                return VisitInvocation(expression.To<InvocationExpression>());
            }
            else
            {
                return expression;
            }
        }

        protected override Expression VisitInvocation(InvocationExpression expression)
        {
            if(expression.ToString() == Replaceable.ToString())
            {
                return Replacer;
            }

            return base.VisitInvocation(expression);
        }
    }
}
