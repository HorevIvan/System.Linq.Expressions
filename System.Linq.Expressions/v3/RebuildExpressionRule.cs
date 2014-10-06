using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace v3
{
    public abstract class RebuildRule
    {
        public abstract Boolean IsMatch(Expression expression);

        public abstract Expression Modifier(Expression expression);
    }

    public class SubstitutionRule : RebuildRule
    {
        public Expression Target { private set; get; }

        public Expression Replacement { private set; get; }

        public SubstitutionRule(Expression target, Expression replacement)
        {
            Target = target;

            Replacement = replacement;
        }

        public override Boolean IsMatch(Expression expression)
        {
            return (expression.ToString() == Target.ToString());
        }

        public override Expression Modifier(Expression expression)
        {
            return Replacement;
        }
    }
}
