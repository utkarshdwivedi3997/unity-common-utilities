using UnityEngine;

namespace Utkarsh.UnityCore.DataStructures
{
    public enum OverflowMode
    {
        /// <summary>
        /// When max size is hit, no more items can't be added
        /// </summary>
        DisallowPushing,
        /// <summary>
        /// When max size is hit, the first item at head is replaced by the new item
        /// </summary>
        ReplaceOldItems
    };

    public class CircularQueue<T>
    {
        private T[] items;
        private int maxSize;
        public int Head { get; private set; }
        public int Tail { get; private set; }
        private int count;
        public OverflowMode Mode;
        public bool HasItems => count > 0;

        public CircularQueue(int maxSize, OverflowMode mode = OverflowMode.DisallowPushing)
        {
            this.maxSize = maxSize;
            items = new T[maxSize];
            Head = 0;
            Tail = -1;
            count = 0;
            this.Mode = mode;
        }

        public void Insert(T item)
        {
            if (count == maxSize)
            {
                if (Mode == OverflowMode.DisallowPushing)
                {
                    Debug.LogWarning("Max size reached! Can't add more items");
                    return;
                }
                else
                {
                    Debug.LogWarning("Max size reached! Replacing item at head with new item");
                }
            }

            Head = (Head + 1) % maxSize;
            items[Head] = item;
            count = Mathf.Clamp(count + 1, 0, maxSize);
        }

        public T Delete()
        {
            if (count == 0)
            {
                Debug.LogWarning("Queue empty. Can't delete anything");
                return default(T);
            }

            Tail = (Tail + 1) % maxSize;
            count--;
            return items[Tail];
        }

        public void Clear()
        {
            Head = 0;
            Tail = -1;
            count = 0;
        }
    }

    public class CircularStack<T>
    {
        private T[] items;
        private int maxSize;
        public int Head { get; private set; }
        private int count;
        public OverflowMode Mode;
        public bool HasItems => count > 0;

        public CircularStack(int maxSize, OverflowMode mode = OverflowMode.DisallowPushing)
        {
            this.maxSize = maxSize;
            items = new T[maxSize];
            Head = -1;
            count = 0;
            this.Mode = mode;
        }

        public void Push(T item)
        {
            if (count == maxSize)
            {
                if (Mode == OverflowMode.DisallowPushing)
                {
                    Debug.LogWarning("Max size reached! Can't add more items");
                    return;
                }
                else
                {
                    Debug.LogWarning("Max size reached! Replacing item at head with new item");
                }
            }

            Head = (Head + 1).PositiveModulo(maxSize);
            items[Head] = item;
            count = Mathf.Clamp(count + 1, 0, maxSize);
        }

        public T Pop()
        {
            if (count == 0)
            {
                Debug.LogWarning("Stack empty. Can't delete anything");
                return default(T);
            }

            T tmp = items[Head];
            Head = (Head - 1).PositiveModulo(maxSize);
            count--;
            return tmp;
        }

        public T Peek()
        {
            if (count == 0)
            {
                Debug.LogWarning("Stack empty.");
                return default(T);
            }

            return items[Head];
        }

        public void Clear()
        {
            count = 0;
            Head = -1;
        }
    }
}
