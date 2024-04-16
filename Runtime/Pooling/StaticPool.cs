using System;
using System.Collections.Generic;

namespace Utkarsh.UnityCore.Pooling
{
    public class StaticPool<T> : IPool<T>
    {
        private Func<T> produce;
        private int capacity;

        List<T> objects;

        public StaticPool(Func<T> create, int maxSize)
        {
            capacity = maxSize;
            produce = create;
            objects = new List<T>();

            for (int i = 0; i < capacity; i++)
            {
                objects.Add(produce());
            }
        }

        public StaticPool(List<T> objects)
        {
            this.objects = objects;
            capacity = objects.Count;
        }

        public T GetPooledObject()
        {
            if (capacity > 0)
            {
                capacity--;
                T removed = objects[0];
                objects.RemoveAt(0);
                return removed;
            }
            else return default(T);
        }

        public void ReturnToPool(T toReturn)
        {
            objects.Insert(0, toReturn);
            capacity++;
        }

        public void ForEach(Action<T> function)
        {
            if (objects != null)
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    function(objects[i]);
                }
            }
        }
    }
}