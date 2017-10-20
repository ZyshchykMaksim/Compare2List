using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compare2List
{
    /// <summary>
    /// Immutable class containing changes
    /// </summary>
    public sealed class ChangeResult<T>
    {
        private readonly ReadOnlyCollection<T> deleted;

        private readonly ReadOnlyCollection<T> changed;

        private readonly ReadOnlyCollection<T> inserted;

        public ChangeResult(IList<T> deleted, IList<T> changed, IList<T> inserted)
        {
            this.deleted = new ReadOnlyCollection<T>(deleted);
            this.changed = new ReadOnlyCollection<T>(changed);
            this.inserted = new ReadOnlyCollection<T>(inserted);
        }

        public IList<T> Deleted => this.deleted;

        public IList<T> Changed => this.changed;

        public IList<T> Inserted => this.inserted;
    }
}
