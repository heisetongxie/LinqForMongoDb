using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LinqForMongodb
{
    public class Dbset<T> :IQueryable<T>, IQueryable
    {

        public Dbset()
        {
            this.Expression = Expression.Constant(this);
            this.Provider = new MongoDbProvider<T>();
        }

        public Dbset(Expression expression, IQueryProvider provider)
        {
            this.Expression = expression;
            this.Provider = provider;
        }

        public Type ElementType
        {
            get
            {
                 return typeof(Dbset<T>);
            }
        }

        public Expression Expression { get; set; }

        public IQueryProvider Provider { get; set; }

        public IEnumerator GetEnumerator()
        {
            var result = Provider.Execute<List<T>>(Expression);
            if (result == null)
                yield break;
            foreach (var item in result)
            {
                yield return item;
            }
        }


        //public T Add(T TEntity)
        //{

        //}

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return Provider.Execute<IEnumerator<T>>(Expression);
        }
    }
}
