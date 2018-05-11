using ATS.Core.Interface;
using System;
using System.Linq.Expressions;
using ATS.Core.Helper;
using ATS.Core.Model;

namespace ATS.Core.Helper
{
    public class SimpleQueryBuilder<T> : ISimpleQueryable<T, SimpleQueryModel>
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
       ________________________________________________________________________     
            Use Compile() method to generate delegate as [item=>item.Property == Value]
      ********************************************************************************************************/
        public Expression<Func<T, bool>> GetQuery(SimpleQueryModel query )
        {
            try
            {
                var predicate = PredicateBuilder.True<T>();
                if (query != null)
                {
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
                value = (value == null) ?value : new Guid(value.ToString());
            }
            else if (type.Equals(typeof(DateTime)) )
            {
                DateTime.TryParse(Convert.ToString(value), out DateTime date);
                value = date;
            }
            else if ( type.Equals(typeof(DateTime?)))
            {
                DateTime.TryParse(Convert.ToString(value), out DateTime date);
                value = (value == null) ? value : date;
            }
            return value;
        }
    }
}
