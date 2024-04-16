using System;

namespace Utkarsh.UnityCore.Pooling
{
    public class ArrayStackPool<T> : IPool<T>
    {
        private Func<T> produce;
        private int capacity;
        private int head = -1;

        T[] objects;

        public ArrayStackPool(Func<T> create, int maxSize)
        {
            capacity = maxSize;
            produce = create;
            objects = new T[capacity];
            head = 0;

            for (int i = 0; i < capacity; i++)
            {
                objects[i] = produce();
            }
        }

        public ArrayStackPool(T[] objects)
        {
            head = 0;
            this.objects = objects;
            capacity = objects.Length;
        }

        public T GetPooledObject()
        {
            if (head < capacity)
            {
                T removed = objects[head];
                head++;
                return removed;
            }
            else return default(T);
        }

        public void ReturnToPool(T toReturn)
        {
            if (head > 0 && head <= capacity)
            {
                head--;
                objects[head] = toReturn;
            }
        }

        public void ForEach(Action<T> function)
        {
            if (objects != null)
            {
                for (int i = 0; i < objects.Length; i++)
                {
                    function(objects[i]);
                }
            }
        }
    }
}