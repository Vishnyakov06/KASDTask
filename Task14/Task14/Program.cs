namespace Task14
{
    class MyArrayDeque<T>
    {
        private T[] elements;
        private int head;
        private int tail;
        public MyArrayDeque()
        {
            elements = new T[16];
            head = 0;
            tail = 0;
        }
        public MyArrayDeque(T[] array)
        {
            elements = new T[array.Length];
            head = 0;
            tail = array.Length;
            for (int i = 0; i < array.Length; i++)
            {
                elements[i] = array[i];
            }
        }
        public MyArrayDeque(int numElements)
        {
            elements = new T[numElements];
            head = 0;
            tail = numElements;
        }
        public void Add(T value)
        {
            if (tail == elements.Length)
            {
                T[] array = new T[(tail * 2) + 1];
                for (int i = 0; i < tail; i++) array[i] = elements[i];
                elements = array;
                elements[tail++] = value;
            }
            else elements[tail++] = value;
        }
        public void AddAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++) Add(a[i]);
        }
        public void Clear()
        {
            tail = 0;
        }
        public bool Contains(T o)
        {
            foreach (T t in elements)
                if (t.Equals(o)) return true;
            return false;
        }
        public bool ContainsAll(T[] a)
        {
            bool[] b = new bool[a.Length];
            for (int i = 0; i < a.Length; i++)
                if (Contains(a[i])) b[i] = true;
            for (int i = 0; i < b.Length; i++)
                if (!b[i]) return false;
            return true;
        }
        public bool IsEmpty()
        {
            return tail == 0;
        }
        public int Get(T o)
        {

            for (int i = 0; i < tail; i++)
            {
                if (o.Equals(elements[i])) return i;
            }
            return -1;
        }
        public void Remove(T o)
        {
            if (Contains(o))
            {
                int index = Get(o);
                T[] array = new T[--tail];
                for (int i = 0; i < index; i++) array[i] = elements[i];
                for (int i = index; i < tail; i++) array[i] = elements[i+1];
                elements = array;
            }
            else Console.WriteLine("элемент отсутствует");
        }
        public void RemoveAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++)
                Remove(a[i]);
        }
        public void RetainAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++) elements[i] = a[i];
            tail = a.Length;
        }
        public int Size() => tail;
        public T[] ToArray() => elements;
        public T Element() => elements[head];
        public bool Offer(T obj)
        {
            Add(obj);
            if (Contains(obj)) return true;
            return false;
        }
        public T Peek()
        {
            if (tail == 0) return default(T);
            else return elements[0];
        }
        public T Poll()
        {
            if (tail == 0) return default(T);
            else
            {
                T obj = elements[head];
                Remove(elements[head]);
                return obj;
            }
        }
        public void AddFirst(T obj)
        {
            T[] array = new T[tail + 1];
            array[0] = obj;
            for (int i = 1, j = 0; i < tail+1; i++, j++) array[i] = elements[j];
            elements = array;
            tail++;
        }
        public void AddLast(T obj)
        {
            Add(obj);
        }
        public T GetFirst() => elements[head];
        public T GetLast() => elements[tail-1];
        public bool OfferFirst(T obj)
        {
            AddFirst(obj);
            if (Contains(obj)) return true;
            else return false;
        }
        public bool OfferLast(T obj)
        {
            AddLast(obj);
            if (Contains(obj)) return true;
            else return false;
        }
        public T Pop()
        {
            T element = elements[head];
            Remove(element);
            return element;
        }
        public T PeekFirst()
        {
            if (tail == 0) return default(T);
            else return elements[head];
        }
        public T PeekLast()
        {
            if (tail == 0) return default(T);
            else return elements[tail-1];
        }
        public T PollFirst()
        {
            if (tail == 0) return default(T);
            else
            {
                T q = elements[head];
                Remove(elements[head]);
                return q;
            }
        }
        public T PollLast()
        {
            if (tail == 0) return default(T);
            else
            {
                T q = elements[tail - 1];
                Remove(elements[tail - 1]);
                return q;
            }
        }
        public T RemoveLast()
        {
            T obj = elements[tail-1];
            Remove(elements[tail-1]);
            return obj;
        }
        public T RemoveFirst()
        {
            T obj = elements[head];
            Remove(elements[head]);
            return obj;
        }
        public void Push(T obj)
        {
            T[] array = new T[tail + 1];
            array[0] = obj;
            for (int i = 1, j = 0; i <= tail; i++, j++) array[i] = elements[j];
            elements = array;
            tail++;
        }
        public void ToArray(T[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                Add(a[i]);
            }
            tail += a.Length;
            T[] array = new T[tail];
            for (int i = 0; i < array.Length; i++) array[i] = elements[i];
        }
        public bool RemoveFirstOccurrence(T obj)
        {
            int index = -1;
            for (int i = 0; i < tail; i++)
            {
                if (elements[i].Equals(obj))
                {
                    index = i;
                    break;
                }
            }
            if (index > -1)
            {
                T[] array = new T[tail--];
                for (int i = 0; i < index; i++) array[i] = elements[i];
                for(int i=index+1; i<tail--; i++) array[i] = elements[i];
                elements=array;
                tail--;
                return true;
            }
            return false;

        }
        public bool RemoveLastOccurrence(T obj)
        {
            int index = -1;
            for (int i = 0; i < tail; i++)
            {
                if (elements[i].Equals(obj))
                    index = i;
            }
            if (index > -1)
            {
                T[] array = new T[tail--];
                for (int i = 0; i < index; i++) array[i] = elements[i];
                for (int i = index + 1; i < tail--; i++) array[i] = elements[i];
                elements = array;
                tail--;
                return true;
            }
            return false;
        }
    }
}