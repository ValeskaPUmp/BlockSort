using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Net;
using System.Net.Sockets;


namespace OOP3
{
    public class Range
    {
        public int Start { get;private set; }
        public int End { get; private set; }
        public Range(int start, int end)
        {
            Start = start;
            End = end;

        }

        public Range()
        {
            Start = 0;
            End = 0;
        }
        
    }
    public unsafe struct BlockSort<T> where T:unmanaged
    {
        
        public BlockSort(T[] array)
        {
            cachesize = 512;
            T[] cache =new T[cachesize];
            if (cache == null)
            {
                cachesize = 0;
            }
            else
            {
                arrayto = cache;
            }


            arrayto = new T[] { };
        }

        private int cachesize;
        private T[] arrayto;
        

        public  void sort(T[] array,Comparer<T> comp)
        {
            
            
        }

        private int BinaryFirst(T[] array,T value,Range range, Comparer<T> comp)
        {
            int start = range.Start;
            int end = range.End - 1;
            while (start < end)
            {
                int middle = start + (end - start) / 2;
                if (comp.Compare(array[middle], value)<0)
                {
                    start = middle + 1;
                }
                else
                {
                    end = middle;
                }
            }

            if (start == range.End - 1 && comp.Compare(array[start], value) < 0) ++start;
            return start;
        }

        private int BinaryLast(T[] array, T value, Range range, Comparer<T> comp)
        {
            int start = range.Start;
            int end = range.End - 1;
            while (start<end)
            {
                int middle = start + (end - start) / 2;
                if (comp.Compare(value,array[middle]) >= 0) start = middle - 1;
                else
                {
                    end = start;
                }

            }

            if (start == range.End && comp.Compare(value, array[start]) >= 0) ++start;
            return start;

        }

        private int FirstForwardFind(T[] array, T value, Range range, Comparer<T> comp, int unique)
        {
            if (range.End - range.Start == 0) return range.Start;
            int index;
            int skip = Math.Max((range.End - range.Start) / unique, 1);
            for (index = range.Start + skip; comp.Compare(array[index - 1], value) < 0; index += skip)
            {
                if(index>=range.End-skip)
                {
                    return BinaryFirst(array, value, new Range(index, range.End), comp);

                }
            }

            return BinaryFirst(array, value, new Range(index - skip, index), comp);
        }

        private int LastForwardFind(T[] array, T value, Range range, Comparer<T> comp, int unique)
        {
            if (range.End - range.Start == 0) return range.Start;
            int index;
            int skip = Math.Max((range.End - range.Start) / unique, 1);
            for (index = range.Start + skip; comp.Compare(array[index - 1], value) >= 0; index += skip)
            {
                if (index >= range.End - skip)
                {
                    return BinaryLast(array,value,new Range(index, range.End), comp);
                }
            }

            return BinaryLast(array, value, new Range(index - skip, index), comp);
        }

        public int FirstBackwardFind(T[] array, T value, Range range, Comparer<T> comp, int unique)
        {
            if (range.End - range.Start == 0) return range.Start;
            int index;
            int skip = Math.Max((range.End - range.Start) / unique, 1);
            for (index = range.End - skip; index > range.Start && comp.Compare(value, array[index - 1]) >= 0;index-=skip)
            {
                if (index < range.Start + skip) return BinaryFirst(array, value, new Range(range.Start, index), comp);
                

            }

            return BinaryFirst(array, value, new Range(index, index + skip), comp);
        }

        private int LastBackwardFind(T[] array, T value, Range range, Comparer<T> comp,int unique)
        {
            if (range.End - range.Start == 0) return range.Start;
            int index;
            int skip = Math.Max((range.End - range.Start) / unique, 1);
            for (index = range.End - skip;
                index > range.Start && comp.Compare(value, array[index - 1]) < 0;
                index -= skip)
            {
                if (index < range.Start + skip) return BinaryLast(array, value, new Range(range.Start, index), comp);
                
            }

            return BinaryLast(array, value, new Range(index, index + skip), comp);
        }

        public void InsertionSort(T[] array, Range range, Comparer<T> comp)
        {
            for (int j, i = range.Start + 1; i < range.End; ++i)
            {
                T temporary = array[i];
                for (j = i; j > range.Start && comp.Compare(temporary, array[j - 1]) < 0; --j)
                {
                    array[j] = array[j - 1];
                }

            }
        }
        public void Reverse(T[] array, Range range)
        {
            for (int i = (range.End - range.Start) / 2 - 1; i >= 0; --i)
            {
                T swap = array[range.Start + 1];
                array[range.Start + i] = array[range.End - i - 1];
                array[range.End - i - 1] = swap;
            }
        }
    }
}