using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Globals
{
    [DataContract]
    public class SoftvList<T> : ICollection<T>
    {
        public SoftvList()
        {
            Entities = new List<T>();
        }

        [DataMember]
        public int totalCount { get; set; }

        [DataMember]
        public List<T> Entities { get; set; }


        public void Add(T item)
        {
            Entities.Add(item);
        }

        public void Clear()
        {
            Entities.Clear();
        }

        public bool Contains(T item)
        {
            return  Entities.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Entities.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return Entities.Count; }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(T item)
        {
            return Entities.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in Entities)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Entities.GetEnumerator();
        }
    }
}
