using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace повторяюшарп
{
    public class NumericBox<T> : IEquatable<NumericBox<T>>, IComparableBox<T> where T : struct, IComparable<T>
    {
        public NumericBox(int сapacity = 4)
        {
            if (сapacity < 0) throw new ArgumentOutOfRangeException();
            else if (сapacity == 0) Array.Resize(ref mas, _defaultcapacity);
            else Array.Resize(ref mas, сapacity);
        }
        public NumericBox(T[] mas)
        {
            if (mas.Length != 0) 
            {
                Array.Resize(ref this.mas, mas.Length * 2);
                Array.Copy(mas, this.mas, mas.Length);
                Count = mas.Length;
            }
            else Array.Resize(ref mas, _defaultcapacity);
        }
        public int Capacity
        {
            get => mas.Length;
        }
        private const int _defaultcapacity = 4;

        public int Count { get; private set; }

        private T[] mas = [];

        public T this[int index]
        {
            get
            {
                if (index < Count && index >= 0)
                    return mas[index];
                else throw new IndexOutOfRangeException($"индекс {index} выходит за пределы массива, содержащего возможные индексы от 0 до {Count - 1}");
            }
            set
            {
                if (index < Count && index >= 0)
                    mas[index] = value;
                else throw new IndexOutOfRangeException($"индекс {index} выходит за пределы массива, содержащего возможные индексы от 0 до {Count - 1}");
            }
        }
        public T Max()
        {
            if (Count == 0) throw new InvalidOperationException("Массив пустой, не представляется возможным найти максимальное число");
            T num = mas[0];
            for (int i = 1; i < Count; i++)
            {
                if (num.CompareTo(mas[i]) < 0)
                    num = mas[i];
            }
            return num;
        }

        public void Add(T item)
        {
            if (mas.Length == Count)
            {
                Array.Resize(ref mas, mas.Length * 2);
            }
            Count++;
            mas[Count - 1] = item;
        }
        public void Add(T item, int index)
        {
            if (index >= 0 && index < Count)
            {
                if (mas.Length == Count) Array.Resize(ref mas, mas.Length * 2);
                Count++;
                Array.Copy(mas, index, mas, index + 1, Count - 1 - index);
                mas[index] = item;
            }
            else if (index == Count && index >= 0) Add(item);
            else throw new IndexOutOfRangeException($"Индекс {index} выходит за пределы массива mas, содержащего возможные индексы от 0 до {Count - 1}");
        }
        public void Remove(T number)
        {
            for (int i = 0; i < Count; i++)
            {
                if (mas[i].CompareTo(number) == 0)
                {
                    RemoveAt(i);
                    return;
                }
            }
        }
        public void RemoveAt(int index)
        {
            if (index < Count && index >= 0)
            {
                Array.Copy(mas, index + 1, mas, index, Count - 1 - index);
                Count--;
            }
            else throw new IndexOutOfRangeException($"Индекс {index} выходит за пределы массива mas, содержащего возможные индексы от 0 до {Count - 1}");
        }
        public bool Equals(NumericBox<T> other)
        {
            if (other is null) return false;
            if (Count != other.Count) return false;
            for (int i = 0; i < other.Count; i++)
            {
                if (mas[i].CompareTo(other[i]) != 0)
                    return false;
            }
            return true;
        }
        public void Sort()
        {
            QuickSort(0, Count - 1, mas);
        }
        private void QuickSort(int left, int right, T[] mas)
        {
            if (left >= right) return;
            int i = left;
            int j = right;
            T pivot = mas[(left + right) / 2];
            while (i <= j)
            {
                while (mas[i].CompareTo(pivot) < 0) i++;
                while (mas[j].CompareTo(pivot) > 0) j--;

                if (i <= j)
                {
                    T temp = mas[i];
                    mas[i] = mas[j];
                    mas[j] = temp;

                    i++;
                    j--;
                }
            }
            if (left < j) QuickSort(left, j, mas);
            if (i < right) QuickSort(i, right, mas);
        }
    }
}
