using ATS.Core.Interface;
using System;
using System.Linq.Expressions;
using ATS.Core.Helper;

namespace ATS.Core.Model
{
    public class SimpleQuery<T> : ISimpleQueryable<T, SimpleQueryModel>
    {

        /************************HOW EXPRESSION WORKS*************************************** 
                            Lambda
                           /         \
                        Equal       Parameter ( Item)
                       /       \          
           PropertyName  \
                |                Constant  "Value"
                |      
            Property(Item)       
      **********************************************************************************/
        public Expression<Func<T, bool>> GetQuery(SimpleQueryModel query)
        {
            try
            {
                var predicate = PredicateBuilder.True<T>();
                foreach (var qry in query.Properties)
                {
                    Type type = typeof(T);
                    var model = Expression.Parameter(typeof(T), query.ModelName);
                    var prop = Expression.Property(model, qry.Key);
                    object valueFound = GetValueByType(prop.Type, qry.Value);
                    var castedValue = Expression.Convert(Expression.Constant(valueFound), prop.Type);
                    var equal = Expression.Equal(prop, castedValue);
                    var lambda = Expression.Lambda<Func<T, bool>>(equal, model);
                    predicate = predicate.And(lambda);
                }
                return predicate;
            }
            catch { throw; }
        }
        public object GetValueByType(Type type, object value)
        {

            if (type.Equals(typeof(Guid)))
            {
                value = new Guid(value.ToString());
            }
            else if (type.Equals(typeof(Guid?)))
            {
                value = (value == null) ?Guid.Empty : new Guid(value.ToString());
            }
            return value;
        }
    }
}
