using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFAum4
{
    public sealed class MinHeap<T>
        where T : class, IComparable<T>
    {
        private List<T> items;
        public T this[int i]
        { get { return items[i]; } }

        public int Count { get { return items.Count; } }
        public bool IsEmpty { get { return (Count == 0); } }
        public T Min { get { return items[0]; } }

        public MinHeap()
        {
            items = new List<T>();
        }

        public T Extract()
        {
            T val = items[0];
            items[0] = items[Count - 1];
            items.RemoveAt(Count - 1);
            FixDown(0);
            return val;
        }

        public void Insert(T item)
        {
            items.Add(item);
            FixUp(Count - 1);
        }

        public void Clear()
        {
            items.Clear();
            items.TrimExcess();
        }

        public int IndexOf(T item)
        {
            int count = Count;
            for (int i = 0; i < count; ++i)
                if (items[i].CompareTo(item) == 0)
                    return i;
            return -1;
        }

        public T Remove(T item)
        {
            int i = IndexOf(item);
            if (i == -1) return null;
            T val = items[i];
            items[i] = items[Count - 1];
            items.RemoveAt(Count - 1);
            FixDown(i);
            return val;
        }

        private void FixUp(int i)
        {
            int p = (i - 1) >> 1;

            while(i > 0 && items[i].CompareTo(items[p]) < 0)
            {
                Swap(i, p);
                i = p;
                p = (i - 1) >> 1;
            }
        }

        private void FixDown(int i)
        {
            int l = (i << 1) + 1;

            while (l < Count)
            {
                int r = l + 1;
                int m = (items[l].CompareTo(items[i]) < 0) ? l : i;

                if (r < Count && items[r].CompareTo(items[m]) < 0)
                    m = r;

                if (m != i)
                {
                    Swap(i, m);
                    i = m;
                    l = (i << 1) + 1;
                }
                else break;
            }
        }

        private void Swap(int i1, int i2)
        {
            T t = items[i1];
            items[i1] = items[i2];
            items[i2] = t;
        }
    }
}
