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
        public Expression<Func<T, bool>> GetQuery(SimpleQueryModel query)
        {
            try
            {
                var predicate = PredicateBuilder.True<T>();
                if (query != null && query.Properties != null)
                {
                    foreach (var qry in query.Properties)
                    {

                        Type type = typeof(T);
                        var model = Expression.Parameter(typeof(T), query.ModelName);
                        var prop = Expression.Property(model, Convert.ToString(qry.Key));
                        object valueFound = GetValueByType(prop.Type, qry.Value.DataValue);
                        var castedValue = Expression.Convert(Expression.Constant(valueFound), prop.Type);
                        var condition = GetCondition(prop, castedValue, qry.Value.QueryCondition);
                        var lambda = Expression.Lambda<Func<T, bool>>(condition, model);
                        predicate = GetPredicate(predicate,lambda, qry.Value.QueryType);
                    }
                }
                return predicate;
            }
            catch { throw; }
        }
        private object GetValueByType(Type type, object value)
        {

            if (type.Equals(typeof(Guid)))
            {
                value = new Guid(value.ToString());
            }
            else if (type.Equals(typeof(Guid?)))
            {
                value = (value == null) ? value : new Guid(value.ToString());
            }
            else if (type.Equals(typeof(DateTime)))
            {
                DateTime.TryParse(Convert.ToString(value), out DateTime date);
                value = date;
            }
            else if (type.Equals(typeof(DateTime?)))
            {
                DateTime.TryParse(Convert.ToString(value), out DateTime date);
                value = (value == null) ? value : date;
            }
            return value;
        }

        private Expression GetCondition(MemberExpression prop, UnaryExpression value, QueryType expression)
        {
            var condition = Expression.Equal(prop, value);
            switch (expression)
            {
                case QueryType.NotEqual:
                    condition = Expression.NotEqual(prop, value);
                    break;
                case QueryType.GreaterThan:
                    condition = Expression.GreaterThan(prop, value);
                    break;
                case QueryType.GreaterThanEqual:
                    condition = Expression.GreaterThanOrEqual(prop, value);
                    break;
                case QueryType.LessThan:
                    condition = Expression.LessThan(prop, value);
                    break;
                case QueryType.LessThanEqual:
                    condition = Expression.LessThanOrEqual(prop, value);
                    break;
                case QueryType.Equal:
                default:
                    condition = Expression.Equal(prop, value);
                    break;
            }
            return condition;
        }

        private Expression<Func<T, bool>> GetPredicate(Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> lambda, QueryType expression)
        {
            //var predicate = PredicateBuilder.True<T>();
            switch (expression)
            {
                case QueryType.Or:
                    predicate = predicate.Or(lambda);
                    break;
                case QueryType.And:
                default:
                    predicate = predicate.And(lambda);
                    break;
            }
            return predicate;
        }
    }
}
