using MyVector;
public class Project
{
    public class MyStack<T> : MyVector<T>
    {
        private MyVector<T> stack;
        private int top;
        public MyStack(){
            top = -1;
            stack = new MyVector<T>(1);
        }
        public void Push(T item)
        {
            top++;
            stack.Add(item);
        }
        public void Pop() { 
            stack.Remove(top);
            top--;
        }
        public T Peek()
        {
            if(top==-1) throw new Exception("Stack is empty");
            else return stack.Get(top);
        }
        public bool Empty()
        {
            return stack.Count == 0;
        }
        public int Search(T item) { 
            if(stack.IndexOf(item) == -1) return -1;
            return top-stack.IndexOf(item)+1;
        }
        public void print()
        {
            for(int i=0;i<=top; i++)
                Console.WriteLine(stack.Get(i));
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            MyStack<int> stack = new MyStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            Console.WriteLine(stack.Peek());
            stack.Pop();
            Console.WriteLine(stack.Search(44));
        }
    }
}