using System;
using System.Collections.Generic;

namespace Utkarsh.UnityCore.Pooling
{
    /// <summary>
    /// Similar to <seealso cref="StaticPool{T}"/>, but this can grow in capacity similar to Lists in case it runs out of items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DynamicPool<T> : IPool<T>
    {
        private Func<T> produce;
        private int capacity;

        List<T> objects;

        public DynamicPool(Func<T> create, int initialSize)
        {
            capacity = initialSize;
            produce = create;
            objects = new List<T>();

            for (int i = 0; i < capacity; i++)
            {
                objects.Add(produce());
            }
        }

        public T GetPooledObject()
        {
            if (capacity <= 0)
            {
                capacity++;
                objects.Add(produce());
            }

            if (capacity > 0)
            {
                capacity--;
                T removed = objects[0];
                objects.RemoveAt(0);
                return removed;
            }
            else
            {
                return default(T);
            }
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