using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace повторяюшарп
{
    public interface IComparableBox<T> where T: struct, IComparable<T>
    {
        int Count { get; }
        T Max();
        void Add(T item);
        void Sort();
    }
}
