﻿using MV;
namespace Task10
{
    public class Heap<T> where T : IComparable<T>
    {
        public MyArrayList<T> heap = new MyArrayList<T>(10);
        private int size;
        public Heap(T[] array)
        {
            size = array.Length;
            for (int i = 0; i < size; i++)
                heap.Add(array[i]);
                
            for (int i = size/2-1; i >=0; i--)
                Heapify(i);
            Print();
        }
        public T SearchMaximum() => heap.Get(0);
        public T DeleteMaximum()
        {
            T maximum= heap.Get(0);
            heap.Set(0, heap.Get(size - 1));
            size--;
            Heapify(0);
            return maximum;
        }
        
        public void Heapify(int i)
        {
            int leftChild;
            int rightChild;
            int parents = i;
            while (true)
            {
                leftChild = 2 * i + 1;
                rightChild = 2 * i + 2;
                if(rightChild<size && heap.Get(rightChild).CompareTo(heap.Get(parents)) > 0)
                    parents = rightChild;
                if (leftChild < size && heap.Get(leftChild).CompareTo(heap.Get(parents)) > 0)
                    parents = leftChild;
                if (parents == i)
                    break;
                Swap(parents, i);
                i = parents;
            }
        }
        private void Swap(int i, int j) {
            T temp1=heap.Get(i);
            T temp2=heap.Get(j);
            heap.Set(j, temp1);
            heap.Set(i, temp2);
        }
        public void Print()
        {
            for (int i = 0; i<size; i++)
                Console.Write(heap.Get(i) + "\n");
        }

        public void AddElement(T element)
        {
            heap.Set(size,element);
            size++;
            for (int i = size / 2; i >= 0; i--)
                Heapify(i);
        }
        public void KeyIncr(int index, T e)
        {
            if (index > heap.Size - 1)
                throw new IndexOutOfRangeException();
            heap.Set(index, e);
            for (int i = size / 2; i >= 0; i--)
                Heapify(i);
        }
        public void HeapMerge(Heap<T> newHeap)
        {
            while(newHeap.size > 0)
            {
                T element = newHeap.DeleteMaximum();
                AddElement(element);
            }
            for(int i = size / 2; i >= 0; i--)
                Heapify(i);
        }
    }
    class Project
    {
        static void Main(string[] args)
        {
            int[] mas = { 2,10,4,11,6,9};
            int[] mas1 = { 14, 57, 89 };
            Heap<int> hp = new Heap<int>(mas);
            Heap<int>hp1 = new Heap<int>(mas1);
            hp.DeleteMaximum();
            Console.WriteLine("======");
            hp.HeapMerge(hp1);
            hp.Print();
        }
    }
}