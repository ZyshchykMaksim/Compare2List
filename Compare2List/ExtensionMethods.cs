using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compare2List
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Gets the changes [Deleted, changed, inserted] comparing this collection to another.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValues"></typeparam>
        /// <param name="collectionA">The source collection.</param>
        /// <param name="collectionB">The remote collection to compare against.</param>
        /// <param name="keySelector">The primary key selector function</param>
        /// <param name="comparableValuesSelector"></param>
        /// <returns></returns>
        public static ChangeResult<TSource> CompareTo<TSource, TKey, TValues>(
           this IEnumerable<TSource> collectionA,
            IEnumerable<TSource> collectionB,
            Func<TSource, TKey> keySelector,
            Func<TSource, TValues> comparableValuesSelector)
        {

            if (collectionA == null)
            {
                throw new ArgumentNullException(nameof(collectionA));
            }

            if (collectionB == null)
            {
                throw new ArgumentNullException(nameof(collectionB));
            }

            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            var byId = new
            {
                A = collectionA.ToDictionary(keySelector),
                B = collectionB.ToDictionary(keySelector),
            };

            var sameIds = new HashSet<TKey>(byId.A.Keys.Where(byId.B.ContainsKey));

            var changedIds = (from id in sameIds
                              let a = byId.A[id]
                              let b = byId.B[id]
                              where !comparableValuesSelector(a).Equals(comparableValuesSelector(b))
                              select id).ToList();

            var deleteIds = byId.A.Keys.Where(id => !byId.B.ContainsKey(id)).ToList();
            var newIds = byId.B.Keys.Where(id => !byId.A.ContainsKey(id)).ToList();

            List<TSource> changeList = changedIds.Select(changedId => byId.B[changedId]).ToList();
            List<TSource> deleteList = deleteIds.Select(deleteId => byId.A[deleteId]).ToList();
            List<TSource> newList = newIds.Select(newid => byId.B[newid]).ToList();

            return new ChangeResult<TSource>(deleteList, changeList, newList);
        }

    }
}
