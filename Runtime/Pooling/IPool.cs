namespace Utkarsh.UnityCore.Pooling
{
    public interface IPool<T>
    {
        T GetPooledObject();
        void ReturnToPool(T obj);
    }
}