using System;

namespace Utkarsh.UnityCore.Pooling
{
    public class QueuePool<T> : IPool<T>
    {
        private Func<T> produce;
        private int capacity;

        T[] objects;
        int index;

        public QueuePool(Func<T> create, int maxSize)
        {
            capacity = maxSize;
            produce = create;
            index = -1;
            objects = new T[maxSize];

            for (int i = 0; i < capacity; i++)
            {
                objects[i] = produce();
            }
        }

        public QueuePool(T[] objects)
        {
            this.objects = objects;
            capacity = objects.Length;
            index = -1;
        }

        public T GetPooledObject()
        {
            index = (index + 1) % capacity;

            return objects[index];
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

        public void ReturnToPool(T obj)
        {

        }
    }
}